using Core.BaseClasses;

namespace Core
{
    public class EntryPoint : BaseEntity
    {
        public string Name { get; set; }
        public List<Catalog> Catalogs { get; set; }
    }
}