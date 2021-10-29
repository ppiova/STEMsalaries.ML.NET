using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.ML;
using STEMsalaries.Web.MachineLearning;
using STEMsalaries.Web.Models;
using STEMsalaries.Web.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace STEMsalaries.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly IEnumerable<Company> companies;
        private readonly IEnumerable<Location> locations;
        private readonly IEnumerable<Title> titles;
        private readonly IEnumerable<Race> races;
        private readonly IEnumerable<Education> educations;
        

        public SelectList Companies { get; }
        public SelectList Locations { get; }
        public SelectList Titles { get; }
        public SelectList Races { get; }
        public SelectList Educations { get; }
        public SelectList YearsExperience { get; } = new SelectList(Enumerable.Range(0,30));
        public SelectList YearsCompany { get; } = new SelectList(Enumerable.Range(0, 30));

        public SelectList Genders { get; }
        public bool ShowSalary { get; private set; } = false;


        [BindProperty]
        public int IdCompany { get; set; }

        [BindProperty]
        public int IdLocation { get; set; }

        [BindProperty]
        public int IdTitle { get; set; }

        [BindProperty]
        public int IdRace { get; set; }

        [BindProperty]
        public int IdEducation { get; set; }

        [BindProperty]
        public string Gender { get; set; }

        [BindProperty]
        public Salary Salary { get; set; }

       


        public IndexModel(
            ILogger<IndexModel> logger,
            ICompanyService companyService,
            InterfaceLocationService locationService,
            ITitleService titleservice,
            IRaceService raceService,
            IEducationService educationService)
        {
            _logger = logger;

            companies = companyService.GetCompanies();
            Companies = new SelectList(companies, "Id", "CompanyName", default);
            locations = locationService.GetLocations();
            Locations = new SelectList(locations, "Id", "LocationName", default);
            titles = titleservice.GetTitles();
            Titles = new SelectList(titles, "Id", "TitleName", default);
            races = raceService.GetRaces();
            Races = new SelectList(races, "Id", "RaceName", default);
            educations = educationService.GetEducations();
            Educations = new SelectList(educations, "Id", "EducationName", default);

        }

        public void OnGet()
        {

        }
        //USE MLModelBuildermbconfig.zip or MLModel.zip
        private static string MLNetModelPath = Path.GetFullPath("wwwroot/data/MLModelBuildermbconfig.zip");

        public async Task OnPostAsync()
        {
            Company selectedCompany = companies.Where(x => x.Id == IdCompany).FirstOrDefault();
            Location selectedLocation= locations.Where(x => x.Id == IdLocation).FirstOrDefault();
            Title selectedTitle = titles.Where(x => x.Id == IdTitle).FirstOrDefault();
            Race selectedRace= races.Where(x => x.Id == IdRace).FirstOrDefault();
            Education selectedEducation = educations.Where(x => x.Id == IdEducation).FirstOrDefault();

            //Create MLContext
            MLContext mlContext = new MLContext();

            // Load Trained Model
            DataViewSchema predictionPipelineSchema;

            ITransformer predictionPipeline = mlContext.Model.Load(MLNetModelPath, out predictionPipelineSchema);

            // Create PredictionEngines
            var predictionEngine = mlContext.Model.CreatePredictionEngine<SalaryModel, SalaryPrediction>(predictionPipeline);


            SalaryModel salaryModel = new SalaryModel()
            {
                Company = selectedCompany.CompanyName.ToString(),
                Location = selectedLocation.LocationName.ToString(),
                Title = selectedTitle.TitleName.ToString(),
                YearsExperience = Salary.YearsExperience,
                YearsCompany = Salary.YearsCompany,
                Race = selectedRace.RaceName.ToString(),
                Education = selectedEducation.EducationName.ToString(),
                Gender = Gender.ToString()

            };
           
            // Get Prediction
            SalaryPrediction prediction = predictionEngine.Predict(salaryModel);

            Salary.AnualCompensation = prediction.Score;

            ShowSalary = true;
        }

    }
}
