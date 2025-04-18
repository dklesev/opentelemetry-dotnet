// Copyright The OpenTelemetry Authors
// SPDX-License-Identifier: Apache-2.0

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using Examples.AspNetCore.Persistence.Context;
using Examples.AspNetCore.Ports.Dependencies.Database;

namespace Examples.AspNetCore.Persistence.RegExt;

public static class RegistrationExtensions
{
    public static void RegisterTodosContext(this IServiceCollection serviceCollection, PostgresConfiguration config)
    {
        PostgresCredentials crd = JsonSerializer.Deserialize<PostgresCredentials>(config.UsernamePassword);

        serviceCollection.AddDbContext<TodosContext>(options => options.UseNpgsql($"Host={config.Host};Port={config.Port};Database={config.Database};Username={config.Username};Password={crd.password}"));
    }

    public static void RegisterTodoRepository(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ITodoRepository, TodoRepository>();
    }
}
