using AntiFraud.API.Models;

namespace AntiFraud.API.FraudCheckers
{
    interface IFraudChecker
    {
        bool IsFraud(Purchase purchase);
    }
}
