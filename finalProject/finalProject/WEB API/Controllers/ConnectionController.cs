using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace finalProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConnectionController : ControllerBase
    {
        private readonly IConnectionBL fromBl;

        public ConnectionController(IConnectionBL connectionBL)
        {
            fromBl = connectionBL;
        }

        [HttpGet("UserLogin/{userName}/{userPassword}")]
        public IActionResult UserLogin(string userName, int userPassword)
        {
            var ook = fromBl.UserLogin(userName, userPassword);
            return Ok(new { ook });
        }
    }
}
