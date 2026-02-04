namespace EES.Application.Employees.Queries;

public class EmployeeDto
{
    // Employee Data Transfer Object
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string JobTitle { get; set; } = string.Empty;
    public DateTime HireDate { get; set; }
}