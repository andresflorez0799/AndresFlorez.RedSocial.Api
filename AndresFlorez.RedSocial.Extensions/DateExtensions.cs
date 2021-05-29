using System;

namespace AndresFlorez.RedSocial.Extensions
{
    public static class DateExtensions
    {
        public static DateTime ToDateTime(this string fechaString)
        {
            if (!string.IsNullOrEmpty(fechaString))
            {
                if (fechaString.Contains("/") || fechaString.Contains("-"))
                    return Convert.ToDateTime(fechaString);
                else
                    return DateTime.ParseExact(fechaString, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            }
            else
                return DateTime.MinValue;
        }

        public static TimeSpan ToTimeSpan(this string horaString)
        {
            if (!string.IsNullOrEmpty(horaString))
            {
                if (horaString.Contains(":"))
                {
                    double minutes = Convert.ToDateTime(string.Format("{0} {1}", DateTime.MinValue, horaString)).Minute;
                    return TimeSpan.FromMinutes(minutes);
                }
                else
                    return TimeSpan.ParseExact(horaString, "hhmmss", System.Globalization.CultureInfo.InvariantCulture);
            }
            else
                return TimeSpan.Zero;
        }

        public static DateTime FirstDayOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1);
        }

        public static int DaysInMonth(this DateTime value)
        {
            return DateTime.DaysInMonth(value.Year, value.Month);
        }
        public static DateTime LastDayOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.DaysInMonth());
        }

    }
}
