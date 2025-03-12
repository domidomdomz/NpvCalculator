import { Calculation, CalculationRequest, CalculationResponse } from '../models/calculation';
import { Observable } from 'rxjs';

export abstract class CalculationRepository {
  abstract calculateNpv(request: CalculationRequest): Observable<number>;
  abstract getHistory(): Observable<Calculation[]>;
}