using EES.Application.Common.Interfaces;
using EES.Domain.Entities;
using MediatR;

namespace EES.Application.Employees.Commands;

// --- The contract (Request) ---
public record CreateEmployeeCommand(
    string FirstName,
    string LastName,
    string Email,
    string JobTitle,
    DateTime HireDate) : IRequest<int>;

// --- The logic (Handler) ---
public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateEmployeeHandler(IApplicationDbContext context) => _context = context;

    public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = new Employee
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            JobTitle = request.JobTitle,
            HireDate = request.HireDate
        };

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync(cancellationToken);

        return employee.Id;
    }
}