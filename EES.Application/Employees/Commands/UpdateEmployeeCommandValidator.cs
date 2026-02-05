using FluentValidation;

namespace EES.Application.Employees.Commands;

/// <summary>
/// Validator for the UpdateEmployeeCommand.
/// Ensures that existing record updates comply with business rules and data constraints.
/// </summary>
public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator()
    {
        // ------------- Validation Rules -------------

        // Id: Mandatory, must be a positive integer to identify the record correctly
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("A valid Employee ID is required for updates.");

        // FirstName: Mandatory, maximum length of 50 characters
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

        // LastName: Mandatory, maximum length of 50 characters
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");

        // JobTitle: Mandatory, maximum length of 100 characters
        RuleFor(x => x.JobTitle)
            .NotEmpty().WithMessage("Job title is required.")
            .MaximumLength(100).WithMessage("Job title must not exceed 100 characters.");
    }
}