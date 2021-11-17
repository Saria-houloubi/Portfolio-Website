using Portfolio.Localization.Abstractions;
using System.Collections.Concurrent;
using System.Text.Json;

namespace Portfolio.Localization
{
    /// <summary>
    /// Gets the data based on Json file from configuration
    /// </summary>
    internal class JsonFileAppLocalizer : IAppLocalizer
    {

        #region Properties
        /// <summary>
        /// Options required for the app to run
        /// </summary>
        public JsonFIleAppLocalizerOptions Options { get; private set; }

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
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        /// <param name="options">Options and configuration required to run</param>
        /// <exception cref="ArgumentNullException">Throws exception the sent options are null</exception>
        public JsonFileAppLocalizer(JsonFIleAppLocalizerOptions options)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
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
                    EnsureDataIsLoadedAsync().AsTask().Wait();

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

        public string CurrentLang => "_";

        #region Helpers
        /// <summary>
        /// Loads the <see cref="_localizedKeys"/> from file path in <see cref="Options"/>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException">if the file in options.FilePath is not found</exception>
        /// <exception cref="ArgumentNullException">if we could correctly read data from file in options.Filepath</exception>
        public async ValueTask<bool> EnsureDataIsLoadedAsync(bool hardreload = false)
        {
            try
            {
                //Wait if there is prevous thread in
                _semaphoreSlim.Wait();

                //Recheck if the data was loaded
                if (!hardreload && _localizedKeys is not null && _localizedKeys.Any())
                    return true;

                //Check for the file if found
                if (!File.Exists(Options.FilePath))
                    throw new FileNotFoundException(Options.FilePath);

                var text = await File.ReadAllTextAsync(Options.FilePath);

                if (text == null)
                    throw new ArgumentNullException($"Empty file in {Options.FilePath}, could not resolve any data");

                var _loadedData = JsonSerializer.Deserialize<Dictionary<string, IEnumerable<KeyValuePair<string, string>>>>(text);

                if (_loadedData == null)
                    throw new ArgumentNullException($"Could not desrialize or fetch andy data from {Options.FilePath}");

                _localizedKeys = new ConcurrentDictionary<string, string>();


                //Add the items to dictionary
                foreach (var lang in _loadedData)
                {
                    foreach (var key in lang.Value)
                        _localizedKeys.Add($"{lang.Key}.{key.Key}", key.Value);
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
