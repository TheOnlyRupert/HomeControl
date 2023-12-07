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
bool isOverride;
unsigned long previousMillisTemp;
unsigned long previousMillisHVAC;
unsigned long previousMillisCooldown;

/* PINS   === 4 -> Fan, 5 -> Cooling, 6 -> Heating, 8 -> Interior Temp Input */
/* RELAYS === Relay 4 -> Fan, Relay 3 -> Cooling, Relay 2 -> Heating */
void setup() {
  previousMillis = 60000;
  coolDown = 0;
  isFanAuto = true;
  isHeatingMode = false;
  isProgramRunning = false;
  isStandby = true;
  tempSet = 22;  //70F
  Serial.begin(9600);
  dhtInt.begin();
  dhtExt.begin();

  pinMode(4, OUTPUT);
  pinMode(5, OUTPUT);
  pinMode(6, OUTPUT);
}

/* Current Commands:
 * <HVACFanAuto> or <HVACFanOn>
 * <HVACCoolingMode> or <HVACHeatingMode>
 * <HVACProgramOn> or <HVACProgramOff>
 * <HVACStandby> or <HVACRunning>
 * <HVAC: TEMP_SET_X> (where X is A-P)
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
          Serial.print("<HVACFanAuto>");
        } else {
          Serial.print("<HVACFanOn>");
        }

        if (isHeatingMode) {
          Serial.print("<HVACHeatingMode>");
        } else {
          Serial.print("<HVACCoolingMode>");
        }
          
        if (isProgramRunning) {
          Serial.print("<HVACProgramOn>");
        } else {
          Serial.print("<HVACProgramOff>");
        }

        if (isStandby) {
          Serial.print("<HVACStandby>");
        } else {
          Serial.print("<HVACRunning>");
        }

        Serial.print("<HVAC: TEMP_SET_");
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
        Serial.print("<HVACStandby>");

        break;
      /* Program Off */
      case '4':
        isProgramRunning = false;
        isStandby = true;
        Serial.print("<HVAC: Program Off>");
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
        Serial.print("<HVAC: Heating Mode>");
        Serial.print("<HVACStandby>");

        break;
      /* Cooling Mode */
      case '6':
        isHeatingMode = false;
        isStandby = true;
        Serial.print("<HVAC: Cooling Mode>");
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
        Serial.print("<HVAC: TEMP_SET_");
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
          Serial.print("<HVACStandby/>");

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