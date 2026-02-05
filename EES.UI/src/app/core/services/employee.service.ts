import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EmployeeDto, PaginatedList, CreateEmployeeCommand, UpdateEmployeeCommand } from '../models/employee.model';
import { environment } from '../../../environments/environment';

/**
 * Service responsible for managing employee-related data operations.
 * Acts as the bridge between the Angular frontend and the .NET backend API.
 */
@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  private readonly endpoint = `${environment.apiUrl}/employees`;

  constructor(private http: HttpClient) { }

  /**
   * Registers a new employee in the system.
   * @param employee Data command for creating a new employee.
   * @returns An Observable containing the ID of the newly created employee.
   */
  createEmployee(employee: CreateEmployeeCommand): Observable<number> {
    return this.http.post<number>(this.endpoint, employee);
  }

  /**
   * Retrieves a paginated list of employees based on search criteria.
   * @param pageNumber Current page index.
   * @param pageSize Number of records per page.
   * @param searchTerm Optional string to filter by name, email, position, or date.
   * @returns An Observable with the paginated results and metadata.
   */
  getEmployees(pageNumber: number, pageSize: number, searchTerm?: string): Observable<PaginatedList<EmployeeDto>> {
    let params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());

    if (searchTerm) {
      params = params.set('searchTerm', searchTerm);
    }

    return this.http.get<PaginatedList<EmployeeDto>>(this.endpoint, { params });
  }

  /**
   * Updates an existing employee's information.
   * @param id The unique identifier of the employee to update.
   * @param command The updated data payload.
   * @returns An Observable that completes when the update is successful.
   */
  updateEmployee(id: number, command: UpdateEmployeeCommand): Observable<void> {
    // Ensures the request is sent to the specific resource URL: /api/employees/{id}
    return this.http.put<void>(`${this.endpoint}/${id}`, command);
  }

  /**
   * Removes an employee record from the database.
   * @param id The unique identifier of the employee to delete.
   * @returns An Observable that completes upon successful deletion.
   */
  deleteEmployee(id: number): Observable<void> {
    return this.http.delete<void>(`${this.endpoint}/${id}`);
  }
}