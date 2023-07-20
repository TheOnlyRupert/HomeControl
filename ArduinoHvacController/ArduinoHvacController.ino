#include <DHT.h>

DHT dhtInt(8, DHT22);
DHT dhtExt(9, DHT22);

float humInt;
float humExt;
float tempInt;
float tempExt;
int tempSet;
bool isFanAuto;
bool isProgramRunning;
bool isStandby;
bool isHeatingMode;
bool overrideDebug;
unsigned long previousMillis;

/* PINS: 4 -> Fan, 5 -> Cooling, 6 -> Heating, 8 -> Interior Temp Input */
/* Relay 4 -> Fan, Relay 3 -> Cooling, Relay 2 -> Heating */
void setup() {
  previousMillis = 60000;
  isFanAuto = true;
  isHeatingMode = false;
  isProgramRunning = false;
  overrideDebug = false;
  isStandby = true;
  tempSet = 21; //70F
  Serial.begin(9600);
  dhtInt.begin();
  dhtExt.begin();

  pinMode(4, OUTPUT);
  pinMode(5, OUTPUT);
  pinMode(6, OUTPUT);
}

void loop() {
  unsigned long currentMillis = millis();

  if (currentMillis - previousMillis >= 10000) {
    previousMillis = currentMillis;

    GetTemps();
    UpdateHvacState();
  }

  while (Serial.available() > 0) {
    char readWpf = Serial.read();

    switch (readWpf) {
      /* Force Refresh */
      case '0':
        if (isFanAuto) {
          Serial.print("<HVAC: Fan Auto>");
        } else {
          Serial.print("<HVAC: Fan On>");
        }

        if (isHeatingMode) {
          Serial.print("<HVAC: Heating Mode>");
          if (isProgramRunning) {
            if (isStandby) {
              Serial.print("<HVAC: Heating Standby>");
            } else {
              Serial.print("<HVAC: Heating Running>");
            }
          } else {
            Serial.print("<HVAC: Program Off>");
          }
        } else {
          Serial.print("<HVAC: Cooling Mode>");
          if (isProgramRunning) {
            if (isStandby) {
              Serial.print("<HVAC: Cooling Standby>");
            } else {
              Serial.print("<HVAC: Cooling Running>");
            }
          } else {
            Serial.print("<HVAC: Program Off>");
          }
        }

        if (overrideDebug) {
          Serial.print("<HVAC: Override TRUE>");
        } else {
          Serial.print("<HVAC: Override FALSE>");
        }

        Serial.print("<HVAC: TEMP_SET_");
        Serial.print(tempSet);
        Serial.print(">");
        break;

      /* Fan Mode: On */
      case '1':
        isFanAuto = false;

        Serial.print("<HVAC: Fan On>");
        digitalWrite(4, HIGH);

        break;
      /* Fan Mode: Auto */
      case '2':
        isFanAuto = true;

        Serial.print("<HVAC: Fan Auto>");
        if (!isProgramRunning) {
          digitalWrite(6, LOW);
          delay(2000);
          digitalWrite(5, LOW);
          delay(2000);
          digitalWrite(4, LOW);
        }

        break;
      /* Cooling On */
      case '3':
        if (isHeatingMode) {
          digitalWrite(6, LOW);
          delay(2000);
        }

        isHeatingMode = false;
        isProgramRunning = true;
        isStandby = true;

        Serial.print("<HVAC: Cooling Mode>");

        break;
      /* Heating On */
      case '4':
        if (!isHeatingMode) {
          digitalWrite(5, LOW);
          delay(2000);
        }

        isHeatingMode = true;
        isProgramRunning = true;
        isStandby = true;

        Serial.print("<HVAC: Heating Mode>");

        break;
      case '5':
        /* Stop Hvac */
        isProgramRunning = false;

        Serial.print("<HVAC: Program Off>");
        digitalWrite(6, LOW);
        delay(2000);
        digitalWrite(5, LOW);
        if (isFanAuto) {
          delay(2000);
          digitalWrite(4, LOW);
        }
        break;
      case '6':
        overrideDebug = true;

        Serial.print("<HVAC: Override TRUE>");

        break;
      case '7':
        overrideDebug = false;

        Serial.print("<HVAC: Override FALSE>");

        break;
      case '8':
      isHeatingMode = !isHeatingMode;
      break;
      case 'A':
      case 'B':
      case 'C':
      case 'D':
      case 'E':
      case 'F':
      case 'G':
      case 'H':
      case 'I':
      case 'J':
      case 'K':
      case 'L':
      case 'M':
      case 'N':
      case 'O':
      case 'P':
        Serial.print("<TEMP SET: ");
        Serial.print(readWpf - 50);
        Serial.print(">");

        break;
    }

    UpdateHvacState();
  }
}

void GetTemps() {
  humInt = dhtInt.readHumidity();
  tempInt = dhtInt.readTemperature();
  humExt = dhtExt.readHumidity();
  tempExt = dhtExt.readTemperature();

  Serial.print("<INT: ");
  Serial.print(tempInt);
  Serial.print(",");
  Serial.print(humInt);
  Serial.print(">");

  Serial.print("<EXT: ");
  Serial.print(tempExt);
  Serial.print(",");
  Serial.print(humExt);
  Serial.print(">");
}

void UpdateHvacState() {
  if (isProgramRunning) {
    if (isHeatingMode) {
      if (isStandby) {
        if (isFanAuto) {
          digitalWrite(4, LOW);
        }

        if (tempSet - tempInt > 2) {
          isStandby = false;
          Serial.print("<HVAC: Heating Running>");

          digitalWrite(4, HIGH);
          delay(2000);
          digitalWrite(5, LOW);
          delay(2000);
          digitalWrite(6, HIGH);
        }
      } else {
        if (tempSet - tempInt <= 0) {
          isStandby = true;
          Serial.print("<HVAC: Heating Standby>");

          digitalWrite(5, LOW);
          delay(2000);
          digitalWrite(6, LOW);
          if (isFanAuto) {
            delay(2000);
            digitalWrite(4, LOW);
          }
        }
      }
    } else {
      if (isStandby) {
        if (isFanAuto) {
          digitalWrite(4, LOW);
        }

        if (tempInt - tempSet > 2) {
          isStandby = false;
          Serial.print("<HVAC: Cooling Running>");

          digitalWrite(4, HIGH);
          delay(2000);
          digitalWrite(6, LOW);
          delay(2000);
          digitalWrite(5, HIGH);
        }
      } else {
        if (tempInt - tempSet <= 0) {
          isStandby = true;
          Serial.print("<HVAC: Cooling Standby>");

          digitalWrite(6, LOW);
          delay(2000);
          digitalWrite(5, LOW);
          if (isFanAuto) {
            delay(2000);
            digitalWrite(4, LOW);
          }
        }
      }
    }
  }
}
