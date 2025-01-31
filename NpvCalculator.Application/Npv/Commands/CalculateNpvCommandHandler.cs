using MediatR;
using NpvCalculator.Application.Services;
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
            var npv = NpvCalculatorService.CalculateNpv(request.InitialInvestment, request.DiscountRate, request.CashFlows);

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
    }
}
