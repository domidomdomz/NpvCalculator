import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Calculation, CalculationResponse } from '../../core/models/calculation';
import { CalculationRepository } from '../../core/repositories/calculation.repository';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';

@Component({
  standalone: true,
  selector: 'app-history',
  imports: [MatTableModule, MatCardModule, MatIconModule, CommonModule],
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.scss']
})
export class HistoryComponent implements OnInit {
  displayedColumns = ['calculatedAt', 'initialInvestment', 'discountRate', 'result', 'cashFlows'];
  dataSource: Calculation[] = [];
  loading = false;

  constructor(private calculationRepo: CalculationRepository) { }

  async ngOnInit() {
    this.loading = true;

    this.calculationRepo.getHistory().subscribe({
      next: (calculations: Calculation[]) => {
        console.log(JSON.stringify(calculations)); // Log calculations as JSON string
        this.dataSource = calculations; // Already mapped
        this.loading = false;
      },
      error: () => this.loading = false
    });
  }
}
