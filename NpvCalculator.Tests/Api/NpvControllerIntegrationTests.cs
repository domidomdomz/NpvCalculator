using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using NpvCalculator.Core.Common;

namespace NpvCalculator.Tests.Api
{
    public class NpvControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public NpvControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder => { });
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task Calculate_ReturnsValidNpvResult()
        {
            // Arrange
            var command = new
            {
                InitialInvestment = 1000,
                DiscountRate = 0.1m,
                CashFlows = new List<decimal> { 500, 500, 500 }
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/npv", command);

            // Assert
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<decimal>();
            Assert.InRange(result, 243.42m, 243.43m);
        }

        [Fact]
        public async Task Calculate_ReturnsException_When_InitialInvestment_Is_Zero()
        {
            // Arrange
            var command = new
            {
                InitialInvestment = 0,
                DiscountRate = 0.1m,
                CashFlows = new List<decimal> { 500, 500, 500 }
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/npv", command);
            var responseContent = await response.Content.ReadFromJsonAsync<ValidationErrorResponse>();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Equal("Validation failed", responseContent.Message);
            Assert.Contains(responseContent.Errors, e => e.PropertyName == "InitialInvestment" && e.ErrorMessage == "Initial investment must be positive");

        }

        [Fact]
        public async Task Calculate_ReturnsValidationError_When_DiscountRate_Is_Negative()
        {
            // Arrange
            var command = new
            {
                InitialInvestment = 100,
                DiscountRate = -0.01m,
                CashFlows = new List<decimal> { 500, 500, 500 }
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/npv", command);
            var responseContent = await response.Content.ReadFromJsonAsync<ValidationErrorResponse>();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Equal("Validation failed", responseContent.Message);
            Assert.Contains(responseContent.Errors, e => e.PropertyName == "DiscountRate" && e.ErrorMessage == "Discount rate must be between 0% and 100%");
        }

        [Fact]
        public async Task Calculate_ReturnsValidationError_When_CashFlows_Is_Empty()
        {
            // Arrange
            var command = new
            {
                InitialInvestment = 100,
                DiscountRate = 0.1m,
                CashFlows = new List<decimal>()
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/npv", command);
            var responseContent = await response.Content.ReadFromJsonAsync<ValidationErrorResponse>();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Equal("Validation failed", responseContent.Message);
            Assert.Contains(responseContent.Errors, e => e.PropertyName == "CashFlows" && e.ErrorMessage == "At least one cash flow required");
        }

        [Fact]
        public async Task Calculate_ReturnsValidationError_When_Any_CashFlow_Is_Negative()
        {
            // Arrange
            var command = new
            {
                InitialInvestment = 100,
                DiscountRate = 0.1m,
                CashFlows = new List<decimal> { 500, -500, 500 }
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/npv", command);
            var responseContent = await response.Content.ReadFromJsonAsync<ValidationErrorResponse>();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.Equal("Validation failed", responseContent.Message);
            Assert.Contains(responseContent.Errors, e => e.PropertyName == "CashFlows" && e.ErrorMessage == "All cash flows must be positive");
        }
    }
}
