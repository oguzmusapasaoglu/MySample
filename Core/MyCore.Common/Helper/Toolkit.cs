using System.Text.Json;
public static class Toolkit
{
    public static string ToJson<T>(this IQueryable<T> data) where T : class =>
        JsonSerializer.Serialize(data);
    public static IQueryable<T> FromJson<T>(this string jsonData) where T : class =>
        JsonSerializer.Deserialize<List<T>>(jsonData).AsQueryable();
    public static int ToInt(this bool value) => (value) ? 1 : 0;
    public static bool IsNullOrEmpty(this object value) =>
        (value == null || value.ToString().Trim() == string.Empty);
    public static bool IsNullOrLessOrEqToZero(this object value) =>
        (value == null || value.ToLong() <= 0);
    public static bool DataIsNullOrEmpty<T>(this IEnumerable<T>? value) =>
        (value == null || !value.Any());
    public static bool IsNotNullOrEmpty(this object value) =>
        (value == null || value.ToString().Trim() == string.Empty) ? false : true;
    public static int ToInt(this object value)
    {
        int ParmOut;
        return int.TryParse(value.ToString(), out ParmOut)
            ? ParmOut
            : 0;
    }
    public static long ToLong(this object value)
    {
        long ParmOut;
        return long.TryParse(value.ToString(), out ParmOut)
            ? ParmOut
            : 0;
    }
}