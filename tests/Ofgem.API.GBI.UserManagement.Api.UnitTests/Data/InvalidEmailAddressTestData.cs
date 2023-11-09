using Xunit;

namespace Ofgem.API.GBI.UserManagement.Api.UnitTests.Data
{
	public class InvalidEmailAddressTestData : TheoryData<string?>
	{
		public InvalidEmailAddressTestData()
		{
			Add(null);
			Add("");
			Add(" ");
			Add($"{new string('a', 251)}@example.com");
		}
	}
}
