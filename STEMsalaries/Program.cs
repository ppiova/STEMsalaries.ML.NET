using Microsoft.ML;
using System.Linq;

namespace STEMsalaries
{
    class Program
    {
        //USE YOUR LOCAL PATH
        private static string dataset_file = @"E:\GitMLDotNet\STEMsalaries\DataSTEMSalary.csv";
        private static string rutaModelo = @"E:\GitMLDotNet\STEMsalaries\STEMsalaries\MLModel.zip";

        static void Main(string[] args)
        {
            MLContext contexto = new();

            Console.WriteLine("Load data...");
            IDataView data = contexto.Data.LoadFromTextFile<Salary>(
                path: dataset_file,
                hasHeader: true,
                separatorChar: ',');

            var datosSplit = contexto.Data.TrainTestSplit(data, testFraction: 0.2);

            Console.WriteLine("Before training, wait a few seconds...");

            var pipeline = contexto.Transforms.Categorical.OneHotEncoding(outputColumnName: "CompanyEncoded", inputColumnName: "Company")
                   .Append(contexto.Transforms.Categorical.OneHotEncoding(outputColumnName: "TitleEncoded", inputColumnName: "Title"))
                   .Append(contexto.Transforms.Categorical.OneHotEncoding(outputColumnName: "GenderEncoded", inputColumnName: "Gender"))
                   .Append(contexto.Transforms.Categorical.OneHotEncoding(outputColumnName: "LocationEncoded", inputColumnName: "Location"))
                   .Append(contexto.Transforms.Categorical.OneHotEncoding(outputColumnName: "RaceEncoded", inputColumnName: "Race"))
                   .Append(contexto.Transforms.Categorical.OneHotEncoding(outputColumnName: "EducationEncoded", inputColumnName: "Education"))

                   .Append(contexto.Transforms
                       .Concatenate("Features", "YearsExperience", "YearsCompany", "CompanyEncoded", "TitleEncoded", "GenderEncoded", "LocationEncoded", "RaceEncoded", "EducationEncoded"))
                   .Append(contexto.Transforms.NormalizeMinMax("Features", "Features"))
                   .AppendCacheCheckpoint(contexto);

            var trainer = contexto.Regression.Trainers.LbfgsPoissonRegression();
            var pipelineTraining = pipeline.Append(trainer);

            Console.WriteLine("Training the model ...");
            Console.WriteLine($"Start: {DateTime.Now.ToShortTimeString()}");
            var model = pipelineTraining.Fit(datosSplit.TrainSet);
            Console.WriteLine($"Ends: {DateTime.Now.ToShortTimeString()}");

            Console.WriteLine("Predictions about training and testing ");
            IDataView TrainingPredictions = model.Transform(datosSplit.TrainSet);
            IDataView TestPredictions = model.Transform(datosSplit.TestSet);

            var TrainingMetrics = contexto.Regression
                .Evaluate(TrainingPredictions, labelColumnName: "Label", scoreColumnName: "Score");

            var TestMetrics = contexto.Regression
                .Evaluate(TestPredictions, labelColumnName: "Label", scoreColumnName: "Score");

            Console.WriteLine($"R-Squared Set de Entrenamiento: {TrainingMetrics.RSquared}");
            Console.WriteLine($"R-Squared Set de Prueba: {TestMetrics.RSquared}");

            var crossValidation = contexto.Regression.CrossValidate(data, pipelineTraining, numberOfFolds: 5);
            var R_Squared_Avg = crossValidation.Select(modelo => modelo.Metrics.RSquared).Average();
            Console.WriteLine($"R-Squared Cross Validation: {R_Squared_Avg}");

            Console.WriteLine("Guardando el modelo...");
            contexto.Model.Save(model, data.Schema, rutaModelo);


        }

    }
}