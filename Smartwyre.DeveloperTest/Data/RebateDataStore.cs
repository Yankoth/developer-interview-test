using Smartwyre.DeveloperTest.Database;
using Smartwyre.DeveloperTest.Types;
using System.Linq;

namespace Smartwyre.DeveloperTest.Data;

public class RebateDataStore : IRebateDataStore
{
    public Rebate GetRebate(string rebateIdentifier)
    {
        // Access dummy database to retrieve rebate
        return DbContext.Rebates.FirstOrDefault(x => x.Identifier == rebateIdentifier);
    }

    public void StoreCalculationResult(Rebate account, decimal rebateAmount)
    {
        // Update rebate in database, code removed for brevity
    }
}
