# XPike.RequestContext

[![Build Status](https://dev.azure.com/xpike/xpike/_apis/build/status/xpike.datastores?branchName=master)](https://dev.azure.com/xpike/xpike/_build/latest?definitionId=14&branchName=master)
![Nuget](https://img.shields.io/nuget/v/XPike.DataStores)

Provides the XPike DataStore pattern for developing simple, performant and reliable data access libraries.

## Overview

Supports a pluggable system for obtaining database connections and connection strings,
suitable for various approaches like sharding, pooling and multi-tenancy.

ORMs:
- Entity Framework
- Dapper

> For optimal performance, it's recommended to use Dapper instead of Entity Framework.

Connection string providers:
- Raw connection string
- From a setting value

> The XPike Multi-Tenancy project provides support for database-per-tenant connection management
> through the XPike.DataStores.MultiTenant package.

Connection providers:
- MSSQL
- MySql
  - MySqlConnector
  - Oracle
  - Pomelo
- Oracle
- PostgreSQL

## Usage

**Setup in .NET Core:**

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddXPikeDependencyInjection()
            .AddXPikePomeloMySqlDataStores();
}

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseXPikeDependencyInjection();
}
```

## DataStore Pattern

The XPike DataStore pattern is analagous to what most would call a "Repository".  We use a different
name because that makes it clear that it's for the storage aspect only, and follows a more strict
pattern than the loosely defined concept of a repository.

In XPike, we also prescribe a Repository Pattern which is a layer which rests on top of a DataStore
(or other data source, such as a Driver) and provides N-tier caching (eg: local + Redis) as well as
support for distributed invalidation of local-memory caches.

##### Recommended directory structure:

```
- /Example.Library
  - /DataStores
    - IExampleDataStore.cs  
    - /MySql
      - IMySqlExampleDataStore.cs
      - MySqlExampleDataStore.cs  
      - MySqlExampleDataStoreSql.cs
      - IMySqlExampleRecordMapper.cs
      - MySqlExampleRecordMapper.cs
      - /Records
        - MySqlContactInfoRecord.cs
        - MySqlUserRecord.cs
    - /EntityFramework
      - IEntityFrameworkExampleDataStore.cs
      - EntityFrameworkExampleDataStore.cs
      - /DbContexts
        - ExampleDbContext.cs
  - /Models
    - ContactInfo.cs
    - User.cs
    - UserType.cs
```

> Key things to note:
> - `IExampleDataStore` is the database-agnostic representation of the DataStore, intended for injection.
> - The `IMySqlExampleDataStore` and `IEntityFrameworkExampleDataStore` are defined so consumers can choose a specific implementation if they need to.
> - `MySqlExampleDataStoreSql` is a `static class` with only `public const string`s containing the raw SQL queries so they don't "clutter" the DataStore code.
> - `MySqlExampleRecordMapper` converts between the Records (which should remain internal to the DataStore as they are implementation-specific) and the Models used throughout the business logic and/or exposed via an API/SDK.
> - `DbContexts` could just as easily be at the root level, a sibling to `Models`, if it's intended to be used directly without going through the DataStore.

#### Add a Connection String

**`appsettings.json`:**

```json
{
  "ExampleDB": "Server=localhost;Database=mysql;User=root;Password=itsasecrettoeveryone;TreatTinyAsBoolean=true;"
}
```

#### Create the Example Schema

**`mysql-example-schema.sql`:**

```sql
CREATE DATABASE users;

USE users;

CREATE TABLE users.userInfo (
	Id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
	`Name` NVARCHAR(100) NOT NULL,
	Credits DECIMAL(19, 2) NOT NULL DEFAULT 0,
	`Enabled` TINYINT(1) NOT NULL DEFAULT 1,
	UserTypeId INT NOT NULL,
	Expires DATETIME NULL DEFAULT NULL,
	Created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	UserLevel INT NOT NULL
);

CREATE INDEX IX_UserInfo_Name ON users.userInfo(`Name`);

CREATE DATABASE contacts;

USE contacts;

CREATE TABLE contacts.contactInfo (
	ContactId INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
	UserId INT NOT NULL,
	EmailAddress NVARCHAR(100),
	PhoneNumber NVARCHAR(20)
);

CREATE UNIQUE INDEX IX_ContactInfo_UserId ON contacts.contactInfo(UserId);

```

#### Define your Models

**`/Models/ContactInfo.cs`:**

```csharp
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using ProtoBuf;
using XPike.Contracts;

namespace Example.Library.Models
{
    [Serializable]
    [DataContract]
    [ProtoContract]
    public class ContactInfo
        : IModel
    {
        [DataMember]
        [ProtoMember(1)]
        public string EmailAddress { get; set; }

        [DataMember]
        [ProtoMember(2)]
        public string PhoneNumber { get;set; }

        [DataMember]
        [ProtoMember(3)]
        public int UserId { get; set; }

        [DataMember]
        [ProtoMember(4)]
        [Key]
        public int ContactId { get; set; }

        [DataMember]
        [ProtoMember(5)]
        public User User { get; set; }
    }
}
```

**`/Models/User.cs`:**

```csharp
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ProtoBuf;
using XPike.Contracts;

namespace Example.Library.Models
{
    [Serializable]
    [DataContract]
    [ProtoContract]
    public class User
        : IModel
    {
        [DataMember]
        [ProtoMember(1)]
        [Key]
        public int Id { get; set; }

        [DataMember]
        [ProtoMember(2)]
        public string Name { get; set; }
       
        [DataMember]
        [ProtoMember(3)]
        public decimal Credits { get; set; }
        
        [DataMember]
        [ProtoMember(4)]
        public DateTime Created { get; set; }
        
        [DataMember]
        [ProtoMember(5)]
        public bool Enabled { get; set; }
        
        [DataMember]
        [ProtoMember(6)]
        [JsonConverter(typeof(StringEnumConverter))]
        public UserType UserType { get; set; }
        
        [DataMember]
        [ProtoMember(7)]
        public DateTime? Expires { get; set; }

        [DataMember]
        [ProtoMember(8)]
        public int UserLevel { get; set; }

        [DataMember]
        [ProtoMember(9)]
        public ContactInfo ContactInfo { get; set; }
    }
}
```

**`/Models/UserType.cs`:**

```csharp
using System;
using System.Runtime.Serialization;
using ProtoBuf;

namespace Example.Library.Models
{
    [Serializable]
    [DataContract]
    [ProtoContract]
    public enum UserType
    {
        [EnumMember]
        [ProtoEnum]
        Unknown = 0,

        [EnumMember]
        [ProtoEnum]
        User = 1,
        
        [EnumMember]
        [ProtoEnum]
        Member = 2,
        
        [EnumMember]
        [ProtoEnum]
        Admin = 3
    }
}
```

##### Define the DataStore Interface

**`/DataStores/IExampleDataStore.cs`:**

```csharp
using System.Threading.Tasks;
using Example.Library.Models;
using XPike.DataStores;

namespace Example.Library.DataStores
{
    public interface IExampleDataStore
        : IDataStore
    {
        Task<User> GetExampleAsync(int id);

        Task<int?> CreateExampleAsync(User model);

        Task<bool> DeleteExampleAsync(int id);

        Task<bool> UpdateExampleAsync(User model);
    }
}
```

### Quick Start: Using Entity Framework Core

##### Define a DbContext

**`/DbContexts/ExampleDbContext.cs`:**

```csharp
using System;
using System.Linq.Expressions;
using Example.Library.Models;
using Microsoft.EntityFrameworkCore;
using XPike.DataStores;
using XPike.DataStores.EntityFrameworkCore;

namespace Example.Library.DbContexts
{
    public class ExampleDbContext
        : XPikeDbContextBase
    {
        protected override string ConnectionString => "ExampleDB";

        public DbSet<User> Examples { get; set; }

        public ExampleDbContext(ISettingsConnectionStringManager connectionManager, DbContextOptions<ExampleDbContext> options)
            : base(connectionManager, options)
        {
        }

        public ExampleDbContext(ISettingsConnectionStringManager connectionManager)
            : base(connectionManager)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(GetManagedConnectionString());
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(e =>
                                      {
                                          e.ToTable("userInfo", "users")
                                           .HasOne(u => u.ContactInfo)
                                           .WithOne(c => c.User)
                                           .HasPrincipalKey((Expression<Func<ContactInfo, object>>) (c => c.UserId));

                                          e.Property(u => u.UserType)
                                              .HasColumnName("UserTypeId");
                                      });

            modelBuilder.Entity<ContactInfo>(e =>
                                             {
                                                 e.ToTable("contactInfo", "contacts")
                                                  .HasOne(c => c.User)
                                                  .WithOne(u => u.ContactInfo)
                                                  .HasForeignKey((Expression<Func<ContactInfo, object>>) (c => c.UserId));
                                             });

            base.OnModelCreating(modelBuilder);
        }
    }
}
```

##### Create an Entity Framework DataStore

**`/DataStores/EntityFramework/IEntityFrameworkExampleDataStore.cs`:**

```csharp
using Example.Library.DbContexts;
using XPike.DataStores.EntityFrameworkCore;

