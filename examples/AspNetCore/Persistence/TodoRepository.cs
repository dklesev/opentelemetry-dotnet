// Copyright The OpenTelemetry Authors
// SPDX-License-Identifier: Apache-2.0

using Examples.AspNetCore.Models;
using Examples.AspNetCore.Persistence.Context;
using Examples.AspNetCore.Persistence.Entities;
using Examples.AspNetCore.Ports.Dependencies.Database;

namespace Examples.AspNetCore.Persistence;

public class TodoRepository(TodosContext todosContext) : ITodoRepository
{
    public IEnumerable<TodoItem> GetAllItems() =>
        todosContext.Items
            .Select(item => item.ToDomainModel());

    public TodoItem? GetItemById(int id) =>
        todosContext.Items
            .Where(item => item.Id == id)
            .Select(item => item.ToDomainModel())
            .FirstOrDefault();

    public TodoItem PersistItem(TodoItem todoItem)
    {
        TodoItemEntity entity;

        if (todoItem.Id.HasValue)
        {
            entity = FetchOrCreateItemEntityWithId(todoItem.Id.Value);
        }
        else
        {
            entity = new TodoItemEntity();
            todosContext.Items.Add(entity);
        }

        entity.SetFieldsFrom(todoItem);
        todosContext.SaveChanges();

        return entity.ToDomainModel();
    }

    private TodoItemEntity FetchOrCreateItemEntityWithId(int id)
    {
        var entity = todosContext.Items
            .FirstOrDefault(i => i.Id == id);

        if (entity != null)
        {
            return entity;
        }

        entity = new TodoItemEntity
        {
            Id = id
        };

        todosContext.Items.Add(entity);
        return entity;
    }
}
