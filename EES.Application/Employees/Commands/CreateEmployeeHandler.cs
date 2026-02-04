using EES.Domain.Entities;
using EES.Infrastructure.Persistence;
using MediatR;

namespace EES.Application.Employees.Commands;

public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, int>
{
    private readonly ApplicationDbContext _context;

    public CreateEmployeeHandler(ApplicationDbContext context) => _context = context;

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