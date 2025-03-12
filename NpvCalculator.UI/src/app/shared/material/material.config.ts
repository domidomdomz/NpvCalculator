import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';

export const materialConfig: ApplicationConfig = {
  providers: [
    importProvidersFrom(
        MatProgressBarModule,
        MatButtonModule, 
        MatCardModule, 
        MatFormFieldModule, 
        MatInputModule, 
        MatIconModule, 
        // Add other Material modules here
    )
  ]
};