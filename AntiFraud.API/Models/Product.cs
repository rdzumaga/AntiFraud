namespace AntiFraud.API.Models
{
    public class Product
    {
        public string Name { get; private set; }

        public int Quantity { get; private set; }

        private Product()
        {

        }

        public Product(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }
    }
}
