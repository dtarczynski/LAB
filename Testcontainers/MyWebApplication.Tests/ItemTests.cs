using MyWebApplication.Api.Domain;
using MyWebApplication.Tests.Abstract;
using MyWebApplication.Tests.Factories;
using Xunit;

namespace TestContainers.PostgreSql;

public class ItemTests : BaseIntegrationTest
{
    public ItemTests(
        IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Can_Create_Item()
    {
        // Arrange
        var id = Guid.NewGuid();
        var item = new Item(id, "Some test product");

        DbContext.Items.Add(item);
        await DbContext.SaveChangesAsync();

        // Act
        var testItem = DbContext.Items.SingleOrDefault(p => p.Id == id);

        // Assert
        Assert.NotNull(testItem);
        Assert.True(testItem.Id == id);
    }
}
