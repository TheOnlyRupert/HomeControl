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
} hvac_state = hvac_cooling_off;

unsigned long cool_down_timer; // Timer for cool-down period

/* PINS: 4 -> Fan, 5 -> Cooling, 6 -> Heating, 8 -> Interior Temp Input */
/* RELAYS: Relay 4 -> Fan, Relay 3 -> Cooling, Relay 2 -> Heating */

void setup()
{
    previous_millis_temp = 60000;
    previous_millis_hvac = 600000;
    is_fan_auto = true;
    temp_set = 22.00; // 70°F
    cool_down_timer = 0; // Initialize cool-down timer to 0
    Serial.begin(9600);
    dht_int.begin();

    pinMode(4, OUTPUT); // Fan
    pinMode(5, OUTPUT); // Cooling
    pinMode(6, OUTPUT); // Heating

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

        /* Cool-down timer for fan to keep running for 10 minutes after cooling/heating */
        if (cool_down_timer > 0 && current_millis_hvac - cool_down_timer >= 600000)
        {
            hvac_state = (hvac_state == hvac_cooling_running || hvac_state == hvac_heating_running) ? hvac_cooling_standby : hvac_state;
            cool_down_timer = 0; // Reset timer
            Serial.print("<HvacState_2>");
            digitalWrite(5, LOW);
            digitalWrite(6, LOW);
            if (is_fan_auto)
            {
                digitalWrite(4, LOW); // Turn off fan if auto
            }
        }

        update_hvac_state();
    }

    while (Serial.available() > 0)
    {
        char read_wpf = Serial.read();

        /* Force Refresh */
        if (read_wpf == '0')
        {
            send_hvac_status();
            return;
        }

        /* Handle Fan Modes */
        if (read_wpf == '1')
        {
            set_fan_mode(false);
            return;
        }
        if (read_wpf == '2')
        {
            set_fan_mode(true);
            return;
        }

        /* Handle Heating and Cooling Modes */
        handle_mode_changes(read_wpf);
        update_hvac_state();
    }
}

void send_hvac_status()
{
    Serial.print(is_fan_auto ? "<HvacFanAuto>" : "<HvacFanOn>");
    Serial.print("<HvacState_");
    Serial.print(hvac_state);
    Serial.print(">");
    Serial.print("<TEMP_SET_");
    Serial.print(temp_set);
    Serial.print(">");
}

void set_fan_mode(bool auto_mode)
{
    is_fan_auto = auto_mode;
    Serial.print(auto_mode ? "<HvacFanAuto>" : "<HvacFanOn>");
    if (auto_mode)
    {
        handle_off_modes();
    }
    else
    {
        digitalWrite(4, HIGH); // Keep fan on
    }
}

void handle_mode_changes(char mode)
{
    switch (mode)
    {
    case '3': // Program On
        switch (hvac_state)
        {
        case hvac_cooling_off:
        case hvac_heating_off:
            hvac_state = (temp_int > temp_set) ? hvac_heating_standby : hvac_cooling_standby;
            break;
        }
        break;
    case '4': // Program Off
        hvac_state = hvac_cooling_off;
        digitalWrite(5, LOW);
        digitalWrite(6, LOW);
        if (is_fan_auto) digitalWrite(4, LOW); // Turn off fan if auto
        break;
    case '5': // Heating Mode
        if (hvac_state != hvac_cooling_running && hvac_state != hvac_cooling_standby)
            hvac_state = hvac_heating_standby;
        break;
    case '6': // Cooling Mode
        if (hvac_state != hvac_heating_running && hvac_state != hvac_heating_standby)
            hvac_state = hvac_cooling_standby;
        break;
    }
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
    case hvac_heating_running:
        if (temp_int >= temp_set || temp_int <= temp_set)
        {
            hvac_state = hvac_cooling_purging;
            Serial.print("<HvacState_3>");
            digitalWrite(5, LOW);
            digitalWrite(6, LOW);
            cool_down_timer = millis(); // Start the cool-down period
        }
        break;
    case hvac_cooling_standby:
    case hvac_heating_standby:
        if ((temp_int - temp_set) > 2)
        {
            hvac_state = (hvac_state == hvac_cooling_standby) ? hvac_cooling_running : hvac_heating_running;
            digitalWrite(5, HIGH);
            digitalWrite(6, HIGH);
            digitalWrite(4, HIGH);
        }
        break;
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

void handle_off_modes()
{
    digitalWrite(5, LOW);
    digitalWrite(6, LOW);
    digitalWrite(4, LOW);
}
