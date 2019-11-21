using MachineLearning.Classification;
using MachineLearning.Core;

namespace MachineLearning.Validation
{
	/// <summary>
	/// Interface that describes an classifier validation
	/// </summary>
	public interface IValidation
	{
		/// <summary>
		/// Compute the accuracy of validation method
		/// </summary>
		/// <param name="classifier"></param>
		/// <param name="csvReader"></param>
		double Validate(IClassifier classifier, Dataset csvReader);
	}
}
