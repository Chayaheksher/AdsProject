
using finalProject.Models;
using finalProject.Models1;
using System.Text.Json;
using static finalProject.COMMON.Enums;

namespace finalProject
{
    public class ConnectionBL
    {
        ConnectionDAL fromDal = new ConnectionDAL();
        public async Task<UserLogin> UserLogin(string userName, int userPassword)
        {
            return await fromDal.UserLogin(userName, userPassword);
        }
    }
}
