using EES.Application.Common.Models;
using EES.Application.Employees.Commands;
using EES.Application.Employees.Queries;
using Microsoft.AspNetCore.Mvc;

namespace EES.API.Controllers;

/// <summary>
/// Controller for managing employee records.
/// Provides endpoints for CRUD operations, pagination, and filtering[cite: 9, 16].
/// </summary>
public class EmployeesController : BaseApiController
{
    /// <summary>
    /// Creates a new employee record.
    /// </summary>
    /// <param name="command">The employee data (First Name, Last Name, Email, Job Title, Hire Date).</param>
                /// <returns>The ID of the newly created employee.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Create(CreateEmployeeCommand command)
    {
        var result = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetEmployees), new { id = result }, result);
    }

    /// <summary>
    /// Retrieves a paginated list of employees with support for sorting and filtering.
                /// </summary>
    /// <param name="query">Pagination parameters (PageNumber, PageSize).</param>
                /// <returns>A paginated list of EmployeeDTOs.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedList<EmployeeDto>>> GetEmployees([FromQuery] GetEmployeesWithPaginationQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    /// <summary>
    /// Updates an existing employee record.
    /// </summary>
    /// <param name="id">The unique identifier of the employee.</param>
    /// <param name="command">The updated employee data.</param>
    /// <returns>No content if successful, BadRequest if IDs mismatch, or NotFound.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, UpdateEmployeeCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("ID mismatch between URL and request body.");
        }

        var success = await Mediator.Send(command);
        return success ? NoContent() : NotFound();
    }

    /// <summary>
    /// Deletes an employee record from the system.
    /// </summary>
    /// <param name="id">The unique identifier of the employee to delete.</param>
    /// <returns>No content if successful, or NotFound.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await Mediator.Send(new DeleteEmployeeCommand(id));
        return success ? NoContent() : NotFound();
    }
}