// Copyright The OpenTelemetry Authors
// SPDX-License-Identifier: Apache-2.0

using Examples.AspNetCore.Models;
using Examples.AspNetCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace Examples.AspNetCore.Controllers;

[ApiKeyAuth]
[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private readonly ITodoService todoService;
    private readonly ILogger<TodosController> logger;

    public TodosController(ITodoService todoService, ILogger<TodosController> logger)
    {
        this.todoService = todoService;
        this.logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        this.logger.GettingAllTodos();
        var result = await this.todoService.GetAllAsync(cancellationToken).ConfigureAwait(false);
        return result.IsSuccess ? this.Ok(result.Value) : this.StatusCode(500, result.Errors);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        this.logger.GettingTodoById(id);
        var result = await this.todoService.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
        return result.IsSuccess ? this.Ok(result.Value) : this.NotFound(result.Errors);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TodoItem newItem, CancellationToken cancellationToken)
    {
        this.logger.CreatingNewTodo();
        var result = await this.todoService.CreateAsync(newItem, cancellationToken).ConfigureAwait(false);
        if (result.IsSuccess)
        {
            return this.CreatedAtAction(nameof(this.GetById), new { id = result.Value.Id }, result.Value);
        }

        return this.BadRequest(result.Errors);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TodoItem updatedItem, CancellationToken cancellationToken)
    {
        this.logger.UpdatingTodoById(id);
        var result = await this.todoService.UpdateAsync(id, updatedItem, cancellationToken).ConfigureAwait(false);
        return result.IsSuccess ? this.NoContent() : this.NotFound(result.Errors);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        this.logger.DeletingTodoById(id);
        var result = await this.todoService.DeleteAsync(id, cancellationToken).ConfigureAwait(false);
        return result.IsSuccess ? this.NoContent() : this.NotFound(result.Errors);
    }
}
