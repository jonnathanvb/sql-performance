using Infra.Enitty;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Connection
{
    public class ContextEntityFramework : DbContext
    {

        public ContextEntityFramework(string connectionString) : base(GetConnectionString(connectionString))
        {
        }

        private static DbContextOptions GetConnectionString(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ContextEntityFramework>();
            optionsBuilder.UseSqlServer(connectionString);
            return optionsBuilder.Options;
        }

        public DbSet<Pessoa> Pessoa { get; set; }

    }
}
