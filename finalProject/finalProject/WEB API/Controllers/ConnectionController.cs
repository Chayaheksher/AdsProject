using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace finalProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConnectionController : ControllerBase
    {
        BL fromBl = new BL();

        [HttpGet("UserLogin/{userName}/{userPassword}")]
        public IActionResult UserLogin(string userName, int userPassword)
        {
            try
            {
                var ook = fromBl.UserLogin(userName, userPassword);
                return Ok(new { ook });
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message+" בעיה בקונטרולר");
            }
        }
    }
}
