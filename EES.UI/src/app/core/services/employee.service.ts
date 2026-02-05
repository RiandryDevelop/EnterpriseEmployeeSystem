import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EmployeeDto, PaginatedList, CreateEmployeeCommand, UpdateEmployeeCommand } from '../models/employee.model';
import { apiUrl } from '../../../environment';
@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private http: HttpClient) { }

  // Create a new employee (CRUD - Create)
  createEmployee(employee: CreateEmployeeCommand): Observable<number> {
    return this.http.post<number>(apiUrl, employee);
  }


  // Get all employees (CRUD - Read)
  getEmployees(pageNumber: number, pageSize: number, searchTerm?: string): Observable<PaginatedList<EmployeeDto>> {
    let params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());

    if (searchTerm) {
      params = params.set('searchTerm', searchTerm);
    }

    return this.http.get<PaginatedList<EmployeeDto>>(apiUrl, { params });
  }

  // Update an existing employee (CRUD - Update)
  updateEmployee(id: number, command: UpdateEmployeeCommand): Observable<void> {
    return this.http.put<void>(`${apiUrl}/${id}`, command);
  }

// Delete an employee (CRUD - Delete)
  deleteEmployee(id: number): Observable<void> {
    return this.http.delete<void>(`${apiUrl}/${id}`);
  }

}