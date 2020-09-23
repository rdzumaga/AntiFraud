using AntiFraud.API.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AntiFraud.API.Dto
{
    public class PurchaseDto : IValidatableObject
    {
        public string Id { get; set; }

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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResult = new List<ValidationResult>();
            if (string.IsNullOrWhiteSpace(Email)) validationResult.Add(new ValidationResult("Email cannot be empty", new[] { nameof(Email) }));
            else
            {
                var emailValid = new EmailAddressAttribute().IsValid(Email);
                if (!emailValid) validationResult.Add(new ValidationResult("Email is invalid", new[] { nameof(Email) }));
            }

            return validationResult;
        }
    }

    public static class PurchaseExtensions
    {
        public static PurchaseDto ToDto(this Purchase purchase)
        {
            var dto = new PurchaseDto()
            {
                Id = purchase.Id,
                Email = purchase.Email,
                Amount = purchase.Amount,
                Currency = purchase.Currency,
                Address = new AddressDto()
                {
                    City = purchase.Address.City,
                    Country = purchase.Address.Country,
                    Street = purchase.Address.Street,
                    Zipcode = purchase.Address.Zipcode
                },
                Products = purchase.Products.Select(x => new ProductDto()
                {
                    Name = x.Name,
                    Quantity = x.Quantity
                }).ToList()
            };

            return dto;
        }
    }
}
