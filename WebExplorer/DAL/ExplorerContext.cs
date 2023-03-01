using Core;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ExplorerContext : DbContext
    {
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<CatalogInCatalog> CatalogsInCatalogs { get; set; }

        public ExplorerContext() { }
        public ExplorerContext(DbContextOptions<ExplorerContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=ZHEKA;Database=Explorer;Trusted_Connection=True;TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CatalogInCatalog>().HasKey(cinc => new { cinc.ParentId, cinc.ChildId });
            modelBuilder.Entity<Catalog>().HasMany(catalog => catalog.Catalogs).WithOne(cinc => cinc.ParentCatalog).HasForeignKey(cinc => cinc.ParentId);
            modelBuilder.Entity<Catalog>().HasMany(catalog => catalog.Catalogs).WithOne(cinc => cinc.ParentCatalog).OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }
    }
}