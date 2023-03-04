using Core.BaseClasses;

namespace Core
{
    public class Catalog : BaseEntity
    {
        public string Name { get; set; } = String.Empty;
        public bool IsProcessed { get; set; } = true;
        public int? EntryPointId { get; set; }
        public EntryPoint EntryPoint { get; set; }

        public int? ParentId { get; set; }
        public Catalog? Parent { get; set; }

        public List<Catalog> Catalogs { get; set; } = new List<Catalog>();
    }
}