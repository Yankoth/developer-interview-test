using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;
using System;
using Smartwyre.DeveloperTest.Strategies;

namespace Smartwyre.DeveloperTest.Services
{
    public class RebateCalculationService : IRebateCalculationService
    {
        private readonly Dictionary<IncentiveType, Func<IRebateCalculationStrategy>> _rebateCalculationStrategies;

        public RebateCalculationService()
        {
            _rebateCalculationStrategies = new Dictionary<IncentiveType, Func<IRebateCalculationStrategy>>
            {
                { IncentiveType.AmountPerUom, () => new AmountPerUomStrategy() },
                { IncentiveType.FixedCashAmount, () => new FixedCashAmountStrategy() },
                { IncentiveType.FixedRateRebate, () => new FixedRateRebateStrategy() },
            };
        }

        public IRebateCalculationStrategy GetRebateCalculationStrategy(RebateCalculation rebateCalculation)
        {
            if (_rebateCalculationStrategies.TryGetValue(rebateCalculation.IncentiveType, out Func<IRebateCalculationStrategy> CreateRebateStrategy))
            {
                return CreateRebateStrategy();
            }

            throw new NotImplementedException($"Rebate Calculation Strategy not implemented for {rebateCalculation.IncentiveType}.");
        }

        public bool IsSupported(RebateCalculation rebateCalculation, IRebateCalculationStrategy rebateCalculationStrategy)
        {
            return rebateCalculation.SupportedIncentives.HasFlag(rebateCalculationStrategy.SupportedIncentiveType);
        }

        public decimal? GetCalculationResult(RebateCalculation rebateCalculation)
        {
            var rebateCalculationStrategy = GetRebateCalculationStrategy(rebateCalculation);

            if (!IsSupported(rebateCalculation, rebateCalculationStrategy))
            {
                throw new InvalidOperationException("Rebate calculation not supported.");
            }

            return rebateCalculationStrategy.GetResult(rebateCalculation);
        }
    }
}
