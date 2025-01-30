using NpvCalculator.Core.Common;

namespace NpvCalculator.Core.Entities
{
    public class NpvCalculation : BaseEntity
    {
        public decimal InitialInvestment { get; set; }
        public decimal DiscountRate { get; set; }
        public decimal Result { get; set; }
        public DateTime CalculatedAt { get; set; } = DateTime.UtcNow;
        public List<CashFlow> CashFlows { get; set; } = new();
    }
}
