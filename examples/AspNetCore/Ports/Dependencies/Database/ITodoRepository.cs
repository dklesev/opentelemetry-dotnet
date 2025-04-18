// Copyright The OpenTelemetry Authors
// SPDX-License-Identifier: Apache-2.0

using Examples.AspNetCore.Models;

namespace Examples.AspNetCore.Ports.Dependencies.Database;

public interface ITodoRepository
{
    IEnumerable<TodoItem> GetAllItems();

    TodoItem PersistItem(TodoItem todoItem);

    TodoItem? GetItemById(int id);
}
