using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;

namespace Smartwyre.DeveloperTest
{
    // Dummy representative dependency injection
    public static class DependencyInjection
    {
        private static readonly IRebateDataStore RebateDataStore;
        private static readonly IProductDataStore ProductDataStore;
        private static readonly IRebateCalculationService RebateCalculationService;
        private static readonly IRebateService RebateService;

        static DependencyInjection()
        {
            RebateDataStore = new RebateDataStore();
            ProductDataStore = new ProductDataStore();
            RebateCalculationService = new RebateCalculationService();
            RebateService = new RebateService(RebateDataStore, ProductDataStore, RebateCalculationService);
        }

        public static IRebateDataStore GetRebateDataStore() => RebateDataStore;
        public static IProductDataStore GetProductDataStore() => ProductDataStore;
        public static IRebateCalculationService GetRebateCalculationService() => RebateCalculationService;
        public static IRebateService GetRebateService() => RebateService;
    }
}
