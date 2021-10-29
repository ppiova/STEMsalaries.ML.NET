using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using STEMsalaries.Web.MachineLearning;
using STEMsalaries.Web.Services;
using System.IO;
using Microsoft.Extensions.ML;


namespace STEMsalaries.Web
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private static string modelPath;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            services.AddSingleton<ICompanyService, CompanyService>(
                   (options) =>
                   {
                       string pathCompany = Path.Join(_env.WebRootPath, "data", "companies.json");
                       return new CompanyService(pathCompany);
                   });
            services.AddSingleton<InterfaceLocationService, LocationService>(
                  (options) =>
                  {
                      string pathLocation = Path.Join(_env.WebRootPath, "data", "locations.json");
                      return new LocationService(pathLocation);
                  });
            services.AddSingleton<ITitleService, TitleService>(
                (options) =>
                {
                    string pathTitle = Path.Join(_env.WebRootPath, "data", "titles.json");
                    return new TitleService(pathTitle);
                });
            services.AddSingleton<IRaceService, RaceService>(
                (options) =>
                {
                    string pathRace = Path.Join(_env.WebRootPath, "data", "races.json");
                    return new RaceService(pathRace);
                });
            services.AddSingleton<IEducationService, EducationService>(
                 (options) =>
                 {
                     string pathEducation = Path.Join(_env.WebRootPath, "data", "educations.json");
                     return new EducationService(pathEducation);
                 });

         }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
