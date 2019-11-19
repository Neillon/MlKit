using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace MachineLearning.IO
{
	public class DatasetReader
	{
		private int ClassIndex = -1;
		private int NumFeatures = 0;
		private ICollection<string> Lines = new List<string>();

		public double[,] Input { get; set; }
		public int[] Output { get; set; }

		public DatasetReader(string filePath, int? classIndex = 0)
		{
			using (var reader = new StreamReader(filePath))
			{
				while (!reader.EndOfStream)
				{
					var line = reader.ReadLine();
					Lines.Add(line);
				}
			}

			// Remove all empty lines
			Lines = Lines
				.Where(p => !string.IsNullOrEmpty(p))
				.ToList();

			// initialize the values
			NumFeatures = Lines.FirstOrDefault().Split(',').Length - 1;
			ClassIndex = classIndex ?? NumFeatures;
			Input = new double[Lines.Count, NumFeatures];

			// populate the input matrix
			var lines = Lines.ToArray();

			for (int i = 0; i < Lines.Count; i++)
			{
				var features = lines[i].Split(',');
				for (int j = 0; j < features.Length; j++)
				{
					if (j != ClassIndex)
						// Invariant culture is used to get the point when converting to decimal value
						Input[i, j] = Convert.ToDouble(features[j], CultureInfo.InvariantCulture);
				}
			}

			Output = TransformOutputs(lines);
		}

		private int[] TransformOutputs(string[] lines)
		{
			var classes = new List<string>();

			for (int i = 0; i < Lines.Count; i++)
			{
				var features = lines[i].Split(',');
				classes.Add(features[ClassIndex].ToLower().Trim());
			}

			var outputClasses = classes.Distinct();
			var outputs = classes.Select(p =>
			{
				return outputClasses
					.Select((Value, Index) => new { Value, Index })
					.Where(o => o.Value.Equals(p))
					.Select(q => q.Index)
					.FirstOrDefault();

			});

			return outputs.ToArray();
		}
	}
}
