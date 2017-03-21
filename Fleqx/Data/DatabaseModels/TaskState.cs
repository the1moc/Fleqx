using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fleqx.Data.DatabaseModels
{
	public class TaskState
	{
		/// <summary>
		/// Gets or sets the task state identifier.
		/// </summary>
		/// <value>
		/// The task state identifier.
		/// </value>
		public int TaskStateID { get; set; }

		/// <summary>
		/// Gets or sets the current task state.
		/// </summary>
		/// <value>
		/// The current task state.
		/// </value>
		[Required]
		public string TaskStateCurrent { get; set; }

		public virtual ICollection<Task> Tasks { get; set; }
	}
}