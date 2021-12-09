using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Html;

namespace Portfolio.Web.Extensions
{
    public static class ComponentBaseExtensions
    {
        /// <summary>
        /// return html string content
        /// </summary>
        /// <param name="component"></param>
        /// <param name="rawhtml"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static MarkupString RenderRawHtml(this ComponentBase component, string rawhtml)
        {
            if (component is null)
            {
                throw new ArgumentNullException(nameof(component));
            }

            if (rawhtml is null)
            {
                throw new ArgumentNullException(nameof(rawhtml));
            }

            return (MarkupString)rawhtml;
        }
    }
}
