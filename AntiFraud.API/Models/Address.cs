namespace AntiFraud.API.Models
{
    public class Address
    {
        public string Street { get; private set; }

        public string Zipcode { get; private set; }

        public string City { get; private set; }

        public string Country { get; private set; }

        private Address()
        {

        }

        public Address(string street, string zipcode, string city, string country)
        {
            Street = street;
            Zipcode = zipcode;
            City = city;
            Country = country;
        }
    }
}
