using BLL;
using BLL.Abstractions;
using BLL.Abstractions.Model;
using BLL.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using WebExplorer.Models;

namespace WebExplorer.Controllers
{
    public class HomeController : Controller
    {
        private readonly CatalogServiceFabric _catalogServiceFabric;
        private readonly HelperFunctions _helperFunctions;

        public HomeController(CatalogServiceFabric catalogServiceFabric, HelperFunctions helperFunctions)
        {
            _catalogServiceFabric = catalogServiceFabric;
            _helperFunctions = helperFunctions;
        }

        public async Task<IActionResult> Index(string fromWhat, int? id)
        {
            CatalogModel model = new CatalogModel();

            model.CurrentEntryPointName = fromWhat;

            TypeOfCatalogService typeOfCatalogService = fromWhat.ToLower() switch
            {
                "db" => TypeOfCatalogService.FromDB,
                "local" => TypeOfCatalogService.FromLocalMachine,
                _ => TypeOfCatalogService.External
            };

            model.TypeOfCatalogService = typeOfCatalogService;

            ICatalogService catalogService = _catalogServiceFabric.Create(typeOfCatalogService, fromWhat);

            if (!id.HasValue)
            {
                model.ChildCatalogDTOs = await catalogService.GetFirstCatalogs();
            }
            else
            {
                model.ChildCatalogDTOs = await catalogService.GetAllSubCatalogs(id.Value);
            }

            model.EntryPointNames = await _helperFunctions.GetEntryPoints();

            return View("Index", model);
        }

        public async Task<IActionResult> UploadFile()
        {
            IFormFileCollection fileCollection = Request.Form.Files;

            string fromWhat = await _helperFunctions.ProcessFiles(fileCollection);

            return await Index(fromWhat, null);
        }

        public async Task<FileContentResult> DownloadFile(string fromWhat)
        {
            FileDTO file = await _helperFunctions.SerializeFile(fromWhat);
            return File(Encoding.UTF8.GetBytes(file.FileContent), "text/plain", file.Name);
        }

        public IActionResult LocalMachine()
        {
            return View();
        }
    }
}