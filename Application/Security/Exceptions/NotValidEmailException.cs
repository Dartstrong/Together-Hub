namespace Application.Security.Exceptions
{
    public class NotValidEmailException(string email) : NotValidDataException($"Email {email} is already in use"){}
}
