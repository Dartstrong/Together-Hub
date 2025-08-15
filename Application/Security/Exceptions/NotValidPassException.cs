namespace Application.Security.Exceptions
{
    public class NotValidPassException() : NotValidDataException($"The password isn't valid") { }
}
