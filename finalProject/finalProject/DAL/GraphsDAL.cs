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

public class GraphsDAL
{
    private readonly AdCycContext _db;

    public GraphsDAL()
    {
        _db = new AdCycContext();
    }

    public class ApprovalUsersClass
    {
        public int? userId { get; set; }
        public string? userName { get; set; }
        public int countIshurim { get; set; }
    }
    public List<ApprovalUsersClass> ApprovalUsers(string selectedMonth)
    {
        DateTime parsedDate = DateTime.ParseExact(selectedMonth, "yyyy-MM", null);
        var approvalUsers = from adsIshurim in _db.IshurForAds
                            join users in _db.Users on adsIshurim.UserId equals users.UserId
                            where (adsIshurim.KodStatusIshur == (int)StatusIshurEnum.approved) &&
                             adsIshurim.ExecutionDate == parsedDate
                            group adsIshurim by new { adsIshurim.UserId, users.UserName } into grouped
                            select new ApprovalUsersClass
                            {
                                userId = grouped.Key.UserId,
                                userName = grouped.Key.UserName,
                                countIshurim = grouped.Count()
                            };
        return approvalUsers.ToList();
    }

    public class WhereStatusAdsClass
    {
        public string? adStatusName { get; set; }
        public int countAdsInStatus { get; set; }
    }
    public List<WhereStatusAdsClass> WhereStatusAds(string selectedMonth)
    {
        DateTime parsedDate = DateTime.ParseExact(selectedMonth, "yyyy-MM", null);
        var whereStatusAds = from adsIshurim in _db.IshurForAds
                             join ishurim in _db.Ishurims on adsIshurim.KodIshur equals ishurim.KodIshur
                             join statusAd in _db.StatusAds on ishurim.StatusAdId equals statusAd.StatusAdId
                             where adsIshurim.ExecutionDate == parsedDate
                             group statusAd by new { statusAd.StatusAdId, statusAd.StatusAdName } into grouped
                             select new WhereStatusAdsClass
                             {
                                 adStatusName = grouped.Key.StatusAdName,
                                 countAdsInStatus = grouped.Count()
                             };
        return whereStatusAds.ToList();
    }

    public class AdCategoryClass
    {
        public string? adTypesName { get; set; }
        public int countAdsInType { get; set; }
    }
    public List<AdCategoryClass> AdCategory(string selectedMonth)
    {
        DateTime parsedDate = DateTime.ParseExact(selectedMonth, "yyyy-MM", null);
        var adCategory = from ads in _db.Ads
                         join adsType in _db.AdTypes on ads.AdTypeId equals adsType.AdTypeId
                         join publicationDate in _db.PublicationDates on ads.AdId equals publicationDate.AdId
                         where publicationDate.PublicationDate1 == parsedDate
                         group adsType by new { adsType.AdTypeId, adsType.DescriptionAdTypes } into grouped
                         select new AdCategoryClass
                         {
                             adTypesName = grouped.Key.DescriptionAdTypes,
                             countAdsInType = grouped.Count()
                         };
        return adCategory.ToList();
    }

    public class sumCustomerCharge
    {
        public string customerName { get; set; }
        public decimal? sumCharge { get; set; }
    }
    public List<sumCustomerCharge> SumCustomerCharge(string selectedMonth)
    {
        DateTime parsedDate = DateTime.ParseExact(selectedMonth, "yyyy-MM", null);
        var sumCustomerCharge = from customers in _db.Customers
                                join customerCharge in _db.CustomerCharges on customers.CustomerId equals customerCharge.CustomerId
                                where customerCharge.ChargeDate == parsedDate
                                group customerCharge by new { customerCharge.CustomerId, customers.CustomerName } into grouped
                                select new sumCustomerCharge
                                {
                                    customerName = grouped.Key.CustomerName,
                                    sumCharge = grouped.Sum(x => x.ChargeAmount)
                                };
        return sumCustomerCharge.ToList();
    }
}


