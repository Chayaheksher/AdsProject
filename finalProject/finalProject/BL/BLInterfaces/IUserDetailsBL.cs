using finalProject.Models;
using finalProject.Models1;
using System.Collections.Generic;

namespace finalProject
{
    public interface IUserDetailsBL
    {
        GetUsersDetails4[] GetUsers();
        List<string> RoleName();
        communicationClass[] GetCommunication();
        string[] CommunicationName();
        void UpsertUserAndCommunications(SaveUserDedails userDetails);
    }
}