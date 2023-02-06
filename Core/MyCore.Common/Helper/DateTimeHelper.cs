using System.Globalization;
public static class DateTimeHelper
{
    public static DateTime? ToDateTime(this long numberDate)
    {
        DateTime result;
        if (numberDate == 0)
            return null;

        if (DateTime.TryParseExact(numberDate.ToString(), new[] { "yyyyMMddHHmmss", "yyyyMMddHHmm" }, CultureInfo.InvariantCulture,
            DateTimeStyles.None, out result))
            return result;
        return null;
    }

    public static long Now => DateTime.Now.ToLong();
    public static int GetDay(this long dt) => dt.ToDateTime().Value.Day;
    public static long ToLongDate(this DateTime dt)
    {
        long outResult;
        return long.TryParse(dt.ToString("yyyyMMddHHmmss"), out outResult)
            ? outResult
            : 0;
    }
    public static long ToShortDate(this DateTime dt)
    {
        long outResult;
        return long.TryParse(dt.ToString("yyyyMMdd"), out outResult)
            ? outResult
            : 0;
    }
}
