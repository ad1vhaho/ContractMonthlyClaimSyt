
using System.Collections.Generic;

namespace ContractMonthlyClaimSyt.Models
{
    public class CoordinatorDashboardViewModel
    {
        public List<Claim> PendingClaims { get; set; } = new();
        public List<Claim> ApprovedClaims { get; set; } = new();
        public List<Claim> RejectedClaims { get; set; } = new();
    }
}
