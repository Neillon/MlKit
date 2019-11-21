using System;
using System.Collections.Generic;
using System.Linq;

namespace MachineLearning.Classification
{
	/// <summary>
	/// K nearest neighbors classifier 
	/// </summary>
	public class KNN : IClassifier
	{
		public int K { private get; set; }
		private double[,] _input;
		private int[] _output;

		#region constructors
		/// <summary>
		/// Initialize a new KNN classifier 
		/// </summary>
		public KNN() { K = 3; }

		/// <summary>
		/// Initialize a new KNN classifier 
		/// </summary>
		/// <param name="k"></param>
		public KNN(int k = 3) { K = k; }

		/// <summary>
		/// Initialize a new KNN classifier 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="output"></param>
		/// <param name="k"></param>
		public KNN(double[,] input, int[] output, int? k)
		{
			K = k ?? 3;
			_input = input;
			_output = output;
		}

		/// <summary>
		/// Initialize a new KNN classifier 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="output"></param>
		public KNN(double[,] input, int[] output)
		{
			K = 3;
			_input = input;
			_output = output;
		}

		#endregion

		/// <summary>
		/// Compute the euclidian distance of two vector of points in space
		/// </summary>
		/// <param name="p"></param>
		/// <param name="q"></param>
		/// <returns></returns>
		private double EuclidianDistance(double[] p, double[] q)
		{
			var total = 0.0;

			for (int i = 0; i < p.Length; i++)
			{
				var sub = p[i] - q[i];
				total += sub * sub;
			}

			return Math.Sqrt(total);
		}

		/// <summary>
		/// Predict a class of new value
		/// </summary>
		/// <param name="newValue"></param>
		/// <returns>int</returns>
		public int Predict(double[] newValue)
		{
			var distances = new List<double>();

			// Compute distances
			var points = new double[_input.GetLength(1)];
			for (int i = 0; i < _input.GetLength(0); i++)
			{
				for (int j = 0; j < _input.GetLength(1); j++)
					points[j] = _input[i, j];

				distances.Add(EuclidianDistance(points, newValue));
			}

			// Get the class of each element
			var grouped = distances
				.Select((value, index) => new { Distance = value, Class = _output[index] });

			// Take the K nearest distances from new value 
			grouped = grouped
				.OrderBy(p => p.Distance)
				.Take(K)
				.ToList();

			// Get the most common class in the array of outputs
			// - Group values by class, oder by most common class and 
			// - select the key value of most common
			var mostCommon = grouped
				.GroupBy(i => i.Class)
				.OrderByDescending(grp => grp.Count())
				.Select(grp => grp.Key)
				.FirstOrDefault();

			return mostCommon;
		}

		/// <summary>
		/// Learn from input matrix and output array
		/// </summary>
		/// <param name="input"></param>
		/// <param name="output"></param>
		public void Learn(double[,] input, int[] output)
		{
			_input = input;
			_output = output;
		}
	}
}
