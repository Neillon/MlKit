using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace MachineLearning.Core
{
	/// <summary>
	/// Read the files and prepare the data for apply the ML algorithims
	/// </summary>
	public class Dataset
	{
		public int ClassIndex { get; }
		public double[,] Input { get; set; }
		public int[] Output { get; set; }

		/// <summary>
		/// Receives the file path and the index of predicted classes<br />
		/// <b>Accepts .csv, .data files</b>
		/// </summary>
		/// <param name="filePath"></param>
		/// <param name="classIndex"></param>
		public Dataset(string filePath, int? classIndex = 0)
		{
			// initialize some values
			var lines = ReadFile(filePath);
			var featuresCount = lines[0].Split(',').Length - 1;
			ClassIndex = classIndex ?? featuresCount;

			Input = GetInputFromStringArray(lines, featuresCount);
			Output = GetOutputFromStringArray(lines.ToArray());
		}

		/// <summary>
		/// Read a file and return an string array that contains all non-empty lines of readed file on each index
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns>string[]</returns>
		private string[] ReadFile(string filePath)
		{
			var lines = new List<string>();
			using (var reader = new StreamReader(filePath))
			{
				while (!reader.EndOfStream)
				{
					var line = reader.ReadLine();
					lines.Add(line);
				}
			}

			return lines
				.Where(p => !string.IsNullOrEmpty(p))
				.ToArray();
		}

		/// <summary>
		/// Takes ans list of string, split and returns the converted input data
		/// </summary>
		/// <param name="lines"></param>
		/// <param name="numFeatures"></param>
		/// <returns>double[,]</returns>
		private double[,] GetInputFromStringArray(string[] lines, int numFeatures)
		{
			
			Input = new double[lines.Count(), numFeatures];

			// populate input matrix
			for (int i = 0; i < lines.Count(); i++)
			{
				var features = lines[i].Split(',');
				for (int j = 0; j < features.Length; j++)
				{
					if (j != ClassIndex)
						// Invariant culture is used to get the point when converting to decimal value
						Input[i, j] = Convert.ToDouble(features[j], CultureInfo.InvariantCulture); 
				}
			}
			return Input;
		}

		/// <summary>
		/// Takes ans list of string, split and returns the outputs based on defined ClassIndex 
		/// </summary>
		/// <param name="lines"></param>
		/// <returns>int[]</returns>
		private int[] GetOutputFromStringArray(string[] lines)
		{
			var classes = new List<string>();

			#region Get array of classes
			for (int i = 0; i < lines.Count(); i++)
			{
				var features = lines[i].Split(',');
				classes.Add(features[ClassIndex].ToLower().Trim());
			}
			#endregion

			#region Transform to integer outputs
			var outputClasses = classes
				.Distinct()
				.ToList();
			var outputs = new int[lines.Count()];
			// for each element find the index of class
			for (int i = 0; i < classes.Count(); i++)
			{
				var classValue = classes[i].ToLower().Trim();
				outputs[i] = outputClasses.FindIndex(p => p.Equals(classValue));
			}
			#endregion

			return outputs.ToArray();
		}
	}
}
