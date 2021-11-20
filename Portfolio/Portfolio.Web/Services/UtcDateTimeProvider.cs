using Portfolio.Core.Abstractions;

namespace Portfolio.Web.Services
{
    /// <summary>
    /// Makes sure that all dates are in UTC
    /// </summary>
    public class UtcDateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.UtcNow;
        public DateTimeOffset NowOffSet => DateTimeOffset.UtcNow;
    }
}
