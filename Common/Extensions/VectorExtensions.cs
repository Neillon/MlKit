using System.Linq;

namespace Common.Extensions
{
	/// <summary>
	/// Provide methods to manipulate arrays
	/// </summary>
	public static class VectorExtensions
	{
		/// <summary>
		/// Creates an array of length x with zeros in all positions
		/// </summary>
		/// <param name="length"></param>
		/// <returns>int[]</returns>
		public static int[] IntegerVectorOfZeros(int length)
		{
			var output = new int[length];
			for (int i = 0; i < length; i++)
				output[i] = 0;

			return output;
		}

		/// <summary>
		/// Creates an array of length x with zeros in all positions
		/// </summary>
		/// <param name="length"></param>
		/// <returns>double[]</returns>
		public static double[] DecimalVectorOfZeros(int length)
		{
			var output = new double[length];
			for (int i = 0; i < length; i++)
				output[i] = 0.0;

			return output;
		}

		/// <summary>
		/// Creates an array of length x with ones in all positions
		/// </summary>
		/// <param name="length"></param>
		/// <returns>int[]</returns>
		public static int[] IntegerVectorOfOnes(int length)
		{
			var output = new int[length];
			for (int i = 0; i < length; i++)
				output[i] = 1;

			return output;
		}

		/// <summary>
		/// Creates an array of length x with ones in all positions
		/// </summary>
		/// <param name="length"></param>
		/// <returns>double[]</returns>
		public static double[] DecimalVectorOfOnes(int length)
		{
			var output = new double[length];
			for (int i = 0; i < length; i++)
				output[i] = 1.0;

			return output;
		}

		/// <summary>
		/// Categorize an array by your value <br />
		/// Use for classification outputs that requires the output to be a int array
		/// </summary>
		/// <param name="classes"></param>
		/// <returns>int[]</returns>
		public static int[] Categorize(this string[] classes)
		{
			// transform the values lowercased strings without leading and trailing whit-spaces
			// Get the distinct values of a string[]
			var distinctClasses = classes
				.Select(p => p.ToLower().Trim())
				.Distinct()
				.ToArray();

			// quickly return the one class vector (Array of zeros)
			if (distinctClasses.Length <= 1)
				return IntegerVectorOfZeros(classes.Length);

			#region Transform the string classes into numeral classes (Index of array)
			var output = new int[classes.Length];
			for (int i = 0; i < classes.Length; i++)
			{
				for (int j = 0; j < distinctClasses.Length; j++)
					if (distinctClasses[j].Equals(classes[i]))
					{
						output[i] = j;
						break;
					}
			}
			#endregion

			return output;
		}
	}
}
