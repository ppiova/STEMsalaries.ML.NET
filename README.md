### ML.NET &  Model Builder and (Bonus Track) ASP.NET App to consume the model. Enjoy it! 🤟 <br />
![](images/MLNETAPI.png)<br />
+ Create a App Console and add ML.NET from NugetPackage. I installed 1.7.0-preview.final (I love use and test previews versions 😁)<br /><br />
![](images/mldotnet-0.png)<br />

+ Create ML.NET context. MlContext is the staring point for all ML.NET operations. The MLContext is used for all aspects of creating and consuming an ML.NET model.<br />
+ Load Data: Machine learning uses known data (for example, training data) to find patterns in order to make predictions on new, unknown data.
The inputs for machine learning are called Features, which are the attributes used to make predictions. The output of machine learning is called the Label, which is the actual prediction.<br />
Data in ML.NET is represented as an IDataView, which is a flexible, efficient way of describing tabular data (for example, rows and columns). IDataView objects can contain numbers, text, booleans, vectors, and more. You can load data from files or from real-time streaming sources to an IDataView.
LoadFromTextFile allows you to load data from TXT, CSV, TSV, and other file formats.<br />
### Use the "DataSTEMSalary.csv" file for load data<br />
Tip: LoadFromEnumerable enables loading from in-memory collections, JSON/XML, relational and non-relational databases (for example, SQL, CosmosDB, MongoDB), and many other data sources. Link for more information: https://docs.microsoft.com/en-us/dotnet/machine-learning/how-to-guides/load-data-ml-net?WT.mc_id=dotnet-35129-website <br /><br />
![](images/mldotnet-001.png)<br /><br />
+ Transform data: In most cases, the data that you have available isn't suitable to be used directly to train a machine learning model. The raw data needs to be pre-processed using data transformations.<br />
+ Choose algorithm: When using machine learning and ML.NET, you must choose a machine learning task that goes along with your scenario. ML.NET offers over 30 algorithms (or trainers) for a variety of ML tasks:
![](images/mldotnet-002.png)<br /><br />
I used LbfgsPoissonRegression.<br /><br />
![](images/mldotnet-00.png)<br /><br />
+ Train model: The data transformations and algorithms you have specified are not executed until you call the Fit() method (because of ML.NET's lazy loading approach). This is when model training happens. <br />
An estimator takes in data, learns from the data, and creates a transformer. In the case of model training, the training data is the input, and the trained model is the output; the trained model is thus a transformer that turns the input Features from new data into output predictions.<br />
Link for more information aboul model training: https://docs.microsoft.com/en-us/dotnet/machine-learning/how-to-guides/train-machine-learning-model-ml-net?WT.mc_id=dotnet-35129-website <br /><br />

+ Evaluate model: ML.NET offers evaluators that assess the performance of your model on a variety of metrics. Link evaluation metrics: https://docs.microsoft.com/en-us/dotnet/machine-learning/resources/metrics?WT.mc_id=dotnet-35129-website<br /><br />
![](images/mldotnet-021.png)<br /><br />
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
### Use the same example with ML.NET Model Builder 💪<br />
ML.NET Model Builder is an intuitive graphical Visual Studio extension to build, train, and deploy custom machine learning models.
Model Builder uses automated machine learning (AutoML) to explore different machine learning algorithms and settings to help you find the one that best suits your scenario.<br /><br />
How to install ML.NET Model Builder Link: https://docs.microsoft.com/en-us/dotnet/machine-learning/how-to-guides/install-model-builder?tabs=visual-studio-2022 <br /> <br /> 
+ Create a App Console and add ML.NET from NugetPackage. I installed 1.7.0-preview.final. <br /> <br />
![](images/mldotnetModelBuilder-09.png)<br /><br />
+ Select Value prediction Scenario. <br /><br />
![](images/mldotnetModelBuilder-01.png)<br /><br />
+ Select Trainning Environment. <br /><br />
![](images/mldotnetModelBuilder-02.png)<br /><br />
+ Once you have chosen your scenario, Model Builder asks you to provide a dataset.<br /><br />
![](images/mldotnetModelBuilder-03.png)<br /><br />
+ Click the Advanced data options link and check the categorical columns. <br /><br />
![](images/mldotnetModelBuilder-10.png)<br /><br />
+ Train: The machine learning task used to train the annual salary prediction model in this tutorial is regression. 
![](images/mldotnetModelBuilder-04.png)<br /><br />
During the model training process, Model Builder trains separate models using different regression algorithms and settings to find the best-performing model for your dataset.<br /><br />
![](images/mldotnetModelBuilder-11.png)<br /><br />
+ Evaluate: The result of the training step will be one model which had the best performance. The evaluation step of the Model Builder tool, in the Best model section, will contain the algorithm used by the best performing model in the Model entry along with metrics for that model in RSquared. <br /><br />
![](images/mldotnetModelBuilder-12.png)<br /><br />
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
### Bonus Track!!! I share a web application in NET Core 3.1 using these models to predict the annual salary. 😊<br />
I was training with a little more time with model builder and then exported the model to use it in my ASP.NET application.<br />
![](images/ASPNETWebSalary.png)<br /><br />

### Keep creating and training models with ML.NET. <br />
### Happy Coding !!! 

-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
### DataSet: https://www.kaggle.com/jackogozaly/data-science-and-stem-salaries

## ML.NET Documentation
>https://docs.microsoft.com/en-us/dotnet/machine-learning/<br />
>https://dotnet.microsoft.com/learn/ml-dotnet/what-is-mldotnet<br />
>https://dotnet.microsoft.com/apps/machinelearning-ai/ml-dotnet/model-builder