namespace Example.Library.DataStores.EntityFrameworkCore
{
    public interface IEntityFrameworkExampleDataStore
        : IEntityFrameworkDataStore<ExampleDbContext>,
          IExampleDataStore
    {
    }
}
```

**`/DataStores/EntityFramework/EntityFrameworkExampleDataStore.cs`:**

```csharp
using System.Threading.Tasks;
using Example.Library.DbContexts;
using Example.Library.Models;
using Microsoft.EntityFrameworkCore;
using XPike.DataStores;
using XPike.DataStores.EntityFrameworkCore;

namespace Example.Library.DataStores.EntityFrameworkCore
{
    public class EntityFrameworkExampleDataStore
        : EntityFrameworkDataStoreBase<ExampleDbContext>,
          IEntityFrameworkExampleDataStore
    {
        protected override string ConnectionString => "Unused";

        public EntityFrameworkExampleDataStore(IEntityFrameworkDataConnectionProvider<ExampleDbContext> provider,
                                               IConnectionStringManager connectionStringManager)
            : base(provider, connectionStringManager)
        {
        }

        public Task<User> GetExampleAsync(int id) =>
            WithContextAsync(x => x.Examples.Include(y => y.ContactInfo)
                .SingleOrDefaultAsync(y => y.Id == id));

        public Task<int?> CreateExampleAsync(User model) =>
            WithContextAsync(async context =>
                             {
                                 context.Examples.Add(model);

                                 return (await context.SaveChangesAsync() > 0) ? model.Id : (int?) null;
                             });

        public Task<bool> DeleteExampleAsync(int id) =>
            WithContextAsync(async context =>
                             {
                                 context.Examples.Remove(await GetExampleAsync(id));
                                 return await context.SaveChangesAsync() > 0;
                             });

        public Task<bool> UpdateExampleAsync(User model) =>
            WithContextAsync(async context =>
                             {
                                 context.Examples.Update(model);
                                 return await context.SaveChangesAsync() > 0;
                             });
    }
}
```

### High-Performance: Dapper and Pomelo MySql

This example will use Dapper and Pomelo MySql for Entity Framework Core to build a high-performance,
low-overhead DataStore which uses hand-crafted SQL for optimum flexibility.

It also demonstrates how to use the `ISettingsConnectionStringManager` and the `Dapper.Mapper`
extensions to re-construct a complex object model from a `SELECT` query which joins across tables.

##### Define your SQL Record definitions

**`/DataStores/MySql/Records/MySqlContactInfoRecord.cs`:**

```csharp
using XPike.Contracts;

