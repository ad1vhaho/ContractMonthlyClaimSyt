using System.ComponentModel.DataAnnotations;

namespace ContractMonthlyClaimSyt.Models
{
    public class Claim
    {
        public int Id { get; set; }   // Internal ID for the in-memory list

        [Display(Name = "Claim Number")]
        public string ClaimNumber { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Lecturer Name")]
        public string LecturerName { get; set; } = string.Empty;

        [Required]
        [Range(1, 12)]
        public int Month { get; set; } = DateTime.Today.Month;

        [Required]
        [Range(2020, 2100)]
        public int Year { get; set; } = DateTime.Today.Year;

        [Display(Name = "Module / Subject")]
        public string ModuleName { get; set; } = string.Empty;

        [Required]
        [Range(0, 400)]
        [Display(Name = "Hours Worked")]
        public decimal HoursWorked { get; set; }

        [Required]
        [Range(0, 5000)]
        [Display(Name = "Hourly Rate (R)")]
        public decimal HourlyRate { get; set; }

        [Display(Name = "Total Amount (R)")]
        public decimal Amount => HoursWorked * HourlyRate;

        [Display(Name = "Additional Notes")]
        public string? AdditionalNotes { get; set; }

        public ClaimStatus Status { get; set; } = ClaimStatus.Pending;

        [Display(Name = "Submitted On")]
        public DateTime SubmittedOn { get; set; } = DateTime.Now;

        // Only file names are stored (marking requires lists, not real storage)
        public List<string> AttachmentFileNames { get; set; } = new();
    }
}
