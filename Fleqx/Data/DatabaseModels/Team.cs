using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fleqx.Data.DatabaseModels
{
	public class Team
	{
		// The team ID (PK).
		public int TeamID { get; set; }

		// The task state.
		[Required]
		public string TeamTitle { get; set; }

		public virtual ICollection<User> User { get; set; }
	}
}