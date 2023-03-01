using Core.Enums;

namespace Core
{
    public class Catalog
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public FirstNodeOf FirstNodeOf { get; set; } = FirstNodeOf.None;
        public bool IsProcessed { get; set; } = true;
        public List<CatalogInCatalog> Catalogs { get; set; } = new List<CatalogInCatalog>();
    }
}