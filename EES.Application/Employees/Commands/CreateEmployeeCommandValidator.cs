using FluentValidation;

namespace EES.Application.Employees.Commands;

public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().MaximumLength(50);

        RuleFor(x => x.LastName)
            .NotEmpty().MaximumLength(50);

        RuleFor(x => x.Email)
            .NotEmpty().EmailAddress()
            .WithMessage("A valid corporate email address is required.");

        RuleFor(x => x.JobTitle)
            .NotEmpty().MaximumLength(100);

        RuleFor(x => x.HireDate)
            .NotEmpty().LessThanOrEqualTo(DateTime.Now)
            .WithMessage("Hire date cannot be in the future.");
    }
}