namespace Example.Library.DataStores.MySql.Records
{
    internal class MySqlContactInfoRecord
        : IRecord
    {
        public string EmailAddress { get; set; }
        public string PhoneNumber { get;set; }
        public int UserId { get; set; }
        public int ContactId { get; set; }
        public MySqlUserRecord User { get; set; }
    }
}
```

**`/DataStores/MySql/Records/MySqlUserRecord.cs`:**

```csharp
using System;
using XPike.Contracts;

namespace Example.Library.DataStores.MySql.Records
{
    internal class MySqlUserRecord
        : IRecord
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Credits { get; set; }
        public DateTime Created { get; set; }
        public bool Enabled { get; set; }
        public int UserTypeId { get; set; }
        public DateTime? Expires { get; set; }
        public int UserLevel { get; set; }
        public MySqlContactInfoRecord ContactInfo { get; set; }
    }
}
```

##### Define your SQL queries

**`/DataStores/MySql/MySqlExampleDataStoreSql.cs`:**

```csharp
namespace Example.Library.DataStores.MySql
{
    public static class MySqlExampleDataStoreSql
    {
        public const string GET_USER_SQL = @"
SELECT
    *
FROM
    users.userInfo AS u
    LEFT JOIN contacts.contactInfo AS c ON c.UserId = u.Id
WHERE
    id = @id;";

