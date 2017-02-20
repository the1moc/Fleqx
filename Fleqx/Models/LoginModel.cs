using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Fleqx.Models
{
	public class LoginModel
	{
		/// <summary>
		/// Gets or sets the name of the user.
		/// </summary>
		/// <value>
		/// The name of the user.
		/// </value>
		[Required]
		[DataType(DataType.Text)]
		public string UserName { get; set; }

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		/// <value>
		/// The password.
		/// </value>
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		/// <summary>
		/// Gets or sets the requested URL.
		/// </summary>
		/// <value>
		/// The requested URL.
		/// </value>
		[HiddenInput]
		[DataType(DataType.Text)]
		public string RequestedUrl { get; set; }
	}
}