import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { routes } from './app.routes';
import { provideNativeDateAdapter } from '@angular/material/core';
import { errorInterceptor } from './core/interceptors/error.interceptor';

/**
 * Global application configuration.
 * Defines the providers and essential services for the standalone application bootstrap.
 */
export const appConfig: ApplicationConfig = {
  providers: [
    /**
     * Configures the global routing system using the defined application routes.
     */
    provideRouter(routes),

    /**
     * Configures the HTTP client with a global error interceptor 
     * to handle server-side errors and provide user feedback.
     */
    provideHttpClient(withInterceptors([errorInterceptor])), 

    /**
     * Enables asynchronous browser animations, required for Angular Material 
     * components to render correctly without blocking the main thread.
     */
    provideAnimationsAsync(),

    /**
     * Provides the necessary implementation to handle Date objects within 
     * Material components like the Datepicker.
     */
    provideNativeDateAdapter()
  ]
};