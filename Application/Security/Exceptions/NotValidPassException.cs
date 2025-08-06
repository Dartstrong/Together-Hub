namespace Application.Security.Exceptions
{
    public class NotValidPassException() : NotValidDataException($"The password is not valid") { }
}
