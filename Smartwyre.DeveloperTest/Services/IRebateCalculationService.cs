using Smartwyre.DeveloperTest.Strategies;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services
{
    public interface IRebateCalculationService
    {
        IRebateCalculationStrategy GetRebateCalculationStrategy(RebateCalculation rebateCalculation);
        bool IsSupported(RebateCalculation rebateCalculation, IRebateCalculationStrategy rebateCalculationStrategy);
        decimal? GetCalculationResult(RebateCalculation rebateCalculation);
    }
}
