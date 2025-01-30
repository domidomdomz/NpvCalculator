using MediatR;

namespace NpvCalculator.Application.Npv.Commands
{
    public class CalculateNpvCommandHandler : IRequestHandler<CalculateNpvCommand, decimal>
    {
        public CalculateNpvCommandHandler() { }

        public Task<decimal> Handle(CalculateNpvCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
