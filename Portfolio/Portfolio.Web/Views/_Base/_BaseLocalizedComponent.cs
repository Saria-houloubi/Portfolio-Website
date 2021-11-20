using Microsoft.AspNetCore.Components;
using Portfolio.Localization.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace Portfolio.Web.Views._Base
{
    /// <summary>
    /// A base localized component that allows access to <see cref="IAppLocalizer"/>
    /// </summary>
    public abstract class _BaseLocalizedComponent : ComponentBase
    {
        #region Properties
        [MaybeNull] [Inject] protected IAppLocalizer? _al { get; private set; }
        #endregion

        #region Helpers
        /// <summary>
        /// Helpers to access localizer
        ///     Note: it has been shortcuted to _l for shorter use
        /// </summary>
        /// <param name="key">The key to search for</param>
        /// <returns></returns>
        protected string _l(string key) => _al?[key] ?? string.Empty;
        /// <summary>
        /// Helpers to access localizer
        ///     Note: it has been shortcuted to _l for shorter use
        /// </summary>
        /// <param name="key">The key to search for</param>
        /// <param name="lang">Force language if not sent will take default one</param>
        /// <returns></returns>
        protected string _l(string key, string lang) => _al?[key, lang] ?? string.Empty;
        #endregion
    }
}
