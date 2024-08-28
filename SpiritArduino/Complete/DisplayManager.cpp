#include "DisplayManager.h"

DisplayManager::DisplayManager(uint8_t w, uint8_t h, TwoWire *twi, int8_t rst_pin)
    : display(w, h, twi, rst_pin) {}

void DisplayManager::begin() {
    display.begin(SSD1306_SWITCHCAPVCC, 0x3C);
    display.display();
    delay(3000);
}

void DisplayManager::displayDisconnect() {
    display.clearDisplay();
    display.setTextSize(1);
    display.setTextColor(WHITE);
    display.setCursor(45, 5);
    display.println("Mat");
    display.setCursor(15, 35);
    display.println("Ket Noi");
    display.display();
}

void DisplayManager::displayComplete() {
    display.clearDisplay();
    display.setTextSize(2);
    display.setTextColor(WHITE);
    display.setCursor(5, 30);
    display.println("Hoan Thanh");
    display.display();
}

void DisplayManager::displayDataOnFiveSecond(float temperature, float avgBeat, double ESpO2) {
    display.clearDisplay();
    display.setTextSize(1);
    display.setTextColor(WHITE);
    display.setCursor(0, 10);
    display.print("Nhiet Do: "); display.print(temperature);
    display.setCursor(0, 30);
    display.print("Nhip Tim:"); display.print(avgBeat);
    display.setCursor(0, 50);
    display.print("ESpO2: "); display.print(String(ESpO2) + "%");
    display.display();
}

void DisplayManager::displayFingerPlease() {
    display.clearDisplay();
    display.setTextSize(2);
    display.setTextColor(WHITE);
    display.setCursor(45, 5);
    display.println("Dat");
    display.setCursor(15, 35);
    display.println("Ngon Tay");
    display.display();
}

void DisplayManager::displayAvgResult(float avgTemperature, float avgBPM, float avgSpO2) {
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
}