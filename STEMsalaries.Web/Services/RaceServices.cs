using STEMsalaries.Web.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace STEMsalaries.Web.Services
{
    public class RaceService : IRaceService
    {
        private readonly IEnumerable<Race> races;
        public RaceService(string pathFileRace)
        {
            string race = File.ReadAllText(pathFileRace);
            races = JsonSerializer.Deserialize<IEnumerable<Race>>(race);
        }
        public IEnumerable<Race> GetRaces() => races;
    }
}
