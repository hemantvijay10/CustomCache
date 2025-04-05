using System.Collections.Concurrent;
using static CustomCache.Program;

namespace CustomCache
{
    internal class Program
    {
        static void Main()
        {
            // Create a cache for string keys and string values.
            MyCache<string, string> cache = new MyCache<string, string>();

            // First fetch: Data is not yet cached, so DownloadData is called (slow).
            Console.WriteLine("First fetch:");
            string data1 = cache.Get("id1", DownloadData);
            Console.WriteLine("Result: " + data1);
            Console.WriteLine();

            // Second fetch: The data should now be in the cache and returned instantly.
            Console.WriteLine("Second fetch:");
            string data2 = cache.Get("id1", DownloadData);
            Console.WriteLine("Result: " + data2);

            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }

        /// <summary>
        /// Simulates a slow data fetch operation by waiting for 1 second.
        /// In a real scenario, this might be an API or database call.
        /// </summary>
        static string DownloadData(string id)
        {
            Console.WriteLine($"Downloading data for '{id}' (this may take around 1 second)...");
            Thread.Sleep(1000); // Simulate delay
            return $"Data for {id}";
        }

        /// <summary>
        /// A generic cache that stores data of any type (TValue) identified by keys of any type (TKey).
        /// When data for a given key is not available in the cache, the provided fetchFunction is invoked
        /// to retrieve it. The value is then stored for fast, subsequent retrievals.
        /// </summary>
        public class MyCache<TKey, TValue>
        {
            // Using ConcurrentDictionary to ensure thread-safe access.
            private readonly ConcurrentDictionary<TKey, TValue> _cache = new ConcurrentDictionary<TKey, TValue>();

            /// <summary>
            /// Retrieves the cached value for the specified key. If the key is not found,
            /// the fetchFunction is executed to fetch the data, which is then cached and returned.
            /// </summary>
            /// <param name="key">The key that identifies the data.</param>
            /// <param name="fetchFunction">A function that fetches the data if it is not already cached.</param>
            /// <returns>The cached or freshly fetched data.</returns>
            public TValue Get(TKey key, Func<TKey, TValue> fetchFunction)
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }
                if (fetchFunction == null)
                {
                    throw new ArgumentNullException(nameof(fetchFunction));
                }                    

                // GetOrAdd ensures that the fetchFunction is only called once per key in a thread-safe manner.
                return _cache.GetOrAdd(key, fetchFunction);
            }
        }
    }
}