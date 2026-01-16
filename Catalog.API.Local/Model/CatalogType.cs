using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Local.Model
{
    public class CatalogType
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public string Type { get; set; }
    }
}
