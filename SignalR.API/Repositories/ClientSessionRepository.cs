using SignalR.API.Models;
using System.Data;
using System.Data.SqlClient;

namespace SignalR.API.Repositories
{
    public class ClientSessionRepository
    {
        string connectionString;

        public ClientSessionRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<ClientSession> GetClientSessions(string createdBy)
        {
            List<ClientSession> clientSessions = new List<ClientSession>();

            var data = GetClientSessionDetailsFromDb(createdBy);

            foreach (DataRow row in data.Rows)
            {
                clientSessions.Add(new ClientSession
                {
                    CreatedBy = row["CreatedBy"].ToString(),
                    SessionID = row["SessionID"].ToString()
                });
            }

            return clientSessions;
        }

        public void SaveClientSession(string createdBy, string sessionID)
        {
            // Create a new SqlConnection object
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the connection
                connection.Open();

                // Create a new SqlCommand object with a parameterized query
                string query = "INSERT INTO dbo.ClientSession (CreatedBy, SessionID) VALUES (@Value1, @Value2)";
                SqlCommand command = new SqlCommand(query, connection);

                // Add parameters to the command
                command.Parameters.AddWithValue("@Value1", createdBy);
                command.Parameters.AddWithValue("@Value2", sessionID);
                //command.Parameters.AddWithValue("@Value3", DateTime.Now);

                // Execute the command
                int rowsAffected = command.ExecuteNonQuery();

                // Check the number of rows affected
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Data saved successfully.");
                }
                else
                {
                    Console.WriteLine("No data saved.");
                }
            }
        }

        private DataTable GetClientSessionDetailsFromDb(string createdBy)
        {
            var query = "SELECT CreatedBy, SessionID FROM dbo.ClientSession WHERE CreatedBy = '" + createdBy + "'";
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
