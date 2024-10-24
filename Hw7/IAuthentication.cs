

public interface IAuthentication
{
    bool UserLogin(string userName, string password);
    bool UserRegister(string fullName, string userName, string email, string password, string addres);
}

