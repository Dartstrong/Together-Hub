namespace Application.Security.Exceptions
{
    public class NotValidEmailException(string email) : NotValidDataException($"Email {email} isn't valid"){}
}
