using Sitecore.Analytics;
using Sitecore.Analytics.Model;
using SitecoreCoffee.Foundation.Logging;
using System;
using System.Web;

namespace SitecoreCoffee.Feature.Commerce.Services
{
    public class ContactService : IContactService
    {
        private readonly ILoggerService _logService;

        public ContactService(ILoggerService logService)
        {
            _logService = logService;
        }

        public bool IdentifyAs(string email)
        {
            var result = false;

            if (Tracker.Current.Session != null &&
                    (Tracker.Current.Contact.Identifiers.IdentificationLevel == ContactIdentificationLevel.None ||
                    !string.Equals(email, Tracker.Current.Contact.Identifiers.Identifier, StringComparison.OrdinalIgnoreCase)))
            {
                Tracker.Current.Session.Identify(email);
                
                result = true;
            }
            
            _logService.Info($"ContactService.IdentifyAs: Identifying contact as '{email}', result: {result}");

            return result;
        }

        public void SessionAbandon()
        {
            _logService.Info($"ContactService.SessionAbandon: Abandoning session");

            Tracker.Current.EndTracking();
            HttpContext.Current.Session.Abandon();
        }
    }
}