using JsonConverterApp.Core.Interfaces;
using JsonConverterApp.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace JsonConverterApp.Controllers
{
    [Route("/xml-api/{id}.xml")]
    public class ConverterController : ControllerBase
    {
        private readonly ICompanyDetails _companyDetails;
        public ConverterController(ICompanyDetails companyDetails)
        {
            _companyDetails = companyDetails;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var response = _companyDetails.GetCompany(id);
            return Ok(response.Result);
        }
    }
}
