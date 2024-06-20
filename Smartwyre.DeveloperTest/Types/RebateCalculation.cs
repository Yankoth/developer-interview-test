namespace Smartwyre.DeveloperTest.Types;

public class RebateCalculation
{
    public int Id { get; set; }
    public string Identifier { get; set; }
    public string RebateIdentifier { get; set; }
    public IncentiveType IncentiveType { get; set; }
    public decimal Amount { get; set; }
    public SupportedIncentiveType SupportedIncentives { get; set; }
    public decimal Volume { get; set; }
    public decimal Percentage { get; set; }
    public decimal Price { get; set; }
}
