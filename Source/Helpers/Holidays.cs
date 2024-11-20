namespace HomeControl.Source.Helpers;

public static class Holidays {
    public static List<HolidayBlock> GetHolidays(int year) {
        List<HolidayBlock> holidays = new();

        // New Years Day -- January 1st
        DateTime newYearsDate = new(year, 1, 1);
        holidays.Add(new HolidayBlock {
            Date = newYearsDate,
            Holiday = "New Year's"
        });

        // MLK Day -- 3rd Monday in January 
        int mlk = (from day in Enumerable.Range(1, 31)
            where new DateTime(year, 1, day).DayOfWeek == DayOfWeek.Monday
            select day).ElementAt(2);
        DateTime mlkDay = new(year, 1, mlk);
        holidays.Add(new HolidayBlock {
            Date = mlkDay,
            Holiday = "MLK"
        });

        // Valentine's Day -- February 14th
        DateTime valentinesDay = new(year, 2, 14);
        holidays.Add(new HolidayBlock {
            Date = valentinesDay,
            Holiday = "Valentine's"
        });

        // Presidents Day -- 3rd Monday in February 
        int presidents = (from day in Enumerable.Range(1, 28)
            where new DateTime(year, 2, day).DayOfWeek == DayOfWeek.Monday
            select day).ElementAt(2);
        DateTime presidentsDay = new(year, 2, presidents);
        holidays.Add(new HolidayBlock {
            Date = presidentsDay,
            Holiday = "President's"
        });

        // Easter Sunday -- Complicated 
        int g = year % 19;
        int c = year / 100;
        int h = (c - c / 4 - (8 * c + 13) / 25 + 19 * g + 15) % 30;
        int i = h - h / 28 * (1 - h / 28 * (29 / (h + 1)) * ((21 - g) / 11));

        int easterDay = i - (year + year / 4 + i + 2 - c + c / 4) % 7 + 28;
        int easterMonth = 3;

        if (easterDay > 31) {
            easterMonth++;
            easterDay -= 31;
        }

        holidays.Add(new HolidayBlock {
            Date = new DateTime(year, easterMonth, easterDay),
            Holiday = "Easter"
        });

        // Mother's Day -- 2nd Sunday in May
        int mothers = (from day in Enumerable.Range(1, 31)
            where new DateTime(year, 5, day).DayOfWeek == DayOfWeek.Sunday
            select day).ElementAt(1);
        DateTime mothersDay = new(year, 5, mothers);
        holidays.Add(new HolidayBlock {
            Date = mothersDay,
            Holiday = "Mother's"
        });

        // Memorial Day -- Last monday in May 
        DateTime memorialDay = new(year, 5, 31);
        DayOfWeek dayOfWeek = memorialDay.DayOfWeek;
        while (dayOfWeek != DayOfWeek.Monday) {
            memorialDay = memorialDay.AddDays(-1);
            dayOfWeek = memorialDay.DayOfWeek;
        }

        holidays.Add(new HolidayBlock {
            Date = memorialDay,
            Holiday = "Memorial"
        });

        // Father's Day -- 3nd Sunday in June
        int fathers = (from day in Enumerable.Range(1, 30)
            where new DateTime(year, 6, day).DayOfWeek == DayOfWeek.Sunday
            select day).ElementAt(2);
        DateTime fathersDay = new(year, 6, fathers);
        holidays.Add(new HolidayBlock {
            Date = fathersDay,
            Holiday = "Father's"
        });

        // Juneteenth - June 19th
        DateTime juneteenth = new(year, 6, 19);
        holidays.Add(new HolidayBlock {
            Date = juneteenth,
            Holiday = "Juneteenth"
        });


        // Independence Day - July 4th
        DateTime independenceDay = new(year, 7, 4);
        holidays.Add(new HolidayBlock {
            Date = independenceDay,
            Holiday = "Independence"
        });

        // Labor Day -- 1st Monday in September 
        DateTime laborDay = new(year, 9, 1);
        dayOfWeek = laborDay.DayOfWeek;
        while (dayOfWeek != DayOfWeek.Monday) {
            laborDay = laborDay.AddDays(1);
            dayOfWeek = laborDay.DayOfWeek;
        }

        holidays.Add(new HolidayBlock {
            Date = laborDay,
            Holiday = "Labor"
        });

        // 9/11 -- September 11th
        DateTime nineEleven = new(year, 9, 11);
        holidays.Add(new HolidayBlock {
            Date = nineEleven,
            Holiday = "9/11"
        });

        // Veterans Day -- November 11th
        DateTime veteransDay = new(year, 11, 11);
        holidays.Add(new HolidayBlock {
            Date = veteransDay,
            Holiday = "Veterans"
        });

        // Halloween -- October 31st
        DateTime halloweenDay = new(year, 10, 31);
        holidays.Add(new HolidayBlock {
            Date = halloweenDay,
            Holiday = "Halloween"
        });

        // Thanksgiving Day -- 4th Thursday in November 
        int thanksgiving = (from day in Enumerable.Range(1, 30)
            where new DateTime(year, 11, day).DayOfWeek == DayOfWeek.Thursday
            select day).ElementAt(3);
        DateTime thanksgivingDay = new(year, 11, thanksgiving);
        holidays.Add(new HolidayBlock {
            Date = thanksgivingDay,
            Holiday = "Thanksgiving"
        });

        // Day After Thanksgiving
        holidays.Add(new HolidayBlock {
            Date = thanksgivingDay.AddDays(1),
            Holiday = "Black Friday"
        });

        // Christmas Eve -- December 24th
        DateTime christmasEve = new(year, 12, 24);
        holidays.Add(new HolidayBlock {
            Date = christmasEve,
            Holiday = "Christmas Eve"
        });

        // Christmas Day 
        holidays.Add(new HolidayBlock {
            Date = christmasEve.AddDays(1),
            Holiday = "Christmas"
        });

        // Daylight Savings Start -- 2nd Sunday of March
        int dlsStart = (from day in Enumerable.Range(1, 31)
            where new DateTime(year, 3, day).DayOfWeek == DayOfWeek.Sunday
            select day).ElementAt(1);
        DateTime dlsStartDay = new(year, 3, dlsStart);
        holidays.Add(new HolidayBlock {
            Date = dlsStartDay,
            Holiday = "DST Start"
        });

        // Daylight Savings End -- 1st Sunday in November
        DateTime dlsEnd = new(year, 11, 1);
        dayOfWeek = dlsEnd.DayOfWeek;
        while (dayOfWeek != DayOfWeek.Sunday) {
            dlsEnd = dlsEnd.AddDays(1);
            dayOfWeek = dlsEnd.DayOfWeek;
        }

        holidays.Add(new HolidayBlock {
            Date = dlsEnd,
            Holiday = "DST End"
        });

        return holidays;
    }

    public class HolidayBlock {
        public DateTime Date { get; set; }
        public string Holiday { get; set; }
    }
}