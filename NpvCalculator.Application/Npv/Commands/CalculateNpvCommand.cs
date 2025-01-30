using MediatR;

namespace NpvCalculator.Application.Npv.Commands
{
    public record CalculateNpvCommand(
        decimal InitialInvestment, 
        decimal DiscountRate, 
        List<decimal> CashFlows) : IRequest<decimal>;
}
