import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideHttpClient } from '@angular/common/http';
import { routes } from './app.routes';
import { MaterialModule  } from './shared/material/material.module';
import { CalculationRepository } from './core/repositories/calculation.repository';
import { ApiService } from './core/services/api.service';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideAnimations(),
    provideHttpClient(),
    importProvidersFrom(
      MaterialModule
    ),
    // Provide repository implementation at application level
    { provide: CalculationRepository, useClass: ApiService }
  ]
};