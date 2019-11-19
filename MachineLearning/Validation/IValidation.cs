using MachineLearning.Classification;
using MachineLearning.IO;

namespace MachineLearning.Validation
{
	public interface IValidation
	{
		void Validate(IClassifier classifier, DatasetReader csvReader);
	}
}
