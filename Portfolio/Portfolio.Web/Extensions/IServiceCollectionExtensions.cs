using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Portfolio.Core.Abstractions;
using Portfolio.Shared.Extensions;
using Portfolio.Web.Models;

namespace Portfolio.Web.Setup
{
    public static class IServiceCollectionExtensions
    {

        /// <summary>
        /// Calls all <see cref="IAppRegister"/> Implementation in folder under path under key App:Register:Dll:Path
        ///     if key is not found in counfiguration then it will take current executing folder path
        /// </summary>
        /// <param name="serviceCollection">Service colleciton to be injected to all <see cref="IAppRegister"/> implementation</param>
        /// <param name="configuration">configuration colleciton to be injected to all <see cref="IAppRegister"/> implementation</param>
        public static void RegisterSolutionServices(this IServiceCollection serviceCollection, [NotNull] IConfiguration configuration)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            //the path to pick up IAppRegister implemenation
            var _dllPath = string.Empty;
            //Check if there is a configuration attached
            if (configuration is not null)
            {
                _dllPath = configuration["App:Register:Dll:Path"];
            }

            _dllPath = string.IsNullOrEmpty(_dllPath)
                //if the path was not set then we will try to pick up from main current folder
                ? Directory.GetParent(Assembly.GetExecutingAssembly().Location)?.FullName
                : _dllPath;
            //the list will hold all registers
            var _appRegisterList = new List<IAppRegister>();
            //Get the IVodStartup from all Vod assemblies 
            var dlls = Directory.GetFiles(_dllPath, "Portfolio.**.dll");

            if (dlls is null)
                throw new ArgumentNullException("Could not load any dlls for startup");

            foreach (var item in dlls)
            {
                var assemblyStartups = item.GetOrLoadAssembly().GetExportedTypes()
                    .Where(item => item.GetInterface(typeof(IAppRegister).Name) != null)
                    .Select(item => Activator.CreateInstance(item) as IAppRegister);

                if (assemblyStartups != null)
                    _appRegisterList.AddRange(assemblyStartups);
            }
            //Order the list of registers
            _appRegisterList = new List<IAppRegister>(_appRegisterList.OrderByDescending(i => i.Order));
            //Call each register
            foreach (var register in _appRegisterList)
            {
                register?.RegisterServices(serviceCollection, configuration);
            }
        }
        /// <summary>
        /// Shortcut to regisert an IEnumrable of <see cref="ProjectModel"/>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration">the configuration to read from</param>
        /// <param name="keyPath">The key path in confiuguration to read values in</param>
        public static void Data_RegisterProjectsFromConfig(this IServiceCollection services, IConfiguration configuration, string keyPath = "Me:Projects")
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (string.IsNullOrEmpty(keyPath))
            {
                throw new ArgumentException($"'{nameof(keyPath)}' cannot be null or empty.", nameof(keyPath));
            }

            var projects = new List<ProjectModel>();

            //Read the configuration values
            configuration.Bind(keyPath, projects);

            //Add it in the injection kernel
            services.AddSingleton<IEnumerable<ProjectModel>>(projects);
        }
    }
}
