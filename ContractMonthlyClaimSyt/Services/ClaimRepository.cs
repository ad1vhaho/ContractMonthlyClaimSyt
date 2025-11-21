using ContractMonthlyClaimSyt.Models;

namespace ContractMonthlyClaimSyt.Services
{
    // Stores data in lists for the duration of the app – exactly what the brief wants.
    public class ClaimRepository
    {
        private readonly List<Claim> _claims = new();
        private int _nextId = 1;

        public List<Claim> GetAll() => _claims;

        public Claim? GetById(int id) =>
            _claims.FirstOrDefault(c => c.Id == id);

        public Claim Add(Claim claim)
        {
            claim.Id = _nextId++;

            if (string.IsNullOrWhiteSpace(claim.ClaimNumber))
            {
                claim.ClaimNumber = $"CLM-{claim.Id:0000}";
            }

            _claims.Add(claim);
            return claim;
        }

        public void Update(Claim claim)
        {
            var index = _claims.FindIndex(c => c.Id == claim.Id);
            if (index >= 0)
            {
                _claims[index] = claim;
            }
        }
    }
}
