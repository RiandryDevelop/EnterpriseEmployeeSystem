import { Routes } from '@angular/router';
import { EmployeeListComponent } from './features/employees/components/employee-list/employee-list.component';

/**
 * Application routing configuration.
 * Defines the navigation paths for the Employee Management System.
 */
export const routes: Routes = [
  /** * Default route (Root): Redirects or loads the Employee List as the landing page.
   */
  { path: '', component: EmployeeListComponent },

  /**
   * Route for the main employee directory dashboard.
   */
  { path: 'employees', component: EmployeeListComponent },
];