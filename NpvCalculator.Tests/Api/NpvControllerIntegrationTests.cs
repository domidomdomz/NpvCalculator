using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using NpvCalculator.Api;

namespace NpvCalculator.Tests.Api
{
    public class NpvControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        public NpvControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder => { });
        }

        private readonly WebApplicationFactory<Program> _factory;

        [Fact]
        public async Task Calculate_ReturnsValidNpvResult()
        {
            // Arrange
            var client = _factory.CreateClient();
            var command = new
            {
                InitialInvestment = 1000,
                DiscountRate = 0.1m,
                CashFlows = new List<decimal> { 500, 500, 500 }
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/npv", command);

            // Assert
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<decimal>();
            Assert.InRange(result, 243.42m, 243.43m);
        }
    }
}
