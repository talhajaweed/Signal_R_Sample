using Microsoft.AspNetCore.SignalR;
using SignalR.API.Repositories;

namespace SignalR.API.Hubs
{
    public class DashboardHub : Hub
    {
        ProductRepository productRepository;
        ClientSessionRepository clientSessionRepository;

        public DashboardHub(IConfiguration configuration)
        {            
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            productRepository = new ProductRepository(connectionString);
            clientSessionRepository = new ClientSessionRepository(connectionString);
        }

        // public async Task SendProducts()
        // {
        //     var products = productRepository.GetProducts();
        //     await Clients.All.SendAsync("ReceivedProducts", products);
        // }

        public async Task SendProducts(string createdBy)
        {
            var products = productRepository.GetProducts(createdBy);
            var clientSessions = clientSessionRepository.GetClientSessions(createdBy);
            foreach (var sess in clientSessions)
            {
                if(!string.IsNullOrWhiteSpace(sess.SessionID))
                await Clients.Client(sess.SessionID).SendAsync("ReceivedProducts", products);
            }            
        }

        public async Task SaveSession(string createdBy)
        {
            var sessionId = Context.ConnectionId;
            clientSessionRepository.SaveClientSession(createdBy, sessionId);
        }
    }
}