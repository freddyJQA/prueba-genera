using App.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace App.Repositories
{
    public class DatabaseRepository
    {
        private readonly string _connectionString;

        public DatabaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default")
                ?? throw new InvalidOperationException("No se encontró la cadena de conexión.");
        }

        public async Task<Dictionary<string, int>> GetWorkersIdsByRuts(
            List<string> ruts)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            const string sql = """
                SELECT TrabajadorId, Rut FROM Trabajador WHERE Rut IN @Ruts
                """;

            var result = await connection.QueryAsync<(int Id, string Rut)>(
                sql,
                new { Ruts = ruts });

            return result.ToDictionary(x => x.Rut, x => x.Id);
        }
    }
}
