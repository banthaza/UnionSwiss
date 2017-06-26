using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnionSwiss.Domain.Common
{
    [DebuggerStepThrough]
   public class Guard
    {
        public static void ArgumentNotNull(object argument, string argumentName)
        {
            if (argument == null)
                throw new ArgumentNullException(argumentName);
        }
        public static void ArgumentNotNullOrEmpty(string argument, string argumentName)
        {
            if (string.IsNullOrWhiteSpace(argument))
                throw new ArgumentNullException($"Argument ${argumentName} can not be null or empty");
        }

        public static void ArgumentNotZero(long argument, string argumentName)
        {
            if (argument == 0)
                throw new ArgumentOutOfRangeException(  $"Argument ${argumentName} can not be 0");
        }

        public static void ArgumentBetween(int argument, int lowerBound, int upperBound , string argumentName)
        {
            if (argument <= lowerBound)
                throw  new ArgumentOutOfRangeException($"{argumentName} must be greater than {lowerBound}");
            if (argument >= upperBound)
                throw new ArgumentOutOfRangeException($"{argumentName} must be less than {upperBound}");
        }

        public static void ArgumentGreaterZero(decimal argument, string argumentName)
        {
            if (argument < 0)
                throw new ArgumentOutOfRangeException($"{argumentName} must be greater than {0}");

        }

        public static void ArgumentIsValidDate(string argument, string argumentName)
        {
            var date = DateTime.MinValue;
            if (!DateTime.TryParse(argument, out date))
                throw new InvalidCastException($"{argumentName} is not a valid date: {0}");

        }

        public static void ArgumentIsValidDate(DateTime argument, string argumentName)
        {
            if (argument == DateTime.MinValue)
                throw new InvalidCastException($"{argumentName} is not a valid date: {0}");
        }

    }
}
