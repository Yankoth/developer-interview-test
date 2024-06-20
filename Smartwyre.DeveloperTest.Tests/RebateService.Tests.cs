using Moq;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests;

public class RebateServiceTests
{
    private readonly Mock<IRebateDataStore> _rebateDateStoreMock;
    private readonly Mock<IProductDataStore> _productDataStoreMock;
    private readonly IRebateCalculationService _rebateCalculationService;
    private readonly IRebateService _rebateService;

    public RebateServiceTests()
    {
        _rebateDateStoreMock = new Mock<IRebateDataStore>();
        _productDataStoreMock = new Mock<IProductDataStore>();
        _rebateCalculationService = new RebateCalculationService();
        _rebateService = new RebateService(_rebateDateStoreMock.Object, _productDataStoreMock.Object, _rebateCalculationService);
    }

    [Fact]
    public void CalculateAndStoreResult_ShouldThrow_When_RebateIsNull()
    {
        var calculateRebateRequest = new CalculateRebateRequest();

        _rebateDateStoreMock.Setup(x => x.GetRebate(It.IsAny<string>()))
            .Returns((string rebateIdentifier) => null);

        _productDataStoreMock.Setup(x => x.GetProduct(It.IsAny<string>()))
            .Returns((string productIdentifier) => new Product());

        Assert.Throws<InvalidOperationException>(() => _rebateService.CalculateAndStoreResult(calculateRebateRequest));
    }

    [Fact]
    public void CalculateAndStoreResult_ShouldThrow_When_ProductIsNull()
    {
        var calculateRebateRequest = new CalculateRebateRequest();

        _rebateDateStoreMock.Setup(x => x.GetRebate(It.IsAny<string>()))
            .Returns((string rebateIdentifier) => new Rebate());

        _productDataStoreMock.Setup(x => x.GetProduct(It.IsAny<string>()))
            .Returns((string productIdentifier) => null);

        Assert.Throws<InvalidOperationException>(() => _rebateService.CalculateAndStoreResult(calculateRebateRequest));
    }

    [Fact]
    public void CalculateAndStoreResult_With_AmountPerUomStrategy()
    {
        _rebateDateStoreMock.Setup(x => x.GetRebate(It.IsAny<string>()))
            .Returns((string rebateIdentifier) => new Rebate
            {
                Incentive = IncentiveType.AmountPerUom,
                Amount = 5
            });

        _productDataStoreMock.Setup(x => x.GetProduct(It.IsAny<string>()))
            .Returns((string productIdentifier) => new Product
            {
                SupportedIncentives = SupportedIncentiveType.AmountPerUom
            });

        var result = _rebateService.CalculateAndStoreResult(new CalculateRebateRequest { Volume = 5 });

        Assert.Multiple(
            () => Assert.True(result.Success),
            () => Assert.Equal(25, result.Amount)
            // Missing assert to check if result was stored
        );
    }

    [Fact]
    public void CalculateAndStoreResult_With_FixedCashAmountStrategy()
    {
        _rebateDateStoreMock.Setup(x => x.GetRebate(It.IsAny<string>()))
            .Returns((string rebateIdentifier) => new Rebate
            {
                Incentive = IncentiveType.FixedCashAmount,
                Amount = 5
            });

        _productDataStoreMock.Setup(x => x.GetProduct(It.IsAny<string>()))
            .Returns((string productIdentifier) => new Product
            {
                SupportedIncentives = SupportedIncentiveType.FixedCashAmount
            });

        var result = _rebateService.CalculateAndStoreResult(new CalculateRebateRequest { });

        Assert.Multiple(
            () => Assert.True(result.Success),
            () => Assert.Equal(5, result.Amount)
            // Missing assert to check if result was stored
        );
    }

    [Fact]
    public void CalculateAndStoreResult_With_FixedRateRebateStrategy()
    {
        _rebateDateStoreMock.Setup(x => x.GetRebate(It.IsAny<string>()))
            .Returns((string rebateIdentifier) => new Rebate
            {
                Incentive = IncentiveType.FixedRateRebate,
                Percentage = 5
            });

        _productDataStoreMock.Setup(x => x.GetProduct(It.IsAny<string>()))
            .Returns((string productIdentifier) => new Product
            {
                SupportedIncentives = SupportedIncentiveType.FixedRateRebate,
                Price = 5
            });

        var result = _rebateService.CalculateAndStoreResult(new CalculateRebateRequest { Volume = 5 });

        Assert.Multiple(
            () => Assert.True(result.Success),
            () => Assert.Equal(125, result.Amount)
            // Missing assert to check if result was stored
        );
    }
}
