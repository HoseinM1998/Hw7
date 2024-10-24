
using System.Data;

public class Authentication : IAuthentication
{
    public bool UserLogin(string username, string password)
    {
        foreach (var user in Storage.Users)
        {
            if (user is not null && user.UserName == username && user.Password == password)
            {
                Storage.OnlineUser = user;
                return true;
            }
        }
        return false;
    }

    public bool UserRegister(string fullName, string userName, string email, string password, string addres)
    {
        foreach (var user in Storage.Users)
        {
            if (user is not null && user.UserName == userName)
            {
                return false;
            }
        }
        Storage.Users.Add(new User(fullName, userName, email, password, addres));
        return true;
    }
}
