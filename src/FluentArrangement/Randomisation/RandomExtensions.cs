using System;

namespace FluentArrangement
{
    internal static class RandomExtensions
    {
        /// <summary>
        /// Returns an Int32 with a random value across the entire range of
        /// possible values.
        /// </summary>
        /// <remarks>
        /// Taken from https://stackoverflow.com/a/609529/369247
        /// </remarks>
        public static int NextInt32(this Random random)
        {
            int firstBits = random.Next(0, 1 << 4) << 28;
            int lastBits = random.Next(0, 1 << 28);
            return firstBits | lastBits;
        }

        /// <remarks>
        /// Taken from https://stackoverflow.com/a/609529/369247
        /// </remarks>
        public static decimal NextDecimal(this Random random)
        {
            byte scale = (byte) random.Next(29);
            bool sign = random.Next(2) == 1;

            return new decimal(random.NextInt32(), 
                                random.NextInt32(),
                                random.NextInt32(),
                                sign,
                                scale);
        }

        public static double NextDouble(this Random random, double min, double max)
        { 
            return random.NextDouble() * (max - min) + min;
        }

        /// <summary>
        /// Returns a random long from min (inclusive) to max (exclusive)
        /// </summary>
        /// <remarks>
        /// Taken from https://stackoverflow.com/a/13095144/369247
        /// </remarks>
        /// <param name="random">The given random instance</param>
        /// <param name="min">The inclusive minimum bound</param>
        /// <param name="max">The exclusive maximum bound.  Must be greater than min</param>
        public static long NextInt64(this Random random, long min, long max)
        {
            if (max <= min)
                throw new ArgumentOutOfRangeException("max", "max must be > min!");

            //Working with ulong so that modulo works correctly with values > long.MaxValue
            ulong uRange = (ulong)(max - min);

            //Prevent a modolo bias; see https://stackoverflow.com/a/10984975/238419
            //for more information.
            //In the worst case, the expected number of calls is 2 (though usually it's
            //much closer to 1) so this loop doesn't really hurt performance at all.
            ulong ulongRand;
            do
            {
                byte[] buf = new byte[8];
                random.NextBytes(buf);
                ulongRand = (ulong)BitConverter.ToInt64(buf, 0);
            } while (ulongRand > ulong.MaxValue - ((ulong.MaxValue % uRange) + 1) % uRange);

            return (long)(ulongRand % uRange) + min;
        }

        /// <summary>
        /// Returns a random long from 0 (inclusive) to max (exclusive)
        /// </summary>
        /// <param name="random">The given random instance</param>
        /// <param name="max">The exclusive maximum bound.  Must be greater than 0</param>
        public static long NextInt64(this Random random, long max)
        {
            return random.NextInt64(0, max);
        }

        /// <summary>
        /// Returns a random long over all possible values of long (except long.MaxValue, similar to
        /// random.Next())
        /// </summary>
        /// <param name="random">The given random instance</param>
        public static long NextInt64(this Random random)
        {
            return random.NextInt64(long.MinValue, long.MaxValue);
        }
    }
}