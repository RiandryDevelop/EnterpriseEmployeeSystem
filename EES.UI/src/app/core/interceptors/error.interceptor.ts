import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { catchError, throwError } from 'rxjs';

/**
 * Functional HTTP Interceptor to handle global error logic.
 * Intercepts all outgoing requests and incoming responses to catch 
 * communication or server-side failures.
 */
export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  /** Injecting MatSnackBar to provide visual feedback to the user */
  const snackBar = inject(MatSnackBar);

  return next(req).pipe(
    catchError((error) => {
      let message = 'An unexpected error occurred';
      
      /** * Handle Connectivity Issues: 
       * Status 0 usually indicates a blocked request or a server that is down.
       */
      if (error.status === 0) {
        message = 'Cannot connect to the server. Please check if the API is running.';
      } 
      /** * Handle Internal Server Errors: 
       * Status 500+ indicates a failure in the .NET backend logic.
       */
      else if (error.status >= 500) {
        message = 'Server Error: The technical team has been notified.';
      }

      /** Display the error message using a temporary snackbar notification */
      snackBar.open(message, 'Close', { 
        duration: 5000, 
        panelClass: ['error-snackbar'] 
      });

      /** Re-throws the error so it can still be handled by specific services if needed */
      return throwError(() => error);
    })
  );
};