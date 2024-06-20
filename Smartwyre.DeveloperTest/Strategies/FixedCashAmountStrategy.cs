using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Strategies
{
    public class FixedCashAmountStrategy : IRebateCalculationStrategy
    {
        public SupportedIncentiveType SupportedIncentiveType => SupportedIncentiveType.FixedCashAmount;

        public decimal? GetResult(RebateCalculation rebateCalculation)
        {
            if (rebateCalculation.Amount == 0)
            {
                return null;
            }

            return rebateCalculation.Amount;
        }
    }
}
