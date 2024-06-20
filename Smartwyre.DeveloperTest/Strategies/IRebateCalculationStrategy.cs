using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Strategies
{
    public interface IRebateCalculationStrategy
    {
        SupportedIncentiveType SupportedIncentiveType { get; }
        decimal? GetResult(RebateCalculation rebateCalculation);
    }
}
