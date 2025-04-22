// Copyright The OpenTelemetry Authors
// SPDX-License-Identifier: Apache-2.0

using Examples.AspNetCore.Models;

namespace Examples.AspNetCore.Persistence.Entities;

public class TodoItemEntity
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public DateTime? DueDate { get; set; }

    public bool IsCompleted { get; set; }

    public TodoItem ToDomainModel() =>
        new()
        {
            Id = this.Id,
            Title = this.Title,
            DueDate = this.DueDate,
            IsCompleted = this.IsCompleted,
        };

    public void SetFieldsFrom(TodoItem domainModel)
    {
        ArgumentNullException.ThrowIfNull(domainModel);
        this.Title = domainModel.Title;
        this.DueDate = domainModel.DueDate;
        this.IsCompleted = domainModel.IsCompleted;
    }
}
