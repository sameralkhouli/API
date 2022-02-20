namespace Inlamningsuppgift.Models
{
    public class ProductModel
    {
        public ProductModel(int id, string productName, string disc, string price)
        {
            Id = id;
            ProductName = productName;
            Disc = disc;
            Price = price;
        }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Disc { get; set; }
        public string Price { get; set; }
    }
}
