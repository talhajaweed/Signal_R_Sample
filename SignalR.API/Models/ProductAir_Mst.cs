using System.ComponentModel.DataAnnotations.Schema;

namespace SignalR.API.Models
{
    [Table("ProductAir_Mst", Schema = "HashMoveOwn")]
    public class ProductAir_Mst
    {
        public long ProductAirID { get; set; }

        public string ProductAirName { get; set; }

        public string CreatedBy { get; set; }

        public decimal Price { get; set; }
    }
}