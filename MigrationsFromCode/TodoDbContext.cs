using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MigrationsFromCode.Entities;

namespace MigrationsFromCode;
public class TodoDbContext : DbContext
{
    public TodoDbContext()
    {

    }

    public TodoDbContext(DbContextOptions<TodoDbContext> options)
        : base(options)
    {
    }

    public DbSet<Todo> Todos => Set<Todo>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TodoDb;Trusted_Connection=True;MultipleActiveResultSets=true");
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name, DbLoggerCategory.Migrations.Name }, LogLevel.Information);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        const string schema = "dbo";

        modelBuilder.Entity<Todo>().ToTable("todos", schema);

        modelBuilder.Entity<Todo>()
            .Property(t => t.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Todo>()
           .HasKey(t => t.Id);

        modelBuilder.Entity<Todo>()
            .Property(t => t.Description)
            .HasMaxLength(255)
            .IsRequired();

        modelBuilder.Entity<Todo>()
            .Property(b => b.IsDone)
            .HasDefaultValueSql("0");

        base.OnModelCreating(modelBuilder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<string>().AreUnicode(false);

        base.ConfigureConventions(configurationBuilder);
    }
}
