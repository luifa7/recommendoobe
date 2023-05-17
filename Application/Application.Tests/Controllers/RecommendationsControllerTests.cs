using Application.Core.Commands.RecommendationCommands;
using Application.Core.Controllers;
using Application.Core.Interfaces;
using Domain.Core.Objects;
using DTOs.Recommendations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Application.Tests.Controllers;

public class RecommendationsControllerTests
{
    private readonly Mock<IMediator> _mockMediator;
    private readonly Mock<IRecommendationCrudService> _mockRecommendationService;
    private readonly Mock<ITagCrudService> _mockTagService;
    private readonly RecommendationsController _controller;

    public RecommendationsControllerTests()
    {
        _mockMediator = new Mock<IMediator>();
        _mockRecommendationService = new Mock<IRecommendationCrudService>();
        _mockTagService = new Mock<ITagCrudService>();
        _controller = new RecommendationsController(_mockMediator.Object, _mockRecommendationService.Object, _mockTagService.Object);
    }

    [Fact]
    public void GetAll_ReturnsAllRecommendations()
    {
        // Arrange
        var recommendations = new List<Recommendation> 
        { 
            new Recommendation("1", "Test PlaceName", "Test Title", "Test Text", "Test Address", "Test Maps", 
            "Test Website", "Test Instagram", "Test Facebook", "Test OtherLink", "Test Photo", 1234567890, 
            "Test CityDId", "Test FromUserDId", "Test ToUserDId") 
        };
        _mockRecommendationService.Setup(service => service.GetAll()).Returns(recommendations);

        // Act
        var result = _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<ReadRecommendation>>(okResult.Value);
        Assert.Equal(recommendations.Count, returnValue.Count);
    }

    [Fact]
    public void GetByDId_ReturnsRecommendation_WhenRecommendationExists()
    {
        // Arrange
        var recommendation = new Recommendation("1", "Test PlaceName", "Test Title", "Test Text", "Test Address", "Test Maps", 
        "Test Website", "Test Instagram", "Test Facebook", "Test OtherLink", "Test Photo", 1234567890, 
        "Test CityDId", "Test FromUserDId", "Test ToUserDId");
        _mockRecommendationService.Setup(service => service.GetByDId("1")).Returns(recommendation);

        // Act
        var result = _controller.GetByDId("1");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<ReadRecommendation>(okResult.Value);
        Assert.Equal(recommendation.DId, returnValue.DId);
    }

    [Fact]
    public void GetByDId_ReturnsNotFound_WhenRecommendationDoesNotExist()
    {
        // Arrange
        _mockRecommendationService.Setup(service => service.GetByDId("1")).Returns((Recommendation)null);

        // Act
        var result = _controller.GetByDId("1");

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Create_ReturnsCreatedAtAction_WhenRecommendationIsCreated()
    {
        // Arrange
        var recommendation = new Recommendation("1", "Test PlaceName", "Test Title", "Test Text", "Test Address",
            "Test Maps",
            "Test Website", "Test Instagram", "Test Facebook", "Test OtherLink", "Test Photo", 1234567890,
            "Test CityDId", "Test FromUserDId", "Test ToUserDId");
        _mockMediator
            .Setup(mediator => mediator.Send(It.IsAny<CreateRecommendationCommand>(), default(CancellationToken)))
            .ReturnsAsync(recommendation);
        var createRecommendation = new CreateRecommendation
        {
            PlaceName = "Test PlaceName",
            Title = "Test Title",
            Text = "Test Text",
            Address = "Test Address",
            Maps = "Test Maps",
            Website = "Test Website",
            Instagram = "Test Instagram",
            Facebook = "Test Facebook",
            OtherLink = "Test OtherLink",
            Photo = "Test Photo",
            CityDId = "Test CityDId",
            Tags = new string[] { "Test Tag" } ?? new string[0], // Add null check here
            FromUserDId = "Test FromUserDId",
            ToUserDId = "Test ToUserDId"
        };

        // Act
        var result = await _controller.Create(createRecommendation);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(recommendation.DId, createdAtActionResult.RouteValues["dId"]);
    }
    

    [Fact]
    public async Task Create_ReturnsBadRequest_WhenRecommendationCreationFails()
    {
        // Arrange
        _mockMediator.Setup(mediator => mediator.Send(It.IsAny<CreateRecommendationCommand>(), default(CancellationToken))).ReturnsAsync((Recommendation)null);
        var createRecommendation = new CreateRecommendation
        {
            PlaceName = "Test PlaceName",
            Title = "Test Title",
            Text = "Test Text",
            Address = "Test Address",
            Maps = "Test Maps",
            Website = "Test Website",
            Instagram = "Test Instagram",
            Facebook = "Test Facebook",
            OtherLink = "Test OtherLink",
            Photo = "Test Photo",
            CityDId = "Test CityDId",
            Tags = new string[] { "Test Tag" },
            FromUserDId = "Test FromUserDId",
            ToUserDId = "Test ToUserDId"
        };

        // Act
        var result = await _controller.Create(createRecommendation);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }
    
     [Fact]
    public async Task Update_ReturnsNoContent_WhenRecommendationIsUpdated()
    {
        // Arrange
        _mockMediator.Setup(mediator => mediator.Send(It.IsAny<UpdateRecommendationCommand>(), default(CancellationToken))).ReturnsAsync(true);
        var updateRecommendation = new UpdateRecommendation
        {
            PlaceName = "Test PlaceName",
            Title = "Test Title",
            Text = "Test Text",
            Address = "Test Address",
            Maps = "Test Maps",
            Website = "Test Website",
            Instagram = "Test Instagram",
            Facebook = "Test Facebook",
            OtherLink = "Test OtherLink",
            Photo = "Test Photo",
            Tags = new string[] { "Test Tag" }
        };

        // Act
        var result = await _controller.Update("1", updateRecommendation);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Update_ReturnsBadRequest_WhenRecommendationUpdateFails()
    {
        // Arrange
        _mockMediator.Setup(mediator => mediator.Send(It.IsAny<UpdateRecommendationCommand>(), default(CancellationToken))).ReturnsAsync(false);
        var updateRecommendation = new UpdateRecommendation
        {
            PlaceName = "Test PlaceName",
            Title = "Test Title",
            Text = "Test Text",
            Address = "Test Address",
            Maps = "Test Maps",
            Website = "Test Website",
            Instagram = "Test Instagram",
            Facebook = "Test Facebook",
            OtherLink = "Test OtherLink",
            Photo = "Test Photo",
            Tags = new string[] { "Test Tag" }
        };

        // Act
        var result = await _controller.Update("1", updateRecommendation);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenRecommendationIsDeleted()
    {
        // Arrange
        _mockMediator.Setup(mediator => mediator.Send(It.IsAny<DeleteRecommendationCommand>(), default(CancellationToken))).ReturnsAsync(true);

        // Act
        var result = await _controller.Delete("1");

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsBadRequest_WhenRecommendationDeletionFails()
    {
        // Arrange
        _mockMediator.Setup(mediator => mediator.Send(It.IsAny<DeleteRecommendationCommand>(), default(CancellationToken))).ReturnsAsync(false);

        // Act
        var result = await _controller.Delete("1");

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }
                
}