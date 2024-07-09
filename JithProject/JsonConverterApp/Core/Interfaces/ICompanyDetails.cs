using JsonConverterApp.Core.Models;

namespace JsonConverterApp.Core.Interfaces
{
    public interface ICompanyDetails
    {
        Task<string> GetCompany(int id);
    }
}
