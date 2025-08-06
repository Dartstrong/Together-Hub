namespace Application.Security.Exceptions
{
    public class NotValidUsernameException(string login) : NotValidDataException($"Username {login} is already in use") { }
}
