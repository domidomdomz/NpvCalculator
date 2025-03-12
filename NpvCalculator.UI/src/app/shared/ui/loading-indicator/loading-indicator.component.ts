import { Component } from '@angular/core';
import { MatProgressBarModule } from '@angular/material/progress-bar';

@Component({
  standalone: true,
  selector: 'app-loading-indicator',
  template: `
    <div class="loading-container">
      <mat-progress-bar mode="indeterminate"></mat-progress-bar>
      <p class="loading-text">Loading...</p>
    </div>
  `,
  imports: [MatProgressBarModule], // Explicit Material import
  styleUrls: ['./loading-indicator.component.scss']
})
export class LoadingIndicatorComponent {}