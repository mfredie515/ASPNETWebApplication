using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ASPNETWebApplication.Helpers
{
    public class DbContext : IDbContext
    {
        private string _connectionString { get; }

        public DbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("awsMatthewFrederick");

        }

        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }
    }
}
