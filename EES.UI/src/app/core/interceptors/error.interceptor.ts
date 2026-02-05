import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const snackBar = inject(MatSnackBar);

  return next(req).pipe(
    catchError((error) => {
      let message = 'An unexpected error occurred';
      
      if (error.status === 0) {
        message = 'Cannot connect to the server. Please check if the API is running.';
      } else if (error.status >= 500) {
        message = 'Server Error: The technical team has been notified.';
      }

      snackBar.open(message, 'Close', { duration: 5000, panelClass: ['error-snackbar'] });
      return throwError(() => error);
    })
  );
};