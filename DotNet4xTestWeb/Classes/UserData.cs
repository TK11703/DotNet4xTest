
namespace DotNet4xTestWeb.Classes
{
	public class UserData
	{
		public string DisplayName { get; set; } = string.Empty;

		public string Email { get; set; } = string.Empty;

		public string JobTitle { get; set; } = string.Empty;

		public string CompanyName { get; set; } = string.Empty;

		public string Address { get; set; } = string.Empty;

		public string City { get; set; } = string.Empty;

		public string StateProvince { get; set; } = string.Empty;

		public string CountryRegion { get; set; } = string.Empty;

		public byte[] Avatar { get; set; } = new byte[0];
	}
}