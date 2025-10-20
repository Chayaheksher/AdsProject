using finalProject.Models;
using finalProject.Models1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace finalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersDetailsController : ControllerBase
    {
        UserDetailsBL fromBl = new UserDetailsBL();

        [HttpGet]
        public IActionResult GetUsers()
        {
            try
            {
               var users = fromBl.GetUsers();
                return Ok(new { users });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("api/rolenames")]
        [HttpGet]
        public IActionResult RoleName()
        {
            try
            {
                var rolenames = fromBl.RoleName();
                return Ok(rolenames);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("communication")]
        [HttpGet]
        public ActionResult<communicationClass[]> getCommunication()
        {
            try
            {
                var communicationArray = fromBl.GetCommunication();
                return Ok(communicationArray);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("communication/comName")]
        [HttpGet]
        public IActionResult CommunicationName()
        {
            try
            {
                return Ok(fromBl.CommunicationName());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("saveUserDetails")]
        public IActionResult UpsertUser([FromBody] SaveUserDedails userDetails)
        {
            try
            {
                if (userDetails == null)
                {
                    return BadRequest("Invalid user details");
                }

                fromBl.UpsertUserAndCommunications(userDetails);
                var str = "User details saved successfully";
                return Ok(new { str });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
