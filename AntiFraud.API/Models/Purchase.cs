using System.Collections.Generic;

namespace AntiFraud.API.Models
{
    public class Purchase
    {
        public string Email { get; private set; }

        public decimal Amount { get; private set; }

        public string Currency { get; private set; }

        public string Street { get; private set; }

        public string Zipcode { get; private set; }

        public string City { get; private set; }

        public string Country { get; private set; }

        public List<Product> Products { get; private set; }

        private Purchase()
        {

        }
    }
}
