import { Component } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-navigation',
  template: `
    <mat-toolbar color="primary">
      <span class="logo">NPV Calculator</span>
      <nav>
        <a mat-button routerLink="/calculator" routerLinkActive="active">
          <mat-icon>calculate</mat-icon>
          Calculator
        </a>
        <a mat-button routerLink="/history" routerLinkActive="active">
          <mat-icon>history</mat-icon>
          History
        </a>
      </nav>
    </mat-toolbar>
  `,
  imports: [
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    RouterModule
  ],
  styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent {}