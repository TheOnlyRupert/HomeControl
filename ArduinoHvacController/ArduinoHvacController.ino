#include <DHT.h>

DHT dhtInt(8, DHT22);
DHT dhtExt(9, DHT22);

float humInt;
float tempInt;
float tempSet;
bool isFanAuto;
unsigned long previousMillisTemp;
unsigned long previousMillisHVAC;
unsigned long previousMillisCooldown;

enum { HvacCoolingOff, HvacCoolingRunning, HvacCoolingStandby, HvacCoolingPurging, HvacHeatingOff, HvacHeatingRunning, HvacHeatingStandby, HvacHeatingPurging } HvacState = HvacCoolingOff;

/* PINS   === 4 -> Fan, 5 -> Cooling, 6 -> Heating, 8 -> Interior Temp Input */
/* RELAYS === Relay 4 -> Fan, Relay 3 -> Cooling, Relay 2 -> Heating */
void setup() {
  previousMillisTemp = 60000;
  previousMillisHVAC = 600000;
  previousMillisCooldown = 60000;
  isFanAuto = true;
  tempSet = 22;  //70F
  Serial.begin(9600);
  dhtInt.begin();

  pinMode(4, OUTPUT);
  pinMode(5, OUTPUT);
  pinMode(6, OUTPUT);
}

/* Current Commands:
 * <HVACFanAuto> or <HVACFanOn>
 * <HVACCoolingMode> or <HVACHeatingMode>
 * <HVACProgramOn> or <HVACProgramOff>
 * <HVACStandby> or <HVACRunning>
 * <TEMP_SET_X> (where X is A-P)
 */
void loop() {
  unsigned long currentMillisTemp = millis();
  unsigned long currentMillisHVAC = millis();
  unsigned long currentMillisCooldown = millis();

  /* Get interior temp every 1 minute */
  if (currentMillisTemp - previousMillisTemp >= 60000) {
    previousMillisTemp = currentMillisTemp;
    GetTemps();
  }

  /* Check HVAC state every 10 minutes */
  if (currentMillisHVAC - previousMillisHVAC >= 600000) {
    previousMillisHVAC = currentMillisHVAC;
    UpdateHvacState();
  }

  while (Serial.available() > 0) {
    char readWpf = Serial.read();
    switch (readWpf) {
      /* Force Refresh */
      case '0':
        if (isFanAuto) {
          Serial.print("<HvacFanAuto>");
        } else {
          Serial.print("<HvacFanOn>");
        }

        Serial.print("<");
        Serial.print(HvacState);
        Serial.print(">");

        Serial.print("<TEMP_SET_");
        Serial.print(tempSet);
        Serial.print(">");
        break;

      /* Fan Mode: On */
      case '1':
        isFanAuto = false;
        Serial.print("<HVACFanOn>");
        digitalWrite(4, HIGH);

        break;
      /* Fan Mode: Auto */
      case '2':
        isFanAuto = true;
        Serial.print("<HVACFanAuto>");
        
        /* Keep safety checks in place */
        if (!isProgramRunning) {
          digitalWrite(6, LOW);
          digitalWrite(5, LOW);
          digitalWrite(4, LOW);
        }

        break;
      /* Program On */
      case '3':
        isProgramRunning = true;
        isStandby = true;
        Serial.print("<HVACProgramOn>");

        break;
      /* Program Off */
      case '4':
        isProgramRunning = false;
        isStandby = true;
        Serial.print("<HVACProgramOff>");
        digitalWrite(6, LOW);
        digitalWrite(5, LOW);
        
        if (isFanAuto) {
          digitalWrite(4, LOW);
        }

        break;
      /* Heating Mode */
      case '5':
        isHeatingMode = true;
        isStandby = true;
        Serial.print("<HVACHeatingMode>");
        Serial.print("<HVACStandby>");

        break;
      /* Cooling Mode */
      case '6':
        isHeatingMode = false;
        isStandby = true;
        Serial.print("<HVACCoolingMode>");
        Serial.print("<HVACStandby>");

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
        tempSet = readWpf - 50;
        Serial.print("<TEMP_SET_");
        Serial.print(readWpf - 50);
        Serial.print(">");

        break;
      default:
      return;
    }

    UpdateHvacState();
  }
}

void GetTemps() {
  tempInt = dhtInt.readTemperature();
  humInt = dhtInt.readHumidity();

  Serial.print("<INT,");
  Serial.print(tempInt);
  Serial.print(",");
  Serial.print(humInt);
  Serial.print(">");
}

void UpdateHvacState() {
  Serial.print("<DEBUG,TempSet_");
  Serial.print(tempSet);
  Serial.print(",TempInt_");
  Serial.print(tempInt);
  Serial.print(">");

  /*if (isProgramRunning) {
    if (isHeatingMode) {
      if (isStandby) {
         if (tempSet - tempInt > 2) {
          isStandby = false;
          Serial.print("<HVACRunning>");
          
          digitalWrite(4, HIGH);
          digitalWrite(5, LOW);
          digitalWrite(6, HIGH);
        }
      } else {
        if (tempSet >= tempInt) {
          isStandby = true;
          Serial.print("<HVACStandby>");

          digitalWrite(5, LOW);
          digitalWrite(6, LOW);

          if (isFanAuto) {
            digitalWrite(4, LOW);
          }
        }
      }

    } else {
      if (isStandby) {
         if (tempSet - tempInt < 2) {
          isStandby = false;
          Serial.print("<HVACRunning>");
          
          digitalWrite(4, HIGH);
          digitalWrite(6, LOW);
          digitalWrite(5, HIGH);
        }
      } else {
        if (tempSet >= tempInt) {
          isStandby = true;
          Serial.print("<HVACStandby>");

          digitalWrite(6, LOW);
          digitalWrite(5, LOW);

          if (isFanAuto) {
            digitalWrite(4, LOW);
          }
        }
      }
    }
  }
}