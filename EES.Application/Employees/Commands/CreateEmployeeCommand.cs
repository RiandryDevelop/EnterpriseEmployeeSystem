using MediatR;

namespace EES.Application.Employees.Commands;

public record CreateEmployeeCommand(
    string FirstName,
    string LastName,
    string Email,
    string JobTitle,
    DateTime HireDate) : IRequest<int>;