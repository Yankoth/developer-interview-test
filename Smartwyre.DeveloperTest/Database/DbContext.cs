using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;

namespace Smartwyre.DeveloperTest.Database
{
    public static class DbContext
    {
        public static readonly List<Rebate> Rebates;
        public static readonly List<Product> Products;

        static DbContext()
        {
            Rebates = new List<Rebate>();
            Products = new List<Product>();
        }
    }
}
