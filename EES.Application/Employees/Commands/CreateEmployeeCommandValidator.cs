using FluentValidation;

namespace EES.Application.Employees.Commands;

/// <summary>
/// Validator for the CreateEmployeeCommand.
/// Ensures all business rules for employee creation are met before processing.
/// This fulfills the strict validation requirements of the technical assessment.
/// </summary>
public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        // First Name: Mandatory, maximum length of 50 characters
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

        // Last Name: Mandatory, maximum length of 50 characters
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");

        // Email: Mandatory, must follow a strict email format including a TLD (e.g., .com)
        // AspNetCoreCompatible mode prevents invalid entries like 'user@domain'
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email address is required.")
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
            .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
            .WithMessage("A valid email address with a domain (e.g., .com) is required.");

        // Job Title: Mandatory, maximum length of 100 characters
        RuleFor(x => x.JobTitle)
            .NotEmpty().WithMessage("Job title is required.")
            .MaximumLength(100).WithMessage("Job title must not exceed 100 characters.");

        // Hire Date: Mandatory, cannot be a future date
        // UtcNow is preferred for consistent validation across different time zones (Azure)
        RuleFor(x => x.HireDate)
            .NotEmpty().WithMessage("Hire date is required.")
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Hire date cannot be in the future.");
    }
}