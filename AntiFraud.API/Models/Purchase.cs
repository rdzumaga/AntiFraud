using System;
using System.Collections.Generic;

namespace AntiFraud.API.Models
{
    public class Purchase
    {
        public string Id { get; private set; }

        // SQLite does not support DateTimeOffset
        public DateTime Date { get; set; }

        public string Email { get; private set; }

        public decimal Amount { get; private set; }

        public string Currency { get; private set; }

        public Address Address { get; private set; }

        public List<Product> Products { get; private set; }

        private Purchase()
        {

        }

        public Purchase(string email, decimal amount, string currency, Address address, List<Product> products)
        {
            Date = DateTime.UtcNow;
            Email = email;
            Amount = amount;
            Currency = currency;
            Address = address;
            Products = products;
        }
    }
}
