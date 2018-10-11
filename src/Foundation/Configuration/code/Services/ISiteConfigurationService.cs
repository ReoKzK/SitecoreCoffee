namespace SitecoreCoffee.Foundation.Configuration.Services
{
    public interface ISiteConfigurationService
    {
        T GetConfiguration<T>() where T : class;
    }
}