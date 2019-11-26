using Common.Exceptions;

namespace Common.Extensions
{
	/// <summary>
	/// Provide methods to manipulate matrix
	/// </summary>
	public static class MatrixExtensions
	{
		/// <summary>
		/// Get a row from a double dataset
		/// </summary>
		/// <param name="input"></param>
		/// <param name="index"></param>
		/// <returns>double[]</returns>
		public static double[] GetRow(this double[,] input, int index)
		{
			var featuresArray = new double[input.GetLength(1)];
			for (int i = 0; i < input.GetLength(1); i++)
				featuresArray[i] = input[index, i];

			return featuresArray;
		}

		/// <summary>
		/// Get a column from a double matrix
		/// </summary>
		/// <param name="input"></param>
		/// <param name="index"></param>
		/// <returns>double[]</returns>
		public static double[] GetColumn(this double[,] input, int index)
		{
			var featuresArray = new double[input.GetLength(1)];
			for (int i = 0; i < input.GetLength(0); i++)
				featuresArray[i] = input[i, index];

			return featuresArray;
		}

		/// <summary>
		/// Add a row with values parameter in the end of dataset <br />
		/// If it`s `not provided, the values will be zeros
		/// </summary>
		/// <param name="input"></param>
		/// <returns>double[,]</returns>
		public static double[,] AddRow(this double[,] input)
		{
			var values = VectorExtensions.DecimalVectorOfZeros(input.GetLength(1));
			var output = new double[input.GetLength(0) + 1, input.GetLength(1)];

			for (int i = 0; i < input.GetLength(1); i++)
				output[input.GetLength(0), i] = values[i];

			return output;
		}

		/// <summary>
		/// Add a row at the end with parameter values in the end of dataset <br />
		/// If values has a bigger length than matrix, the returned matrix has the n* first elements of provided values, where n* if the number of rows of the matrix
		/// </summary>
		/// <param name="input"></param>
		/// <returns>double[,]</returns>
		public static double[,] AddRow(this double[,] input, double[] values)
		{
			if (values.Length < input.GetLength(1))
				throw new InvalidParameterLengthException("values", input.GetLength(1), values.Length);

			var output = new double[input.GetLength(0) + 1, input.GetLength(1)];

			for (int i = 0; i < input.GetLength(1); i++)
				output[input.GetLength(0), i] = values[i];

			return output;
		}

		/// <summary>
		/// Remove a row from dataset
		/// </summary>
		/// <param name="input"></param>
		/// <param name="index"></param>
		/// <returns>double[,]</returns>
		public static double[,] RemoveRow(this double[,] input, int index)
		{
			var matrix = new double[input.GetLength(0) - 1, input.GetLength(1)];
			var iAux = 0;

			for (int i = 0; i < input.GetLength(0); i++)
			{
				if (i != index)
				{
					for (int j = 0; j < input.GetLength(1); j++)
						matrix[iAux, j] = input[i, j];
					iAux++;
				}
				else
					iAux--;
			}

			return matrix;
		}

		/// <summary>
		/// Add a column with values parameter in the end of dataset<br />
		/// If it`s `not provided, the values will be zeros
		/// </summary>
		/// <param name="input"></param>
		/// <returns>double[,]</returns>
		public static double[,] AddColumn(this double[,] input)
		{
			var values = VectorExtensions.DecimalVectorOfZeros(input.GetLength(1));
			var output = new double[input.GetLength(0), input.GetLength(1) + 1];

			for (int i = 0; i < input.GetLength(0); i++)
				output[i, input.GetLength(1)] = values[i];

			return output;
		}

		/// <summary>
		/// Add a column with parameter values in the end of matrix <br />
		/// If values has a bigger length than matrix, the returned matrix has the n* first elements of provided values, where n* if the number of rows of the matrix
		/// </summary>
		/// <param name="input"></param>
		/// <returns>double[,]</returns>
		public static double[,] AddColumn(this double[,] input, double[] values)
		{
			if (values.Length < input.GetLength(0))
				throw new InvalidParameterLengthException("values", input.GetLength(0), values.Length);

			var output = new double[input.GetLength(0), input.GetLength(1) + 1];

			for (int i = 0; i < input.GetLength(0); i++)
				output[i, input.GetLength(1)] = values[i];

			return output;
		}

		/// <summary>
		/// Remove a column from dataset
		/// </summary>
		/// <param name="input"></param>
		/// <param name="index"></param>
		/// <returns>double[,]</returns>
		public static double[,] RemoveColumn(this double[,] input, int index)
		{
			var matrix = new double[input.GetLength(0), input.GetLength(1) - 1];
			var jAux = 0;

			for (int i = 0; i < input.GetLength(0); i++)
			{
				for (int j = 0; j < input.GetLength(1); j++)
				{
					if (j != index)
					{
						matrix[i, jAux] = input[i, j];
						jAux++;
					} 
					else
						jAux--;
				}
				jAux = 0;
			}

			return matrix;
		}
	}
}
