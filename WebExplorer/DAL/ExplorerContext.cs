using Core;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ExplorerContext : DbContext
    {
        public DbSet<Catalog> Catalogs { get; set; }
        //public DbSet<CatalogInCatalog> CatalogsInCatalogs { get; set; }
        public DbSet<EntryPoint> EntryPoints { get; set; }

        public ExplorerContext() { }
        public ExplorerContext(DbContextOptions<ExplorerContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=ZHEKA;Database=Explorer;Trusted_Connection=True;TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Catalog>().HasMany(catalog => catalog.Catalogs).WithOne(cinc => cinc.Parent).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<EntryPoint>().HasData(new EntryPoint (){ Id = 1, Name="db" });

            modelBuilder.Entity<Catalog>().HasData(
                new Catalog() { Id = 1, Name="Creating Digital Images", EntryPointId = 1},
                new Catalog() { Id = 2, Name="Resources", ParentId = 1},
                new Catalog() { Id = 3, Name="Evidence", ParentId=1},
                new Catalog() { Id = 4, Name = "Graphic products", ParentId = 1 },
                new Catalog() { Id = 5, Name = "Primary Sources", ParentId = 2 },
                new Catalog() { Id = 6, Name = "Secondary Sources", ParentId = 2 },
                new Catalog() { Id = 7, Name = "Process", ParentId = 4 },
                new Catalog() { Id = 8, Name = "Final Product", ParentId = 4 }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}