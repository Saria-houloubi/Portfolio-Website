namespace Portfolio.Localization
{
    /// <summary>
    /// The options <see cref="JsonFileAppLocalizer"/> need to run
    /// </summary>
    internal class JsonFIleAppLocalizerOptions
    {
        #region Properties
        public static readonly string ConfigurationPath = "Localizer:Json";
        /// <summary>
        /// The file path needed to load json file
        /// </summary>
        public string? FilePath { get; set; }
        #endregion
    }
}
