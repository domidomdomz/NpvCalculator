import { Component, signal, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { FormGroup, FormBuilder, FormArray, Validators, ReactiveFormsModule } from '@angular/forms';
import { CalculationRepository } from '../../core/repositories/calculation.repository';
import { ApiService } from '../../core/services/api.service';
import { LoadingIndicatorComponent } from '../../shared/ui/loading-indicator/loading-indicator.component';
import { Calculation } from '../../core/models/calculation';
import { CurrencyFormatPipe } from '../../shared/providers/currency-format.pipe';
import { PercentageFormatPipe } from '../../shared/providers/percentage-format.pipe';

@Component({
  standalone: true,
  selector: 'app-calculator',
  imports: [CommonModule,
            MatButtonModule, 
            MatCardModule, 
            MatFormFieldModule, 
            MatInputModule, 
            MatIconModule, 
            MatProgressBarModule,
            LoadingIndicatorComponent,
            ReactiveFormsModule],
  templateUrl: './calculator.component.html',
  styleUrls: ['./calculator.component.scss']
})
export class CalculatorComponent {
  private readonly calculationRepo: CalculationRepository = inject(CalculationRepository);
  private readonly snackBar = inject(MatSnackBar);

  form: FormGroup;
  result = signal<number | null>(null);
  loading = signal(false);

  constructor(private fb: FormBuilder) {
    this.form = this.fb.group({
      initialInvestment: [1000, [Validators.required, Validators.min(0)]],
      discountRate: [0.1, [Validators.required, Validators.min(0), Validators.max(1)]],
      cashFlows: this.fb.array([this.createCashFlow()])
    });
  }

  createCashFlow() {
    return this.fb.control(500, [Validators.required, Validators.min(0)]);
  }

  get cashFlows() {
    return this.form.get('cashFlows') as FormArray;
  }

  addCashFlow() {
    this.cashFlows.push(this.createCashFlow());
  }

  removeCashFlow(index: number) {
    this.cashFlows.removeAt(index);
  }

  async calculate() {
    if (this.form.invalid) return;

    this.loading.set(true);

    // TODO: Add API call here
    try {
      this.calculationRepo.calculateNpv(this.form.value).subscribe({
        next: (result: number) => {
          // calculation is already in UI-friendly format
          this.result.set(result);
    
          this.snackBar.open(`NPV Calculated: ${result}`, 'Close', {
            duration: 3000,
          });
    
          this.loading.set(false);
        },
        error: (error) => {
          this.snackBar.open('Calculation failed. Please try again.', 'Close');
          this.loading.set(false);
        }
      });
    } catch (error) {
      this.snackBar.open('Calculation failed. Please try again.', 'Close');
      this.loading.set(false);
    }
    


    setTimeout(() => {
      this.result.set(2434.56);
      this.loading.set(false);
    }, 1000);
  }

}
