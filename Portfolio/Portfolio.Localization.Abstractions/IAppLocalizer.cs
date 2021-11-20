namespace Portfolio.Localization.Abstractions
{
    /// <summary>
    /// Localize and centrilize values
    /// </summary>
    public interface IAppLocalizer
    {
        #region Properties
        public string CurrentLang { get; }
        #endregion
        /// <summary>
        /// Access a value based on key and <see cref="CurrentLang"/>
        /// </summary>
        /// <param name="key">the key name</param>
        /// <returns></returns>
        public string this[string key] { get; }
        /// <summary>
        /// Access a value based on key and lang paramters
        /// </summary>
        /// <param name="key">The key name</param>
        /// <param name="lang">language 2 char code</param>
        /// <returns></returns>
        public string this[string key, string lang] { get; }
        /// <summary>
        /// Makes sure to load the data
        ///     Note: there is a need to call this function as it will be called interanly
        /// </summary>
        /// <param name="hardreload">Forces a reload of the data</param>
        /// <returns>True if the load was succesfull false otherwise</returns>
        public bool EnsureDataIsLoaded(bool hardreload =false);
    }
}
