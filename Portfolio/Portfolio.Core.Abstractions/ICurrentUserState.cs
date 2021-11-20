namespace Portfolio.Core.Abstractions
{
    /// <summary>
    /// Gets data on the current connected user
    /// </summary>
    public interface ICurrentUserState
    {
        #region Properties
        public string Language { get; set; }
        #endregion
    }
}