        public const string CREATE_USER_SQL = @"
INSERT INTO users.userInfo (
    Name,
    Credits,
    Created,
    Enabled,
    UserTypeId,
    Expires,
    UserLevel
)
VALUES (
    @Name,
    @Credits,
    @Created,
    @Enabled,
    @UserTypeId,
    @Expires,
    @UserLevel
);
SELECT LAST_INSERT_ID();";

        public const string UPDATE_USER_SQL = @"
UPDATE
    users.userInfo
SET
    Name = @Name,
    Credits = @Credits,
    Created = @Created,
    Enabled = @Enabled,
    UserTypeId = @UserTypeId,
    Expires = @Expires,
    UserLevel = @UserLevel
WHERE
    id = @id;
SELECT ROW_COUNT();";

        public const string DELETE_USER_SQL = @"
DELETE FROM
    users.userInfo
WHERE
    id = @id;
SELECT ROW_COUNT();";
    }
}
```

##### Create a Record Mapper

**`/DataStores/MySql/IMySqlExampleRecordMapper.cs`:**

```csharp
using Example.Library.DataStores.MySql.Records;
using Example.Library.Models;
using XPike.Contracts;

namespace Example.Library.DataStores.MySql
{
    internal interface IMySqlExampleRecordMapper
        : IMapRecord<MySqlUserRecord, User>,
          IMapModel<User, MySqlUserRecord>,
          IMapRecord<MySqlContactInfoRecord, ContactInfo>,
          IMapModel<ContactInfo, MySqlContactInfoRecord>,
          IMap<int, UserType>,
          IMap<UserType, int>
    {
    }
}
```

**`/DataStores/MySql/MySqlRecordMapper.cs`:**

```csharp
using Example.Library.DataStores.MySql.Records;
using Example.Library.Models;
using XPike.Contracts;

namespace Example.Library.DataStores.MySql
{
    internal class MySqlExampleRecordMapper
        : IMySqlExampleRecordMapper
    {
        public User Map(MySqlUserRecord input) =>
            input == null
                ? null
                : new User
                  {
                      ContactInfo = Map(input.ContactInfo),
                      UserType = ((IMap<int, UserType>)this).Map(input.UserTypeId),
                      Created = input.Created,
                      Credits = input.Credits,
                      Enabled = input.Enabled,
                      Expires = input.Expires,
                      Name = input.Name,
                      UserLevel = input.UserLevel,
                      Id = input.Id
                  };

        public MySqlUserRecord Map(User input) =>
            input == null
                ? null
                : new MySqlUserRecord
                  {
                      ContactInfo = Map(input.ContactInfo),
                      Credits = input.Credits,
                      Id = input.Id,
                      Enabled = input.Enabled,
                      Name = input.Name,
                      Expires = input.Expires,
                      Created = input.Created,
                      UserLevel = input.UserLevel,
                      UserTypeId = ((IMap<UserType, int>) this).Map(input.UserType)
                  };

        public ContactInfo Map(MySqlContactInfoRecord input) =>
            input == null
                ? null
                : new ContactInfo
                  {
                      User = Map(input.User),
                      UserId = input.UserId,
                      ContactId = input.ContactId,
                      EmailAddress = input.EmailAddress,
                      PhoneNumber = input.PhoneNumber
                  };

        public MySqlContactInfoRecord Map(ContactInfo input) =>
            input == null
                ? null
                : new MySqlContactInfoRecord
                  {
                      User = Map(input.User),
                      EmailAddress = input.EmailAddress,
                      UserId = input.UserId,
                      ContactId = input.ContactId,
                      PhoneNumber = input.PhoneNumber
                  };

        int IMap<UserType, int>.Map(UserType input) =>
            (int) input;

        UserType IMap<int, UserType>.Map(int input) =>
            (UserType) input;
    }
}
```

##### Create a MySql Implementation of the DataStore

**`/DataStores/MySql/IMySqlExampleDataStore.cs`:**

```csharp
using XPike.DataStores.Dapper;

namespace Example.Library.DataStores.MySql
{
    public interface IMySqlExampleDataStore
        : IDapperDataStore,
          IExampleDataStore
    {
    }
}
```

**`/DataStores/MySql/MySqlExampleDataStore.cs`:**

```csharp
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Example.Library.DataStores.MySql.Records;
using Example.Library.Models;
using XPike.DataStores;
using XPike.DataStores.Dapper;
using XPike.DataStores.MySql;

