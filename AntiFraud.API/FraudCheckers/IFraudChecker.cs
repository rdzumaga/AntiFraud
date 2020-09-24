using AntiFraud.API.Models;

namespace AntiFraud.API.FraudCheckers
{
    public interface IFraudChecker
    {
        bool IsFraud(Purchase purchase);
    }
}
