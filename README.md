# NpvCalculator

Net Present Value calculator - implement the app using .NET 8


## EF Core Migrations

To perform EF Core migrations, follow these steps:

### Step 1: Open Package Manager Console

In Visual Studio, navigate to **Tools** > **NuGet Package Manager** > **Package Manager Console**.

### Step 2: Add a New Migration

Run the following command to add a new migration:

```powershell
Add-Migration InitialCreate -Project NpvCalculator.Infrastructure -StartupProject NpvCalculator.Api
```

### Step 3: Update the Database
Run the following command to update the database with the new migration:

```powershell
Update-Database -Project NpvCalculator.Infrastructure -StartupProject NpvCalculator.Api
```
