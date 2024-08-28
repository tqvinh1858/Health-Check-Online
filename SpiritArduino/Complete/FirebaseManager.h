#ifndef FirebaseManager_h
#define FirebaseManager_h

#include <FirebaseESP32.h>

class FirebaseManager
{
public:
    FirebaseManager();
    void begin();
    void uploadData(const String& deviceCode, float temperature, int avgBeat, double spO2);
    bool isReady();

private:
    FirebaseAuth auth;
    FirebaseConfig config;
    FirebaseData firebaseData;
};

#endif