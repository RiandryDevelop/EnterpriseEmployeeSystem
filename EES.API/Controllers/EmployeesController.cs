using EES.Application.Common.Models;
using EES.Application.Employees.Commands;
using EES.Application.Employees.Queries;
using Microsoft.AspNetCore.Mvc;

namespace EES.API.Controllers;

public class EmployeesController : BaseApiController
{
    // GET: api/employees (Con paginación y filtrado)
    [HttpGet]
    public async Task<ActionResult<PaginatedList<EmployeeDto>>> GetEmployees([FromQuery] GetEmployeesWithPaginationQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    // POST: api/employees (Crear)
    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateEmployeeCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    // GET: api/employees/test-error (Para probar la alerta de email 500)
    [HttpGet("test-error")]
    public IActionResult TestError()
    {
       // [cite_start]// Esto disparará nuestro ExceptionMiddleware y el EmailAlertService [cite: 12]
        throw new Exception("Simulated enterprise-level system failure for notification testing.");
    }
}