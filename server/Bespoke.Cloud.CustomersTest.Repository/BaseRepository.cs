using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

namespace DataManagement.Repository
{
    public class BaseRepository : IDisposable
    {
        ////protected IDbConnection con;

        private readonly IConfiguration _config;
        protected SqlConnection _conn;


        public BaseRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection GetOpenConnection()
        {
            string cs = _config["ConnectionStrings:DefaultConnection"];
            _conn = new SqlConnection(cs);
            _conn.Open();
            return _conn;
        }

        public void Dispose()
        {
            _conn.Close();
        }
    }
}