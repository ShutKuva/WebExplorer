using AutoMapper;
using BLL.Abstractions.Model;
using Core;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace BLL
{
    public class HelperFunctions
    {
        private readonly IMapper _mapper;
        private readonly ExplorerContext _context;

        public HelperFunctions(IMapper mapper, ExplorerContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public Task<List<string>> GetEntryPoints()
        {
            return Task.FromResult(_context.EntryPoints.Select(ep => ep.Name).ToList());
        }

        public async Task<string> ProcessFile(IFormFile file)
        {
            using MemoryStream memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            string contentOfFile = Encoding.UTF8.GetString(memoryStream.ToArray());
            EntryPoint fileEntryPoint = JsonConvert.DeserializeObject<EntryPoint>(contentOfFile);
            fileEntryPoint.Name = file.FileName;

            _context.Add(fileEntryPoint);
            _context.SaveChanges();

            return fileEntryPoint.Name;
        }

        public async Task<string> ProcessFiles(IFormFileCollection formFileCollection)
        {
            string result = String.Empty;
            foreach (IFormFile file in formFileCollection)
            {
                result = await ProcessFile(file);
            }

            return result;
        }

        public Task<FileDTO> SerializeFile(string entryPointName)
        {
            EntryPoint entryPoint = _context.EntryPoints.Include(ep => ep.Catalogs).ThenInclude(catalog => catalog.Catalogs).FirstOrDefault(ep => ep.Name == entryPointName);

            string serializedEntryPoint = JsonConvert.SerializeObject(entryPoint, Formatting.None, new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Task.FromResult(new FileDTO { Name = entryPointName + ".json", FileContent = serializedEntryPoint});
        }
    }
}