// Copyright The OpenTelemetry Authors
// SPDX-License-Identifier: Apache-2.0

namespace Examples.AspNetCore.Controllers;

internal static partial class TodosControllerLog
{
    [LoggerMessage(EventId = 1, Level = LogLevel.Information, Message = "Getting all Todo items")]
    public static partial void GettingAllTodos(this ILogger logger);

    [LoggerMessage(EventId = 2, Level = LogLevel.Information, Message = "Getting Todo item with ID: {TodoId}")]
    public static partial void GettingTodoById(this ILogger logger, int todoId);

    [LoggerMessage(EventId = 3, Level = LogLevel.Information, Message = "Creating new Todo item")]
    public static partial void CreatingNewTodo(this ILogger logger);

    [LoggerMessage(EventId = 4, Level = LogLevel.Information, Message = "Updating Todo item with ID: {TodoId}")]
    public static partial void UpdatingTodoById(this ILogger logger, int todoId);

    [LoggerMessage(EventId = 5, Level = LogLevel.Information, Message = "Deleting Todo item with ID: {TodoId}")]
    public static partial void DeletingTodoById(this ILogger logger, int todoId);
}
