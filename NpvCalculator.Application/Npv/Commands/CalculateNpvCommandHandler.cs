using MediatR;
using NpvCalculator.Core.Entities;
using NpvCalculator.Core.Interfaces;

namespace NpvCalculator.Application.Npv.Commands
{
    public class CalculateNpvCommandHandler : IRequestHandler<CalculateNpvCommand, decimal>
    {
        private readonly IApplicationDbContext _context;
        public CalculateNpvCommandHandler(IApplicationDbContext context) 
        { 
            _context = context;
        }

        public async Task<decimal> Handle(CalculateNpvCommand request, CancellationToken cancellationToken)
        {
            var npv = CalculateNpv(request.InitialInvestment, request.DiscountRate, request.CashFlows);

            var calculation = new NpvCalculation
            {
                InitialInvestment = request.InitialInvestment,
                DiscountRate = request.DiscountRate,
                Result = npv,
                CashFlows = request.CashFlows.Select((cf, i) => new CashFlow
                {
                    Amount = cf,
                    Period = i + 1
                }).ToList()
            };

            _context.NpvCalculations.Add(calculation);
            await _context.SaveChangesAsync(cancellationToken);

            return npv;
        }

        private static decimal CalculateNpv(decimal initialInvestment, decimal discountRate, List<decimal> cashFlows)
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
