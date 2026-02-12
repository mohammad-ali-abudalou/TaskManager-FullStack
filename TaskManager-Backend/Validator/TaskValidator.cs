using FluentValidation;
using TaskManager.Models;

namespace TaskManager.Validator;

public class TaskValidator : AbstractValidator<TaskItem>
{
    public TaskValidator()
    {
        // Validation rules for TaskItem properties.
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title required")
            .MaximumLength(100).WithMessage("The title must not exceed 100 characters.");

        // Validation rules for the Description property.
        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("The description is very long");
    }
}
