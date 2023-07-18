using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleAPI.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; } = String.Empty;

        [StringLength(200)]
        public string Description { get; set; } = String.Empty;
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
