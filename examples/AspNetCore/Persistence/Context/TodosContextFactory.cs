// Copyright The OpenTelemetry Authors
// SPDX-License-Identifier: Apache-2.0

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Examples.AspNetCore.Persistence.Context;

public class TodosContextFactory : IDesignTimeDbContextFactory<TodosContext>
{
    public TodosContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var host = config.GetSection("Postgres")["Host"] ?? "localhost";
        var port = config.GetSection("Postgres")["Port"] ?? "5432";
        var db = config.GetSection("Postgres")["Database"] ?? "postgres";
        var user = config.GetSection("Postgres")["Username"] ?? "postgres";
        var pass = config.GetSection("Postgres")["Password"] ?? "postgres";
        var connStr = $"Host={host};Port={port};Database={db};Username={user};Password={pass}";

        var optionsBuilder = new DbContextOptionsBuilder<TodosContext>();
        optionsBuilder.UseNpgsql(connStr);

        return new TodosContext(optionsBuilder.Options);
    }
}
