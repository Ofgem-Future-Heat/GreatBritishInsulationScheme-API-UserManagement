using System.Text.Json.Serialization;

namespace Ofgem.Database.GBI.Users.Domain.Enums
{
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum ExternalUserType
	{
		AuthorisedSignatoryRole,
		AdditionalUserRole
	}
}
