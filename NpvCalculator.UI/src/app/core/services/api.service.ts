import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CalculationRepository } from '../repositories/calculation.repository';
import { CalculationRequest, Calculation, CalculationResponse } from '../models/calculation';
import { firstValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class ApiService implements CalculationRepository {
  private readonly baseUrl = environment.baseUrl;

  constructor(private http: HttpClient) {}

  calculateNpv(request: CalculationRequest): Observable<number> {
    return this.http.post<number>(this.baseUrl, request);
  }

  getHistory(): Observable<Calculation[]> {
    return this.http.get<CalculationResponse[]>(`${this.baseUrl}/history`).pipe(
      map(responses => responses.map(response => this.mapToViewModel(response)))
    );
  }

  private mapToViewModel(response: CalculationResponse): Calculation {
    return {
      id: response.id.toString(),
      initialInvestment: response.initialInvestment,
      discountRate: response.discountRate,
      result: response.result,
      calculatedAt: new Date(response.calculatedAt),
      cashFlows: response.cashFlows.map(cf => cf.amount)
    };
  }
  
}