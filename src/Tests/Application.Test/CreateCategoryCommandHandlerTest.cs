using eTenpo.Product.Application.CategoryFeature.Create;
using Product.Application.Test.Base;
using SharedLibrary;
using Xunit;

namespace Product.Application.Test;

public class CreateCategoryCommandHandlerTest(IntegrationTestWebApplicationFactory factory)
    : BaseApplicationTestFixture(factory)
{
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
