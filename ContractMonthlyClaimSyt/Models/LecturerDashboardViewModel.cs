// Models/LecturerDashboardViewModel.cs
using System.Collections.Generic;

namespace ContractMonthlyClaimSyt.Models
{
    public class LecturerDashboardViewModel
    {
        public Claim NewClaim { get; set; } = new Claim();
        public List<Claim> MyClaims { get; set; } = new();
    }
}
