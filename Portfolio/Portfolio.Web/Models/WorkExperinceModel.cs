using System.Diagnostics.CodeAnalysis;

namespace Portfolio.Web.Models
{
    /// <summary>
    /// Work experince and company information model
    /// </summary>
    public class WorkExperinceModel
    {
        #region Properties
        [MaybeNull]
        public string CompanyName { get; set; }
        [MaybeNull]
        public string Location { get; set; }
        [MaybeNull]
        public string Title { get; set; }
        [MaybeNull]
        public string LogoUrl { get; set; }
        public int StartYear { get; set; }
        [MaybeNull]
        public int? EndYear { get; set; }
        /// <summary>
        /// Describs the responsiblites will be rendered as html
        /// </summary>
        [MaybeNull]
        public string RawDescription { get; set; }
        [MaybeNull]
        public IEnumerable<string> Tags { get; set; }
        [MaybeNull]
        public IEnumerable<string> RawLinks { get; set; }
        #endregion
    }
}
