using Microsoft.ML;
using Microsoft.ML.Data;
using static Microsoft.ML.DataOperationsCatalog;


namespace Twitter_Sentiment_API.CreateMLModel
{
    class Program
    {
        private static readonly string DataPath = Path.Combine(Environment.CurrentDirectory,"CreateMLModel", "Dataset_labelled.txt");
        public static string Start(string inputText)
        {
            var mlContext = new MLContext();
            var splitDataView = LoadData(mlContext);
            var model = BuildAndTrainModel(mlContext, splitDataView.TrainSet);
            return ResultsFromModel(mlContext, model,inputText) + Evaluate(mlContext, model, splitDataView.TestSet);
        }

        private static TrainTestData LoadData(MLContext mlContext)
        {
            var dataView = mlContext.Data.LoadFromTextFile<SentimentAnalysis>(DataPath, hasHeader: false);
            var splitDataView = mlContext.Data.TrainTestSplit(dataView, testFraction: 0.2);
            return splitDataView;
        }

        private static ITransformer BuildAndTrainModel(MLContext mlContext, IDataView splitTrainSet)
        {
            var estimator = mlContext.Transforms.Text.FeaturizeText(outputColumnName: "Features", inputColumnName: nameof(SentimentAnalysis.SentimentText))
                .Append(mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: "Label", featureColumnName: "Features"));
            var model = estimator.Fit(splitTrainSet);
            return model;
        }

        private static string Evaluate(MLContext mlContext, ITransformer model, IDataView splitTestSet)
        {
            var predictions = model.Transform(splitTestSet);
            var metrics = mlContext.BinaryClassification.Evaluate(predictions, "Label");
            return $"\n\nModel Accuracy: {metrics.Accuracy:P2}";
        }

        private static string ResultsFromModel(MLContext mlContext, ITransformer model, string inputText)
        {
            PredictionEngine<SentimentAnalysis, SentimentPrediction> predictionFunction = mlContext.Model.CreatePredictionEngine<SentimentAnalysis, SentimentPrediction>(model);
            SentimentAnalysis sampleStatement = new SentimentAnalysis
            {
                SentimentText = inputText
            };
            var resultPrediction = predictionFunction.Predict(sampleStatement);
            return
               // $"Sentiment: {resultPrediction.SentimentText} | Prediction: {(Convert.ToBoolean(resultPrediction.Prediction) ? "Positive" : "Negative")} | Probability: {resultPrediction.Probability} ";
                $"Prediction: {(Convert.ToBoolean(resultPrediction.Prediction) ? "Positive" : "Negative")} | Probability: {resultPrediction.Probability} ";
        }
    }
}