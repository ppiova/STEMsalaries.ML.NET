using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.ML;

namespace STEMsalariesFunction
{
    public static class STEMsalariesPrediction
        
    {
        [FunctionName("STEMsalariesPredictionAPI")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var taxiTrip = JsonConvert.DeserializeObject<Salary>(requestBody);


            bool isLocal = false; // set to false before publishing to Azure

            MLContext mlContext = new MLContext();

            ITransformer mlModel = isLocal
                ? LoadModelLocal(mlContext)
                : LoadModelAzure(mlContext);

            var predictionEnginePool = mlContext.Model
                .CreatePredictionEngine<Salary, SalaryPrediction>(mlModel);
            
            var prediction = predictionEnginePool.Predict(taxiTrip);

            return new OkObjectResult(prediction);

        }
        //The same MLModelBuilder generated
        static readonly string MLModelFileName = "MLModel.zip";
        
        static readonly string MLModelPath =
            Path.Combine(Environment.CurrentDirectory, MLModelFileName);

        private static ITransformer LoadModelLocal(MLContext mlContext)
        {
            return mlContext.Model.Load(MLModelPath, out _);
        }

        private static ITransformer LoadModelAzure(MLContext mlContext)
        {
            // Only when publishing to Azure
            var fileInfo = new FileInfo(Assembly.GetExecutingAssembly().Location);
            var path = fileInfo.Directory.Parent.FullName;
            var mlModelPath = Path.Combine(path, MLModelFileName);

            return mlContext.Model.Load(mlModelPath, out _);
        }

    }
}
