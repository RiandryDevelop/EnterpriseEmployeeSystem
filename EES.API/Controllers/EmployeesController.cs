using EES.Application.Common.Models;
using EES.Application.Employees.Commands;
using EES.Application.Employees.Queries;
using Microsoft.AspNetCore.Mvc;

namespace EES.API.Controllers;

public class EmployeesController : BaseApiController
{
    // POST: api/employees (Create)
    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateEmployeeCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
    // GET: api/employees  (Read)
    [HttpGet]
    public async Task<ActionResult<PaginatedList<EmployeeDto>>> GetEmployees([FromQuery] GetEmployeesWithPaginationQuery query)
    {
        return Ok(await Mediator.Send(query));
    }


    // PUT: api/employees/{id} (Update)
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateEmployeeCommand command)
    {
        if (id != command.Id) return BadRequest();
        var success = await Mediator.Send(command);
        return success ? NoContent() : NotFound();
    }

    // DELETE: api/employees/{id} (Delete)
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await Mediator.Send(new DeleteEmployeeCommand(id));
        return success ? NoContent() : NotFound();
    }
}