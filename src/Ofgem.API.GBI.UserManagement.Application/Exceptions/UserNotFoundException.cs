namespace Ofgem.API.GBI.UserManagement.Application.Exceptions
{
    public class UserNotFoundException: ApplicationException
    {
        public UserNotFoundException() : base("User not found") { }
    }
}
