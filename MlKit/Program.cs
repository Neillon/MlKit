using MachineLearning.Classification;
using MachineLearning.Core;
using MachineLearning.Validation;
using System;

namespace MlKit
{
	/// <summary>
	/// Class to test the algorithims
	/// </summary>
	class Program
	{ 
		static void Main(string[] args)
		{

			var reader = new Dataset(@"C:\datasets\iris.csv", null);

			var knn = new KNN(3);
			
			IValidation holdoutValidation = new HoldoutValidation(0.8);

			var acr = holdoutValidation.Validate(knn, reader);

			Console.WriteLine($"{acr}");
		}
	}
}
