using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MigrationsFromCode;
using MigrationsFromCode.Entities;
using System.Text;

await using var dbTodoContext = new TodoDbContext();
// migrate any database changes on startup (includes initial db creation), itself does not generate migrations.
// dbTodoContext.Database.Migrate();
// dbTodoContext.Database.EnsureCreated();

try
{
    if (!await dbTodoContext.Todos.AnyAsync())
    {
        Console.WriteLine("Adding some todo...");
        dbTodoContext.AddRange(
            new Todo { Description = "First Migration", IsDone = true },
            new Todo { Description = "Second Migration" },
            new Todo { Description = "Thirth Migration" }
        );
        await dbTodoContext.SaveChangesAsync();
    }
    else
    {
        Console.WriteLine("!!!Table todo has already populated!!!");
    }

    Console.WriteLine($"\nTable Todo #records:{dbTodoContext.Todos.AsNoTrackingWithIdentityResolution().Count()}");
}
catch (SqlException sqlException)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("!!! Check if before run command for create migrations !!!");
    DisplaySqlErrors(sqlException);
    Console.ResetColor();
}

Console.ReadKey();

static void DisplaySqlErrors(SqlException exception)
{
    StringBuilder errorMessages = new();

    for (int i = 0; i < exception.Errors.Count; i++)
    {
        errorMessages.Append("Error #" + i + "\n" +
                             "Message: " + exception.Errors[i].Message + "\n" +
                             "LineNumber: " + exception.Errors[i].LineNumber + "\n" +
                             "Source: " + exception.Errors[i].Source + "\n" +
                             "Procedure: " + exception.Errors[i].Procedure + "\n");
    }
    Console.WriteLine(errorMessages.ToString());
}
