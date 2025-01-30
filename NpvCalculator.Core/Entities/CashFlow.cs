using NpvCalculator.Core.Common;

namespace NpvCalculator.Core.Entities
{
    public class CashFlow : BaseEntity
    {
        public decimal Amount { get; set; }
        public int Period { get; set; }
        public int NpvCalculationId { get; set; }
    }
}
