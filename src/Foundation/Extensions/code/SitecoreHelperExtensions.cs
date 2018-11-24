using Sitecore.Mvc.Helpers;

namespace SitecoreCoffee.Foundation.Extensions
{
    public static class SitecoreHelperExtensions
    {
        /// <summary>
        /// Gets current rendering property
        /// </summary>
        /// <param name="sitecoreHelper">SitecoreHelper instance</param>
        /// <param name="key">A key of property</param>
        /// <returns>Property value</returns>
        public static string GetRenderingProperty(this SitecoreHelper sitecoreHelper, string key)
        {
            string value = string.Empty;

            var renderingContext = Sitecore.Mvc.Presentation.RenderingContext.CurrentOrNull;

            if (renderingContext != null)
            {
                value = renderingContext.Rendering?.Properties[key];
            }

            return value;
        }
    }
}