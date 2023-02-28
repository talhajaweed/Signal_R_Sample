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

        public ClientSession GetClientSessions(string createdBy)
        {
            ClientSession ClientSession = null;

            var data = GetClientSessionDetailsFromDb(createdBy);

            foreach (DataRow row in data.Rows)
            {
                ClientSession = new ClientSession
                {
                    CreatedBy = row["CreatedBy"].ToString(),
                    SessionID = row["SessionID"].ToString()
                };
            }

            return ClientSession;
        }

        private DataTable SaveClientSession(string createdBy, string sessionID)
        {
            var query = "SELECT CreatedBy, SessionID FROM dbo.ClientSession WHERE CreatedBy = '" + (createdBy) + "'";
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
