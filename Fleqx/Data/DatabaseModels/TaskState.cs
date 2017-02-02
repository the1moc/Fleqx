using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fleqx.Data.DatabaseModels
{
	public class TaskState
	{
		// The task state ID (PK).
		public int TaskStateID { get; set; }

		// The task state.
		[Required]
		public string TaskStateCurrent { get; set; }

		public virtual ICollection<Task> Tasks { get; set; }
	}
}