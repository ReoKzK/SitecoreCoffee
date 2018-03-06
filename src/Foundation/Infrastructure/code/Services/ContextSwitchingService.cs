using System;
using Sitecore.Data;

namespace SitecoreCoffee.Foundation.Infrastructure.Services
{
    public class ContextSwitchingService : IContextSwitchingService
    {
        public bool IsExperienceEditor => Sitecore.Context.PageMode.IsExperienceEditor;

        public void SwitchContextItem(Guid id)
        {
            var itemId = new ID(id);
            SwitchContextItem(itemId);
        }

        public void SwitchContextItem(ID id)
        {
            Sitecore.Context.Item = Sitecore.Context.Database.GetItem(id);
            Sitecore.Mvc.Presentation.PageContext.Current.Item = Sitecore.Context.Database.GetItem(id);
        }
    }
}