using STEMsalaries.Web.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace STEMsalaries.Web.Services
{
    public class LocationService : InterfaceLocationService
    {
        private readonly IEnumerable<Location> locations;
        public LocationService(string pathFileLocation)
        {
            string location = File.ReadAllText(pathFileLocation);
            locations = JsonSerializer.Deserialize<IEnumerable<Location>>(location);
        }
        public IEnumerable<Location> GetLocations() => locations;
    }
}
