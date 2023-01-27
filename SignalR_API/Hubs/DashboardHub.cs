using Microsoft.AspNetCore.SignalR;
using SignalR_API.Repositories;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SignalR_API.Hubs
{
    public class DashboardHub : Hub
    {
        ProductRepository productRepository;

        public DashboardHub(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            productRepository = new ProductRepository(connectionString);
        }

        public async Task SendProducts()
        {
            var products = productRepository.GetProducts();
            await Clients.All.SendAsync("ReceivedProducts", products);

            // var productsForGraph = productRepository.GetProductsForGraph();
            // await Clients.All.SendAsync("ReceivedProductsForGraph", productsForGraph);
        }
    }
}