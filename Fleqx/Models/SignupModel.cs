using System.ComponentModel.DataAnnotations;

namespace Fleqx.Models
{
	public class SignupModel
	{
		// The username field.
		[Required]
		[DataType(DataType.Text)]
		public string UserName { get; set; }

		// The password field.
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		// The first name field.
		[Required]
		[DataType(DataType.Text)]
		public string FirstName { get; set; }

		// The last name field.
		[Required]
		[DataType(DataType.Text)]
		public string LastName { get; set; }

		// The user role ID.
		[Required]
		public int Role { get; set; }
	}
}