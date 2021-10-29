using STEMsalaries.Web.Models;
using System.Collections.Generic;

namespace STEMsalaries.Web.Services
{
    public interface IEducationService
    {
        IEnumerable<Education> GetEducations();
    }
}