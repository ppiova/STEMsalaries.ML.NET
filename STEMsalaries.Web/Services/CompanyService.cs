using STEMsalaries.Web.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace STEMsalaries.Web.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IEnumerable<Company> companies;
        public CompanyService(string pathFileCompany)
        {
            string company = File.ReadAllText(pathFileCompany);
            companies = JsonSerializer.Deserialize<IEnumerable<Company>>(company);
        }
        public IEnumerable<Company> GetCompanies() => companies;

    }
}
