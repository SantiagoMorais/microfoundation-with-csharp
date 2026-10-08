# Tips

## How to run a migration:

1. Create a model class in the Models folder.
2. Add a DbSet property in the AppDbContext class. Example:
```cs
public DbSet<Vehicle> Vehicles { get; set; }
public DbSet<Consume> Consumes { get; set; }
```
    3. Run the following command in the terminal to create a migration:
```bash
dotnet ef migrations add <MigrationName>
```

Output: 
```bash
Build started...
Build succeeded.
Done. To undo this action, use 'ef migrations remove'
```

4. Apply the migration to the database by running the following command:
```bash
dotnet ef database update
```

## EF syntax:

1. Key: defines a primary key for the entity.
```cs
[Key]
public int Id { get; set; }
```

2. Required: defines a required property for the entity.
```cs
[Required(ErrorMessage = "The model year is required")]
public int ModelYear { get; set; }
```

3. ForeignKey: defines a foreign key for the entity.
```cs
[ForeignKey("VehicleId")]
public Vehicle Vehicle { get; set; }
```

4. Display: defines a display name for the property.
```cs
[Display(Name = "Model Year")]
public int ModelYear { get; set; }
```

## Installing Packages:

Access [NuGet](https://www.nuget.org/) to install the following packages:
- Microsoft.EntityFrameworkCore
    Function: Provides the core Entity Framework functionality for working with data models.
- Microsoft.EntityFrameworkCore.SqlServer
    Function: Provides the SQL Server provider for Entity Framework.
