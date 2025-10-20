using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace finalProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConnectionController : ControllerBase
    {
        ConnectionBL fromBl = new ConnectionBL();

        [HttpGet("UserLogin/{userName}/{userPassword}")]
        public IActionResult UserLogin(string userName, int userPassword)
        {
            var ook = fromBl.UserLogin(userName, userPassword);
            return Ok(new { ook });
        }
    }
}
