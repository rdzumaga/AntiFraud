using AntiFraud.API.Enums;
using System;
using System.Collections.Generic;

namespace AntiFraud.API.Models
{
    public class Purchase
    {
        public string Id { get; private set; }

        // SQLite EFCore does not support DateTimeOffset
        public DateTime Date { get; set; }

        public string Email { get; private set; }

        public decimal Amount { get; private set; }

        public string Currency { get; private set; }

        public Address Address { get; private set; }

        public List<Product> Products { get; private set; }

        public PurchaseStatus Status { get; private set; }

        private Purchase()
        {

        }

        public void SetValid()
        {
            Status = PurchaseStatus.Valid;
        }

        public void SetInvalid()
        {
            Status = PurchaseStatus.Invalid;
        }

        public Purchase(string email, decimal amount, string currency, Address address, List<Product> products)
        {
            Status = PurchaseStatus.New;
            Date = DateTime.UtcNow;
            Email = email;
            Amount = amount;
            Currency = currency;
            Address = address;
            Products = products;
        }
    }
}
