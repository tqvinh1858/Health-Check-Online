#include <Wire.h>
#include <Adafruit_GFX.h>    //OLED libraries
#include <Adafruit_SSD1306.h> //OLED libraries
#include <Adafruit_MLX90614.h> //Cảm biến nhiệt độ
#include "MAX30105.h"           //MAX3010x library
#include "heartRate.h"          //Heart rate calculating algorithm
#include "ESP32Servo.h"
//Bluetooth
#include <Arduino.h>
#include <BLEDevice.h>
#include <BLEServer.h>
#include <BLEUtils.h>

// BLE SECTION
BLEServer *pServer = NULL;
BLECharacteristic *message_characteristic = NULL;

#define SERVICE_UUID "4fafc201-1fb5-459e-8fcc-c5c9c331914b"
#define MESSAGE_CHARACTERISTIC_UUID "6d68efe5-04b6-4a85-abc4-c2670b7bf7fd"

class MyServerCallbacks : public BLEServerCallbacks
{
  void onConnect(BLEServer *pServer)
  {
    Serial.println("Connected");
  };

  void onDisconnect(BLEServer *pServer)
  {
    Serial.println("Disconnected");
  }
};

// MAX30105 sensor and MLX90614 temperature sensor
MAX30105 particleSensor;
Adafruit_MLX90614 mlx = Adafruit_MLX90614();

// Variables for heart rate and SpO2 calculations
const int RATE_SIZE = 4;
byte rates[RATE_SIZE];
byte rateSpot = 0;
long lastBeat = 0;
float beatsPerMinute;
int beatAvg;

double avered = 0;
double aveir = 0;
double sumirrms = 0;
double sumredrms = 0;

double SpO2 = 0;
double ESpO2 = 90.0;
double FSpO2 = 0.7;
double frate = 0.95;

int i = 0;
int Num_Readings = 30;// Number of readings for SpO2 averaging

// Constants for readability
#define FINGER_ON 7000    
#define MINIMUM_SPO2 90.0 

// OLED display settings
#define SCREEN_WIDTH 128
#define SCREEN_HEIGHT 64
#define OLED_RESET    -1
Adafruit_SSD1306 display(SCREEN_WIDTH, SCREEN_HEIGHT, &Wire, OLED_RESET); //Declaring the display name (display)


// SetUp For Avg
unsigned long startTime;
float totalTemperature = 0;
float totalBPM = 0;
double totalSpO2 = 0;
int numTemReadings = 0;
int numBpmReadings = 0;
int numSpO2Readings = 0;


void setup() {
  Serial.begin(9600);
  Serial.println("System Start");
  display.begin(SSD1306_SWITCHCAPVCC, 0x3C); //Start the OLED display
  display.display();
  delay(3000);

  if (!particleSensor.begin(Wire, I2C_SPEED_FAST)) //Use default I2C port, 400kHz speed
  {
    Serial.println("Not Found MAX30102");
    while (1);  
  }
  //Tùy Chọn
  byte ledBrightness = 0x7F; //亮度建議=127, Options: 0=Off to 255=50mA
  byte sampleAverage = 4; //Options: 1, 2, 4, 8, 16, 32
  byte ledMode = 2; //Options: 1 = Red only(心跳), 2 = Red + IR(血氧)
  int sampleRate = 800; //Options: 50, 100, 200, 400, 800, 1000, 1600, 3200
  int pulseWidth = 215; //Options: 69, 118, 215, 411
  int adcRange = 16384; //Options: 2048, 4096, 8192, 16384

  // Set up the wanted parameters
  particleSensor.setup(ledBrightness, sampleAverage, ledMode, sampleRate, pulseWidth, adcRange); //Configure sensor with these settings
  particleSensor.enableDIETEMPRDY();
  particleSensor.setPulseAmplitudeRed(0x0A); //Turn Red LED to low to indicate sensor is running
  particleSensor.setPulseAmplitudeGreen(0); //Turn off Green LED

  mlx.begin(); // Bật cảm biến nhiệt độ
  startTime = millis(); // Lưu lại thời gian bắt đầu đo

  // Setting BlueTooth
  // Create the BLE Device
  BLEDevice::init("BLEExample");
  // Create the BLE Server
  pServer = BLEDevice::createServer();
  pServer->setCallbacks(new MyServerCallbacks());
  // Create the BLE Service
  BLEService *pService = pServer->createService(SERVICE_UUID);
  delay(100);

  // Create a BLE Characteristic
  message_characteristic = pService->createCharacteristic(
      MESSAGE_CHARACTERISTIC_UUID,
      BLECharacteristic::PROPERTY_READ |
          BLECharacteristic::PROPERTY_NOTIFY);

  // Start the BLE service
  pService->start();

  // Start advertising
  pServer->getAdvertising()->start();

  Serial.println("Waiting for a client connection to notify...");
}


