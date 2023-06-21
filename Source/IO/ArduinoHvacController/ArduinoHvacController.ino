#include <DHT.h>;

DHT dhtInt(7, DHT22);
DHT dhtExt(8, DHT22);

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

/* PINS: 13 -> Fan, 12 -> Cooling, 11 -> Heating */
void setup() {
  previousMillis = 60000;
  isFanAuto = true;
  isHeatingMode = false;
  isProgramRunning = false;
  overrideDebug = false;
  isStandby = true;
  tempSet = 70;
  Serial.begin(9600);
  dhtInt.begin();
  dhtExt.begin();

  pinMode(13, OUTPUT);
  pinMode(12, OUTPUT);
  pinMode(11, OUTPUT);
}

void loop() {
  unsigned long currentMillis = millis();

  if (currentMillis - previousMillis >= 60000) {
    previousMillis = currentMillis;

    humInt = dhtInt.readHumidity();
    tempInt = dhtInt.readTemperature() * 9 / 5 + 32;
    humExt = dhtExt.readHumidity();
    tempExt = dhtExt.readTemperature() * 9 / 5 + 32;

    char buffer[4];
    dtostrf(tempInt, -5, 2, buffer);
    Serial.print("<INT: ");
    Serial.print(buffer);
    Serial.print(",");

    dtostrf(humInt, -5, 2, buffer);
    Serial.print(buffer);
    Serial.print(">");

    dtostrf(tempExt, -5, 2, buffer);
    Serial.print("<EXT: ");
    Serial.print(buffer);
    Serial.print(",");

    dtostrf(humExt, -5, 2, buffer);
    Serial.print(buffer);
    Serial.print(">");

    UpdateHvacState();
  }

  while (Serial.available() > 0) {
    char readWpf = Serial.read();

    switch (readWpf) {
      /* Force Refresh */
      case '0':
        previousMillis = 60000;
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
        digitalWrite(13, HIGH);

        break;
      /* Fan Mode: Auto */
      case '2':
        isFanAuto = true;

        Serial.print("<HVAC: Fan Auto>");
        if (!isProgramRunning) {
          digitalWrite(11, LOW);
          delay(2000);
          digitalWrite(12, LOW);
          delay(2000);
          digitalWrite(13, LOW);
        }

        break;
      /* Cooling On */
      case '3':
        if (isHeatingMode) {
          digitalWrite(11, LOW);
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
          digitalWrite(12, LOW);
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
        digitalWrite(11, LOW);
        delay(2000);
        digitalWrite(12, LOW);
        if (isFanAuto) {
          delay(2000);
          digitalWrite(13, LOW);
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
      case 'Q':
      case 'R':
      case 'S':
      case 'T':
      case 'U':
      case 'V':
      case 'W':
      case 'X':
      case 'Y':
      case 'Z':
      case '[':
      case '\\':
      case ']':
      case '^':
      case '_':
        int index = readWpf;
        tempSet = index - 15;
        Serial.print("<TEMP SET: ");
        Serial.print(tempSet);
        Serial.print(">");

        break;
    }

    UpdateHvacState();
  }
}

void UpdateHvacState() {
  if (isProgramRunning) {
    if (isHeatingMode) {
      if (isStandby) {
        if (isFanAuto) {
          digitalWrite(13, LOW);
        }

        if (tempSet - tempInt > 2) {
          isStandby = false;
          Serial.print("<HVAC: Heating Running>");

          digitalWrite(13, HIGH);
          delay(2000);
          digitalWrite(12, LOW);
          delay(2000);
          digitalWrite(11, HIGH);
        }
      } else {
        if (tempSet - tempInt <= 1) {
          isStandby = true;
          Serial.print("<HVAC: Heating Standby>");

          digitalWrite(12, LOW);
          delay(2000);
          digitalWrite(11, LOW);
          if (isFanAuto) {
            delay(2000);
            digitalWrite(13, LOW);
          }
        }
      }
    } else {
      if (isStandby) {
        if (isFanAuto) {
          digitalWrite(13, LOW);
        }

        if (tempInt - tempSet > 2) {
          isStandby = false;
          Serial.print("<HVAC: Cooling Running>");

          digitalWrite(13, HIGH);
          delay(2000);
          digitalWrite(11, LOW);
          delay(2000);
          digitalWrite(12, HIGH);
        }
      } else {
        if (tempInt - tempSet <= 1) {
          isStandby = true;
          Serial.print("<HVAC: Cooling Standby>");

          digitalWrite(11, LOW);
          delay(2000);
          digitalWrite(12, LOW);
          if (isFanAuto) {
            delay(2000);
            digitalWrite(13, LOW);
          }
        }
      }
    }
  }
}
