using MachineLearning.Classification;
using MachineLearning.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MachineLearning.Validation
{
	public class HoldoutValidation : IValidation
	{
		private double _percentage;
		private double[,] _trainingDataset;
		private int[] _trainingOutput;
		private double[,] _testDataSet;
		private int[] _testOutput;

		public HoldoutValidation() { _percentage = 0.8; }

		public HoldoutValidation(double? percentageToTrain = 0.8)
		{
			var hasPercentage = percentageToTrain.HasValue && percentageToTrain > 1;
			_percentage = hasPercentage ? percentageToTrain.Value : 0.8;
		}

		private void SplitDataset(double[,] input, int[] output)
		{
			var classes = output.Distinct();

			#region Join dataset with output (group)
			var dataset = new double[input.GetLength(0), input.GetLength(1) + 1];
			for (int i = 0; i < dataset.GetLength(0); i++)
			{
				for (int j = 0; j < dataset.GetLength(1) - 1; j++)
					dataset[i, j] = input[i, j];

				dataset[i, dataset.GetLength(1) - 1] = output[i];
			}

			#endregion

			#region Get the shape of each group and creates a dictionary with the quantities to split
			var shapes = GetShapes(dataset, classes.ToArray());
			var quantitiesToSplit = shapes
				.Select(p => Convert.ToInt32(p.Value * _percentage));

			#endregion

			// mount the training and test dataset
			SplitDatasetIntoTrainingAndTest(dataset, quantitiesToSplit.ToArray()); 
		}

		private void SplitDatasetIntoTrainingAndTest(double[,] dataset, int[] quantitiesToSplit)
		{
			#region Initialize the grouped datasets
			var datasetByGroup = new Dictionary<int, double[,]>();
			for (int i = 0; i < quantitiesToSplit.Count(); i++)
				datasetByGroup.Add(i, new double[quantitiesToSplit[i], dataset.GetLength(1) - 1]);

			#endregion

			#region Get the dictionary of complete datasets
			var splitedDatasets = new Dictionary<int, double[,]>();
			var splitedOutputs = new Dictionary<int, int[]>();

			for (int group = 0; group < quantitiesToSplit.Count(); group++)
			{
				var m = Convert.ToInt32(datasetByGroup[group].GetLength(0) / _percentage);
				splitedDatasets.Add(group, new double[m, dataset.GetLength(1) - 1]);
				splitedOutputs.Add(group, new int[m]);
				var iCount = 0;

				for (int i = 0; i < dataset.GetLength(0); i++)
				{
					if (group == dataset[i, dataset.GetLength(1) - 1])
					{
						for (int j = 0; j < dataset.GetLength(1); j++)
							if (j != dataset.GetLength(1) - 1)
								splitedDatasets[group][iCount, j] = dataset[i, j];
							else
								splitedOutputs[group][iCount] = Convert.ToInt32(dataset[i, j]);

						iCount++;
					}
				}
			}
			#endregion

			#region Alocate the correct percentage of data for each dataset
			var totalLines = splitedDatasets.Select(p => p.Value.GetLength(0)).Sum();
			var diffLines = totalLines - Convert.ToInt32(totalLines * _percentage);

			_trainingDataset = new double[totalLines - diffLines, splitedDatasets[0].GetLength(1)];
			_trainingOutput = new int[totalLines - diffLines];
			_testDataSet = new double[diffLines, splitedDatasets[0].GetLength(1)];
			_testOutput = new int[diffLines];

			var countX = 0;
			var countY = 0;
			for (int group = 0; group < splitedDatasets.Count(); group++)
			{
				for (int i = 0; i < splitedDatasets[group].GetLength(0); i++)
				{
					for (int j = 0; j < splitedDatasets[group].GetLength(1); j++)
					{
						if ((Convert.ToInt32(splitedDatasets[group].GetLength(0) * _percentage)) > i)
						{
							_trainingDataset[countX, j] = splitedDatasets[group][i, j];
							_trainingOutput[countX] = splitedOutputs[group][i];
						}
						else
						{
							_testDataSet[countY, j] = splitedDatasets[group][i, j];
							_testOutput[countY] = splitedOutputs[group][i];
						}
					}

					if ((Convert.ToInt32(splitedDatasets[group].GetLength(0) * _percentage)) > i)
						countX++;
					else
						countY++;
				}
			}

			#endregion
		}

		private Dictionary<int, int> GetShapes(double[,] dataset, int[] groups)
		{
			var shapes = new Dictionary<int, int>();

			var n = dataset.GetLength(1);
			foreach (var groupValue in groups)
			{
				var m = 0; // number of columns
				for (int i = 0; i < dataset.GetLength(0); i++)
				{
					if (dataset[i, n - 1] == groupValue)
						m++;
				}

				shapes.Add(groupValue, m);
			}

			return shapes;
		}

		public double Validate(IClassifier classifier, Dataset csvReader)
		{
			SplitDataset(csvReader.Input, csvReader.Output);

			classifier.Learn(_trainingDataset, _trainingOutput);

			var predictedValues = new int[_testOutput.Length];
			for (int i = 0; i < _testDataSet.GetLength(0); i++)
			{
				for (int j = 0; j < _testDataSet.GetLength(1); j++)
				{
					var prediction = classifier.Predict(GetLineOfTestDataset(i));
					predictedValues[i] = prediction;
				}
			}

			var numberOfTrues = 0;
			for (int i = 0; i < _testOutput.Length; i++)
				if (predictedValues[i] == _testOutput[i])
					numberOfTrues++;

			double accuracy = Convert.ToDouble(numberOfTrues) / Convert.ToDouble(_testOutput.Length);

			return accuracy;
		}

		private double[] GetLineOfTestDataset(int line)
		{
			var lineValues = new double[_testDataSet.GetLength(1)];

			for (int j = 0; j < _testDataSet.GetLength(1); j++)
				lineValues[j] = _testDataSet[line, j];

			return lineValues;
		}
	}
}
