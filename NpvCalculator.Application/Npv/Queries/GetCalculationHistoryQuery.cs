using MediatR;
using NpvCalculator.Core.Entities;

namespace NpvCalculator.Application.Npv.Queries
{
    public sealed record GetCalculationHistoryQuery : IRequest<List<NpvCalculation>>;
}
