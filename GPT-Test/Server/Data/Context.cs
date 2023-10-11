using GPT_Test.Server.Data.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace GPT_Test.Server.Data
{
    public class Context : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierConfiguration());
        }

        public string ExecuteRawSqlQuery(string sql)
        {
            try
            {
                var result = new List<string>();
                using var connection = Database.GetDbConnection();
                using var command = connection.CreateCommand();
                command.CommandText = sql;
                connection.Open();

                using var reader = command.ExecuteReader();

                // Get column names
                var columns = new List<string>();
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    columns.Add(reader.GetName(i));
                }
                result.Add(string.Join(",", columns));

                // Get data row by row, column by column
                while (reader.Read())
                {
                    var row = new List<string>();
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        row.Add(reader[i].ToString());
                    }
                    result.Add(string.Join(",", row));
                }

                return string.Join("\n\n", result);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during the execution
                // You can return an error message or throw an exception as needed
                return $"Error: {ex.Message}";
            }
        }
    }
}