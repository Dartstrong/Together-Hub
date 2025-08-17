namespace Application.Security.Exceptions
{
    public class NotValidOrganizerException(string login) : NotValidDataException($"User{login} isn't organizer") { }
}
