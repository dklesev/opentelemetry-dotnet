// Copyright The OpenTelemetry Authors
// SPDX-License-Identifier: Apache-2.0

using Examples.AspNetCore.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Examples.AspNetCore.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterTodosContext(this IServiceCollection serviceCollection, PostgresConfiguration config)
    {
        ArgumentNullException.ThrowIfNull(config);

        serviceCollection.AddDbContext<TodosContext>(options => options.UseNpgsql($"Host={config.Host};Port={config.Port};Database={config.Database};Username={config.Username};Password={config.Password}"));
    }

    public static void RegisterTodoRepository(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ITodoRepository, TodoRepository>();
    }
}
