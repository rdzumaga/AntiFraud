using AntiFraud.API.Models;
using AntiFraud.API.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace AntiFraud.API.Helpers
{
    public class EmailHelper
    {
        private readonly SMTPOptions _options;
        private readonly ILogger<EmailHelper> _logger;
        private readonly DataContext _dataContext;

        public EmailHelper(IOptions<SMTPOptions> options, ILogger<EmailHelper> logger, DataContext dataContext)
        {
            _options = options.Value;
            _logger = logger;
            _dataContext = dataContext;
        }

        public async Task SendPurchaseProcessedEmail(string purchaseId)
        {
            var purchase = await _dataContext.Purchases.FindAsync(purchaseId);
            // todo add email send logic
            _logger.LogInformation($"EMAIL CLIENT: {purchase.Email}/{purchase.Id} processed! Result is {purchase.Status.ToString()}");
        }

        public async Task SendPurchaseNeedsAttentionEmail(string purchaseId)
        {
            var purchase = await _dataContext.Purchases.FindAsync(purchaseId);
            // todo add email send logic
            _logger.LogInformation($"EMAIL SUPPORT: {purchase.Email}/{purchase.Id} is a potential fraud!");
        }

        public async Task SendErrorEmail(string purchaseId)
        {
            var purchase = await _dataContext.Purchases.FindAsync(purchaseId);
            // todo add email send logic
            _logger.LogInformation($"EMAIL SUPPORT: {purchase.Email}/{purchase.Id} processing caused an error! check hangfire logs!");
        }
    }
}
