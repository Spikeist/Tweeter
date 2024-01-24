using final_projectAPI.Models;

namespace final_projectAPI.Repositories;

public interface IAuthService 
{
    User CreateUser(User user);
    string SignIn(string username, string password);
}