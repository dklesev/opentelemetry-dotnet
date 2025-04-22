// Copyright The OpenTelemetry Authors
// SPDX-License-Identifier: Apache-2.0

using Examples.AspNetCore.Models;

namespace Examples.AspNetCore.Persistence;

public interface ITodoRepository
{
    Task<IEnumerable<TodoItem>> GetAllAsync(CancellationToken cancellationToken);

    Task<TodoItem?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<TodoItem> PersistItemAsync(TodoItem todoItem, CancellationToken cancellationToken);

    Task<bool> DeleteItemAsync(int id, CancellationToken cancellationToken);

    Task<IEnumerable<TodoItem>> GetFilteredAsync(string titleSubstringFilter, CancellationToken cancellationToken);
}
