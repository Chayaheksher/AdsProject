using Microsoft.AspNetCore.Mvc;
using static finalProject.DAL;

namespace finalProject.WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdsController : ControllerBase
    {
        BL fromBl = new BL();

        [HttpGet("AdToUser/{userId}")]
        public IActionResult AdToUser(int userId, [FromQuery] DateTime? firstDate, [FromQuery] DateTime? endDate)
        {
            return Ok(fromBl.AdToUser(userId, firstDate, endDate));
        }

        [HttpGet("AdToUserWithDate/{userId}/{firstDate}/{endDate}")]
        public IActionResult AdToUserWithDate(int userId, DateTime firstDate, DateTime endDate)
        {
            return Ok(fromBl.AdToUserWithDate(userId, firstDate, endDate));
        }

        [HttpGet("AdDiary/{adId}")]
        public IActionResult AdDiary(int adId)
        {
            return Ok(fromBl.AdDiary(adId));
        }

        [HttpGet("AdApproval/{adId}/{userId}/{currectAdStatus}/{isApproval}/{ishurForAdNote}")]
        public IActionResult AdApproval(int adId, int userId, int currectAdStatus, bool isApproval, string ishurForAdNote)
        {
            var isSuccess = fromBl.AdApproval(adId, userId, currectAdStatus, isApproval, ishurForAdNote);
            return Ok();
        }

        [HttpGet]
        [Route("ShaarAd")]
        public IActionResult ShaarAd()
        {
            return Ok(fromBl.ShaarAd());
        }

        [HttpGet("OutApi/{userId}/{charactersIDInsertRow}")]
        public async Task<IActionResult> OutApi(int userId, int charactersIDInsertRow)
        {
            bool result = await fromBl.OutApi(userId, charactersIDInsertRow);
            if (result)
            {
                return Ok("Data successfully processed.");
            }
            else
            {
                return BadRequest("Failed to process data.");
            }
        }

        [HttpGet]
        [Route("StatusForRejection")]
        public IActionResult StatusForRejection(int adId)
        {
            return Ok(fromBl.StatusForRejection(adId));
        }

        [HttpGet("CancelApprovalOrRejection/{adRowToDelete}/{adRowToUpdate}/{adId}/{userId}")]
        public IActionResult CancelApprovalOrRejection(int adRowToDelete, int adRowToUpdate, int adId, int userId)
        {
            var isSuccess = fromBl.CancelApprovalOrRejection(adRowToDelete, adRowToUpdate, adId, userId);
            if (isSuccess)
            {
                return Ok("Operation succeeded");
            }
            return BadRequest("Operation failed");
        }
    }
}
