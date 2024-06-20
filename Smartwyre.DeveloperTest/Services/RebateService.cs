using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    private readonly IRebateDataStore _rebateDataStore;
    private readonly IProductDataStore _productDataStore;
    private readonly IRebateCalculationService _rebateCalculationService;

    public RebateService(IRebateDataStore rebateDataStore, IProductDataStore productDataStore, IRebateCalculationService rebateCalculationService)
    {
        _rebateDataStore = rebateDataStore;
        _productDataStore = productDataStore;
        _rebateCalculationService = rebateCalculationService;
    }

    public CalculateRebateResult CalculateAndStoreResult(CalculateRebateRequest request)
    {
        var calculateRebateResult = new CalculateRebateResult
        { 
            Success = false
        };

        try
        {
            var rebate = _rebateDataStore.GetRebate(request.RebateIdentifier) ?? throw new InvalidOperationException("Rebate not found.");
            var product = _productDataStore.GetProduct(request.ProductIdentifier) ?? throw new InvalidOperationException("Product not found.");

            var rebateCalculation = new RebateCalculation
            {
                IncentiveType = rebate.Incentive,
                Amount = rebate.Amount,
                SupportedIncentives = product.SupportedIncentives,
                Volume = request.Volume,
                Percentage = rebate.Percentage,
                Price = product.Price
            };

            var rebateCalculationResult = _rebateCalculationService.GetCalculationResult(rebateCalculation);

            calculateRebateResult.Success = rebateCalculationResult.HasValue;

            if (calculateRebateResult.Success)
            {
                calculateRebateResult.Amount = rebateCalculationResult.Value;
                _rebateDataStore.StoreCalculationResult(rebate, calculateRebateResult.Amount);
            }
            else
            {
                throw new InvalidOperationException("Invalid Rebate Calculation data.");
            }

            return calculateRebateResult;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
