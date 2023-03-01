using Core;

namespace BLL.Abstractions
{
    public interface ICatalogService
    {
        public Task<List<ChildCatalogDTO>> GetAllSubCatalogs(int id);

    }
}