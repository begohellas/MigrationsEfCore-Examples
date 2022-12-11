## Examples migrations Ef Core First Code approach

1. Install from command the tools if you don't yet installed
```bash
dotnet tool install --global dotnet-ef
```

1.b Or update
```bash
dotnet tool update --global dotnet-ef
```

2. Before you can use the tools on a specific project, you'll need to add nuget package *Microsoft.EntityFrameworkCore.Design*
```bash
dotnet add package Microsoft.EntityFrameworkCore.Design
```

3. First Migration
```bash
dotnet ef migrations add <nameMigration> --verbose
```

4. Create your database and schema
```bash
dotnet ef database update --verbose
```