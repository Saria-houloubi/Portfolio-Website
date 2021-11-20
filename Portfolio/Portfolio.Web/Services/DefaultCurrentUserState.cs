using Portfolio.Core.Abstractions;

namespace Portfolio.Web.Services
{
    /// <summary>
    /// The scoped service for each user
    /// </summary>
    public class DefaultCurrentUserState : ICurrentUserState
    {
        #region Properties
        /// <summary>
        /// Get what is the current language prefered for user
        /// </summary>
        string ICurrentUserState.Language { get; set; } = "_"; //Defaults to none;
        #endregion
    }
}
