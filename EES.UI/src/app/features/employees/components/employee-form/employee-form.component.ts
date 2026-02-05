import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators, AbstractControl, ValidationErrors } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';
import { EmployeeDto } from '../../../../core/models/employee.model';

/**
 * Dialog component for adding or editing employee details.
 * Utilizes Reactive Forms for robust validation and data handling.
 */
@Component({
  selector: 'app-employee-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatIconModule
  ],
  templateUrl: './employee-form.component.html',
  styleUrl: './employee-form.component.scss'
})
export class EmployeeFormComponent implements OnInit {
  /** The main form group for employee data */
  employeeForm!: FormGroup;
  
  /** Reference for max date validation in the template */
  today = new Date();
  
  /** Flag to determine if we are creating a new record or updating an existing one */
  isEditMode: boolean = false;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<EmployeeFormComponent>,
    /** Injected data from the calling component, containing employee details for edit mode */
    @Inject(MAT_DIALOG_DATA) public data: {employee: EmployeeDto}
  ) {
    this.isEditMode = !!data?.employee;
  }

  ngOnInit(): void {
    this.initForm();

    /** If in edit mode, populate the form with existing data */
    if (this.isEditMode && this.data.employee) {
      this.populateForm();
    }
  }

  /**
   * Initializes the reactive form structure with validators.
   */
  private initForm(): void {
    this.employeeForm = this.fb.group({
      firstName: ['', [Validators.required, Validators.maxLength(50), Validators.pattern('^[a-zA-Z ]*$')]],
      lastName: ['', [Validators.required, Validators.maxLength(50)]],
      email: ['', [Validators.required, Validators.pattern('^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$')]],
      jobTitle: ['', [Validators.required, Validators.maxLength(100)]],
      hireDate: [new Date(), [Validators.required, this.futureDateValidator]]
    });
  }

  /**
   * Patches the form values and handles UI logic for editing.
   */
  private populateForm(): void {
    // Split full name into first and last name for the specific form fields
    const names = this.data.employee.fullName.split(' ');
    this.employeeForm.patchValue({
      firstName: names[0] || '',
      lastName: names.slice(1).join(' ') || '',
      email: this.data.employee.email,
      jobTitle: this.data.employee.jobTitle,
      hireDate: this.data.employee.hireDate
    });
    
    // Prevent email changes during edit mode to maintain data integrity
    this.employeeForm.get('email')?.disable();
  }

  /**
   * Custom validator to ensure selected hire dates are not in the future.
   * Compares dates at midnight to correctly include the current day.
   */
  futureDateValidator(control: AbstractControl): ValidationErrors | null {
    if (!control.value) return null;

    const selectedDate = new Date(control.value);
    selectedDate.setHours(0, 0, 0, 0);

    const today = new Date();
    today.setHours(0, 0, 0, 0);

    return selectedDate > today ? { futureDate: true } : null;
  }

  /**
   * Finalizes the form submission.
   * Returns raw value to include disabled fields (like email).
   */
  onSubmit(): void {
    if (this.employeeForm.valid) {
      this.dialogRef.close(this.employeeForm.getRawValue());
    }
  }

  /**
   * Closes the dialog without saving changes.
   */
  onCancel(): void {
    this.dialogRef.close();
  }
}