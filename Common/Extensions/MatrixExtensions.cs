using Common.Exceptions;

namespace Common.Extensions
{
	/// <summary>
	/// Provide methods to manipulate matrix
	/// </summary>
	public static class MatrixExtensions
	{
		/// <summary>
		/// Get a row from a double matrix
		/// </summary>
		/// <param name="matrix"></param>
		/// <param name="index"></param>
		/// <returns>double[]</returns>
		public static double[] GetRow(this double[,] matrix, int index)
		{
			var featuresArray = new double[matrix.GetLength(1)];
			for (int i = 0; i < matrix.GetLength(1); i++)
				featuresArray[i] = matrix[index, i];

			return featuresArray;
		}

		/// <summary>
		/// Get a column from a double matrix
		/// </summary>
		/// <param name="matrix"></param>
		/// <param name="index"></param>
		/// <returns>double[]</returns>
		public static double[] GetColumn(this double[,] matrix, int index)
		{
			var featuresArray = new double[matrix.GetLength(1)];
			for (int i = 0; i < matrix.GetLength(0); i++)
				featuresArray[i] = matrix[i, index];

			return featuresArray;
		}

		/// <summary>
		/// Add a row with values parameter in the end of matrix <br />
		/// If it`s `not provided, the values will be zeros
		/// </summary>
		/// <param name="matrix"></param>
		/// <returns>double[,]</returns>
		public static double[,] AddRow(this double[,] matrix)
		{
			var values = VectorExtensions.DecimalVectorOfZeros(matrix.GetLength(1));
			var output = new double[matrix.GetLength(0) + 1, matrix.GetLength(1)];

			for (int i = 0; i < matrix.GetLength(1); i++)
				output[matrix.GetLength(0), i] = values[i];

			return output;
		}

		/// <summary>
		/// Add a row at the end with parameter values in the end of matrix <br />
		/// If values has a bigger length than matrix, the returned matrix has the n* first elements of provided values, where n* if the number of rows of the matrix
		/// </summary>
		/// <param name="matrix"></param>
		/// <returns>double[,]</returns>
		public static double[,] AddRow(this double[,] matrix, double[] values)
		{
			if (values.Length < matrix.GetLength(1))
				throw new InvalidParameterLengthException("values", matrix.GetLength(1), values.Length);

			var output = new double[matrix.GetLength(0) + 1, matrix.GetLength(1)];

			for (int i = 0; i < matrix.GetLength(1); i++)
				output[matrix.GetLength(0), i] = values[i];

			return output;
		}

		/// <summary>
		/// Add a column with values parameter in the end of matrix <br />
		/// If it`s `not provided, the values will be zeros
		/// </summary>
		/// <param name="matrix"></param>
		/// <returns>double[,]</returns>
		public static double[,] AddColumn(this double[,] matrix)
		{
			var values = VectorExtensions.DecimalVectorOfZeros(matrix.GetLength(1));
			var output = new double[matrix.GetLength(0), matrix.GetLength(1) + 1];

			for (int i = 0; i < matrix.GetLength(0); i++)
				output[i, matrix.GetLength(1)] = values[i];

			return output;
		}

		/// <summary>
		/// Add a column with parameter values in the end of matrix <br />
		/// If values has a bigger length than matrix, the returned matrix has the n* first elements of provided values, where n* if the number of rows of the matrix
		/// </summary>
		/// <param name="matrix"></param>
		/// <returns>double[,]</returns>
		public static double[,] AddColumn(this double[,] matrix, double[] values)
		{
			if (values.Length < matrix.GetLength(0))
				throw new InvalidParameterLengthException("values", matrix.GetLength(0), values.Length);

			var output = new double[matrix.GetLength(0), matrix.GetLength(1) + 1];

			for (int i = 0; i < matrix.GetLength(0); i++)
				output[i, matrix.GetLength(1)] = values[i];

			return output;
		}

	}
}
