using STEMsalaries.Web.Models;
using System.Collections.Generic;

namespace STEMsalaries.Web.Services
{
    public interface ITitleService
    {
        IEnumerable<Title> GetTitles();
    }
}