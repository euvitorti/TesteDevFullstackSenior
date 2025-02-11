namespace Infra.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ToUtc(this DateTime dateTime)
        {
            // Se o Kind for Unspecified, define explicitamente como UTC; 
            // caso contrário, usa ToUniversalTime() para converter.
            return dateTime.Kind == DateTimeKind.Unspecified 
                ? DateTime.SpecifyKind(dateTime, DateTimeKind.Utc)
                : dateTime.ToUniversalTime();
        }
    }
}
