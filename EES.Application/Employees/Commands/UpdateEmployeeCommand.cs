using EES.Application.Common.Interfaces;
using MediatR;

namespace EES.Application.Employees.Commands;

// Update Employee Command
public record UpdateEmployeeCommand(int Id, string FirstName, string LastName, string JobTitle) : IRequest<bool>;

public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, bool>
{
    private readonly IApplicationDbContext _context;
    public UpdateEmployeeHandler(IApplicationDbContext context) => _context = context;


    // Handle method
    public async Task<bool> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Employees.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity == null) return false;

        entity.FirstName = request.FirstName;
        entity.LastName = request.LastName;
        entity.JobTitle = request.JobTitle;

        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}