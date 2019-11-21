namespace MachineLearning.Classification
{
	/// <summary>
	/// Interface that describes an classification dataset
	/// </summary>
	public interface IClassifier
	{
		/// <summary>
		/// Learn from a inputs matrix 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="output"></param>
		void Learn(double[,] input, int[] output);

		/// <summary>
		/// Predict the class of new value
		/// </summary>
		/// <param name="newValue"></param>
		/// <returns>int</returns>
		int Predict(double[] newValue);
	}
}
