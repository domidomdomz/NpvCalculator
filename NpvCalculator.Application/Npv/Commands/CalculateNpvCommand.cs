using MediatR;

namespace NpvCalculator.Application.Npv.Commands
{
    public record CalculateNpvCommand(
        decimal InitialInvestment, 
        decimal DiscountRateIncrement, 
        List<decimal> CashFlows) : IRequest<decimal>;
}
