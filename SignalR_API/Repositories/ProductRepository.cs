using SignalR_API.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace SignalR_API.Repositories
{
    public class ProductRepository
    {
        string connectionString;

        public ProductRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<ProviderNews> GetProducts()
        {
            List<ProviderNews> products = new List<ProviderNews>();
            ProviderNews product;

            var data = GetProviderNewsFromDb();

            foreach (DataRow row in data.Rows)
            {
                product = new ProviderNews
                {
                    NewsID = Convert.ToInt64(row["NewsID"]),
                    NewsTitle = row["NewsTitle"].ToString(),
                    ProviderID = Convert.ToInt64(row["ProviderID"])
                };
                products.Add(product);
            }

            return products;
        }

        private DataTable GetProviderNewsFromDb()
        {
            var query = "SELECT NewsID, NewsTitle, ProviderID FROM HashMoveOwn.ProviderNews_Mst where ProviderID  = 159";
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

        public List<ProductForGraph> GetProductsForGraph()
        {
            List<ProductForGraph> productsForGraph = new List<ProductForGraph>();
            ProductForGraph productForGraph;

            var data = GetProductsForGraphFromDb();

            foreach (DataRow row in data.Rows)
            {
                productForGraph = new ProductForGraph
                {
                    Category = row["Category"].ToString(),
                    Products = Convert.ToInt32(row["Products"])
                };
                productsForGraph.Add(productForGraph);
            }

            return productsForGraph;
        }

        private DataTable GetProductsForGraphFromDb()
        {
            var query = "SELECT Category, COUNT(Id) Products FROM ProviderNews GROUP BY Category";
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
