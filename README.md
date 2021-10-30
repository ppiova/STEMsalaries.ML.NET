### ML.NET &  Model Builder and (Bonus Track) ASP.NET App to consume the model. Enjoy it! <br />
![](images/MLNETAPI.png)<br />
+ Create a App Console and add ML.NET from NugetPackage. I installed 1.7.0-preview.final (I love use and test previews versions 😁)
![](images/mldotnet-0.png)<br />

+ Create ML.NET context. MlContext is the staring point for all ML.NET operations. The MLContext is used for all aspects of creating and consuming an ML.NET model.<br />
+ Load Data: Machine learning uses known data (for example, training data) to find patterns in order to make predictions on new, unknown data.
The inputs for machine learning are called Features, which are the attributes used to make predictions. The output of machine learning is called the Label, which is the actual prediction.<br />
Data in ML.NET is represented as an IDataView, which is a flexible, efficient way of describing tabular data (for example, rows and columns). IDataView objects can contain numbers, text, booleans, vectors, and more. You can load data from files or from real-time streaming sources to an IDataView.
LoadFromTextFile allows you to load data from TXT, CSV, TSV, and other file formats.<br />
Tip: LoadFromEnumerable enables loading from in-memory collections, JSON/XML, relational and non-relational databases (for example, SQL, CosmosDB, MongoDB), and many other data sources. Link for more information: https://docs.microsoft.com/en-us/dotnet/machine-learning/how-to-guides/load-data-ml-net?WT.mc_id=dotnet-35129-website
![](images/mldotnet-001.png)<br /><br />
+ Transform data: In most cases, the data that you have available isn't suitable to be used directly to train a machine learning model. The raw data needs to be pre-processed using data transformations.<br />
+ Choose algorithm: When using machine learning and ML.NET, you must choose a machine learning task that goes along with your scenario. ML.NET offers over 30 algorithms (or trainers) for a variety of ML tasks:
![](images/mldotnet-002.png)<br /><br />
I used LbfgsPoissonRegression.
![](images/mldotnet-00.png)<br /><br />
+ Train model: The data transformations and algorithms you have specified are not executed until you call the Fit() method (because of ML.NET's lazy loading approach). This is when model training happens. <br />
An estimator takes in data, learns from the data, and creates a transformer. In the case of model training, the training data is the input, and the trained model is the output; the trained model is thus a transformer that turns the input Features from new data into output predictions.<br />
Link for more information aboul model training: https://docs.microsoft.com/en-us/dotnet/machine-learning/how-to-guides/train-machine-learning-model-ml-net?WT.mc_id=dotnet-35129-website <br /><br />

+ Evaluate model: ML.NET offers evaluators that assess the performance of your model on a variety of metrics. Link evaluation metrics: https://docs.microsoft.com/en-us/dotnet/machine-learning/resources/metrics?WT.mc_id=dotnet-35129-website<br />
![](images/mldotnet-021.png)<br /><br />









### DataSet: https://www.kaggle.com/muhammadshahzadkhan/dogvscat

## ML.NET Documentation
>https://docs.microsoft.com/en-us/dotnet/machine-learning/<br />
>https://dotnet.microsoft.com/learn/ml-dotnet/what-is-mldotnet<br />
>https://dotnet.microsoft.com/apps/machinelearning-ai/ml-dotnet/model-builder
