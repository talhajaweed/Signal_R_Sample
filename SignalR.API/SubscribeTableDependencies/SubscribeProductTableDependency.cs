using SignalR.API.Hubs;
using SignalR.API.Models;
using TableDependency.SqlClient;

namespace SignalR.API.SubscribeTableDependencies
{
    public class SubscribeProductTableDependency
    {
        SqlTableDependency<ProductAir_Mst> tableDependency;
        DashboardHub dashboardHub;

        public SubscribeProductTableDependency(DashboardHub dashboardHub)
        {
            this.dashboardHub = dashboardHub;
        }   

        public void SubscribeTableDependency(string connectionString)
        {
            tableDependency = new SqlTableDependency<ProductAir_Mst>(connectionString);
            tableDependency.OnChanged += TableDependency_OnChanged;
            tableDependency.OnError += TableDependency_OnError;
            tableDependency.Start();
        }

        private void TableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<ProductAir_Mst> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                dashboardHub.SendProducts();
            }
        }

        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(ProductAir_Mst)} SqlTableDependency error: {e.Error.Message}");
        }
    }
}