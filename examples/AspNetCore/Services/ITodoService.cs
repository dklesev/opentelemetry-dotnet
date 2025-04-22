// Copyright The OpenTelemetry Authors
// SPDX-License-Identifier: Apache-2.0

using Examples.AspNetCore.Models;
using FluentResults;

namespace Examples.AspNetCore.Services;

public interface ITodoService
{
    Task<Result<TodoItem>> CreateAsync(TodoItem newItem, CancellationToken cancellationToken);

    Task<Result<TodoItem>> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<Result> UpdateAsync(int id, TodoItem updatedItem, CancellationToken cancellationToken);

    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken);

    Task<Result<IEnumerable<TodoItem>>> GetAllAsync(CancellationToken cancellationToken);

    Task<Result<IEnumerable<TodoItem>>> GetFilteredItemsAsync(string titleSubstringFilter, CancellationToken cancellationToken);
}
