import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EmployeeDto, PaginatedList, CreateEmployeeCommand } from '../models/employee.model';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  // Ajusta esta URL al puerto de tu Docker (5000)
  private apiUrl = 'http://localhost:5000/api/employees';

  constructor(private http: HttpClient) { }

  // Obtener empleados con paginación y búsqueda
  getEmployees(pageNumber: number, pageSize: number, searchTerm?: string): Observable<PaginatedList<EmployeeDto>> {
    let params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());

    if (searchTerm) {
      params = params.set('searchTerm', searchTerm);
    }

    return this.http.get<PaginatedList<EmployeeDto>>(this.apiUrl, { params });
  }

  createEmployee(employee: CreateEmployeeCommand): Observable<number> {
    return this.http.post<number>(this.apiUrl, employee);
  }

  deleteEmployee(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}