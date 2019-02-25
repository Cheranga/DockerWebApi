using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Docker.WebApi.DTO;
using Newtonsoft.Json;
using Xunit;

namespace Docker.WebApi.Tests
{
    public class GetNumberTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly TestWebApplicationFactory _factory;

        public GetNumberTests(TestWebApplicationFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _httpClient = factory.CreateClient();
        }

        private readonly HttpClient _httpClient;

        [Theory]
        [InlineData("trivia", 1)]
        [InlineData("trivia", 2)]
        [InlineData("trivia", 3)]
        [InlineData("trivia", 4)]
        [InlineData("trivia", 5)]
        public async Task A_Number_Must_Return_Fun_Fact_If_It_Exists(string controller, int number)
        {
            //
            // Act
            //
            var httpResponse = await _httpClient.GetAsync($"/api/{controller}/{number}");
            //
            // Assert
            //
            httpResponse.EnsureSuccessStatusCode();

            var triviaResponse = JsonConvert.DeserializeObject<TriviaResponse>(await httpResponse.Content.ReadAsStringAsync());

            Assert.NotNull(triviaResponse);
            Assert.Equal(number, triviaResponse.Number);
            Assert.NotNull(triviaResponse.Text);
        }
    }
}
