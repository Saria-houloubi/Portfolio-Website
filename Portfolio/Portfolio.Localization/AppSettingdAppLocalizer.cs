using Microsoft.Extensions.Configuration;
using Portfolio.Core.Abstractions;
using Portfolio.Localization.Abstractions;
using System.Collections.Concurrent;

namespace Portfolio.Localization
{
    /// <summary>
    /// Gets the data based on AppSettings
    /// </summary>
    internal class AppSettingdAppLocalizer : IAppLocalizer
    {

        #region Properties
        /// <summary>
        /// Options required for the app to run
        /// </summary>
        public AppSettingsAppLocalizerOptions Options { get; private set; }

        /// <summary>
        /// The dictionary to hold all localized keys
        ///     Note: for this implementation we will be using a static varible but you could set in memory cache and load from there
        ///         then hold in a static varibale
        /// </summary>
        static IDictionary<string, string> _localizedKeys = null;

        /// <summary>
        /// Semaphore used to make sure that data is not being loaded using multiple threads
        /// </summary>
        static readonly SemaphoreSlim _semaphoreSlim = new(1, 1);

        /// <summary>
        /// The configuration to be injected for reading from
        /// </summary>
        private readonly IConfiguration _configuration;
        /// <summary>
        /// Gets data on the current user connected
        /// </summary>
        private readonly ICurrentUserState _currentUser;
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        /// <param name="options">Options and configuration required to run</param>
        /// <param name="configuration">The configuration to get localized values from based on <see cref="AppSettingsAppLocalizerOptions.ConfigurationPath"/> oath</param>
        /// <exception cref="ArgumentNullException">Throws exception the sent options or configuration or Icurrentuser is null</exception>
        public AppSettingdAppLocalizer(AppSettingsAppLocalizerOptions options, IConfiguration configuration, ICurrentUserState currentUser)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        }
        #endregion

        public string this[string key] => this[key, CurrentLang];

        public string this[string key, string lang]
        {

            get
            {
                if (string.IsNullOrEmpty(key))
                    throw new ArgumentNullException("Key can not be nul");

                if (string.IsNullOrEmpty(lang))
                    lang = CurrentLang;

                //If the data was not yet loaded or there is no values
                if (_localizedKeys is null || !_localizedKeys.Any())
                    //Wait until it gets loaded
                    _ = EnsureDataIsLoaded();

                if (_localizedKeys is not null)
                {
                    var fullKeyName = $"{lang}.{key}";
                    var DefaultLanguageKeyName = $"_.{key}";

                    if (_localizedKeys.TryGetValue(fullKeyName, out var _v1))
                        return _v1;
                    else if (_localizedKeys.TryGetValue(DefaultLanguageKeyName, out var _v2))
                        return _v2;
                }

                return key;
            }
        }

        public string CurrentLang => _currentUser.Language ?? "_";

        #region Helpers
        /// <summary>
        /// Loads the <see cref="_localizedKeys"/> from file path in <see cref="Options"/>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException">if the file in options.FilePath is not found</exception>
        /// <exception cref="ArgumentNullException">if we could correctly read data from file in options.Filepath</exception>
        public bool EnsureDataIsLoaded(bool hardreload = false)
        {
            try
            {
                //Wait if there is prevous thread in
                _semaphoreSlim.Wait();

                //Recheck if the data was loaded
                if (!hardreload && _localizedKeys is not null && _localizedKeys.Any())
                    return true;

                var section = _configuration.GetSection(AppSettingsAppLocalizerOptions.ConfigurationPath);

                if (!section.Exists())
                    throw new Exception($"Unable to find section {AppSettingsAppLocalizerOptions.ConfigurationPath}");

                var _loadedData = section.AsEnumerable(true);

                _localizedKeys = new ConcurrentDictionary<string, string>();

                //Add the items to dictionary
                foreach (var k in _loadedData)
                {
                    //Skip any null values
                    if (k.Value is null)
                        continue;

                    var dicKey = k.Key.Replace(':', '.');

                    //Check if the key is already added
                    if (!_localizedKeys.ContainsKey(dicKey))
                        _localizedKeys.Add(dicKey, k.Value);
                    else
                        //Throw early excption to break and check
                        throw new Exception($"Duplicate key found {dicKey}");
                }

                //Return true if we were able to load any data
                return _localizedKeys.Any();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //Relase the semaphore no matter what happned 
                _semaphoreSlim.Release();
            }
        }

        #endregion

    }
}
