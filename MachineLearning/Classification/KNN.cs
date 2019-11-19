using System;
using System.Collections.Generic;
using System.Linq;

namespace MachineLearning.Classification
{
	public class KNN : IClassifier
	{
		public int K { private get; set; }
		private double[,] _input;
		private int[] _output;

		public KNN(int k = 3)
		{
			K = k;
		}
		public KNN(double[,] input, int[] output, int? k)
		{
			K = k ?? 3;
			_input = input;
			_output = output;
		}
		public KNN(double[,] input, int[] output)
		{
			_input = input;
			_output = output;
			K = 3;
		}

		private double EuclidianDistance(double[] p, double[] q)
		{
			var subtraction = new List<double>();

			for (int i = 0; i < p.Length; i++)
				subtraction.Add(p[i] - q[i]);

			return Math.Sqrt(Math.Pow(subtraction.Sum(), 2));
		}

		public int Predict(double[] newValue)
		{
			var distances = new List<double>();

			var points = new double[_input.GetLength(1)];
			for (int i = 0; i < _input.GetLength(0); i++)
			{
				for (int j = 0; j < _input.GetLength(1); j++)
					points[j] = _input[i, j];

				distances.Add(EuclidianDistance(points, newValue));
			}

			var grouped = distances
				.Select((value, index) => new { Distance = value, Class = _output[index] });

			grouped = grouped
				.OrderBy(p => p.Distance)
				.Take(K)
				.ToList();

			var mostCommon = grouped
				.GroupBy(i => i.Class)
				.OrderByDescending(grp => grp.Count())
				.Select(grp => grp.Key)
				.FirstOrDefault();

			return mostCommon;
		}

		public void Learn(double[,] input, int[] output)
		{
			_input = input;
			_output = output;
		}
	}
}
