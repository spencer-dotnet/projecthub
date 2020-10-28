using Dapper;
using Microsoft.Extensions.Configuration;
using NelsonHub.Shared.DAL.Helpers;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace NelsonHub.Shared.DAL
{
    public class NelsonHubSqlDataAccess : INelsonHubSqlDataAccess
    {
        private readonly IConfiguration _config;

        public readonly string CONNECTION_STRING_NAME = "Postgres:NelsonHub";
        public readonly string DATABASE_URL_NAME = "DATABASE_URL";

        public string _connectionString;

        public NelsonHubSqlDataAccess(IConfiguration config)
        {
            _config = config;

            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (env == "Development")
            {
                _connectionString = _config[CONNECTION_STRING_NAME];
            }
            else
            {
                string postgresURL = Environment.GetEnvironmentVariable(DATABASE_URL_NAME);

                _connectionString = PostgresConnectionHelper.ParseConnectionURL(postgresURL);
            }            
        }

        public async Task<List<T>> LoadData<T, U>(string sql, U parameters)
        {
            string connectionString = _connectionString;

            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                var data = await connection.QueryAsync<T>(sql, parameters);

                return data.ToList();
            }
        }

        public async Task<T> LoadOneData<T, U>(string sql, U parameters)
        {
            string connectionString = _connectionString;

            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                var data = await connection.QueryFirstAsync<T>(sql, parameters);

                return data;
            }
        }
        public async Task<int> SaveData<T>(string sql, T parameters)
        {
            string connectionString = _connectionString;

            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                var rowsChanged = await connection.ExecuteAsync(sql, parameters);

                return rowsChanged;
            }
        }


    }
}