void loop() {
  long irValue = particleSensor.getIR();   // Kiểm tra xem có ngón tay trên cảm biến hay ko
  // Kiểm tra xem ngón tay có trên cảm biến hay ko ( irValue > 7000)
  if (irValue > FINGER_ON ) {
    float temperature = readTemperature();
    float avgBeat = calculateAvgHeartBeat(irValue);
    double spO2 = calculateSpO2();
    
    //In ra Serial
    // printSerial(irValue, temperature, avgBeat, spO2);

    if(temperature != NULL){
      totalTemperature += temperature;
      numTemReadings++;
      Serial.print("temperature:" + String(temperature));
      Serial.println();
    }
    
    if(avgBeat != NULL){
      totalBPM += avgBeat;
      numBpmReadings++;
      Serial.print("avgBeat:" + String(avgBeat));
      Serial.println();
    }
    
    if(spO2 < 90.0) 
      totalSpO2 += 90.0;
    else 
      totalSpO2 += spO2;
    numSpO2Readings++;	

    // Display data on OLED
    display.clearDisplay();
    display.setTextSize(1);
    display.setTextColor(WHITE);

    display.setCursor(0, 10);
    display.print("Nhiet Do: "); display.print(temperature);
    display.setCursor(0, 30);
    display.print("BPM :"); display.print(avgBeat); 
    display.setCursor(0, 50);
    display.print("ESpO2: "); display.print(String(ESpO2) + "%");
    display.display();
  }

  // Handle no finger detected case
  else {
    for (byte rx = 0 ; rx < RATE_SIZE ; rx++) rates[rx] = 0;
    beatAvg = 0;
    avered = 0; aveir = 0; sumirrms = 0; sumredrms = 0; rateSpot = 0;
    SpO2 = 0; ESpO2 = 90.0; 

    display.clearDisplay();
    display.setTextSize(2);
    display.setTextColor(WHITE);
    display.setCursor(30, 5);
    display.println("Finger");
    display.setCursor(30, 35);
    display.println("Please");
    display.display();
  }

  unsigned long currentTime = millis();
  if (currentTime - startTime >= 20000) {
    // Tính toán giá trị trung bình
    float avgTemperature = totalTemperature / numTemReadings;
    float avgBPM = 0;
    if(numBpmReadings != 0)
      float avgBPM = totalBPM / numBpmReadings;
    double avgSpO2 = totalSpO2 / numSpO2Readings;
    Serial.print("========================Finish Monitor=========================");
    Serial.print("numBpmReadings:" + String(numBpmReadings));
    
    display.clearDisplay();
    display.setTextSize(1);
    display.setTextColor(WHITE);
    display.setCursor(0, 10);
    display.print("Avg Nhiet Do: "); display.print(avgTemperature);
    display.setCursor(0, 30);
    display.print("Avg BPM :"); display.print(avgBPM);
    display.setCursor(0, 50);
    display.print("Avg ESpO2: "); display.print(String(avgSpO2) + "%");
    display.display();
    
    String message = "avgTemperature = " + String(avgTemperature) + 
                 " | avgBPM = " + String(avgBPM) + 
                 " | avgSpO2 = " + String(avgSpO2) + "%";
    
    message_characteristic->setValue(message.c_str());
    message_characteristic->notify();
    Serial.print(message.c_str());
    
    while (true) {
      delay(1000);
    }
  }
  delay(1000);
  particleSensor.nextSample();
}


float calculateAvgHeartBeat(long irValue){
  if (checkForBeat(irValue) == true) {
      //Calculate beatsPerMinute
      long delta = millis() - lastBeat;
      lastBeat = millis();
      beatsPerMinute = 60 / (delta / 1000.0);

      if (beatsPerMinute < 255 && beatsPerMinute > 20) {
        rates[rateSpot++] = (byte)beatsPerMinute;
        rateSpot %= RATE_SIZE;

        beatAvg = 0;
        for (byte x = 0; x < RATE_SIZE; x++)
          beatAvg += rates[x];
        beatAvg /= RATE_SIZE;
      }
    }
    return beatAvg;
}

float readTemperature(){
  float temperature  = mlx.readObjectTempC() + 1.0; 
  return temperature ;
}

double calculateSpO2() {
  uint32_t ir, red ;
  double fred, fir;
  if (particleSensor.available()) {
    i++;
    ir = particleSensor.getFIFOIR(); //Đọc Hồng ngoại
    red = particleSensor.getFIFORed(); //Đọc Đèn đỏ

    fir = (double)ir;
    fred = (double)red;

    aveir = aveir * frate + (double)ir * (1.0 - frate); //average IR level by low pass filter
    avered = avered * frate + (double)red * (1.0 - frate);//average red level by low pass filter
    sumirrms += (fir - aveir) * (fir - aveir);//square sum of alternate component of IR level
    sumredrms += (fred - avered) * (fred - avered); 
    

    if (i % Num_Readings == 0) {
      double R = (sqrt(sumirrms) / aveir) / (sqrt(sumredrms) / avered);
      SpO2 = -23.3 * (R - 0.4) + 100;
      ESpO2 = FSpO2 * ESpO2 + (1.0 - FSpO2) * SpO2;
      if (ESpO2 <= MINIMUM_SPO2) ESpO2 = MINIMUM_SPO2;
      if (ESpO2 > 100) ESpO2 = 99.9;

      sumredrms = 0.0;
      sumirrms = 0.0;
      SpO2 = 0;
      i = 0;
    }
  }

  return ESpO2;
}

void printSerial(long irValue, float temperature, float avgBeat, double spO2){
  Serial.print("irValue: " + String(irValue));
  Serial.print("Nhiet Do: " + String(temperature));
  Serial.print(", Bpm:" + String(avgBeat));
  if (avgBeat > 0)  
    Serial.print(", SPO2:" + String(ESpO2));
  else 
    Serial.print(", SPO2:" + String(ESpO2));

  Serial.println();
}

