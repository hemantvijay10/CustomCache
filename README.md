CustomCache

Overview

CustomCache is a generic caching library written in C# designed to optimize data retrieval performance. When dealing with expensive operations—such as downloading data that takes around 1 second per call—this cache stores the result of a fetch operation, so subsequent requests for the same data are served immediately from memory. The cache is generic, meaning it can work with any type of key and any type of data, making it flexible for a wide range of applications.

Features

1. Generic Types: Works with any key and value types (Cache<TKey, TValue>), not just strings.

2. Thread-Safe: Uses a ConcurrentDictionary to ensure safe access in multi-threaded environments.

3. Efficient Data Retrieval: The first request for a key fetches the data (which may be slow), but subsequent requests return the cached data nearly instantly.

4. Easy Integration: Simply pass a key and a delegate (fetch function) to retrieve or cache data.

How It Works

1. Cache Initialization: The cache is implemented using a ConcurrentDictionary that maps keys to their corresponding data.

2. Data Fetching: The Get method checks if data for a given key is already cached. If it is, it returns the cached value. If not, it calls the provided fetch function (which simulates a slow operation) to retrieve the data, caches it, and then returns it.

3. Thread Safety: The use of ConcurrentDictionary ensures that the cache operations are thread-safe, making it suitable for use in multi-threaded applications.

Prerequisites
.NET 9.0 SDK

Installation

1. Clone the Repository:

git clone https://github.com/yourusername/CustomCache.git

2. Build the Project: Open the solution in your preferred IDE (Visual Studio, VS Code, etc.) or use the .NET CLI:

dotnet build

Security Considerations

1. Input Validation: The cache validates that the key and fetch function are not null, preventing null reference issues.

2. Thread Safety: By leveraging ConcurrentDictionary, the cache is safe for concurrent use, preventing race conditions and ensuring data integrity.

3. Sanitization: If integrating with file I/O or external data sources in the future, ensure proper sanitization and validation to avoid security vulnerabilities like directory traversal or injection attacks.

Future Enhancements

1. Persistence: Extend the cache to support persistent storage, so data can survive application restarts.

2. Expiration Policies: Add features such as time-based expiration or maximum cache size to automatically evict stale or excessive data.

3. Logging & Monitoring: Integrate detailed logging and performance monitoring to track cache hit/miss ratios and debug potential issues.

Acknowledgement: This project was completed as part of the Udemy course ‘Complete C# Masterclass’ by Krystyna Ślusarczyk (link is provided below). Having significant prior experience with C#, my goal was to refresh my skills and familiarize myself with the latest updates, features, and best practices introduced in recent versions.

https://www.udemy.com/course/ultimate-csharp-masterclass/