namespace Example.Library.DataStores.MySql
{
    public class MySqlExampleDataStore
        : DapperDataStoreBase,
            IMySqlExampleDataStore
    {
        private static readonly IMySqlExampleRecordMapper _mapper = 
            new MySqlExampleRecordMapper();

        protected override string ConnectionString => "ExampleDB";

        public MySqlExampleDataStore(IMySqlDataConnectionProvider provider,
            ISettingsConnectionStringManager connectionStringManager)
            : base(provider, connectionStringManager)
        {
        }

        public Task<User> GetExampleAsync(int id) =>
            WithSqlConnectionAsync(async connection =>
                (await connection.QueryAsync<MySqlUserRecord, MySqlContactInfoRecord, User>(
                    MySqlExampleDataStoreSql.GET_USER_SQL,
                    (u, c) =>
                    {
                        u.ContactInfo = c;
                        return _mapper.Map(u);
                    },
                    new
                    {
                        id
                    },
                    splitOn: "ContactId"))
                .SingleOrDefault());

        public Task<int?> CreateExampleAsync(User model) =>
            WithSqlConnectionAsync(async connection =>
                (await connection.QueryAsync<int?>(MySqlExampleDataStoreSql.CREATE_USER_SQL, _mapper.Map(model)))
                .SingleOrDefault());

        public Task<bool> DeleteExampleAsync(int id) =>
            WithSqlConnectionAsync(async connection => (await connection.QueryAsync<int?>(
                                                           MySqlExampleDataStoreSql.DELETE_USER_SQL,
                                                           new
                                                           {
                                                               id
                                                           }))
                                                       .SingleOrDefault() > 0);

        public Task<bool> UpdateExampleAsync(User model) =>
            WithSqlConnectionAsync(async connection =>
                (await connection.QueryAsync<int?>(MySqlExampleDataStoreSql.UPDATE_USER_SQL, _mapper.Map(model)))
                .SingleOrDefault() > 0);
    }
}
```

##### Add injection mapping for the DataStore

**`Package.cs`:**

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddXPikeDependencyInjection()
            .AddXPikePomeloMySqlDataStores()
            .RegisterSingleton<IMySqlExampleDataStore, MySqlExampleDataStore>()
            .RegisterSingleton<IExampleDataStore>(container => 
                container.ResolveDependency<IMySqlExampleDataStore>());
}
```

## Exported Services

### XPike.DataStores

##### Scoped

- **`IRequestContext`**  
  Injects the current context via `IRequestContextAccessor.RequestContext`.

##### Singleton

- **`IRequestContextAccessor`**  
  **=> `RequestContextAccessor`**

- **`IDefaultRequestContextProvider`**  
  **=> `DefaultRequestContextProvider`**

##### Singleton Collection

- **`IRequestContextProvider`**  
  **Add: `IDefaultRequestContextProvider`**

### XPike.RequestContext.Http

##### Singleton Collection

- **`IRequestContextProvider`**  
  **Add: `IHttpRequestContextProvider`**

### XPike.RequestContext.Http.AspNetCore

##### Singleton

- **`IAspNetCoreRequestContextProvider`**  
  **=> `AspNetCoreRequestContextProvider`**

- **`IHttpRequestContextProvider`**  
  **=> `IAspNetCoreRequestContextProvider`**

##### XPike.RequestContext.Http.WebApi

##### Singleton

- **`IWebApiCoreRequestContextProvider`**  
  **=> `WebApiCoreRequestContextProvider`**

- **`IHttpRequestContextProvider`**  
  **=> `IWebApiCoreRequestContextProvider`**

## Building and Testing

Building from source and running unit tests requires a Windows machine with:

* .Net Core 3.0 SDK
* .Net Framework 4.6.1 Developer Pack

## Issues

Issues are tracked on [GitHub](https://github.com/xpike/datastores/issues). Anyone is welcome to file a bug,
an enhancement request, or ask a general question. We ask that bug reports include:

1. A detailed description of the problem
2. Steps to reproduce
3. Expected results
4. Actual results
5. Version of the package xPike
6. Version of the .Net runtime

## Contributing

See our [contributing guidelines](https://github.com/xpike/documentation/blob/master/docfx_project/articles/contributing.md)
in our documentation for information on how to contribute to xPike.

## License

xPike is licensed under the [MIT License](LICENSE).
