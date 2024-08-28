#ifndef DISPLAYMANAGER_H
#define DISPLAYMANAGER_H

#include <Adafruit_SSD1306.h>

class DisplayManager {
public:
    DisplayManager(uint8_t w, uint8_t h, TwoWire *twi = &Wire, int8_t rst_pin = -1);
    void begin();
    void displayDisconnect();
    void displayComplete();
    void displayDataOnFiveSecond(float temperature, float avgBeat, double ESpO2);
    void displayFingerPlease();
    void displayAvgResult(float avgTemperature, float avgBPM, float avgSpO2);

private:
    Adafruit_SSD1306 display;
};

#endif