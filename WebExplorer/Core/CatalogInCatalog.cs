using System.ComponentModel.DataAnnotations.Schema;

namespace Core
{
    public class CatalogInCatalog
    {
        public int? ParentId { get; set; }
        public int? ChildId { get; set; }

        [ForeignKey("ParentId")]
        public Catalog? ParentCatalog { get; set; }
        [ForeignKey("ChildId")]
        public Catalog? ChildCatalog { get; set; }
    }
}