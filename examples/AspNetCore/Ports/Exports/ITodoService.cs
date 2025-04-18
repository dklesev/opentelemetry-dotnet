// Copyright The OpenTelemetry Authors
// SPDX-License-Identifier: Apache-2.0

using Examples.AspNetCore.Models;
using FluentResults;

namespace Examples.AspNetCore.Ports.Exports;

public interface ITodoService
{
    Result<TodoItem> CreateNewItem(string title, DateTime? dueDate);
    Result<TodoItem?> GetItemById(int id);
    Result SetItemCompleted(int id);
    Result SetItemIncomplete(int id);
    Result SetItemTitle(int id, string title);
    Result SetItemDueDate(int id, DateTime? dueDate);
    Result<IEnumerable<TodoItem>> GetAllItems();
    Result<IEnumerable<TodoItem>> GetFilteredItems(string titleSubstringFilter);
}
