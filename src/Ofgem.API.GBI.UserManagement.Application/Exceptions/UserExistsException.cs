namespace Ofgem.API.GBI.UserManagement.Application.Exceptions
{
	public class UserExistsException : ApplicationException
	{
		public UserExistsException() : base("User already exists") { }
	}
}
