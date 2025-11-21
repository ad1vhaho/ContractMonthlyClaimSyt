using ContractMonthlyClaimSyt.Models;
using ContractMonthlyClaimSyt.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContractMonthlyClaimSyt.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly ClaimRepository _repo;

        public ClaimsController(ClaimRepository repo)
        {
            _repo = repo;
        }

        // ----------------- LECTURER -----------------

        [HttpGet]
        public IActionResult Lecturer()
        {
            var vm = new LecturerDashboardViewModel
            {
                NewClaim = new Claim
                {
                    Year = DateTime.Today.Year,
                    Month = DateTime.Today.Month,
                    SubmittedOn = DateTime.Now
                },
                MyClaims = _repo.GetAll()
                                .OrderByDescending(c => c.SubmittedOn)
                                .ToList()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitLecturerClaim(
            LecturerDashboardViewModel vm,
            List<IFormFile>? attachments)
        {
            if (!ModelState.IsValid)
            {
                vm.MyClaims = _repo.GetAll()
                                   .OrderByDescending(c => c.SubmittedOn)
                                   .ToList();
                return View("Lecturer", vm);
            }

            var claim = vm.NewClaim;
            claim.Status = ClaimStatus.Pending;
            claim.SubmittedOn = DateTime.Now;

            if (attachments != null)
            {
                foreach (var file in attachments)
                {
                    if (file.Length > 0)
                    {
                        claim.AttachmentFileNames.Add(file.FileName);
                    }
                }
            }

            _repo.Add(claim);
            TempData["Message"] = $"Claim {claim.ClaimNumber} submitted successfully.";
            return RedirectToAction(nameof(Lecturer));
        }

        // All claims – quick overview
        [HttpGet]
        public IActionResult MyClaims()
        {
            var claims = _repo.GetAll()
                              .OrderByDescending(c => c.SubmittedOn)
                              .ToList();
            return View(claims);
        }

        // ----------------- COORDINATOR -----------------

        [HttpGet]
        public IActionResult Coordinator()
        {
            var vm = new CoordinatorDashboardViewModel
            {
                PendingClaims = _repo.GetAll()
                                     .Where(c => c.Status == ClaimStatus.Pending)
                                     .OrderBy(c => c.SubmittedOn)
                                     .ToList()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Verify(int id, string decision)
        {
            var claim = _repo.GetById(id);
            if (claim == null) return NotFound();

            if (decision == "Approve")
                claim.Status = ClaimStatus.Verified;
            else if (decision == "Reject")
                claim.Status = ClaimStatus.Rejected;

            _repo.Update(claim);
            TempData["Message"] = "Coordinator decision captured.";
            return RedirectToAction(nameof(Coordinator));
        }

        // ----------------- MANAGER -----------------

        [HttpGet]
        public IActionResult Manager()
        {
            var vm = new ManagerDashboardViewModel
            {
                VerifiedClaims = _repo.GetAll()
                                      .Where(c => c.Status == ClaimStatus.Verified)
                                      .OrderBy(c => c.SubmittedOn)
                                      .ToList(),
                RejectedClaims = _repo.GetAll()
                                      .Where(c => c.Status == ClaimStatus.Rejected)
                                      .OrderBy(c => c.SubmittedOn)
                                      .ToList()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Approve(int id)
        {
            var claim = _repo.GetById(id);
            if (claim == null) return NotFound();

            claim.Status = ClaimStatus.Approved;
            _repo.Update(claim);

            TempData["Message"] = "Claim approved by Academic Manager.";
            return RedirectToAction(nameof(Manager));
        }

        // ----------------- HR -----------------

        [HttpGet]
        public IActionResult Hr()
        {
            var vm = new HrDashboardViewModel
            {
                ApprovedClaims = _repo.GetAll()
                                      .Where(c => c.Status == ClaimStatus.Approved)
                                      .OrderBy(c => c.SubmittedOn)
                                      .ToList(),
                InvoicedClaims = _repo.GetAll()
                                      .Where(c => c.Status == ClaimStatus.Invoiced)
                                      .OrderBy(c => c.SubmittedOn)
                                      .ToList()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkInvoiced(int id)
        {
            var claim = _repo.GetById(id);
            if (claim == null) return NotFound();

            claim.Status = ClaimStatus.Invoiced;
            _repo.Update(claim);

            TempData["Message"] = "Claim marked as invoiced.";
            return RedirectToAction(nameof(Hr));
        }
    }
}
