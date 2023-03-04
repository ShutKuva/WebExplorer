using Core;

namespace BLL.Abstractions.Model
{
    public class CatalogWithSubCatalogs
    {
        public CatalogDTO? Catalog { get; set; }
        public List<ChildCatalogDTO> SubCatalogs { get; set; }
    }
}