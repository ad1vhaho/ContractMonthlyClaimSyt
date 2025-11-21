// Models/ManagerDashboardViewModel.cs
using System.Collections.Generic;

namespace ContractMonthlyClaimSyt.Models
{
    public class ManagerDashboardViewModel
    {
        public List<Claim> VerifiedClaims { get; set; } = new();
        public List<Claim> ApprovedClaims { get; set; } = new();
        public List<Claim> RejectedClaims { get; set; } = new();
    }
}
