namespace Application.Security.Exceptions
{
    public class NotValidUsernameException(string login) : NotValidDataException($"Username {login} isn't valid") { }
}
