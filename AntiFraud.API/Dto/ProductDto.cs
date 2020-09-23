using AntiFraud.API.Models;
using Newtonsoft.Json;

namespace AntiFraud.API.Dto
{
    public class ProductDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        public Product ToDomainObject()
        {
            return new Product(Name, Quantity);
        }
    }
}
