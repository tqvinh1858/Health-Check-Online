#include <WiFiManager.h>  // Thêm thư viện WiFiManager
#include <FirebaseESP32.h>

// Khai báo đối tượng WiFiManager
WiFiManager wm;

FirebaseAuth auth;
FirebaseConfig config;
FirebaseData firebaseData;

String DeviceCode = "Device1";

float temperature = 0.0;
float heartbeat = 0.0;
float SpO2 = 0.0;
float count = 0.0;

void connectToWiFi() {
  bool res;
  res = wm.autoConnect("BHEP-Spirit", "12345678"); // Sử dụng WiFiManager để kết nối WiFi

  if (!res) {
    Serial.println("Failed to connect");
  } else {
    Serial.println("Connected to WiFi");
  }
}

void connectToFirebase() {
  config.host = "HOSTNAME";
  config.api_key = "API-KEY";

  auth.user.email = "test@gmail.com";
  auth.user.password = "123456";

  Firebase.begin(&config, &auth);

  if (Firebase.ready()) {
    Serial.println("Firebase initialized successfully");
  } else {
    Serial.println("Failed to initialize Firebase");
  }
}

void setup() {
  Serial.begin(115200);

  connectToWiFi();

  connectToFirebase();
}

void loop() {


  if (WiFi.status() == WL_CONNECTED && Firebase.ready()) {
    // Replace these with your actual sensor readings

    temperature = 37.0 + count;
    heartbeat = 87.0 + count;
    SpO2 = 97.0 + count;

    Firebase.setFloat(firebaseData, "/" + DeviceCode + "/Temperature", temperature);
    Firebase.setFloat(firebaseData, "/" + DeviceCode + "/HeartBeat", heartbeat);
    Firebase.setFloat(firebaseData, "/" + DeviceCode + "/SpO2", SpO2);
    count++;
    if (firebaseData.dataAvailable()) {
      Serial.println("Data sent to Firebase successfully");
    } else {
      Serial.println("Failed to send data to Firebase");
      Serial.println(firebaseData.errorReason());
    }
  } else {
    Serial.println("WiFi or Firebase not ready");
  }

  Serial.print("Temperature: ");
  Serial.println(temperature);
  Serial.print(", HeartBeat: ");
  Serial.println(heartbeat);
  Serial.print(", SpO2: ");
  Serial.println(SpO2);
  
  Serial.println();
  delay(5000);
}