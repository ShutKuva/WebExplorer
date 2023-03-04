using BLL.Abstractions.Model;
using BLL.Enums;
using Core;

namespace WebExplorer.Models
{
    public class CatalogModel
    {
        public string CurrentEntryPointName { get; set; }
        public List<string> EntryPointNames { get; set; }
        public TypeOfCatalogService TypeOfCatalogService { get; set; }
        public CatalogWithSubCatalogs ChildCatalogDTOs { get; set; }
    }
}
