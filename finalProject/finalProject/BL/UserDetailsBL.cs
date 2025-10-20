
using finalProject.Models;
using finalProject.Models1;
using System.Text.Json;
using static finalProject.COMMON.Enums;

namespace finalProject
{
    public class UserDetailsBL:IUserDetailsBL
    {
        private readonly IUserDetailsDAL fromDal;

        public UserDetailsBL(IUserDetailsDAL userDetailsDAL)
        {
            fromDal = userDetailsDAL;
        }
        public GetUsersDetails4[] GetUsers()
        {
            return fromDal.GetUsers();
        }

        public List<string> RoleName()
        {
            return fromDal.RoleName();
        }

        public communicationClass[] GetCommunication()
        {
            return fromDal.GetCommunication();
        }

        public string[] CommunicationName()
        {
            return fromDal.CommunicationName();
        }

        public void UpsertUserAndCommunications(SaveUserDedails userDetails)
        {
            try
            {
                if (userDetails == null)
                {
                    throw new ArgumentNullException(nameof(userDetails), "User details cannot be null.");
                }
                 fromDal.UpsertUserAndCommunications(userDetails);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpsertUserAndCommunications BL: {ex.Message}");
                throw;
            }
        }
    }
}
