using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Fleqx.Models
{
	public class LoginModel
	{
		// The username field.
		[Required]
		[DataType(DataType.Text)]
		public string UserName { get; set; }

		// The password field.
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		// Reuqested url.
		[HiddenInput]
		[DataType(DataType.Text)]
		public string RequestedUrl { get; set; }
	}
}