using AutoMapper;
using Core;

namespace WebExplorer.Mapper
{
    public class CatalogProfile : Profile
    {
        public CatalogProfile()
        {
            CreateMap<CatalogInCatalog, ChildCatalogDTO>().ForMember(ccdto => ccdto.Name, cincRes => cincRes.MapFrom(cinc => cinc.ChildCatalog!.Name));
            CreateMap<Catalog, ChildCatalogDTO>();
            CreateMap<Catalog, CatalogDTO>();
        }
    }
}
