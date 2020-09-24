using AntiFraud.API.Models;
using System.Linq;

namespace AntiFraud.API.FraudCheckers
{
    public class UnusuallyHighAmountFraudChecker : IFraudChecker
    {
        private readonly DataContext _dataContext;

        public UnusuallyHighAmountFraudChecker(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public bool IsFraud(Purchase purchase)
        {
            // get existing valid purchases average
            var avgOrderAmount = _dataContext.Purchases.Where(x => x.Status == Enums.PurchaseStatus.Valid).Average(x => x.Amount);

            if (purchase.Amount > avgOrderAmount * 5) return true;

            return false;
        }
    }
}
