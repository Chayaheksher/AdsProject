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
            try
            {
                return Ok(fromBl.AdToUser(userId, firstDate, endDate));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + " בעיה בקונטרולר");
            }
        }
        [HttpGet("AdToUserWithDate/{userId}/{firstDate}/{endDate}")]
        public IActionResult AdToUserWithDate(int userId, DateTime firstDate, DateTime endDate)
        {
            try
            {
                return Ok(fromBl.AdToUserWithDate(userId, firstDate, endDate));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + " בעיה בקונטרולר");
            }
        }

        [HttpGet("AdDiary/{adId}")]
        public IActionResult AdDiary(int adId)
        {
            try
            {
                return Ok(fromBl.AdDiary(adId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + " בעיה בקונטרולר");
            }
        }
        [HttpGet("AdApproval/{adId}/{userId}/{currectAdStatus}/{isApproval}/{ishurForAdNote}")]
        public IActionResult AdApproval(int adId, int userId, int currectAdStatus, bool isApproval, string ishurForAdNote)
        {
            try
            {
                var isSuccess = fromBl.AdApproval(adId, userId, currectAdStatus, isApproval, ishurForAdNote);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + " בעיה בקונטרולר");
            }
        }
        [HttpGet]
        [Route("ShaarAd")]
        public IActionResult ShaarAd()
        {
            try
            {
                return Ok(fromBl.ShaarAd());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + " בעיה בקונטרולר");
            }
        }

        [HttpGet("OutApi/{userId}/{charactersIDInsertRow}")]
        public async Task<IActionResult> OutApi(int userId, int charactersIDInsertRow)
        {
            try
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
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }


        [HttpGet]
        [Route("StatusForRejection")]
        public IActionResult StatusForRejection(int adId)
        {
            try
            {
                return Ok(fromBl.StatusForRejection(adId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("CancelApprovalOrRejection/{adRowToDelete}/{adRowToUpdate}/{adId}/{userId}")]
        public IActionResult CancelApprovalOrRejection(int adRowToDelete, int adRowToUpdate, int adId, int userId)
        {
            try
            {
                var isSuccess =  fromBl.CancelApprovalOrRejection(adRowToDelete, adRowToUpdate, adId, userId);
                if (isSuccess)
                {
                    return Ok("Operation succeeded");
                }
                return BadRequest("Operation failed");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message} - בעיה בקונטרולר");
            }
        }
    }
}
