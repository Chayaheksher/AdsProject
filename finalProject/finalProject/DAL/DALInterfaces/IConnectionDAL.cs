using finalProject.Models;

public interface IConnectionDAL
{
    Task<UserLogin> UserLogin(string userName, int userPassword);
}