using AntiFraud.API.Models;
using System.Linq;

namespace AntiFraud.API.FraudCheckers
{
    public class NigerianPrinceFraudChecker : IFraudChecker
    {
        private readonly DataContext _dataContext;

        public NigerianPrinceFraudChecker(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool IsFraud(Purchase purchase)
        {
            if (!purchase.Address.Country.Equals("NIGERIA", System.StringComparison.OrdinalIgnoreCase)) return false;

            if (purchase.Amount <= 1000) return false;

            var userPurchasesCount = _dataContext.Purchases.Count(x => x.Email.Equals(purchase.Email, System.StringComparison.OrdinalIgnoreCase));
            if (userPurchasesCount > 0) return false;

            return true;
        }
    }
}
