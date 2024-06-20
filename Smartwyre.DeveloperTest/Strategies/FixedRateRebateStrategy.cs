using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Strategies
{
    public class FixedRateRebateStrategy : IRebateCalculationStrategy
    {
        public SupportedIncentiveType SupportedIncentiveType => SupportedIncentiveType.FixedRateRebate;

        public decimal? GetResult(RebateCalculation rebateCalculation)
        {
            if (rebateCalculation.Percentage == 0 || rebateCalculation.Price == 0 || rebateCalculation.Volume == 0)
            {
                return null;
            }

            return rebateCalculation.Price * rebateCalculation.Percentage * rebateCalculation.Volume;
        }
    }
}
