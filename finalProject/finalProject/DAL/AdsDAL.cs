using finalProject.Models;
using finalProject.Models1;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Xml.Linq;
using static finalProject.COMMON.Enums;

namespace finalProject;

public class AdsDAL : IAdsDAL
{
    private readonly AdCycContext _db;

    public AdsDAL()
    {
        _db = new AdCycContext();
    }

    private string ConvertToXml(List<SaveComm> communications, int? userID)
    {
        XElement xml = new XElement("UserCommunication",
            new XElement("User",
                new XElement("UserID", userID ?? -1),
                new XElement("Communications",
                    from comm in communications
                    select new XElement("Communication",
                        new XElement("CommunicationID", comm.communicationID ?? -1),
                        new XElement("CommunicationType", (comm.communicationTypeDescription == ("מייל")) ? 1 :
        (comm.communicationTypeDescription == ("טלפון")) ? 2 : 3),
                        new XElement("content", comm.communicationDetails),
                        new XElement("Main", comm.mainComunication)
                    )
                )
            )
        );

        return xml.ToString();
    }
    public int UpsertUserAndCommunications(SaveUserDedails userDetails)
    {
        try
        {
            RolesEnum charcToInt = Helper.CharactersNameToEnum(userDetails.charactersName);
            string communicationXml = ConvertToXml(userDetails.getCommunication, userDetails.userId);
            var storedProcedureName = "HayaHUpsertUsersAndCommunications";
            var parameters = new[]
            {
                new SqlParameter("userID", userDetails.userId??-1),
                new SqlParameter("charactersID", (int)charcToInt),
                new SqlParameter("fullName", userDetails.fullName),
                new SqlParameter("userName", userDetails.userName),
                new SqlParameter("passwords", userDetails.passwords),
                new SqlParameter("communicationXML", communicationXml)
            };

            return _db.ExecuteStoredProcedure(storedProcedureName, parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error executing HayaHUpsertUserAndCommunications: {ex.Message}");
            throw;
        }
    }

    public int? GetUserRole(int userId)
    {
        return _db.Users.FirstOrDefault(u => u.UserId == userId).CharactersId;
    }

    public List<IshurimForAdClass> AdToUser(int userId, DateTime? firstDate, DateTime? endDate)
    {
        var today = DateTime.Today;
        var userRole = GetUserRole(userId);

        var listIshurim = (from ads in _db.Ads

                           join adsTypes in _db.AdTypes on ads.AdTypeId equals adsTypes.AdTypeId
                           join adsSize in _db.SizesDescriptions on ads.SizeId equals adsSize.SizeDescriptionId
                           join adsLocation in _db.Locations on ads.LocationId equals adsLocation.LocationId
                           join adsIshurim in _db.IshurForAds on ads.AdId equals adsIshurim.AdId
                           join publicactionDate in _db.PublicationDates on adsIshurim.PublicationDateId equals publicactionDate.PublicationDateId
                           join ishurimType in _db.Ishurims on adsIshurim.KodIshur equals ishurimType.KodIshur
                           join ishurAdStatus in _db.StatusAds on ishurimType.StatusAdId equals ishurAdStatus.StatusAdId
                           let maxExecutionDate = _db.IshurForAds
                           .Where(ia => ia.AdId == ads.AdId)
                           .Max(ia => ia.ExecutionDate)
                           where publicactionDate.PublicationDate1 >=
                           (firstDate ?? today) &&
                             (endDate == null || publicactionDate.PublicationDate1 <= endDate) &&
                         adsIshurim.ExecutionDate == maxExecutionDate &&
                                 (userRole == ishurimType.CharactersId || userRole == (int)RolesEnum.manager || userRole == adsIshurim.CharactersIdinsertRow)
                                 && (adsIshurim.KodStatusIshur == (int)StatusIshurEnum.waiting || adsIshurim.KodStatusIshur == (int)StatusIshurEnum.cancelled || adsIshurim.KodStatusIshur == (int)StatusIshurEnum.published)
                           select new IshurimForAdClass
                           {
                               adNum = ads.AdId,
                               characterIdNeedApproval = ishurimType.CharactersId,
                               ishurForAdId = adsIshurim.IshurForAdId,
                               adType = adsTypes.DescriptionAdTypes,
                               size = adsSize.DescriptionSizes,
                               location = adsLocation.DescriptionLocations,
                               status = ishurAdStatus.StatusAdName,
                               nowStatusId = ishurAdStatus.StatusAdId,
                               statusIshur = adsIshurim.KodStatusIshur
                           }).Distinct().ToList();
        return listIshurim;
    }

    public List<IshurimForAdClass> AdToUserWithDate(int userId, DateTime firstDate, DateTime endDate)
    {
        var userRole = GetUserRole(userId);

        var listIshurim = from ads in _db.Ads
                          join publicactionDate
                          in _db.PublicationDates on ads.AdId equals publicactionDate.AdId
                          join adsTypes in _db.AdTypes on ads.AdTypeId equals adsTypes.AdTypeId
                          join adsSize in _db.SizesDescriptions on ads.SizeId equals adsSize.SizeDescriptionId
                          join adsLocation in _db.Locations on ads.LocationId equals adsLocation.LocationId
                          join adsIshurim in _db.IshurForAds on ads.AdId equals adsIshurim.AdId
                          join ishurimType in _db.Ishurims on adsIshurim.KodIshur equals ishurimType.KodIshur
                          join ishurAdStatus in _db.StatusAds on ads.AdStatusId equals ishurAdStatus.StatusAdId
                          let maxExecutionDate = _db.IshurForAds
    .Where(ia => ia.AdId == ads.AdId)
    .OrderByDescending(ia => ia.ExecutionDate)
    .Select(ia => ia.ExecutionDate)
    .FirstOrDefault()


                          where publicactionDate.PublicationDate1 >= firstDate && publicactionDate.PublicationDate1 <= endDate &&
                                adsIshurim.ExecutionDate == maxExecutionDate &&
                                (GetUserRole(userId) == ishurimType.CharactersId || GetUserRole(userId) == (int)RolesEnum.manager)

                          select new IshurimForAdClass
                          {
                              adNum = ads.AdId,
                              adType = adsTypes.DescriptionAdTypes,
                              size = adsSize.DescriptionSizes,
                              location = adsLocation.DescriptionLocations,
                              status = ishurAdStatus.StatusAdName,
                              nowStatusId = ishurAdStatus.StatusAdId
                          };
        return listIshurim.ToList();
    }
    public List<AdDiaryClass> AdDiary(int adId)
    {

        var adDiary = from adsIshurim in _db.IshurForAds
                      join ishurimType in _db.Ishurims on adsIshurim.KodIshur equals ishurimType.KodIshur
                      join users in _db.Users on adsIshurim.UserId equals users.UserId
                      join statusIshur in _db.StatusIshurs on adsIshurim.KodStatusIshur equals statusIshur.StatusIshurId
                      join PublicationDate in _db.PublicationDates on adsIshurim.PublicationDateId equals PublicationDate.PublicationDateId
                      where adsIshurim.AdId == adId &&
                      statusIshur.StatusIshurId != (int)StatusIshurEnum.waiting
                      select new AdDiaryClass
                      {
                          dateAndTime = adsIshurim.ExecutionDate,
                          userName = users.UserName,
                          publicationDates = PublicationDate.PublicationDate1,
                          ishurType = ishurimType.Description,
                          approvalOrRejection = statusIshur.StatusIshurName,
                          approvalOrRejectionCode = statusIshur.StatusIshurId,
                          note = adsIshurim.Note
                      };
        return adDiary.ToList();
    }

    public bool hasMaterialOrPubDatePast(int adId, int currentAdStatus)
    {
        if (currentAdStatus == (int)AdStatusEnum.CustomerMaterials)
            return _db.Ads.Any(x => x.Content != null && x.AdId == adId);
        else
            return _db.PublicationDates.Any(x => x.PublicationDate1 < DateTime.Today && x.AdId == adId);
    }
    public void UpdateApprovalAd(int adId, int userId, StatusIshurEnum statusIshur, string ishurForAdNote)
    {
        var storedProcedureName = "HayaHUpdateApprovalAd";
        var parameters = new[]
        {
        new SqlParameter("AdId", adId),
        new SqlParameter("UserId", userId),
        new SqlParameter("StatusIshur", (int)statusIshur),
        new SqlParameter("IshurForAdNote",ishurForAdNote)
    };
        _db.ExecuteStoredProcedure(storedProcedureName, parameters);
    }
    public void UpdateDateAd(int adId, StatusIshurEnum statusIshur)
    {
        var storedProcedureName = "HayaHUpdateDateAd";
        var parameters = new[]
        {
            new SqlParameter("AdId", adId),
            new SqlParameter("StatusIshur", (int)statusIshur)
        };
        _db.ExecuteStoredProcedure(storedProcedureName, parameters);
    }
    public bool IsSeveralPublicationDates(int adId)
    {
        return _db.PublicationDates.Any(pd => pd.AdId == adId && pd.StatusIshurId != (int)StatusIshurEnum.published);
    }
    public void InsertNextOrReversAdStatus(AdStatusEnum adStatus, RolesEnum rolesThatApproval, int adId, int userId)
    {
        try
        {
            {
                var storedProcedureName = "HayaHInsertNextOrReversAdStatus";
                var parameters = new[]
                {
                    new SqlParameter("userId", userId),
                    new SqlParameter("AdId", adId),
                    new SqlParameter("adStatus", (int)adStatus),
                    new SqlParameter("rolesThatApproval", (int)rolesThatApproval)
                };
                _db.ExecuteStoredProcedure(storedProcedureName, parameters);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    //public void UpdateDateAd(int adId, StatusIshurEnum statusIshur)
    //{
    //    var storedProcedureName = "HayaHUpdateDateAd";
    //    var parameters = new[]
    //    {
    //        new SqlParameter("AdId", adId),
    //        new SqlParameter("StatusIshur", (int)statusIshur)
    //    };
    //    _db.ExecuteStoredProcedure(storedProcedureName, parameters);
    //}
    //public bool IsSeveralPublicationDates(int adId)
    //{
    //    return _db.PublicationDates.Any(pd => pd.AdId == adId && pd.StatusIshurId != (int)StatusIshurEnum.published);
    //}

    //public class ApprovalUsersClass
    //{
    //    public int? userId { get; set; }
    //    public string? userName { get; set; }
    //    public int countIshurim { get; set; }
    //}
    //public List<ApprovalUsersClass> ApprovalUsers(string selectedMonth)
    //{
    //    DateTime parsedDate = DateTime.ParseExact(selectedMonth, "yyyy-MM", null);
    //    var approvalUsers = from adsIshurim in _db.IshurForAds
    //                        join users in _db.Users on adsIshurim.UserId equals users.UserId
    //                        where (adsIshurim.KodStatusIshur == (int)StatusIshurEnum.approved) &&
    //                         adsIshurim.ExecutionDate == parsedDate
    //                        group adsIshurim by new { adsIshurim.UserId, users.UserName } into grouped
    //                        select new ApprovalUsersClass
    //                        {
    //                            userId = grouped.Key.UserId,
    //                            userName = grouped.Key.UserName,
    //                            countIshurim = grouped.Count()
    //                        };
    //    return approvalUsers.ToList();
    //}

    //public class WhereStatusAdsClass
    //{
    //    public string? adStatusName { get; set; }
    //    public int countAdsInStatus { get; set; }
    //}
    //public List<WhereStatusAdsClass> WhereStatusAds(string selectedMonth)
    //{
    //    DateTime parsedDate = DateTime.ParseExact(selectedMonth, "yyyy-MM", null);
    //    var whereStatusAds = from adsIshurim in _db.IshurForAds
    //                         join ishurim in _db.Ishurims on adsIshurim.KodIshur equals ishurim.KodIshur
    //                         join statusAd in _db.StatusAds on ishurim.StatusAdId equals statusAd.StatusAdId
    //                         where adsIshurim.ExecutionDate == parsedDate
    //                         group statusAd by new { statusAd.StatusAdId, statusAd.StatusAdName } into grouped
    //                         select new WhereStatusAdsClass
    //                         {
    //                             adStatusName = grouped.Key.StatusAdName,
    //                             countAdsInStatus = grouped.Count()
    //                         };
    //    return whereStatusAds.ToList();
    //}

    //public class AdCategoryClass
    //{
    //    public string? adTypesName { get; set; }
    //    public int countAdsInType { get; set; }
    //}
    //public List<AdCategoryClass> AdCategory(string selectedMonth)
    //{
    //    DateTime parsedDate = DateTime.ParseExact(selectedMonth, "yyyy-MM", null);
    //    var adCategory = from ads in _db.Ads
    //                     join adsType in _db.AdTypes on ads.AdTypeId equals adsType.AdTypeId
    //                     join publicationDate in _db.PublicationDates on ads.AdId equals publicationDate.AdId
    //                     where publicationDate.PublicationDate1 == parsedDate
    //                     group adsType by new { adsType.AdTypeId, adsType.DescriptionAdTypes } into grouped
    //                     select new AdCategoryClass
    //                     {
    //                         adTypesName = grouped.Key.DescriptionAdTypes,
    //                         countAdsInType = grouped.Count()
    //                     };
    //    return adCategory.ToList();
    //}

    //public class sumCustomerCharge
    //{
    //    public string customerName { get; set; }
    //    public decimal? sumCharge { get; set; }
    //}
    //public List<sumCustomerCharge> SumCustomerCharge(string selectedMonth)
    //{
    //    DateTime parsedDate = DateTime.ParseExact(selectedMonth, "yyyy-MM", null);
    //    var sumCustomerCharge = from customers in _db.Customers
    //                            join customerCharge in _db.CustomerCharges on customers.CustomerId equals customerCharge.CustomerId
    //                            where customerCharge.ChargeDate == parsedDate
    //                            group customerCharge by new { customerCharge.CustomerId, customers.CustomerName } into grouped
    //                            select new sumCustomerCharge
    //                            {
    //                                customerName = grouped.Key.CustomerName,
    //                                sumCharge = grouped.Sum(x => x.ChargeAmount)
    //                            };
    //    return sumCustomerCharge.ToList();
    //}

    public class StatusForRejectionClass
    {
        public DateTime? executionDate { get; set; }
        public int? ishurForAdRow { get; set; }
        public string? userName { get; set; }
        public string? approvalOrRejection { get; set; }
        public string? approvalType { get; set; }
    }
    public List<StatusForRejectionClass> StatusForRejection(int adId)
    {
        var statusForRejection = from IshurForAd in _db.IshurForAds
                                 join User in _db.Users on IshurForAd.UserId equals User.UserId
                                 join StatusIshur in _db.StatusIshurs on IshurForAd.KodStatusIshur equals StatusIshur.StatusIshurId
                                 join Ishurim in _db.Ishurims on IshurForAd.KodIshur equals Ishurim.KodIshur
                                 join StatusAd in _db.StatusAds on Ishurim.StatusAdId equals StatusAd.StatusAdId
                                 where IshurForAd.AdId == adId && (IshurForAd.KodStatusIshur == (int)StatusIshurEnum.approved || IshurForAd.KodStatusIshur == (int)StatusIshurEnum.rejected)
                                 orderby IshurForAd.IshurForAdId descending
                                 select new StatusForRejectionClass
                                 {
                                     executionDate = IshurForAd.ExecutionDate,
                                     ishurForAdRow = IshurForAd.IshurForAdId,
                                     userName = User.UserName,
                                     approvalOrRejection = StatusIshur.StatusIshurName,
                                     approvalType = StatusAd.StatusAdName
                                 };
        return statusForRejection.ToList();
    }

    public bool DeleteTheNextStatus(int adRowToDelete)
    {
        var storedProcedureName = "HayaHDeleteTheNextStatus";
        var parameters = new[]
        {
        new SqlParameter("adRowToDelete", adRowToDelete),
    };
        var result = _db.ExecuteStoredProcedure(storedProcedureName, parameters);
        return result > 0;
    }

    public bool UpdateTheCanceledStatus(int adRowToUpdate, int adId, int userId)
    {
        var storedProcedureName = "HayaHUpdateTheCanceledStatus";
        var parameters = new[]
        {
        new SqlParameter("adRowToUpdate", adRowToUpdate),
        new SqlParameter("userId", userId),
        new SqlParameter("adId", adId)
    };
        var result = _db.ExecuteStoredProcedure(storedProcedureName, parameters);
        return result > 0;
    }

    public class Shearim
    {
        public string dolar { get; set; }
        public string yuro { get; set; }
        public string ribit { get; set; }
    }
    public Shearim ShaarAd()
    {
        string dolarUrl = "https://boi.org.il/PublicApi/GetExchangeRate?Key=USD";
        string yuroUrl = "https://boi.org.il/PublicApi/GetExchangeRate?Key=EUR";
        string ribitUrl = "https://boi.org.il/PublicApi/GetInterest";
        Shearim shearim = new Shearim() { dolar = dolarUrl, yuro = yuroUrl, ribit = yuroUrl };
        return shearim;
    }

    public bool OutApi(string shearim, int userId, int charactersIDInsertRow)
    {
        var storedProcedureName = "HayaHOutApi";
        var parameters = new[]
        {
        new SqlParameter("shearim", shearim),
        new SqlParameter("userId", userId),
        new SqlParameter("charactersIDInsertRow", charactersIDInsertRow),
    };
        var result = _db.ExecuteStoredProcedure(storedProcedureName, parameters);
        return result > 0;
    }
}


