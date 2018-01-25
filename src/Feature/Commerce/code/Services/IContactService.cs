namespace SitecoreCoffee.Feature.Commerce.Services
{
    public interface IContactService
    {
        bool IdentifyAs(string email);

        void SessionAbandon();
    }
}