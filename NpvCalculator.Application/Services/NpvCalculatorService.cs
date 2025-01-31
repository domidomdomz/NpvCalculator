namespace NpvCalculator.Application.Services
{
    public static class NpvCalculatorService
    {
        public static decimal CalculateNpv(decimal initialInvestment, decimal discountRate, List<decimal> cashFlows)
        {
            decimal npv = -initialInvestment;
            for (int i = 0; i < cashFlows.Count; i++)
            {
                npv += cashFlows[i] / (decimal)Math.Pow(1 + (double)discountRate, i + 1);
            }
            return Math.Round(npv, 4);
        }
    }
}
