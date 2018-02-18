namespace SitecoreCoffee.Foundation.ORM.Repositories
{
    public interface IDatasourceRepository
    {
        string DataSource { get; }

        T GetCurrentDatasourceItem<T>(bool isLazy = true, bool inferType = false) where T : class;
    }
}