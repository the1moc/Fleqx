using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Fleqx.Data.DatabaseModels
{
	public class User : IdentityUser
	{
		// First name of the user.
		[DataType(DataType.Text)]
		[Required]
		public string FirstName { get; set; }

		// Second name of the user.
		[DataType(DataType.Text)]
		[Required]
		public string LastName { get; set; }

		// The team that person is in.
		[Required]
		public int TeamId { get; set; }

		[ForeignKey("TeamId")]
		public virtual Team Team { get; set; }

		public virtual ICollection<Announcement> Announcements { get; set; }

		public virtual ICollection<Task> CreatedTasks { get; set; }
		public virtual ICollection<Task> AssignedTasks { get; set; }
	}
}