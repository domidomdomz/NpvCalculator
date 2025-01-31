using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using NpvCalculator.Application.Npv.Validators;
using MediatR;

namespace NpvCalculator.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            // Configure FluentValidation
            services.AddValidatorsFromAssemblyContaining<CalculateNpvCommandValidator>();

            // Add MediatR Pipeline Behaviors
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
