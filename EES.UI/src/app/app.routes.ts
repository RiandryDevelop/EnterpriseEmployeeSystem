import { Routes } from '@angular/router';
import { EmployeeListComponent } from './features/employees/components/employee-list/employee-list.component';

export const routes: Routes = [
  { path: '', component: EmployeeListComponent },
  { path: 'employees', component: EmployeeListComponent },
  // We will add the 'create' route later
];