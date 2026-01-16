using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Local.Model
{
    public class CatalogBrand
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public string Brand { get; set; }
    }
}
