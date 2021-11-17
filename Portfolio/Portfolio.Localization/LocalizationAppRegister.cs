using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Core.Abstractions;
using Portfolio.Localization.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace Portfolio.Localization
{
    public class LocalizationAppRegister : IAppRegister
    {
        public int Order => 20;

        public void RegisterServices(IServiceCollection serviceCollection, [MaybeNull] IConfiguration configuration)
        {
            if (configuration is not null)
            {
                var options = new JsonFIleAppLocalizerOptions();

                var _configSections = configuration.GetSection(JsonFIleAppLocalizerOptions.ConfigurationPath);

                options.FilePath = _configSections.GetSection(nameof(JsonFIleAppLocalizerOptions.FilePath)).Value;

                serviceCollection.AddSingleton(options);
            }
            
            serviceCollection.AddScoped<IAppLocalizer, JsonFileAppLocalizer>();
        }
    }
}
