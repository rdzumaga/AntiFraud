using AntiFraud.API.Models;
using Newtonsoft.Json;

namespace AntiFraud.API.Dto
{
    public class AddressDto
    {
        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("zipcode")]
        public string Zipcode { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        public Address ToDomainObject()
        {
            return new Address(Street, Zipcode, City, Country);
        }
    }

}
