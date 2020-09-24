using AntiFraud.API.FraudCheckers;
using AntiFraud.API.Models;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AntiFraud.API.Jobs
{
    public class PurchaseCheckJob
    {
        private readonly DataContext _dataContext;
        private readonly IEnumerable<IFraudChecker> _fraudCheckers;

        public PurchaseCheckJob(DataContext dataContext, IEnumerable<IFraudChecker> fraudCheckers)
        {
            _dataContext = dataContext;
            _fraudCheckers = fraudCheckers;
        }

        public async Task CheckNewPurchasesAsync()
        {
            var newPurchases = await _dataContext.Purchases.Where(x => x.Status == Enums.PurchaseStatus.New).ToListAsync();
            foreach (var purchase in newPurchases)
            {
                BackgroundJob.Enqueue<PurchaseCheckJob>(x => x.CheckPurchaseAsync(purchase.Id));
            }
        }

        public async Task CheckPurchaseAsync(string purchaseId)
        {
            var purchase = await _dataContext.Purchases.FindAsync(purchaseId);
            if (purchase == null) throw new NullReferenceException();

            try
            {
                purchase.SetValid();

                foreach (var fraudChecker in _fraudCheckers)
                {
                    if (fraudChecker.IsFraud(purchase))
                    {
                        purchase.SetInvalid();
                        break;
                    }
                }
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
                // todo log error
            }            
        }
    }
}
