using EES.Application.Common.Interfaces;
using MediatR;

namespace EES.Application.Employees.Commands;

// --- The contract (Request) ---
public record DeleteEmployeeCommand(int Id) : IRequest<bool>;

// --- The logic (Handler) ---
public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, bool>
{
    private readonly IApplicationDbContext _context;
    public DeleteEmployeeHandler(IApplicationDbContext context) => _context = context;

    public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Employees.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity == null) return false;

        _context.Employees.Remove(entity);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}