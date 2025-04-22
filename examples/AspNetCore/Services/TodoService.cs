// Copyright The OpenTelemetry Authors
// SPDX-License-Identifier: Apache-2.0

using Examples.AspNetCore.Models;
using Examples.AspNetCore.Persistence;
using FluentResults;

namespace Examples.AspNetCore.Services;

public class TodoService : ITodoService
{
    private readonly ITodoRepository repository;

    public TodoService(ITodoRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<IEnumerable<TodoItem>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var items = await this.repository.GetAllAsync(cancellationToken).ConfigureAwait(false);
        return Result.Ok(items);
    }

    public async Task<Result<TodoItem>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var item = await this.repository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
        return item is null ? Result.Fail($"TodoItem with ID {id} not found.") : Result.Ok(item);
    }

    public async Task<Result<TodoItem>> CreateAsync(TodoItem newItem, CancellationToken cancellationToken)
    {
        var createdItem = await this.repository.PersistItemAsync(newItem, cancellationToken).ConfigureAwait(false);
        return Result.Ok(createdItem);
    }

    public async Task<Result> UpdateAsync(int id, TodoItem updatedItem, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(updatedItem);
        if (id != updatedItem.Id)
        {
            return Result.Fail("ID mismatch between route parameter and request body.");
        }

        var existingItem = await this.repository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
        if (existingItem is null)
        {
            return Result.Fail($"TodoItem with ID {id} not found.");
        }

        await this.repository.PersistItemAsync(updatedItem, cancellationToken).ConfigureAwait(false);
        return Result.Ok();
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var result = await this.repository.DeleteItemAsync(id, cancellationToken).ConfigureAwait(false);
        return result ? Result.Ok() : Result.Fail($"TodoItem with ID {id} not found or could not be deleted.");
    }

    public async Task<Result<IEnumerable<TodoItem>>> GetFilteredItemsAsync(string titleSubstringFilter, CancellationToken cancellationToken)
    {
        var items = await this.repository.GetFilteredAsync(titleSubstringFilter, cancellationToken).ConfigureAwait(false);
        return Result.Ok(items);
    }
}
