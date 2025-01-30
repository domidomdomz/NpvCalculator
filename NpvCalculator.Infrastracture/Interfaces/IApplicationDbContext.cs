using Microsoft.EntityFrameworkCore;
using NpvCalculator.Core.Entities;

namespace NpvCalculator.Infrastracture.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<NpvCalculation> NpvCalculations { get; }
        DbSet<CashFlow> CashFlows { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
