using SignalR.API.Models;
using System.Data;
using System.Data.SqlClient;

namespace SignalR.API.Repositories
{
    public class ProductRepository
    {
        string connectionString;

        public ProductRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<ProductAir_Mst> GetProducts()
        {
            List<ProductAir_Mst> products = new List<ProductAir_Mst>();
            ProductAir_Mst product;

            var data = GetProductDetailsFromDb();

            foreach (DataRow row in data.Rows)
            {
                product = new ProductAir_Mst
                {
                    ProductAirID = Convert.ToInt64(row["ProductAirID"]),
                    ProductAirName = row["ProductAirName"].ToString(),
                    CreatedBy = row["CreatedBy"].ToString(),
                    Price = Convert.ToDecimal(row["Price"])
                };
                products.Add(product);
            }

            return products;
        }

        private DataTable GetProductDetailsFromDb()
        {
            var query = "SELECT ProductAirID, ProductAirName, CreatedBy, (ProductAirID * 100) Price FROM HashMoveOwn.ProductAir_Mst";
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dataTable.Load(reader);
                        }
                    }

                    return dataTable;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
