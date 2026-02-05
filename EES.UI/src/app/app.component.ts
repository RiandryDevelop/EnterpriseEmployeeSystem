import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';

/**
 * Root Component of the Application.
 * Acts as the main container for the entire user interface, 
 * providing the layout structure and the main navigation outlet.
 */
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,      // Provides basic Angular directives (ngIf, ngFor, etc.)
    MatToolbarModule,  // Material component for the application top bar
    RouterOutlet       // Placeholder that Angular fills based on the current router state
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  /**
   * Application name/title identifier.
   */
  title = 'EES.UI';
}