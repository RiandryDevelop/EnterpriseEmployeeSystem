namespace EES.Application.Employees.Queries;

public class EmployeeDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty; // Ejemplo de lógica en DTO
    public string Email { get; set; } = string.Empty;
    public string JobTitle { get; set; } = string.Empty;
    public DateTime HireDate { get; set; }
}