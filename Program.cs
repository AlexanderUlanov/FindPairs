using System;
using System.Collections.Generic;
using System.Linq;

namespace FindParis
{
    class Program
    {
        static void Main(string[] args)
        {
            CheckAndWrite(new[] {2, 2, 2, 2}, 4, 6);
            CheckAndWrite(new[] { 2, 3, 2, 3 }, 5, 4);
            CheckAndWrite(new[] { -2, 3, 1, 0, 0, 1 }, 1, 5);
            CheckAndWrite(new[] { 1, 2, 3, 4, 5 }, 6, 2);
            Console.Read();
        }

        public static void CheckAndWrite(int[] collection, int x, int expectedNumberOfPairs)
        {
            Console.WriteLine($"Collection: {string.Join(" ", collection)}");
            Console.WriteLine($"X: {x}");
            var pairs = GetPairs(collection, x).ToArray();
            if(pairs.Length != expectedNumberOfPairs)
                Console.WriteLine($"Wrong number of paris. Expected {expectedNumberOfPairs}, Actual {pairs.Length}");
            else
                Console.WriteLine($"Number of pairs: {expectedNumberOfPairs}");

            foreach (var pair in pairs)
            {
                Console.WriteLine($"({pair.Item1} {pair.Item2})");
                expectedNumberOfPairs--;
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Returns number of paris which from <see cref="collection"/> which sum is euqal to <see cref="x"/>
        /// </summary>
        /// <remarks>
        /// Idea of internal implementation is to track number of how many times we find the exact addtion to the specific element and to track number of this element.
        /// Thus we traverse only two times and return pairs. Complexity is O(n).
        /// </remarks>
        public static IEnumerable<Tuple<int, int>> GetPairs(int[] collection, int x)
        {
            var numberOfAdditions = new Dictionary<int, int>();
            var firstElelemtCount = new Dictionary<int, int>();
            for (var i = 0; i < collection.Length; i++)
            {
                var el = collection[i];
                var addition = x - el;
                if (numberOfAdditions.ContainsKey(addition))
                {
                    numberOfAdditions[addition]++;
                }
                else
                {
                    if (!numberOfAdditions.ContainsKey(el))
                    {
                        numberOfAdditions.Add(el, 0);
                        firstElelemtCount.Add(el, 1);
                    }
                    else
                    {
                        firstElelemtCount[el]++;
                    }
                }
            }

            foreach (var key in numberOfAdditions.Keys)
            {
                if (numberOfAdditions[key] == 0)
                    continue;

                if (key << 1 == x) // key is the exact middle of the x
                {
                    var pairCount = (numberOfAdditions[key] * (numberOfAdditions[key] + 1)) / 2;
                    for (int i = 0; i < pairCount; i++)
                    {
                        yield return new Tuple<int, int>(key, key);
                    }
                }
                else
                {
                    var pairCount = numberOfAdditions[key] * firstElelemtCount[key];
                    for (int i = 0; i < pairCount; i++)
                    {
                        yield return new Tuple<int, int>(key, x - key);
                    }
                }
            }
        }
    }
}
