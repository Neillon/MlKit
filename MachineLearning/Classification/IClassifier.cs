namespace MachineLearning.Classification
{
	public interface IClassifier
	{
		void Learn(double[,] input, int[] output);
		int Predict(double[] newValue);
	}
}
