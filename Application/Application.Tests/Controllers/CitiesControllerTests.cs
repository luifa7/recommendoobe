using Application.Core.Commands.CityCommands;
using Application.Core.Controllers;
using Application.Core.Interfaces;
using Application.Core.Services;
using AutoMapper;
using Domain.Core.Objects;
using DTOs.Cities;
using DTOs.Recommendations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Application.Tests.Controllers;

public class CitiesControllerTests
{
    private Mock<IMapper> _mockMapper;
    private Mock<IMediator> _mockMediator;
    private Mock<ICityCrudService> _mockCityService;
    private Mock<ITagCrudService> _mockTagService;
    private Mock<IRecommendationCrudService> _mockRecommendationService;
    private Mock<IFriendCrudService> _mockFriendService;
    private CitiesController _controller;

    public CitiesControllerTests()
    {
        _mockMapper = new Mock<IMapper>();
        _mockMediator = new Mock<IMediator>();
        _mockCityService = new Mock<ICityCrudService>();
        _mockTagService = new Mock<ITagCrudService>();
        _mockRecommendationService = new Mock<IRecommendationCrudService>();
        _mockFriendService = new Mock<IFriendCrudService>();
        _controller = new CitiesController(_mockMapper.Object, _mockMediator.Object, _mockCityService.Object, _mockTagService.Object, _mockRecommendationService.Object, _mockFriendService.Object);
    }

    [Fact]
    public void GetAll_ReturnsAllCities()
    {
        // Arrange
        var cities = new List<City> { new City("1", "Test City", "Test Country", "Test Photo", "Test UserDId",false) };
        _mockCityService.Setup(service => service.GetAll()).Returns(cities);

        // Act
        var result = _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<ReadCity>>(okResult.Value);
        Assert.Equal(cities.Count, returnValue.Count);
    }

    [Fact]
    public void GetByDId_ReturnsCity_WhenCityExists()
    {
        // Arrange
        var city = new City("1", "Test City", "Test Country", "Test Photo", "Test UserDId",false);
        _mockCityService.Setup(service => service.GetByDId("1")).Returns(city);

        // Act
        var result = _controller.GetByDId("1");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<ReadCity>(okResult.Value);
        Assert.Equal(city.Name, returnValue.Name);
    }

    [Fact]
    public void GetRecommendationsByCityDId_ReturnsRecommendations_WhenCityExists()
    {
        // Arrange
        var recommendations = new List<Recommendation> 
        { 
            new Recommendation("1", "Test PlaceName", "Test Title", "Test Text", "Test Address", "Test Maps", 
                "Test Website", "Test Instagram", "Test Facebook", "Test OtherLink", "Test Photo", 1234567890, 
                "Test CityDId", "Test FromUserDId", "Test ToUserDId") 
        };

        _mockRecommendationService.Setup(service => service.GetRecommendationsByCityDId("1")).Returns(recommendations);

        // Act
        var result = _controller.GetRecommendationsByCityDId("1");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<ReadRecommendation>>(okResult.Value);
        Assert.Equal(recommendations.Count, returnValue.Count);
    }

    [Fact]
    public async Task Create_ReturnsCreatedAtAction_WhenCityIsCreated()
    {
        // Arrange
        var city = new City("1", "Test City", "Test Country", "Test Photo", "Test UserDId", false);
        var createCity = new CreateCity
        {
            // Set properties with valid data.
            // For example:
            Name = "Test City",
            Country = "Test Country",
            Photo = "Test Photo",
            UserDId = "Test UserDId",
            Visited = false
        };
        _mockMediator.Setup(mediator => mediator.Send(It.IsAny<CreateCityCommand>(), default(CancellationToken))).ReturnsAsync(city);

        // Act
        var result = await _controller.Create(createCity);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(city.DId, createdAtActionResult.RouteValues["dId"]);
    }

    [Fact]
    public async Task Update_ReturnsNoContent_WhenCityIsUpdated()
    {
        // Arrange
        var city = new City("1", "Test City", "Test Country", "Test Photo", "Test UserDId",false);
        _mockMediator.Setup(mediator => mediator.Send(It.IsAny<UpdateCityCommand>(), default(CancellationToken))).ReturnsAsync(true);

        // Act
        var result = await _controller.Update(city.DId, new UpdateCity());

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Update_ReturnsBadRequest_WhenCityUpdateFails()
    {
        // Arrange
        var city = new City("1", "Test City", "Test Country", "Test Photo", "Test UserDId",false);
        _mockMediator.Setup(mediator => mediator.Send(It.IsAny<UpdateCityCommand>(), default(CancellationToken))).ReturnsAsync(false);

        // Act
        var result = await _controller.Update(city.DId, new UpdateCity());

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenCityIsDeleted()
    {
        // Arrange
        var city = new City("1", "Test City", "Test Country", "Test Photo", "Test UserDId",false);
        _mockMediator.Setup(mediator => mediator.Send(It.IsAny<DeleteCityCommand>(), default(CancellationToken))).ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(city.DId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsBadRequest_WhenCityDeletionFails()
    {
        // Arrange
        var city = new City("1", "Test City", "Test Country", "Test Photo", "Test UserDId",false);
        _mockMediator.Setup(mediator => mediator.Send(It.IsAny<DeleteCityCommand>(), default(CancellationToken))).ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(city.DId);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }
}