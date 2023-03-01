using AutoMapper;
using BLL.Abstractions;
using Core;
using DAL;

namespace BLL
{
    public class DBCatalogService : ICatalogService
    {
        private readonly ExplorerContext _context;
        private readonly IMapper _mapper;

        public DBCatalogService(ExplorerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<List<ChildCatalogDTO>> GetAllSubCatalogs(int id)
        {
            return Task.FromResult(_mapper.Map<List<ChildCatalogDTO>>(_context.CatalogsInCatalogs.Where(cinc => cinc.ParentCatalog.Id == id)));
        }
    }
}