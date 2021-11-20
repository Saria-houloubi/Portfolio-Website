namespace Portfolio.Core.Abstractions
{
    /// <summary>
    /// used to get datetime values
    /// </summary>
    public interface IDateTimeProvider
    {
        /// <summary>
        /// Gets the time now
        /// </summary>
        DateTime Now { get; }

        DateTimeOffset NowOffSet { get; }
    }
}
