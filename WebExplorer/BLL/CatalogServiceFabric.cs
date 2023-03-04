using AutoMapper;
using BLL.Abstractions;
using BLL.Enums;
using DAL;

namespace BLL
{
    public class CatalogServiceFabric
    {
        private readonly ExplorerContext _context;
        private readonly IMapper _mapper;

        public CatalogServiceFabric(ExplorerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ICatalogService Create(TypeOfCatalogService typeOfCatalogService, string entryPointName = "db")
        {
            switch (typeOfCatalogService)
            {
                case TypeOfCatalogService.FromLocalMachine:
                    return new LocalMachineCatalogService(_context, _mapper);
                case TypeOfCatalogService.FromDB:
                default:
                    return new DefaultCatalogService(_context, _mapper, entryPointName);
            }
        }
    }
}