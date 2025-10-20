using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static finalProject.DAL;

namespace finalProject.WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GraphsController : ControllerBase
    {
        BL fromBl = new BL();
        [HttpGet("ApprovalUsers/{selectedMonth}")]
        public IActionResult ApprovalUsers(string selectedMonth)
        {
            try
            {
                return Ok(fromBl.ApprovalUsers(selectedMonth));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("WhereStatusAds/{selectedMonth}")]
        public IActionResult WhereStatusAds(string selectedMonth)
        {
            try
            {
                return Ok(fromBl.WhereStatusAds(selectedMonth));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("AdCategory/{selectedMonth}")]
        public IActionResult AdCategory(string selectedMonth)
        {
            try
            {
                return Ok(fromBl.AdCategory(selectedMonth));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }  

        [HttpGet("SumCustomerCharge/{selectedMonth}")]
        public IActionResult SumCustomerCharge(string selectedMonth)
        {
            try
            {
                return Ok(fromBl.SumCustomerCharge(selectedMonth));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
