using eTenpo.Product.Application.CategoryFeature.Create;

namespace Product.Test;

public class CreateCategoryCommandHandlerTest : BaseTestFixture
{
    public CreateCategoryCommandHandlerTest(IntegrationTestWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task ShouldAddNewCategoryToDatabase()
    {
        // Arrange
        var command = new CreateCategoryCommand("FirstProduct", "FirstProductDescription");

        // Act
        var result = await Sender.Send(command);

        // Assert
        Assert.Equal(command.Name, result.Name);
        Assert.Equal(command.Description, result.Description);
    }
}