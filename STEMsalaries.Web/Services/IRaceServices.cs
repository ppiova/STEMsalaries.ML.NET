using STEMsalaries.Web.Models;
using System.Collections.Generic;

namespace STEMsalaries.Web.Services
{
    public interface IRaceService
    {
        IEnumerable<Race> GetRaces();
    }
}