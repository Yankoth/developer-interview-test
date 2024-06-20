using Smartwyre.DeveloperTest.Database;
using Smartwyre.DeveloperTest.Types;
using System.Linq;

namespace Smartwyre.DeveloperTest.Data;

public class ProductDataStore : IProductDataStore
{
    public Product GetProduct(string productIdentifier)
    {
        // Access dummy database to retrieve product
        return DbContext.Products.FirstOrDefault(x => x.Identifier == productIdentifier);
    }
}
