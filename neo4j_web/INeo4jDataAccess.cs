public interface INeo4jDataAccess : IAsyncDisposable
{
    Task<List<string>> ExecuteReadListAsync(string query, string returnObjectKey, IDictionary<string, object>? parameters = null);
       
    Task<List<Dictionary<string, object>>> ExecuteReadDictionaryAsync(string query, string returnObjectKey, IDictionary<string, object>? parameters = null);

    Task<T> ExecuteReadScalarAsync<T>(string query, IDictionary<string, object>? parameters = null);

    Task<IEnumerable<T>> ExecuteWriteTransactionAsync<T>(string query, IDictionary<string, object>? parameters = null) where T : new();
    
    Task RunQueryAsync(string query, IDictionary<string, object>? parameters = null);
}