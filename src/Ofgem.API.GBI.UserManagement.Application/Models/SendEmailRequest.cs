namespace Ofgem.API.GBI.UserManagement.Application.Models
{
	public class SendEmailRequest
	{
		public required string TemplateId { get; set; }
		public Dictionary<string, dynamic>? Placeholders { get; set; }
		public required string Recipient { get; set; }
	}
}
