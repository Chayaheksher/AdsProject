using finalProject.Models;
using finalProject.Models1;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace finalProject
{
    public interface IUserDetailsDAL
    {
        GetUsersDetails4[] GetUsers();
        List<string> RoleName();
        communicationClass[] GetCommunication();
        string[] CommunicationName();
        int UpsertUserAndCommunications(SaveUserDedails userDetails);
        List<UserDetailsDAL.ApprovalUsersClass> ApprovalUsers(string selectedMonth);
        List<UserDetailsDAL.WhereStatusAdsClass> WhereStatusAds(string selectedMonth);
    }
}
