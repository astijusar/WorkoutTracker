using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Core.Models.DTOs.Exercise;
using Core.Models.Enums;
using Core.Models.RequestFeatures;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace API.IntegrationTests
{
    public class ExerciseControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;

        public ExerciseControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        services.AddAuthentication(options =>
                            {
                                options.DefaultAuthenticateScheme = "TestScheme";
                                options.DefaultChallengeScheme = "TestScheme";
                            })
                            .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                                "TestScheme", options => { });
                    });
                })
                .CreateClient(new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false
                });
        }

        [Fact]
        public async Task GET_GetExercises_ValidRequiredParameters_ReturnsOffsetPaginationResponseObject()
        {
            var response = await _client.GetAsync("/api/Exercise");

            var resultObj = await response.Content.ReadAsAsync<OffsetPaginationResponse<ExerciseDto>>();

            response.IsSuccessStatusCode.Should().BeTrue();
            resultObj.Data.Count().Should().Be(9);
            resultObj.Pagination.PageNumber.Should().Be(1);
            resultObj.Pagination.TotalPages.Should().Be(1);
            resultObj.Pagination.HasPrevious.Should().BeFalse();
            resultObj.Pagination.HasNext.Should().BeFalse();
        }

        [Fact]
        public async Task GET_GetExercises_ValidOptionalParameters_ReturnsOffsetPaginationResponseObject()
        {
            var response = await _client
                .GetAsync("/api/Exercise?pageSize=2&equipmentType=Barbell&muscleGroup=Chest&searchTerm=press&SortDescending=false");

            var resultObj = await response.Content.ReadAsAsync<OffsetPaginationResponse<ExerciseDto>>();

            response.IsSuccessStatusCode.Should().BeTrue();
            resultObj.Data.Count().Should().Be(1);
            resultObj.Data.ElementAt(0).EquipmentType.Should().Be(Equipment.Barbell);
            resultObj.Data.ElementAt(0).MuscleGroup.Should().Be(MuscleGroup.Chest);
            resultObj.Data.ElementAt(0).Name.Should().Contain("press");
            resultObj.Pagination.PageNumber.Should().Be(1);
            resultObj.Pagination.TotalPages.Should().Be(1);
            resultObj.Pagination.HasPrevious.Should().BeFalse();
            resultObj.Pagination.HasNext.Should().BeFalse();
        }

        [Fact]
        public async Task GET_GetExercise_ValidRequiredParameters_ReturnsExercise()
        {
            var response = await _client.GetAsync("/api/Exercise/e43f505f-abb1-491d-a75b-cc0dad3d998d");

            var resultObj = await response.Content.ReadAsAsync<ExerciseDto>();

            response.IsSuccessStatusCode.Should().BeTrue();
            resultObj.Should().NotBeNull();
            resultObj.Name.Should().Be("Bench press");
        }

        [Fact]
        public async Task GET_GetExercise_IdDoesNotExist_ReturnsNotFound()
        {
            var response = await _client.GetAsync("/api/Exercise/123");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task POST_CreateExercise_ValidRequiredParameters_ReturnsCreated()
        {
            var requestBody = new ExerciseCreationDto("Test",  "Lorem Ipsum", "chest", "cable");
            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8,
                "application/json");

            var response = await _client.PostAsync("/api/Exercise", content);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }
    }
}