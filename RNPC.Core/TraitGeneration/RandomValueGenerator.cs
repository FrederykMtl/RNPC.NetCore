using System;
using System.Security.Cryptography;
using RNPC.Core.Resources;

namespace RNPC.Core.TraitGeneration
{
    /// <summary>
    /// This class generates numbers in a pecific range
    /// </summary>
    internal static class RandomValueGenerator
    {
        private static readonly object Sync = new object();

        /// <summary>
        /// This class will generate a value between 1 an 100.
        /// </summary>
        /// <returns>a number going from 1 to 100</returns>
        internal static int GeneratePercentileIntegerValue()
        {
            return GenerateRandomNumberWithinRange(1, 100);
        }

        /// <summary>
        /// Generates a value between 1 and inputted number
        /// </summary>
        /// <param name="value">Max value for random range</param>
        /// <returns>generated number</returns>
        internal static int GenerateIntWithMaxValue(int value)
        {
            return GenerateRandomNumberWithinRange(1, value);
        }

        /// <summary>
        /// Generates a value between 81 and 100: a strong point
        /// </summary>
        /// <returns>generated number</returns>
        internal static int GenerateStrongAttributeValue()
        {
            return GenerateRandomNumberWithinRange(Constants.MinStrongPoint, Constants.MaxStrongPoint);
        }

        /// <summary>
        /// Generates a value between 21 and 80: a midrange point
        /// </summary>
        /// <returns></returns>
        internal static int GenerateMidRangeAttributeValue()
        {
            return GenerateRandomNumberWithinRange(Constants.MinAveragePoint, Constants.MaxAveragePoint);
        }

        /// <summary>
        /// Generates a value between 1 and 20: a weak point
        /// </summary>
        /// <returns></returns>
        internal static int GenerateWeakAttributeValue()
        {
            return GenerateRandomNumberWithinRange(Constants.MinWeakPoint, Constants.MaxWeakPoint);
        }

        /// <summary>
        /// This method does all the actual generation
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private static int GenerateRandomNumberWithinRange(int min, int max)
        {
            lock (Sync)
            {
                var cryptoResult = new byte[4];

                new RNGCryptoServiceProvider().GetBytes(cryptoResult);

                int seed = BitConverter.ToInt32(cryptoResult, 0);

                return new Random(seed).Next(min, max + 1);
            }
        }

        /// <summary>
        /// Generates a value between 1 and inputted number
        /// </summary>
        /// <param name="minValue">Min value for random range</param>
        /// <param name="maxValue">Max value for random range</param>
        /// <returns>generated number</returns>
        internal static int GenerateRealWithinValues(int minValue, int maxValue)
        {
            return GenerateRandomNumberWithinRange(minValue, maxValue);
        }
    }
}
