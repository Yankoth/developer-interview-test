using Smartwyre.DeveloperTest.Database;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main(string[] args)
    {
        DbContext.Rebates.AddRange(new List<Rebate>
        {
            new Rebate { Identifier = "R1", Incentive = IncentiveType.FixedRateRebate, Amount = 5, Percentage = 2 },
            new Rebate { Identifier = "R2", Incentive = IncentiveType.AmountPerUom, Amount = 10, Percentage = 4 },
            new Rebate { Identifier = "R3", Incentive = IncentiveType.FixedCashAmount, Amount = 15, Percentage = 6 },
            new Rebate { Identifier = "R4", Incentive = IncentiveType.FixedCashAmount, Amount = 0, Percentage = 8 },
            new Rebate { Identifier = "R5", Incentive = IncentiveType.AmountPerUom, Amount = 20, Percentage = 10 },
            new Rebate { Identifier = "R6", Incentive = IncentiveType.FixedRateRebate, Amount = 25, Percentage = 12 },
        });

        DbContext.Products.AddRange(new List<Product>
        {
            new Product { Identifier = "P1", SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Price = 1 },
            new Product { Identifier = "P2", SupportedIncentives = SupportedIncentiveType.AmountPerUom, Price = 2 },
            new Product { Identifier = "P3", SupportedIncentives = SupportedIncentiveType.FixedCashAmount, Price = 3 },
            new Product { Identifier = "P4", SupportedIncentives = SupportedIncentiveType.FixedCashAmount, Price = 5 },
            new Product { Identifier = "P5", SupportedIncentives = SupportedIncentiveType.AmountPerUom, Price = 8 },
            new Product { Identifier = "P6", SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Price = 13 },
        });

        var rebateService = DependencyInjection.GetRebateService();

        while (true)
        {
            try
            {
                Console.Write("Enter rebate identifier: ");
                var rebateIdentifier = Console.ReadLine();

                Console.Write("Enter product identifier: ");
                var productIdentifier = Console.ReadLine();

                Console.Write("Enter volume: ");
                var volume = decimal.TryParse(Console.ReadLine(), out var result) ? result : throw new InvalidOperationException("Invalid volume.");

                var calculateRebateResult = rebateService.CalculateAndStoreResult(new CalculateRebateRequest
                {
                    RebateIdentifier = rebateIdentifier,
                    ProductIdentifier = productIdentifier,
                    Volume = volume,
                });

                if (calculateRebateResult.Success)
                {
                    Console.WriteLine();
                    Console.Write("Rebate amount: ");
                    Console.WriteLine(calculateRebateResult.Amount);
                }
                else
                {
                    throw new Exception("Internal error.");
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine();
                Console.WriteLine("Rebate failed: " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("============================");
            Console.WriteLine();
        }
    }
}
