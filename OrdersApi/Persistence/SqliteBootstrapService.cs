using Dapper;
using Microsoft.Data.Sqlite;
using OrdersApi.Persistence.Abstract;

namespace OrdersApi.Persistence;

/// <summary>
/// To do in future ...
/// </summary>
public class SqliteBootstrapService : IDatabaseBootstrapService
{
    private readonly DatabaseConfiguration _configuration;
    private readonly ILogger<SqliteBootstrapService> _logger;
    public SqliteBootstrapService(DatabaseConfiguration configuration, ILogger<SqliteBootstrapService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }
    public async Task SetupAsync()
    {
        using (var connection = new SqliteConnection($"Data Source = {_configuration.DataSource}"))
        {
            var tables = await GetTablesAsync(connection);
            _logger.LogInformation($"Found tables count: {tables.Count}");
            
            if (tables.Contains("Product") == false)
            {
                _logger.LogWarning("There is no table 'Product'. Setting it up ...");
                var result = await CreateProductTableAsync(connection);
            }
            if (tables.Contains("Customer") == false)
            {
                _logger.LogWarning("There is no table 'Customer'. Setting it up ...");
                var result = await CreateProductTableAsync(connection);
            }
            if (tables.Contains("Order") == false)
            {
                _logger.LogWarning("There is no table 'Order'. Setting it up ...");
                var result = await CreateProductTableAsync(connection);
            }
        }
    }

    private async Task<List<string>> GetTablesAsync(SqliteConnection connection)
    {
        var tables = await connection.QueryAsync<string>(
            "SELECT name FROM sqlite_master WHERE type='table' AND (name = 'Product' OR name = 'Customer' OR name = 'Order');");

        return tables.ToList();
    }
    private async Task<int> CreateProductTableAsync(SqliteConnection connection)
    {
        return await connection.ExecuteAsync("CREATE TABLE Product (" +
                                             "Id VARCHAR(32) PRIMARY KEY," +
                                             "Name VARCHAR(32) NOT NULL," +
                                             "Price REAL NOT NULL," +
                                             "ReleaseDate TEXT NOT NULL," +
                                             "CreationTime TEXT NOT NULL);");
    }
}