export interface CalculationResponse {
  id: number;
  initialInvestment: number;
  discountRate: number;
  result: number;
  calculatedAt: string;
  cashFlows: CashFlow[];
}

export interface CashFlow {
  id: number;
  amount: number;
  period: number;
  npvCalculationId: number;
}

export interface Calculation {
  id: string; // Convert to string if needed for UI
  initialInvestment: number;
  discountRate: number;
  result: number;
  calculatedAt: Date; // Convert string to Date object
  cashFlows: number[]; // Simplify for UI display
}


export interface CalculationRequest {
  initialInvestment: number;
  discountRate: number;
  cashFlows: number[];
}