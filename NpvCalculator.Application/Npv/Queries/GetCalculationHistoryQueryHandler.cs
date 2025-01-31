using MediatR;
using Microsoft.EntityFrameworkCore;
using NpvCalculator.Core.Entities;
using NpvCalculator.Core.Interfaces;

namespace NpvCalculator.Application.Npv.Queries
{
    public sealed class GetCalculationHistoryQueryHandler
        : IRequestHandler<GetCalculationHistoryQuery, List<NpvCalculation>>
    {
        private readonly IApplicationDbContext _context;

        public GetCalculationHistoryQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<NpvCalculation>> Handle(GetCalculationHistoryQuery request, CancellationToken cancellationToken)
        {
            return await _context.NpvCalculations
                .Include(n => n.CashFlows)
                .OrderByDescending(n => n.CalculatedAt)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
