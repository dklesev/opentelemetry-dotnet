using Microsoft.EntityFrameworkCore;

namespace Examples.AspNetCore.Persistence.Context;

public class TodosContextFactory : IDesignTimeDbContextFactory<TodosContext>
{
    public TodosContext CreateDbContext(string[] args)
    {

        var connectionString = Environment.GetEnvironmentVariable("Postgres__ConnectionString")
                               ?? args.FirstOrDefault()
                               ?? throw new ArgumentException($"Connection string must be provided as an argument.");

        var optionsBuilder = new DbContextOptionsBuilder<TodosContext>();
        optionsBuilder.UseNpgsql(connectionString);
        return new TodosContext(optionsBuilder.Options);

    }
}
