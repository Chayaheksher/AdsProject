using finalProject.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using static finalProject.AdsDAL;
using static finalProject.COMMON.Enums;

namespace finalProject
{
    public interface IAdsDAL
    {
        int UpsertUserAndCommunications(SaveUserDedails userDetails);
        int? GetUserRole(int userId);
        List<IshurimForAdClass> AdToUser(int userId, DateTime? firstDate, DateTime? endDate);
        List<IshurimForAdClass> AdToUserWithDate(int userId, DateTime firstDate, DateTime endDate);
        List<AdDiaryClass> AdDiary(int adId);
        bool hasMaterialOrPubDatePast(int adId, int currentAdStatus);
        void UpdateApprovalAd(int adId, int userId, StatusIshurEnum statusIshur, string ishurForAdNote);
        void UpdateDateAd(int adId, StatusIshurEnum statusIshur);
        bool IsSeveralPublicationDates(int adId);
        void InsertNextOrReversAdStatus(AdStatusEnum adStatus, RolesEnum rolesThatApproval, int adId, int userId);
        List<StatusForRejectionClass> StatusForRejection(int adId);
        bool DeleteTheNextStatus(int adRowToDelete);
        bool UpdateTheCanceledStatus(int adRowToUpdate, int adId, int userId);
        Shearim ShaarAd();
        bool OutApi(string shearim, int userId, int charactersIDInsertRow);
    }
}

