#include "FirebaseManager.h"
#include <Arduino.h>

FirebaseManager::FirebaseManager() {}

void FirebaseManager::begin()
{
  config.host = "HostName";
  config.api_key = "API-Key";

  auth.user.email = "test@gmail.com";
  auth.user.password = "123456";

    Firebase.begin(&config, &auth);

    if (Firebase.ready())
    {
        Serial.println("Firebase initialized successfully");
    }
    else
    {
        Serial.println("Failed to initialize Firebase");
    }
}

void FirebaseManager::uploadData(const String& deviceCode, float temperature, int avgBeat, double spO2)
{
    if (Firebase.ready())
    {
        if (!Firebase.setFloat(firebaseData, "/" + deviceCode + "/Temperature", temperature))
        {
            Serial.println("Failed to send Temperature data to Firebase");
            Serial.println(firebaseData.errorReason());
        }

        if (!Firebase.setFloat(firebaseData, "/" + deviceCode + "/HeartBeat", avgBeat))
        {
            Serial.println("Failed to send HeartBeat data to Firebase");
            Serial.println(firebaseData.errorReason());
        }

        if (!Firebase.setFloat(firebaseData, "/" + deviceCode + "/SpO2", spO2))
        {
            Serial.println("Failed to send SpO2 data to Firebase");
            Serial.println(firebaseData.errorReason());
        }

        if (firebaseData.dataAvailable())
        {
            Serial.println("Data sent to Firebase successfully");
        }
    }
    else
    {
        Serial.println("Firebase not ready");
    }
}

bool FirebaseManager::isReady()
{
    return Firebase.ready();
}