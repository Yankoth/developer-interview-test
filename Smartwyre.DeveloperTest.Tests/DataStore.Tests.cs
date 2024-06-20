using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Database;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests;

public class DataStoreTests
{
    private readonly IRebateDataStore _rebateDateStore;
    private readonly IProductDataStore _productDataStore;

    public DataStoreTests()
    {
        _rebateDateStore = new RebateDataStore();
        _productDataStore = new ProductDataStore();
    }

    [Fact]
    public void GetRebateShould_ReturnNull_When_IdentifierIsEmptyOrNull()
    {
        Assert.Multiple(
            () => Assert.Null(_rebateDateStore.GetRebate(string.Empty)),
            () => Assert.Null(_rebateDateStore.GetRebate(null))
        );
    }

    [Fact]
    public void GetProductShould_ReturnNull_When_IdentifierIsEmptyOrNull()
    {
        Assert.Multiple(
            () => Assert.Null(_productDataStore.GetProduct(string.Empty)),
            () => Assert.Null(_productDataStore.GetProduct(null))
        );
    }

    [Fact]
    public void GetRebateShould_ReturnRebate_When_IdentifierIsValid()
    {
        var identifier = "test";

        // Should mock this
        DbContext.Rebates.Add(new Rebate
        {
            Identifier = identifier,
        });

        Assert.NotNull(_rebateDateStore.GetRebate(identifier));
    }

    [Fact]
    public void GetProductShould_ReturnProduct_When_IdentifierIsValid()
    {
        var identifier = "test";

        // Should mock this
        DbContext.Products.Add(new Product
        {
            Identifier = identifier,
        });

        Assert.NotNull(_productDataStore.GetProduct(identifier));
    }
}
