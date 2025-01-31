using FluentValidation;
using NpvCalculator.Application.Npv.Commands;

namespace NpvCalculator.Application.Npv.Validators
{
    public class CalculateNpvCommandValidator : AbstractValidator<CalculateNpvCommand>
    {
        public CalculateNpvCommandValidator() 
        { 
            RuleFor(x => x.InitialInvestment)
                .GreaterThan(0)
                .WithMessage("Initial investment must be positive");

            RuleFor(x => x.DiscountRate)
                .InclusiveBetween(0, 1)
                .WithMessage("Discount rate must be between 0% and 100%");

            RuleFor(x => x.CashFlows)
                .NotEmpty()
                .WithMessage("At least one cash flow required")
                .Must(x => x.All(amount => amount > 0))
                .WithMessage("All cash flows must be positive");
        }
    }
}
