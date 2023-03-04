using AutoMapper;
using BLL.Abstractions;
using BLL.Abstractions.Model;
using Core;
using DAL;
using Microsoft.EntityFrameworkCore;

namespace BLL
{
    public class DefaultCatalogService : ICatalogService
    {
        private readonly ExplorerContext _context;
        private readonly IMapper _mapper;
        private readonly string _nameOfEntryPoint;

        public DefaultCatalogService(ExplorerContext context, IMapper mapper, string nameOfEntryPoint)
        {
            _context = context;
            _mapper = mapper;
            _nameOfEntryPoint = nameOfEntryPoint;
        }

        public Task<CatalogWithSubCatalogs> GetAllSubCatalogs(int id)
        {
            Catalog mainCatalog =  _context.Catalogs.Include(catalog => catalog.Catalogs).FirstOrDefault(catalog => catalog.Id == id);
            List<ChildCatalogDTO> subCatalogs = _mapper.Map<List<ChildCatalogDTO>>(mainCatalog.Catalogs);

            CatalogWithSubCatalogs result = new CatalogWithSubCatalogs { Catalog = _mapper.Map<CatalogDTO>(mainCatalog), SubCatalogs = subCatalogs };

            return Task.FromResult(result);
        }

        public Task<CatalogWithSubCatalogs> GetFirstCatalogs()
        {
            List<ChildCatalogDTO> subCatalogs = _mapper.Map<List<ChildCatalogDTO>>(_context.EntryPoints.Include(ep => ep.Catalogs).ThenInclude(c => c.Catalogs).FirstOrDefault(entry => entry.Name == _nameOfEntryPoint)?.Catalogs);

            return Task.FromResult(new CatalogWithSubCatalogs { Catalog = null, SubCatalogs = subCatalogs});
        }
    }
}