namespace SignalR_API.Models
{
    public class ProviderNews
    {
        public long NewsID { get; set; }
        public string NewsTitle { get; set; }
        public long ProviderID { get; set; }
    }

    public class ProductForGraph
    {
        public string Category { get; set; }
        public int Products { get; set; }
    }
}
