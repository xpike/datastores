using Example.Library;
using XPike.IoC;

namespace XPikeDataStores
{
    public class Package
        : IDependencyPackage
    {
        public void RegisterPackage(IDependencyCollection dependencyCollection)
        {
            dependencyCollection.LoadPackage(new XPike.Configuration.Microsoft.Package());

            dependencyCollection.AddExampleLibrary();
            
            // NOTE: This is called in Startup.cs against IServiceCollection instead, since we need EF Core support.
            //dependencyCollection.AddXPikePomeloMySqlDataStores();
            dependencyCollection.LoadPackage(new Example.Library.Package());
            dependencyCollection.LoadPackage(new XPike.DataStores.MySql.Pomelo.Package());
        }
    }
}