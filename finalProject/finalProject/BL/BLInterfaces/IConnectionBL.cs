using finalProject.Models;

public interface IConnectionBL
{
    Task<UserLogin> UserLogin(string userName, int userPassword);
}