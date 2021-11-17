using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Portfolio.Core.Abstractions
{
    /// <summary>
    /// Picked up dynimcly on each project to register services
    /// </summary>
    public interface IAppRegister
    {
        /// <summary>
        /// What is the order to be called at
        /// </summary>
        public int Order { get; }
        /// <summary>
        /// Gets called on application register pipeline
        /// </summary>
        /// <param name="serviceDescriptors">Service colleciton to be injected to all <see cref="IAppRegister"/> implementation</param>
        /// <param name="configuration">configuration colleciton to be injected to all <see cref="IAppRegister"/> implementation</param>
        public void RegisterServices(IServiceCollection serviceCollection,[MaybeNull] IConfiguration configuration);
    }
}
