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
            var avgOrderAmount = _dataContext.Purchases.Average(x => x.Amount);

            if (purchase.Amount * 5 > avgOrderAmount) return true;

            return false;
        }
    }
}
