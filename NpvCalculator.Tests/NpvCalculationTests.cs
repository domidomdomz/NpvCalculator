using NpvCalculator.Application.Services;

namespace NpvCalculator.Tests
{
    public class NpvCalculationTests
    {
        [Theory]
        [InlineData(1000, 0.1, new[] { 500.0, 500.0, 500.0 }, 243.4262)]
        public void CalculateNpv_ValidInput_ReturnsCorrectValue(
            decimal initialInvestment, 
            double discountRate, 
            double[] cashFlows,
            decimal expected)
        {
            // Arrange

            // Act
            var result = NpvCalculatorService.CalculateNpv(initialInvestment, 
                                                            (decimal)discountRate, 
                                                            cashFlows.Select(Convert.ToDecimal).ToList());
            
            // Assert
            Assert.Equal(expected, result);

        }
    }
}