using JsonConverterApp.Core.Interfaces;
using JsonConverterApp.Core.Models;
using Newtonsoft.Json;
using System.Xml.Linq;
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JsonConverterApp.Infrastructure
{
    public class CompanyDetails : ICompanyDetails
    {
        private readonly IReadDataHandler _readFileHandler;
   
        public CompanyDetails(IReadDataHandler readFileHandler)
        {
            _readFileHandler = readFileHandler;

        }
        public Task<string> GetCompany(int id)
        {
            var file = ConverterAppHelper.GetPath(ConverterAppHelper.URI, $"{id}.xml");
            var res = _readFileHandler.ReadFileAsync(file).Result;  
            var doc = XDocument.Parse(res);
            var output = JsonConvert.SerializeObject(doc);
            return Task.FromResult(output); ;

        }

     
    }
}
