using AutoMapper;
using BLL.Abstractions;
using Core;
using Core.Enums;
using DAL;
using System.Globalization;
using System.Text;

namespace BLL
{
    public class LocalMachineCatalogService : ICatalogService
    {
        private readonly ExplorerContext _context;
        private readonly IMapper _mapper;

        public LocalMachineCatalogService(ExplorerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ChildCatalogDTO>> GetAllSubCatalogs(int id)
        {
            Catalog? catalog = _context.Catalogs.FirstOrDefault(catalog => catalog.Id == id);

            if (catalog == null)
            {
                throw new ArgumentException("There is no info about this catalog.");
            }
            else if (!catalog.IsProcessed)
            {
                return await GenerateChildrens(catalog);
            } 
            else
            {
                return _mapper.Map<List<ChildCatalogDTO>>(_context.Catalogs.SelectMany(c => c.Catalogs, (_, cincs) => cincs.ChildCatalog));
            }
        }

        private async Task<List<ChildCatalogDTO>> GenerateChildrens(Catalog catalog)
        {
            string absolutePath = GetAbsolutePath(catalog);

            string[] catalogs = Directory.GetDirectories(absolutePath, "*", SearchOption.TopDirectoryOnly);

            List<Catalog> childCatalogs = new List<Catalog>();

            foreach (string name in catalogs)
            {
                Catalog child = new Catalog() { Name = name, IsProcessed = false };
                childCatalogs.Add(child);
                GenerateChildren(catalog, child);
            }

            await _context.SaveChangesAsync();

            return _mapper.Map<List<ChildCatalogDTO>>(childCatalogs);
        }

        private void GenerateChildren(Catalog parent, Catalog child)
        {
            CatalogInCatalog cinc = new CatalogInCatalog { ChildCatalog = child, ParentId = parent.Id };
            _context.Add(cinc);
        }

        private string GetAbsolutePath(Catalog catalog)
        {
            StringBuilder sb = new StringBuilder();

            while (catalog is not null && catalog.FirstNodeOf is FirstNodeOf.None)
            {
                sb.AppendFormat(CultureInfo.CurrentCulture, "{0}/", catalog.Name);

                catalog = _context.CatalogsInCatalogs.FirstOrDefault(cinc => cinc.ChildId == catalog.Id)?.ParentCatalog;
            }

            return sb.ToString();
        }
    }
}