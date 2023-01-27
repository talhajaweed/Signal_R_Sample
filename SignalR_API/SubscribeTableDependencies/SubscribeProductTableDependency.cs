using SignalR_API.Hubs;
using SignalR_API.Models;
using TableDependency.SqlClient;

namespace SignalR_API.SubscribeTableDependencies
{
    public class SubscribeProductTableDependency : ISubscribeTableDependency
    {
        SqlTableDependency<ProviderNews> tableDependency;
        DashboardHub dashboardHub;

        public SubscribeProductTableDependency(DashboardHub dashboardHub)
        {
            this.dashboardHub = dashboardHub;
        }   

        public void SubscribeTableDependency(string connectionString)
        {
            tableDependency = new SqlTableDependency<ProviderNews>(connectionString);
            tableDependency.OnChanged += TableDependency_OnChanged;
            tableDependency.OnError += TableDependency_OnError;
            tableDependency.Start();
        }

        private void TableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<ProviderNews> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                dashboardHub.SendProducts();
            }
        }

        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            //Console.WriteLine($"{nameof(ProviderNews)} SqlTableDependency error: {e.Error.Message}");
        }
    }
}
