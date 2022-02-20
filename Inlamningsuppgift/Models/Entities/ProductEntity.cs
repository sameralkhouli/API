using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inlamningsuppgift.Models.Entities
{
    public class ProductEntity
    {
        public ProductEntity(string productName, string disc, string price, string note)
        {
            ProductName = productName;
            Disc = disc;
            Price = price;
            Note = note;
        }

        public ProductEntity(int id, string productName, string disc, string price, string note)
        {
            Id = id;
            ProductName = productName;
            Disc = disc;
            Price = price;
            Note = note;
        }

        [Key]
        public int Id { get; set; }

        [Required, Column(TypeName = "nvarchar(50)")]
        public string ProductName { get; set; }

        [Required, Column(TypeName = "nvarchar(50)")]
        public string Disc { get; set; }

        [Required, Column(TypeName = "varchar(50)")]
        public string Price { get; set; }

        [Required, Column(TypeName = "nvarchar(50)")]
        public string Note { get; set; }
    }
}

