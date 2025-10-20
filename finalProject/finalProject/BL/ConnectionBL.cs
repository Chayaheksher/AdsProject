
using finalProject.Models;
using finalProject.Models1;
using System.Text.Json;
using static finalProject.COMMON.Enums;

namespace finalProject
{
    public class ConnectionBL:IConnectionBL
    {
        private readonly IConnectionDAL fromDal;

        public ConnectionBL(IConnectionDAL connectionDAL)
        {
            fromDal = connectionDAL;
        }
        public async Task<UserLogin> UserLogin(string userName, int userPassword)
        {
            return await fromDal.UserLogin(userName, userPassword);
        }
    }
}
