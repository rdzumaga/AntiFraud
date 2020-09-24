using AntiFraud.API.Models;
using Microsoft.EntityFrameworkCore;
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
            // check if from nigeria
            if (!purchase.Address.Country.Equals("NIGERIA", System.StringComparison.OrdinalIgnoreCase)) return false;

            // check if amount is over 1000
            if (purchase.Amount <= 1000) return false;

            // if is from nigeria and amount is over 1000
            // check if this user has any existing valid purchases
            // if yes, then it is not a fraud
            var userPurchases = _dataContext.Purchases.Count(x => EF.Functions.Like(x.Email, $"{purchase.Email}") && x.Status == Enums.PurchaseStatus.Valid);
            if (userPurchases > 0) return false;

            return true;
        }
    }
}
