using AutoMapper;
using BLL.Abstractions;
using BLL.Abstractions.Model;
using Core;
using DAL;
using Microsoft.EntityFrameworkCore;

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

        public async Task<CatalogWithSubCatalogs> GetAllSubCatalogs(int id)
        {
            Catalog? catalog = _context.Catalogs.FirstOrDefault(catalog => catalog.Id == id);

            CatalogWithSubCatalogs result = new CatalogWithSubCatalogs { Catalog = _mapper.Map<CatalogDTO>(catalog) };

            if (catalog == null)
            {
                throw new ArgumentException("There is no info about this catalog.");
            }
            else if (!catalog.IsProcessed)
            {
                result.SubCatalogs = await GenerateChildrens(catalog);
            } 
            else
            {
                result.SubCatalogs = _mapper.Map<List<ChildCatalogDTO>>(_context.Catalogs.SelectMany(c => c.Catalogs));
            }

            return result;
        }

        public Task<CatalogWithSubCatalogs> GetFirstCatalogs()
        {
            List<Catalog> resultList = new List<Catalog>();

            EntryPoint localMachineEntryPoint = _context.EntryPoints.Include(ep => ep.Catalogs).FirstOrDefault(entry => entry.Name == "local")!;

            if (localMachineEntryPoint is null)
            {
                localMachineEntryPoint = new EntryPoint() { Name = "local" };

                foreach (string logicalDriver in Directory.GetLogicalDrives())
                {
                    resultList.Add(new Catalog() { Name = logicalDriver, IsProcessed = false });
                }

                localMachineEntryPoint.Catalogs = resultList;
                _context.Add(localMachineEntryPoint);

                _context.SaveChanges();
            } else
            {
                resultList = localMachineEntryPoint.Catalogs;
            }

            return Task.FromResult(new CatalogWithSubCatalogs { Catalog = null, SubCatalogs = _mapper.Map<List<ChildCatalogDTO>>(resultList) });
        }

        private async Task<List<ChildCatalogDTO>> GenerateChildrens(Catalog catalog)
        {
            string absolutePath = catalog.Name;

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
            parent.Catalogs.Add(child);
            child.Parent = parent;
        }

        //private string GetAbsolutePath(Catalog catalog)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    while (catalog is not null && catalog.FirstNodeOf is FirstNodeOf.None)
        //    {
        //        sb.AppendFormat(CultureInfo.CurrentCulture, "{0}/", catalog.Name);

        //        catalog = _context.CatalogsInCatalogs.FirstOrDefault(cinc => cinc.ChildId == catalog.Id)?.ParentCatalog;
        //    }

        //    return sb.ToString();
        //}
    }
}