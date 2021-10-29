using STEMsalaries.Web.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace STEMsalaries.Web.Services
{
    public class EducationService: IEducationService
    {
        private readonly IEnumerable<Education> educations;
        public EducationService(string pathFileEducation)
        {
            string education = File.ReadAllText(pathFileEducation);
            educations = JsonSerializer.Deserialize<IEnumerable<Education>>(education);
        }
        public IEnumerable<Education> GetEducations() => educations;
    }
}
