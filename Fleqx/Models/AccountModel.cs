using System.ComponentModel.DataAnnotations;

namespace Fleqx.Models
{
	public class AccountModel
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
		/// Gets or sets the first name.
		/// </summary>
		/// <value>
		/// The first name.
		/// </value>
		[Required]
		[DataType(DataType.Text)]
		public string FirstName { get; set; }

		/// <summary>
		/// Gets or sets the last name.
		/// </summary>
		/// <value>
		/// The last name.
		/// </value>
		[Required]
		[DataType(DataType.Text)]
		public string LastName { get; set; }

		/// <summary>
		/// Gets or sets the role.
		/// </summary>
		/// <value>
		/// The role.
		/// </value>
		[Required]
		public int Role { get; set; }

		/// <summary>
		/// Gets or sets the team.
		/// </summary>
		/// <value>
		/// The team.
		/// </value>
		[Required]
		public int Team { get; set; }
	}
}