namespace HomeControl.Source.Helpers;

public static class Holidays {
    public static List<HolidayBlock> GetHolidays(int year) {
        List<HolidayBlock> holidays = new();

        // Fixed-date holidays
        AddFixedHoliday(holidays, year, 1, 1, "New Year's Day");
        AddFixedHoliday(holidays, year, 2, 14, "Valentine's Day");
        AddFixedHoliday(holidays, year, 6, 19, "Juneteenth");
        AddFixedHoliday(holidays, year, 7, 4, "Independence Day");
        AddFixedHoliday(holidays, year, 9, 11, "9/11");
        AddFixedHoliday(holidays, year, 10, 31, "Halloween");
        AddFixedHoliday(holidays, year, 11, 11, "Veterans Day");
        AddFixedHoliday(holidays, year, 12, 24, "Christmas Eve");
        AddFixedHoliday(holidays, year, 12, 25, "Christmas Day");

        // Dynamic holidays
        AddDynamicHoliday(holidays, year, 1, DayOfWeek.Monday, 3, "MLK Day");
        AddDynamicHoliday(holidays, year, 2, DayOfWeek.Monday, 3, "Presidents Day");
        AddDynamicHoliday(holidays, year, 3, DayOfWeek.Sunday, 2, "DST Start");
        AddDynamicHoliday(holidays, year, 5, DayOfWeek.Sunday, 2, "Mother's Day");
        AddLastWeekdayHoliday(holidays, year, 5, DayOfWeek.Monday, "Memorial Day");
        AddDynamicHoliday(holidays, year, 6, DayOfWeek.Sunday, 3, "Father's Day");
        AddDynamicHoliday(holidays, year, 9, DayOfWeek.Monday, 1, "Labor Day");
        AddDynamicHoliday(holidays, year, 11, DayOfWeek.Thursday, 4, "Thanksgiving");

        // Black Friday is the day after Thanksgiving (4th Thursday in November)
        DateTime? thanksgiving = holidays.FirstOrDefault(h => h.Holiday == "Thanksgiving")?.Date;
        if (thanksgiving != null) {
            holidays.Add(new HolidayBlock {
                Date = thanksgiving.Value.AddDays(1),
                Holiday = "Black Friday"
            });
        }

        AddDynamicHoliday(holidays, year, 11, DayOfWeek.Sunday, 1, "DST End");

        // Easter (calculated)
        holidays.Add(new HolidayBlock {
            Date = CalculateEasterSunday(year),
            Holiday = "Easter"
        });

        return holidays;
    }

    // Adds a fixed-date holiday
    private static void AddFixedHoliday(List<HolidayBlock> holidays, int year, int month, int day, string name) {
        holidays.Add(new HolidayBlock {
            Date = new DateTime(year, month, day),
            Holiday = name
        });
    }

    // Adds a holiday on the nth occurrence of a specific day in a month
    private static void AddDynamicHoliday(List<HolidayBlock> holidays, int year, int month, DayOfWeek dayOfWeek, int occurrence, string name) {
        DateTime firstDay = new(year, month, 1);
        int offset = ((int)dayOfWeek - (int)firstDay.DayOfWeek + 7) % 7; // Offset to the first occurrence
        DateTime holidayDate = firstDay.AddDays(offset + (occurrence - 1) * 7);
        holidays.Add(new HolidayBlock {
            Date = holidayDate,
            Holiday = name
        });
    }

    // Adds a holiday on the last occurrence of a specific day in a month
    private static void AddLastWeekdayHoliday(List<HolidayBlock> holidays, int year, int month, DayOfWeek dayOfWeek, string name) {
        DateTime lastDay = new(year, month, DateTime.DaysInMonth(year, month));
        int offset = (int)lastDay.DayOfWeek - (int)dayOfWeek;
        if (offset < 0) offset += 7; // Adjust if the day has already passed
        DateTime holidayDate = lastDay.AddDays(-offset);
        holidays.Add(new HolidayBlock {
            Date = holidayDate,
            Holiday = name
        });
    }

    // Calculates Easter Sunday for a given year
    private static DateTime CalculateEasterSunday(int year) {
        int g = year % 19;
        int c = year / 100;
        int h = (c - c / 4 - (8 * c + 13) / 25 + 19 * g + 15) % 30;
        int i = h - h / 28 * (1 - h / 28 * (29 / (h + 1)) * ((21 - g) / 11));
        int day = i - (year + year / 4 + i + 2 - c + c / 4) % 7 + 28;

        return day > 31
            ? new DateTime(year, 4, day - 31) // April
            : new DateTime(year, 3, day); // March
    }

    // Data class for holiday details
    public class HolidayBlock {
        public DateTime Date { get; set; }
        public string Holiday { get; set; }
    }
}