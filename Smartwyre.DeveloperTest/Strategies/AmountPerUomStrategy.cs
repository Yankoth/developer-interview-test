using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Strategies
{
    public class AmountPerUomStrategy : IRebateCalculationStrategy
    {
        public SupportedIncentiveType SupportedIncentiveType => SupportedIncentiveType.AmountPerUom;

        public decimal? GetResult(RebateCalculation rebateCalculation)
        {            
            if (rebateCalculation.Amount == 0 || rebateCalculation.Volume == 0)
            {
                return null;
            }

            return rebateCalculation.Amount * rebateCalculation.Volume;
        }
    }
}
