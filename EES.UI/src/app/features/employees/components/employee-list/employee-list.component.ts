import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button'; 
import { MatDialogModule, MatDialog } from '@angular/material/dialog'; 
import { MatDividerModule } from '@angular/material/divider';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

import { EmployeeService } from '../../../../core/services/employee.service';
import { CreateEmployeeCommand, EmployeeDto } from '../../../../core/models/employee.model';
import { Subject, debounceTime, distinctUntilChanged } from 'rxjs';
import { EmployeeFormComponent } from '../employee-form/employee-form.component';

/**
 * Component for displaying and managing the employee list.
 * Handles pagination, searching, and CRUD operations via dialogs.
 */
@Component({
  selector: 'app-employee-list',
  standalone: true,
  imports: [
    CommonModule, 
    MatTableModule, 
    MatPaginatorModule, 
    MatInputModule, 
    MatFormFieldModule, 
    MatIconModule,
    MatButtonModule,
    MatSnackBarModule,
    MatDialogModule,
    MatDividerModule,
    MatTooltipModule
  ],
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.scss']
})
export class EmployeeListComponent implements OnInit {
  /** Columns displayed in the Material Table */
  displayedColumns: string[] = ['fullName', 'email', 'jobTitle', 'hireDate', 'actions'];
  dataSource: EmployeeDto[] = [];
  
  /** Pagination and Filter State */
  totalCount = 0;
  pageSize = 10;
  pageNumber = 1;
  searchTerm = '';

  /** Subject to handle debounced search inputs */
  private searchSubject = new Subject<string>();

  constructor(
    private employeeService: EmployeeService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {
    /** Sets up the debounced search stream to avoid excessive API calls */
    this.searchSubject.pipe(
      debounceTime(400),
      distinctUntilChanged()
    ).subscribe(value => {
      this.searchTerm = value;
      this.pageNumber = 1;
      this.loadEmployees();
    });
  }

  ngOnInit(): void {
    this.loadEmployees();
  }

  /**
   * Fetches the paginated list of employees from the service.
   */
  loadEmployees(): void {
    this.employeeService.getEmployees(this.pageNumber, this.pageSize, this.searchTerm)
      .subscribe({
        next: (response) => {
          this.dataSource = response.items;
          this.totalCount = response.totalCount;
        },
        error: (err) => console.error('Error loading employees', err)
      });
  }

  /**
   * Opens the dialog to create a new employee.
   */
  openAddDialog(): void {
    const dialogRef = this.dialog.open(EmployeeFormComponent, {
      width: '500px',
      disableClose: true 
    });

    dialogRef.afterClosed().subscribe((result: CreateEmployeeCommand) => {
      if (result) {
        this.employeeService.createEmployee(result).subscribe({
          next: () => this.loadEmployees(),
          error: (err) => console.error('Error creating employee', err)
        });
      }
    });
  }

  /**
   * Opens the dialog to edit an existing employee's details.
   * @param employee The employee data to be edited.
   */
  openEditDialog(employee: EmployeeDto): void {
    const dialogRef = this.dialog.open(EmployeeFormComponent, {
      width: '550px',
      disableClose: true,
      data: { employee } 
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        /** Ensure the ID is present in the payload to match backend requirements */
        const command = { 
          ...result, 
          id: employee.id 
        };

        this.employeeService.updateEmployee(employee.id, command).subscribe({
          next: () => {
            this.loadEmployees();
            this.snackBar.open('Employee updated successfully', 'Close', { duration: 3000 });
          },
          error: (err) => {
            const errorMessage = err.error || 'Update failed';
            this.snackBar.open(errorMessage, 'Close', { duration: 5000 });
            console.error('Update failed', err);
          }
        });
      }
    });
  }

  /**
   * Triggers the deletion process for an employee.
   * @param id The unique identifier of the employee.
   */
  deleteEmployee(id: number): void {
    if (confirm('Are you sure you want to delete this employee?')) {
      this.employeeService.deleteEmployee(id).subscribe({
        next: () => this.loadEmployees(),
        error: (err) => console.error('Error deleting employee', err)
      });
    }
  }

  /**
   * Handles paginator changes (page index or size).
   */
  onPageChange(event: PageEvent): void {
    this.pageNumber = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.loadEmployees();
  }

  /**
   * Passes the search input value to the search stream.
   */
  applyFilter(event: Event): void {
    const filterValue = (event.target as HTMLInputElement).value;
    this.searchSubject.next(filterValue);
  }
}