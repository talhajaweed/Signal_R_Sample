using Microsoft.AspNetCore.Mvc;
//using System.Web.Http;

namespace SignalR.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpGet("{loginId}/{password}")]
        public IActionResult Get(string loginId, string password)
        {
            try
            {
                var loginID = HttpContext.Session.Id;
                return Ok("done");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
