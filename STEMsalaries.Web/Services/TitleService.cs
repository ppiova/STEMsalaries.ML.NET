using STEMsalaries.Web.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace STEMsalaries.Web.Services
{
    public class TitleService : ITitleService
    {

        private readonly IEnumerable<Title> titles;
        public TitleService(string pathFileTitle)
        {
            string title = File.ReadAllText(pathFileTitle);
            titles = JsonSerializer.Deserialize<IEnumerable<Title>>(title);
        }
        public IEnumerable<Title> GetTitles() => titles;


    }
}
