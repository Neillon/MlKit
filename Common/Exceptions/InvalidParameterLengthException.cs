using System;

namespace Common.Exceptions
{
	public class InvalidParameterLengthException : Exception
	{
		public InvalidParameterLengthException(string parameterName)
			: base($"Parameter {parameterName} has an invalid length.")
		{ }

		public InvalidParameterLengthException(string parameterName, int expectedValue, int receivedValue)
			: base($"Parameter {parameterName} has an invalid length. Expected {expectedValue}, but receives {receivedValue}")
		{ }
	}
}
