
using Examples.AspNetCore.Models;

namespace Examples.AspNetCore.Persistence.Entities;

public class TodoItemEntity
{
    public int Id { get; set; }

    public string Title { get; set; }

    public DateTime? DueDate { get; set; }

    public bool IsCompleted { get; set; }

    public TodoItem ToDomainModel() =>
        new()
        {
            Id = Id,
            Title = Title,
            DueDate = DueDate,
            IsCompleted = IsCompleted
        };

    public void SetFieldsFrom(TodoItem domainModel)
    {
        Title = domainModel.Title;
        DueDate = domainModel.DueDate;
        IsCompleted = domainModel.IsCompleted;
    }
}
