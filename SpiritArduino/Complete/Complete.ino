#include <Wire.h>
#include <WiFiManager.h>
#include <FirebaseESP32.h>
#include <Adafruit_GFX.h>      //OLED libraries
#include <Adafruit_SSD1306.h>  //OLED libraries
#include <Adafruit_MLX90614.h> //Cảm biến nhiệt độ
#include "MAX30105.h"          //MAX3010x library
#include "heartRate.h"         //Heart rate calculating algorithm
#include "DisplayManager.h"
#include "FirebaseManager.h"

WiFiManager wm;
FirebaseManager firebaseManager;

String DeviceCode = "Device8"; // DeviceCode

MAX30105 particleSensor;
Adafruit_MLX90614 mlx = Adafruit_MLX90614();

const int RATE_SIZE = 4;
byte rates[RATE_SIZE];
byte rateSpot = 0;
long lastBeat = 0; 
float beatsPerMinute;
int beatAvg;

float avered = 0;
float aveir = 0;
float sumirrms = 0;
float sumredrms = 0;

float SpO2 = 0;
float ESpO2 = 90.0;
float FSpO2 = 0.7; 
float frate = 0.95;
int i = 0;
int Num_Readings = 30;   
#define FINGER_ON 7000    
#define MINIMUM_SPO2 90.0 
// OLED display settings
#define SCREEN_WIDTH 128
#define SCREEN_HEIGHT 64
#define OLED_RESET -1
DisplayManager displayManager(SCREEN_WIDTH, SCREEN_HEIGHT, &Wire, OLED_RESET); // Declaring the display name (display)

// SetUp For Avg
unsigned int startTime;
static unsigned long lastFirebaseUploadTime = 0;

void connectToWiFi()
{
  bool res;
  res = wm.autoConnect("BHEP-Spirit", "12345678"); 

  if (!res)
  {
    Serial.println(F("Failed to connect"));
  }
  else
  {
    Serial.println(F("Connected to WiFi"));
  }
}


void configMax30105()
{
  if (!particleSensor.begin(Wire, I2C_SPEED_FAST)) // Use default I2C port, 400kHz speed
  {
    Serial.println("Not Found MAX30102");
    while (1)
      ;
  }
  // Tùy Chọn
  byte ledBrightness = 0x7F; // 亮度建議=127, Options: 0=Off to 255=50mA
  byte sampleAverage = 4;    // Options: 1, 2, 4, 8, 16, 32
  byte ledMode = 2;          // Options: 1 = Red only(心跳), 2 = Red + IR(血氧)
  int sampleRate = 800;      // Options: 50, 100, 200, 400, 800, 1000, 1600, 3200
  int pulseWidth = 215;      // Options: 69, 118, 215, 411
  int adcRange = 16384;      // Options: 2048, 4096, 8192, 16384
  // Set up the wanted parameters
  particleSensor.setup(ledBrightness, sampleAverage, ledMode, sampleRate, pulseWidth, adcRange); // Configure sensor with these settings
  particleSensor.enableDIETEMPRDY();

  particleSensor.setPulseAmplitudeRed(0x0A); // Turn Red LED to low to indicate sensor is running
  particleSensor.setPulseAmplitudeGreen(0);  // Turn off Green LED
}

void setup()
{
  Serial.begin(9600);
  Serial.println(F("System Start"));

  
  displayManager.begin();

  configMax30105();

  mlx.begin(); 
  
  startTime = millis();

  connectToWiFi();

  firebaseManager.begin();
}

void loop()
{
  if (WiFi.status() == WL_CONNECTED && firebaseManager.isReady())
  {
    long irValue = particleSensor.getIR();

    // Kiểm tra xem ngón tay có trên cảm biến hay ko ( irValue > 7000)
    if (irValue > FINGER_ON)
    {
      float tempResult = readTemperature();
      float heartBeatResult = calculateAvgHeartBeat(irValue);
      float spO2Result = calculateSpO2();

      // Display data on OLED
      displayManager.displayDataOnFiveSecond(tempResult, heartBeatResult, spO2Result);
      unsigned long currentTime = millis();
      if (currentTime - lastFirebaseUploadTime >= 2000) {
        firebaseManager.uploadData(DeviceCode, tempResult, heartBeatResult, spO2Result);
        lastFirebaseUploadTime = currentTime;
      }
    }
    // Handle no finger detected case
    else
    {
      for (byte rx = 0; rx < RATE_SIZE; rx++)
        rates[rx] = 0;
      beatAvg = 0;
      avered = 0;
      aveir = 0;
      sumirrms = 0;
      sumredrms = 0;
      rateSpot = 0;
      SpO2 = 0;
      ESpO2 = 90.0;

      displayManager.displayFingerPlease();
    }

    unsigned int currentTime = millis();
    if (currentTime - startTime >= 60000)
    {
      Serial.println("Stop Monitor");
      displayManager.displayComplete();
      while (true)
      {}
    }
  }
  else
  {
    displayManager.displayDisconnect();
    Serial.println("WiFi or Firebase not ready");
  }

  particleSensor.nextSample();
}

float calculateAvgHeartBeat(int irValue)
{
  if (checkForBeat(irValue) == true)
  {
    // Calculate beatsPerMinute
    long delta = millis() - lastBeat;
    lastBeat = millis();
    beatsPerMinute = 60 / (delta / 1000.0);

    // if (beatsPerMinute < 255 && beatsPerMinute > 20)
    // {                                           // Check if the BPM value is within a valid range
      rates[rateSpot++] = (byte)beatsPerMinute; // Store this reading in the array
      rateSpot %= RATE_SIZE;                    // Wrap variable

      // Take average of readings
      beatAvg = 0;
      for (byte x = 0; x < RATE_SIZE; x++)
        beatAvg += rates[x];
      beatAvg /= RATE_SIZE;
    // }
  }
  return beatAvg;
}

float readTemperature()
{
  float temperature = mlx.readObjectTempC() + 2.0;
  return temperature;
}

float calculateSpO2()
{
  uint32_t ir, red;
  float fred, fir;
  if (particleSensor.available())
  {
    i++;
    ir = particleSensor.getFIFOIR();   
    red = particleSensor.getFIFORed(); 

    fir = (float)ir;
    fred = (float)red;

    aveir = aveir * frate + fir * (1.0 - frate);    // average IR level by low pass filter
    avered = avered * frate + fred * (1.0 - frate); // average red level by low pass filter
    sumirrms += (fir - aveir) * (fir - aveir);             // square sum of alternate component of IR level
    sumredrms += (fred - avered) * (fred - avered);

    if (i % Num_Readings == 0)
    {
      float R = (sqrt(sumirrms) / aveir) / (sqrt(sumredrms) / avered);
      SpO2 = -23.3 * (R - 0.4) + 100;
      ESpO2 = FSpO2 * ESpO2 + (1.0 - FSpO2) * SpO2;

      // if (ESpO2 <= MINIMUM_SPO2)
      //   ESpO2 = MINIMUM_SPO2;
      // if (ESpO2 > 100)
      //   ESpO2 = 99.9;

      sumredrms = 0.0;
      sumirrms = 0.0;
      i = 0;
    }
  }

  return ESpO2;
}