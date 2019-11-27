using Example.Library.DbContexts;
using XPike.DataStores.EntityFrameworkCore;

namespace Example.Library.DataStores.EntityFrameworkCore
{
    public interface IEntityFrameworkExampleDataStore
        : IEntityFrameworkCoreDataStore<ExampleDbContext>,
          IExampleDataStore
    {
    }
}