using System.ComponentModel.DataAnnotations.Schema;

namespace SignalR.API.Models
{
    [Table("ClientSession")]
    public class ClientSession
    {
        public string CreatedBy { get; set; }
        public string SessionID { get; set; }
    }
}
