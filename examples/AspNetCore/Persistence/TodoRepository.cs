// Copyright The OpenTelemetry Authors
// SPDX-License-Identifier: Apache-2.0

using Examples.AspNetCore.Models;
using Examples.AspNetCore.Persistence.Context;
using Examples.AspNetCore.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Examples.AspNetCore.Persistence;

public class TodoRepository(TodosContext todosContext) : ITodoRepository
{
    public async Task<IEnumerable<TodoItem>> GetAllAsync(CancellationToken cancellationToken) =>
        await todosContext.Items
            .Select(item => item.ToDomainModel())
            .ToListAsync(cancellationToken).ConfigureAwait(false);

    public async Task<TodoItem?> GetByIdAsync(int id, CancellationToken cancellationToken) =>
        await todosContext.Items
            .Where(item => item.Id == id)
            .Select(item => item.ToDomainModel())
            .FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);

    public async Task<TodoItem> PersistItemAsync(TodoItem todoItem, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(todoItem);

        TodoItemEntity? entity; // Make entity nullable

        if (todoItem.Id.HasValue)
        {
            // Update existing item
            entity = await todosContext.Items
                .FirstOrDefaultAsync(i => i.Id == todoItem.Id.Value, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                // Handle error: Trying to update an item that doesn't exist
                throw new InvalidOperationException($"TodoItem with ID {todoItem.Id.Value} not found for update.");
            }
        }
        else
        {
            // Create new item - Do not set ID here
            entity = new TodoItemEntity { Title = string.Empty };
            await todosContext.Items.AddAsync(entity, cancellationToken).ConfigureAwait(false); // Use async method
        }

        // Set fields from domain model
        entity.SetFieldsFrom(todoItem);

        // Save changes (inserts new or updates existing)
        await todosContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false); // Use async method

        // Return the domain model (entity will have the generated ID if it was new)
        return entity.ToDomainModel();
    }

    public async Task<bool> DeleteItemAsync(int id, CancellationToken cancellationToken)
    {
        // Use ExecuteDeleteAsync for efficiency if only deleting
        var affectedRows = await todosContext.Items
                                .Where(i => i.Id == id)
                                .ExecuteDeleteAsync(cancellationToken).ConfigureAwait(false); // Use ExecuteDeleteAsync

        return affectedRows > 0; // Return true if one row was deleted
    }

    public async Task<IEnumerable<TodoItem>> GetFilteredAsync(string titleSubstringFilter, CancellationToken cancellationToken) =>
        await todosContext.Items
            .Where(item => item.Title.Contains(titleSubstringFilter)) // Ensure case-insensitivity if needed
            .Select(item => item.ToDomainModel())
            .ToListAsync(cancellationToken).ConfigureAwait(false);
}
