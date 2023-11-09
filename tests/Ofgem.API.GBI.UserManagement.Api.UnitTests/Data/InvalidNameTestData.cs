using Xunit;

namespace Ofgem.API.GBI.UserManagement.Api.UnitTests.Data
{
	public class InvalidNameTestData : TheoryData<string?>
	{
		public InvalidNameTestData()
		{
			Add(null);
			Add("");
			Add(" ");
			Add(new string('a', 51));
		}
	}
}
