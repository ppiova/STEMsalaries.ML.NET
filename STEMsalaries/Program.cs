using Microsoft.ML;
using System.Linq;

namespace STEMsalaries
{
    class Program
    {
        //USE YOUR LOCAL PATH
        private static string dataset_file = @"E:\GitMLDotNet\STEMsalaries\DataSTEMSalary.csv";
        private static string ModelPath = @"E:\GitMLDotNet\STEMsalaries\STEMsalaries\MLModel.zip";

        static void Main(string[] args)
        {
            MLContext context = new();

            Console.WriteLine("Load data...");
            IDataView data = context.Data.LoadFromTextFile<Salary>(
                path: dataset_file,
                hasHeader: true,
                separatorChar: ',');

            var datosSplit = context.Data.TrainTestSplit(data, testFraction: 0.2);

            Console.WriteLine("Before training, wait a few seconds...");

            var pipeline = context.Transforms.Categorical.OneHotEncoding(outputColumnName: "CompanyEncoded", inputColumnName: "Company")
                   .Append(context.Transforms.Categorical.OneHotEncoding(outputColumnName: "TitleEncoded", inputColumnName: "Title"))
                   .Append(context.Transforms.Categorical.OneHotEncoding(outputColumnName: "GenderEncoded", inputColumnName: "Gender"))
                   .Append(context.Transforms.Categorical.OneHotEncoding(outputColumnName: "LocationEncoded", inputColumnName: "Location"))
                   .Append(context.Transforms.Categorical.OneHotEncoding(outputColumnName: "RaceEncoded", inputColumnName: "Race"))
                   .Append(context.Transforms.Categorical.OneHotEncoding(outputColumnName: "EducationEncoded", inputColumnName: "Education"))

                   .Append(context.Transforms
                       .Concatenate("Features", "YearsExperience", "YearsCompany", "CompanyEncoded", "TitleEncoded", "GenderEncoded", "LocationEncoded", "RaceEncoded", "EducationEncoded"))
                   .Append(context.Transforms.NormalizeMinMax("Features", "Features"))
                   .AppendCacheCheckpoint(context);
           
            var trainer = context.Regression.Trainers.LbfgsPoissonRegression();
            var pipelineTraining = pipeline.Append(trainer);

            Console.WriteLine("Training the model ...");
            Console.WriteLine($"Start: {DateTime.Now.ToShortTimeString()}");
            var model = pipelineTraining.Fit(datosSplit.TrainSet);
            Console.WriteLine($"Ends: {DateTime.Now.ToShortTimeString()}");

            Console.WriteLine("Predictions about training and testing ");
            IDataView TrainingPredictions = model.Transform(datosSplit.TrainSet);
            IDataView TestPredictions = model.Transform(datosSplit.TestSet);

            var TrainingMetrics = context.Regression
                .Evaluate(TrainingPredictions, labelColumnName: "Label", scoreColumnName: "Score");

            var TestMetrics = context.Regression
                .Evaluate(TestPredictions, labelColumnName: "Label", scoreColumnName: "Score");

            Console.WriteLine($"R-Squared Train set: {TrainingMetrics.RSquared}");
            Console.WriteLine($"R-Squared Test set: {TestMetrics.RSquared}");

            var crossValidation = context.Regression.CrossValidate(data, pipelineTraining, numberOfFolds: 5);
            var R_Squared_Avg = crossValidation.Select(modelo => modelo.Metrics.RSquared).Average();
            Console.WriteLine($"R-Squared Cross Validation: {R_Squared_Avg}");

            Console.WriteLine("Model saving...");
            context.Model.Save(model, data.Schema, ModelPath);

            Salary salaryExample = new Salary()
            {
                Company = "Apple",
                Location = "Seattle, WA",
                Title = "Software Engineer",
                YearsExperience = 6,
                YearsCompany = 2,
                Race = "Hispanic",
                Education = "Master's Degree",
                Gender = "Female"

            };

            //Load the trained model from .Zip file
            ITransformer mlModel = context.Model.Load(ModelPath, out var modelInputSchema);

            // Create prediction engine related to the loaded trained model
            var predEngine = context.Model.CreatePredictionEngine<Salary, SalaryPrediction>(mlModel);

            //Predict the result from prediction engine
            SalaryPrediction pResult = predEngine.Predict(salaryExample);

            Console.WriteLine("Using model to make single prediction -- Predicted Salary from sample data...\n\n");
            Console.WriteLine($"Company: {salaryExample.Company}");
            Console.WriteLine($"Location: {salaryExample.Location}");
            Console.WriteLine($"Title: {salaryExample.Title}");
            Console.WriteLine($"Years Experience: {salaryExample.YearsExperience}");
            Console.WriteLine($"Years Company: {salaryExample.YearsCompany}");
            Console.WriteLine($"Race: {salaryExample.Race}");
            Console.WriteLine($"Education: {salaryExample.Education}");
            Console.WriteLine($"Gender: {salaryExample.Gender}");
            Console.WriteLine($"\n\nPredicted Salary: {pResult.Score}\n\n");
            Console.WriteLine("=============== End of process, hit any key to finish ===============");
            Console.ReadKey();

        }

    }
}