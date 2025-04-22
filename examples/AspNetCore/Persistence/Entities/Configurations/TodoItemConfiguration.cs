// Copyright The OpenTelemetry Authors
// SPDX-License-Identifier: Apache-2.0

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Examples.AspNetCore.Persistence.Entities.Configurations;

public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItemEntity>
{
    public void Configure(EntityTypeBuilder<TodoItemEntity> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("TodoItems");
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.Title).IsRequired();
        builder.Property(p => p.DueDate).IsRequired(false);
        builder.Property(p => p.IsCompleted).HasColumnName("completed").IsRequired();
    }
}
