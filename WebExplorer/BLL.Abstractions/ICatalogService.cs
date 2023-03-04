using BLL.Abstractions.Model;
using Core;

namespace BLL.Abstractions
{
    public interface ICatalogService
    {
        public Task<CatalogWithSubCatalogs> GetAllSubCatalogs(int id);
        public Task<CatalogWithSubCatalogs> GetFirstCatalogs();
    }
}