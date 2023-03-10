using SignalR.API.Models;
using SignalR.API.Repositories;
using Microsoft.AspNetCore.Mvc;
//using System.Web.Http;

namespace SignalR.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        ClientSessionRepository clientSessionRepository;

        public LoginController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            clientSessionRepository = new ClientSessionRepository(connectionString);
        }

        
        // [HttpPost("SaveSessioin")]
        // public IActionResult SaveSessioin([FromBody] ClientSession clientSession)
        // {
        //     try
        //     {
        //         //var loginID = HttpContext.Session.Id;
        //         //var obj = clientSessionRepository.SaveClientSession(clientSession.CreatedBy, clientSession.);
        //         //return Ok("done with SaveSessioin " + obj.CreatedBy);                
        //     }
        //     catch (Exception ex)
        //     {
        //         throw ex;
        //     }
        // }

        [HttpGet("{loginId}/{password}")]
        public IActionResult Get(string loginId, string password)
        {
            try
            {
                //var loginID = HttpContext.Session.Id;
                var obj = new {
                    message = "done with loginId"
                };
                return Ok(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
