namespace ContractMonthlyClaimSyt.Models
{
    public class HrDashboardViewModel
    {
        public List<Claim> ApprovedClaims { get; set; } = new();
        public List<Claim> InvoicedClaims { get; set; } = new();
    }
}
