namespace EES.Domain.Entities;

public class Employee
{
    // Employee entity
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty; 

    public string LastName { get; set; } = string.Empty;     
    public string Email { get; set; } = string.Empty; 
    public string JobTitle { get; set; } = string.Empty; 
    public DateTime HireDate { get; set; }
    
}