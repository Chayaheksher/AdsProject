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

public class UserDetailsDAL: IUserDetailsDAL
{
    private readonly AdCycContext _db;

    public UserDetailsDAL()
    {
        _db = new AdCycContext();
    }

    public GetUsersDetails4[] GetUsers()
    {
        return _db.GetUsersDetails4s.ToArray();
    }

    public List<string> RoleName()
    {
        return _db.Characters.Select(x => x.CharactersName).ToList();
    }

    public communicationClass[] GetCommunication()
    {
        var commounication = from cu in _db.CommunicationUsers
                             join ct in _db.CommunicationTypes
                             on cu.CommunicationType equals ct.CommunicationType1
                             join u in _db.Users
                             on cu.UserId equals u.UserId
                             select new communicationClass
                             {
                                 userId = u.UserId,
                                 CommunicationTypeDescription = ct.Description,
                                 communicationID = cu.CommunicationId,
                                 communicationDetails = cu.Contant,
                                 mainComunication = cu.Main
                             };
        return commounication.ToArray();
    }

    public string[] CommunicationName()
    {
        return _db.CommunicationTypes.Select(x => x.Description).ToArray();
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
}


