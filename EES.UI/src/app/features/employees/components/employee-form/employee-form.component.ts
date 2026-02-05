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
  employeeForm!: FormGroup;
  today = new Date();
  isEditMode: boolean = false;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<EmployeeFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: {employee: EmployeeDto}
  ) {
    this.isEditMode = !!data?.employee;
  }
ngOnInit(): void {
  this.employeeForm = this.fb.group({
    firstName: ['', [Validators.required, Validators.maxLength(50), Validators.pattern('^[a-zA-Z ]*$')]],
    lastName: ['', [Validators.required, Validators.maxLength(50)]],
    email: ['', [Validators.required, Validators.pattern('^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$')]],
    jobTitle: ['', [Validators.required, Validators.maxLength(100)]],
    hireDate: [new Date(), [Validators.required, this.futureDateValidator]]
  });

  if (this.isEditMode && this.data.employee) {
    const names = this.data.employee.fullName.split(' ');
    this.employeeForm.patchValue({
      firstName: names[0] || '',
      lastName: names.slice(1).join(' ') || '',
      email: this.data.employee.email,
      jobTitle: this.data.employee.jobTitle,
      hireDate: this.data.employee.hireDate
    });
    
    this.employeeForm.get('email')?.disable();
  }
}
futureDateValidator(control: AbstractControl): ValidationErrors | null {
  if (!control.value) return null;

  const selectedDate = new Date(control.value);
  selectedDate.setHours(0, 0, 0, 0);
  const today = new Date();
  today.setHours(0, 0, 0, 0);
  return selectedDate > today ? { futureDate: true } : null;
}

onSubmit(): void {
    if (this.employeeForm.valid) {
      this.dialogRef.close(this.employeeForm.getRawValue());
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}