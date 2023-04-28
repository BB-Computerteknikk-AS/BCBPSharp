using System;
using System.Globalization;

namespace no.bbc.BCBPSharp;

public static class Utils
{
    public static DateTime DayOfYearToDate(string dayOfYear,
        bool hasYearPrefix,
        int? referenceYear)
    {
        var currentYear = referenceYear ?? DateTime.Now.ToUniversalTime().Year;
        var year = currentYear.ToString();
        var daysToAdd = dayOfYear;

        if (hasYearPrefix)
        {
            year = year.Substring(0, year.Length - 1) + daysToAdd[0];
            daysToAdd = daysToAdd.Substring(1);

            if (int.Parse(year) - currentYear > 2)
            {
                year = (int.Parse(year) - 10).ToString();
            }
        }

        var date = new DateTime(int.Parse(year), 1, 1).AddDays(int.Parse(daysToAdd) - 1);
        date = DateTime.SpecifyKind(date, DateTimeKind.Utc);

        var result = date.ToLocalTime();
        return result;
    }

    public static int HexToNumber(string hex)
    {
        return int.Parse(hex, NumberStyles.HexNumber);
    }

    public static string DateToDayOfYear(DateTime date, bool addYearPrefix = false)
    {
        var currentYear = DateTime.Now.Year;
        var start = DateTime.SpecifyKind(new DateTime(currentYear, 1, 1).AddDays(-1), DateTimeKind.Utc).ToLocalTime();
        var diff = date.ToUniversalTime().Subtract(DateTime.UnixEpoch).TotalMilliseconds -
                   start.ToUniversalTime().Subtract(DateTime.UnixEpoch).TotalMilliseconds +
                   (-TimeZoneInfo.Local.GetUtcOffset(start).TotalMinutes -
                    -TimeZoneInfo.Local.GetUtcOffset(date).TotalMinutes) * 60 * 1000;
        var oneDay = 1000 * 60 * 60 * 24;
        var dayOfYear = Math.Floor(diff / oneDay);
        var yearPrefix = "";

        if (addYearPrefix)
        {
            yearPrefix = date.Year.ToString().Substring(0, date.Year.ToString().Length - 1);
        }

        var result = yearPrefix + dayOfYear.ToString(CultureInfo.InvariantCulture);
        return result;
    }
}
