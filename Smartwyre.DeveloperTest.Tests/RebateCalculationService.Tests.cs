using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests;

public class RebateCalculationServiceTest
{
    private readonly IRebateCalculationService _rebateCalculationService;

    public RebateCalculationServiceTest()
    {
        _rebateCalculationService = new RebateCalculationService();
    }

    [Fact]
    public void GetCalculationResult_With_AmountPerUomStrategy()
    {
        RebateCalculation rebateCalculation = new RebateCalculation
        { 
            IncentiveType = IncentiveType.AmountPerUom, SupportedIncentives = SupportedIncentiveType.AmountPerUom
        };

        var resultIfIsSupported = _rebateCalculationService.IsSupported(rebateCalculation, _rebateCalculationService.GetRebateCalculationStrategy(rebateCalculation));

        rebateCalculation.Amount = 0;
        rebateCalculation.Volume = 1;
        var resultIfAmountIsZero = _rebateCalculationService.GetCalculationResult(rebateCalculation);

        rebateCalculation.Amount = 1;
        rebateCalculation.Volume = 0;
        var resultIfVolumeIsZero = _rebateCalculationService.GetCalculationResult(rebateCalculation);

        rebateCalculation.Amount = 5;
        rebateCalculation.Volume = 10;
        var resultIfValidCalculation = _rebateCalculationService.GetCalculationResult(rebateCalculation);

        Assert.Multiple(
            () => Assert.True(resultIfIsSupported),
            () => Assert.Null(resultIfAmountIsZero),
            () => Assert.Null(resultIfVolumeIsZero),
            () => Assert.Equal(50, resultIfValidCalculation)
        );
    }

    [Fact]
    public void GetCalculationResult_With_FixedCashAmountStrategy()
    {
        RebateCalculation rebateCalculation = new RebateCalculation
        { 
            IncentiveType = IncentiveType.FixedCashAmount, SupportedIncentives = SupportedIncentiveType.FixedCashAmount
        };

        var resultIfIsSupported = _rebateCalculationService.IsSupported(rebateCalculation, _rebateCalculationService.GetRebateCalculationStrategy(rebateCalculation));

        rebateCalculation.Amount = 0;
        var resultIfAmountIsZero = _rebateCalculationService.GetCalculationResult(rebateCalculation);

        rebateCalculation.Amount = 5;
        var resultIfValidCalculation = _rebateCalculationService.GetCalculationResult(rebateCalculation);

        Assert.Multiple(
            () => Assert.True(resultIfIsSupported),
            () => Assert.Null(resultIfAmountIsZero),
            () => Assert.Equal(5, resultIfValidCalculation)
        );
    }

    [Fact]
    public void GetCalculationResult_With_FixedRateRebateStrategy()
    {
        RebateCalculation rebateCalculation = new RebateCalculation
        {
            IncentiveType = IncentiveType.FixedRateRebate, SupportedIncentives = SupportedIncentiveType.FixedRateRebate
        };

        var resultIfIsSupported = _rebateCalculationService.IsSupported(rebateCalculation, _rebateCalculationService.GetRebateCalculationStrategy(rebateCalculation));

        rebateCalculation.Percentage = 0;
        rebateCalculation.Price = 1;
        rebateCalculation.Volume = 1;
        var resultIfAmountIsZero = _rebateCalculationService.GetCalculationResult(rebateCalculation);

        rebateCalculation.Percentage = 1;
        rebateCalculation.Price = 0;
        rebateCalculation.Volume = 1;
        var resultIfPriceIsZero = _rebateCalculationService.GetCalculationResult(rebateCalculation);

        rebateCalculation.Percentage = 1;
        rebateCalculation.Price = 1;
        rebateCalculation.Volume = 0;
        var resultIfVolumeIsZero = _rebateCalculationService.GetCalculationResult(rebateCalculation);

        rebateCalculation.Percentage = 5;
        rebateCalculation.Price = 20;
        rebateCalculation.Volume = 10;
        var resultIfValidCalculation = _rebateCalculationService.GetCalculationResult(rebateCalculation);

        Assert.Multiple(
            () => Assert.True(resultIfIsSupported),
            () => Assert.Null(resultIfAmountIsZero),
            () => Assert.Null(resultIfPriceIsZero),
            () => Assert.Null(resultIfVolumeIsZero),
            () => Assert.Equal(1000, resultIfValidCalculation)
        );
    }
}
