using AntiFraud.API.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace AntiFraud.API.Dto
{
    public class PurchaseDto
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("address")]
        public AddressDto Address { get; set; }

        [JsonProperty("products")]
        public List<ProductDto> Products { get; set; }

        public Purchase ToDomainObject()
        {
            return new Purchase(Email, Amount, Currency, Address.ToDomainObject(), Products.Select(x => x.ToDomainObject()).ToList());
        }
    }
}
