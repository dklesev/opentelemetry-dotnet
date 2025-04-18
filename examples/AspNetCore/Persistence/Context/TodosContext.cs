// Copyright The OpenTelemetry Authors
// SPDX-License-Identifier: Apache-2.0

using Examples.AspNetCore.Persistence.Entities;
using Examples.AspNetCore.Persistence.Entities.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Examples.AspNetCore.Persistence.Context;

public class TodosContext(DbContextOptions<TodosContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("TodoSchema");
        modelBuilder.ApplyConfiguration(new TodoItemConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<TodoItemEntity> Items { get; init; }

}
