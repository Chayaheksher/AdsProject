using finalProject.Models;
using finalProject.Models1;
using static finalProject.COMMON.Enums;
using System.Threading.Tasks;
using finalProject;

namespace finalProject
{
    public interface IAdsBL
    {
        List<IshurimForAdClass> AdToUser(int userId, DateTime? firstDate, DateTime? endDate);
        List<IshurimForAdClass> AdToUserWithDate(int UserId, DateTime firstDate, DateTime endDate);
        List<AdDiaryClass> AdDiary(int adId);
        List<AdsDAL.StatusForRejectionClass> StatusForRejection(int adId);
        AdsDAL.Shearim ShaarAd();

        bool AdApproval(int adId, int userId, int currentAdStatus, bool isApproval, string ishurForAdNote);
        bool CancelApprovalOrRejection(int adRowToDelete, int adRowToUpdate, int adId, int userId);

        Task<bool> OutApi(int userId, int charactersIDInsertRow);
    }
}