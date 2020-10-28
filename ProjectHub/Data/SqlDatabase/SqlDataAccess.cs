using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHub.Data.SqlDatabase
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;

        public string ConnectionStringName = "Default";

        public string _connectionString;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;

            // set up the connection string
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (env == "Development")
            {
                _connectionString = _config.GetConnectionString(ConnectionStringName);
            }
            else
            {
                var connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

                // Parse connection URL to connection string for Npgsql
                connUrl = connUrl.Replace("postgres://", string.Empty);
                var pgUserPass = connUrl.Split("@")[0];
                var pgHostPortDb = connUrl.Split("@")[1];
                var pgHostPort = pgHostPortDb.Split("/")[0];
                var pgDb = pgHostPortDb.Split("/")[1];
                var pgUser = pgUserPass.Split(":")[0];
                var pgPass = pgUserPass.Split(":")[1];
                var pgHost = pgHostPort.Split(":")[0];
                var pgPort = pgHostPort.Split(":")[1];

                _connectionString = $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb};SSL Mode=Require;Trust Server Certificate=true";
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
