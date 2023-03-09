using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MockProject.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Review { get; set; }
        public double Price { get; set; }
        public int RemainingQuantity { get; set; }
        public ICollection<Cart> Cart { get; set; }
    }
}
