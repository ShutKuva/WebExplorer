using AutoMapper;
using BLL.Abstractions;
using BLL.Enums;
using DAL;
using System.Runtime.InteropServices;

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

        public ICatalogService Create(TypeOfCatalogService typeOfCatalogService)
        {
            switch (typeOfCatalogService)
            {
                case TypeOfCatalogService.FromLocalMachine:
                    return new LocalMachineCatalogService(_context, _mapper);
                case TypeOfCatalogService.FromDB:
                default:
                    return new DBCatalogService(_context, _mapper);
            }
        }
    }
}