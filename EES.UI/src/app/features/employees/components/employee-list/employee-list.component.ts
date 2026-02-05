import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';

import { EmployeeService } from '../../../../core/services/employee.service';
import { EmployeeDto } from '../../../../core/models/employee.model';
import { Subject, debounceTime, distinctUntilChanged } from 'rxjs';

@Component({
  selector: 'app-employee-list',
  standalone: true,
  imports: [
    CommonModule, 
    MatTableModule, 
    MatPaginatorModule, 
    MatInputModule, 
    MatFormFieldModule, 
    MatIconModule
  ],
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.scss']
})
export class EmployeeListComponent implements OnInit {
  // Table configuration
  displayedColumns: string[] = ['fullName', 'email', 'jobTitle', 'actions'];
  dataSource: EmployeeDto[] = [];
  
  // Pagination state
  totalCount = 0;
  pageSize = 10;
  pageNumber = 1;
  searchTerm = '';

  // Search debouncer to avoid hitting the API on every keystroke
  private searchSubject = new Subject<string>();

  constructor(private employeeService: EmployeeService) {
    this.searchSubject.pipe(
      debounceTime(400),
      distinctUntilChanged()
    ).subscribe(value => {
      this.searchTerm = value;
      this.pageNumber = 1; // Reset to first page on new search
      this.loadEmployees();
    });
  }

  ngOnInit(): void {
    this.loadEmployees();
  }

  loadEmployees(): void {
    this.employeeService.getEmployees(this.pageNumber, this.pageSize, this.searchTerm)
      .subscribe({
        next: (response: { items: EmployeeDto[]; totalCount: number; }) => {
          this.dataSource = response.items;
          this.totalCount = response.totalCount;
        },
        error: (err: any) => console.error('Error loading employees', err)
      });
  }

deleteEmployee(id: number): void {
  if (confirm('Are you sure you want to delete this employee?')) {
    this.employeeService.deleteEmployee(id).subscribe({
      next: () => {
        // Refresh the list after deletion
        this.loadEmployees();
      },
      error: (err) => console.error('Error deleting employee', err)
    });
  }
}

  onPageChange(event: PageEvent): void {
    this.pageNumber = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.loadEmployees();
  }

  applyFilter(event: Event): void {
    const filterValue = (event.target as HTMLInputElement).value;
    this.searchSubject.next(filterValue);
  }
}