using MachineLearning.Classification;
using MachineLearning.IO;
using MachineLearning.Validation;

namespace MlKit
{
	class Program
	{
		static void Main(string[] args)
		{

			var reader = new DatasetReader(@"C:\datasets\iris.data", null);

			var knn = new KNN(3);
			
			IValidation holdoutValidation = new HoldoutValidation(0.8);

			holdoutValidation.Validate(knn, reader);
		}
	}
}
