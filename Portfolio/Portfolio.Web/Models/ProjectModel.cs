using System.Diagnostics.CodeAnalysis;

namespace Portfolio.Web.Models
{
    /// <summary>
    /// Personal projects model to be read from configuration or any data provider source
    /// </summary>
    public class ProjectModel
    {
        #region Properties
        [MaybeNull]
        public string Title { get; set; }
        [MaybeNull]
        public string ShortDescription { get; set; }
        [MaybeNull]
        public string FullDescription { get; set; }
        public int StartYear { get; set; }
        [MaybeNull]
        public IEnumerable<string> Tags { get; set; }
        [MaybeNull]
        public IEnumerable<string> RawLinks { get; set; }
        #endregion
    }
}
