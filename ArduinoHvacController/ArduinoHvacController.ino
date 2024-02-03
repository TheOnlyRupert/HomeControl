#include <DHT.h>

DHT dht_int(8, DHT22);

float hum_int;
float temp_int;
float temp_set;
bool is_fan_auto;
unsigned long previous_millis_temp;
unsigned long previous_millis_hvac;

enum
{
    hvac_cooling_off,
    hvac_cooling_running,
    hvac_cooling_standby,
    hvac_cooling_purging,
    hvac_heating_off,
    hvac_heating_running,
    hvac_heating_standby,
    hvac_heating_purging
}
hvac_state = hvac_cooling_off;

/* PINS: 4 -> Fan, 5 -> Cooling, 6 -> Heating, 8 -> Interior Temp Input */
/* RELAYS: Relay 4 -> Fan, Relay 3 -> Cooling, Relay 2 -> Heating */
void setup()
{
    previous_millis_temp = 60000;
    previous_millis_hvac = 600000;
    is_fan_auto = true;
    temp_set = 22.00; //70F
    Serial.begin(9600);
    dht_int.begin();

    pinMode(4, OUTPUT);
    pinMode(5, OUTPUT);
    pinMode(6, OUTPUT);

    get_temps();
}

/* Current Commands:
 * <HvacFanAuto> or <HvacFanOn>
 * <HvacState_X>
 * <TEMP_SET_X> (where X is A-P)
 */
void loop()
{
    const unsigned long current_millis_temp = millis();
    const unsigned long current_millis_hvac = millis();

    /* Get interior temp every 1 minute */
    if (current_millis_temp - previous_millis_temp >= 60000)
    {
        previous_millis_temp = current_millis_temp;
        get_temps();
    }

    /* Check HVAC state every 10 minutes */
    if (current_millis_hvac - previous_millis_hvac >= 600000)
    {
        previous_millis_hvac = current_millis_hvac;

        /* Check if purging */
        switch (hvac_state)
        {
        case hvac_cooling_purging:
            hvac_state = hvac_cooling_standby;
            Serial.print("<HvacState_2>");
            digitalWrite(6, LOW);
            digitalWrite(5, LOW);

            if (is_fan_auto)
            {
                digitalWrite(4, LOW);
            }

            break;
        case hvac_heating_purging:
            hvac_state = hvac_heating_standby;
            Serial.print("<HvacState_6>");
            digitalWrite(6, LOW);
            digitalWrite(5, LOW);

            if (is_fan_auto)
            {
                digitalWrite(4, LOW);
            }

            break;
        }

        update_hvac_state();
    }

    while (Serial.available() > 0)
    {
        switch (char read_wpf = Serial.read())
        {
        /* Force Refresh */
        case '0':
            if (is_fan_auto)
            {
                Serial.print("<HvacFanAuto>");
            }
            else
            {
                Serial.print("<HvacFanOn>");
            }

            Serial.print("<HvacState_");
            Serial.print(hvac_state);
            Serial.print(">");

            Serial.print("<TEMP_SET_");
            Serial.print(temp_set);
            Serial.print(">");
            break;

        /* Fan Mode: On */
        case '1':
            is_fan_auto = false;
            Serial.print("<HvacFanOn>");
            digitalWrite(4, HIGH);

            break;
        /* Fan Mode: Auto */
        case '2':
            is_fan_auto = true;
            Serial.print("<HvacFanAuto>");

        /* Keep safety checks in place */
            switch (hvac_state)
            {
            case hvac_cooling_standby:
            case hvac_cooling_off:
            case hvac_heating_standby:
            case hvac_heating_off:
                digitalWrite(6, LOW);
                digitalWrite(5, LOW);
                digitalWrite(4, LOW);

                break;
            }

            break;
        /* Program On */
        case '3':
            switch (hvac_state)
            {
            case hvac_cooling_off:
            case hvac_cooling_running:
            case hvac_cooling_standby:
            case hvac_cooling_purging:
                Serial.print("<HvacState_2>");
                hvac_state = hvac_cooling_standby;

                break;
            case hvac_heating_off:
            case hvac_heating_running:
            case hvac_heating_standby:
            case hvac_heating_purging:
                Serial.print("<HvacState_6>");
                hvac_state = hvac_heating_standby;

                break;
            }

            break;
        /* Program Off */
        case '4':
            switch (hvac_state)
            {
            case hvac_cooling_off:
            case hvac_cooling_running:
            case hvac_cooling_standby:
            case hvac_cooling_purging:
                Serial.print("<HvacState_4>");
                hvac_state = hvac_cooling_off;

                break;
            case hvac_heating_off:
            case hvac_heating_running:
            case hvac_heating_standby:
            case hvac_heating_purging:
                Serial.print("<HvacState_4>");
                hvac_state = hvac_heating_off;

                break;
            }

            digitalWrite(6, LOW);
            digitalWrite(5, LOW);

            if (is_fan_auto)
            {
                digitalWrite(4, LOW);
            }

            break;
        /* Heating Mode */
        case '5':
            switch (hvac_state)
            {
            case hvac_cooling_off:
                hvac_state = hvac_heating_off;

                break;
            case hvac_cooling_running:
            case hvac_cooling_standby:
            case hvac_cooling_purging:
                hvac_state = hvac_heating_standby;

                break;
            }

            break;
        /* Cooling Mode */
        case '6':
            switch (hvac_state)
            {
            case hvac_heating_off:
                hvac_state = hvac_cooling_off;

                break;
            case hvac_heating_running:
            case hvac_heating_standby:
            case hvac_heating_purging:
                hvac_state = hvac_cooling_standby;

                break;
            }

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
            temp_set = read_wpf - 50;
            Serial.print("<TEMP_SET_");
            Serial.print(read_wpf - 50);
            Serial.print(".00>");

            break;
        default:
            return;
        }

        update_hvac_state();
    }
}

void get_temps()
{
    temp_int = dht_int.readTemperature();
    hum_int = dht_int.readHumidity();

    Serial.print("<INT,");
    Serial.print(temp_int);
    Serial.print(",");
    Serial.print(hum_int);
    Serial.print(">");
}

void update_hvac_state()
{
    switch (hvac_state)
    {
    case hvac_cooling_off:
    case hvac_heating_off:
        digitalWrite(5, LOW);
        digitalWrite(6, LOW);

        if (is_fan_auto)
        {
            digitalWrite(4, LOW);
        }

        break;
    case hvac_cooling_running:
        if (temp_int <= temp_set)
        {
            hvac_state = hvac_cooling_purging;
            Serial.print("<HvacState_3>");
            digitalWrite(5, LOW);
        }

        break;
    case hvac_cooling_standby:
        if (temp_int - temp_set > 2)
        {
            hvac_state = hvac_cooling_running;
            Serial.print("<HvacState_1>");
            digitalWrite(4, HIGH);
            digitalWrite(5, HIGH);
        }

        break;
    case hvac_heating_running:
        if (temp_int >= temp_set)
        {
            hvac_state = hvac_heating_purging;
            Serial.print("<HvacState_7>");
            digitalWrite(6, LOW);
        }

        break;
    case hvac_heating_standby:
        if (temp_set - temp_int > 2)
        {
            hvac_state = hvac_heating_running;
            Serial.print("<HvacState_5>");
            digitalWrite(4, HIGH);
            digitalWrite(6, HIGH);
        }

        break;
    }
}
