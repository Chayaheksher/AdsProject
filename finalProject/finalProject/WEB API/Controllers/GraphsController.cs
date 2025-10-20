using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace finalProject.WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GraphsController : ControllerBase
    {
        GraphsBL fromBl = new GraphsBL();

        [HttpGet("ApprovalUsers/{selectedMonth}")]
        public IActionResult ApprovalUsers(string selectedMonth)
        {
            return Ok(fromBl.ApprovalUsers(selectedMonth));
        }

        [HttpGet("WhereStatusAds/{selectedMonth}")]
        public IActionResult WhereStatusAds(string selectedMonth)
        {
            return Ok(fromBl.WhereStatusAds(selectedMonth));
        }

        [HttpGet("AdCategory/{selectedMonth}")]
        public IActionResult AdCategory(string selectedMonth)
        {
            return Ok(fromBl.AdCategory(selectedMonth));
        }

        [HttpGet("SumCustomerCharge/{selectedMonth}")]
        public IActionResult SumCustomerCharge(string selectedMonth)
        {
            return Ok(fromBl.SumCustomerCharge(selectedMonth));
        }
    }
}
