using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fleqx.Data.DatabaseModels;

namespace Fleqx.Models
{
	public class TaskModel
	{
		// The task ID (PK).
		public int TaskID { get; set; }

		// The task title.
		public string TaskTitle { get; set; }

		// The task description.
		public string TaskDescription { get; set; }

		// The task importance.
		public int TaskPriority { get; set; }

		// The time the task was created.
		public DateTime OriginalCreationDate { get; set; }

		// The last time the task was renewed.
		public DateTime LastRenewalDate { get; set; }

		// The time the task has to be done.
		public DateTime CriticalFinishDate { get; set; }

		// The estimated number of days it will take to complete.
		public int EstimatedDays { get; set; }

		// The user that created the task.
		public string CreatedUserId { get; set; }

		// The current state of the task.
		public int TaskStateId { get; set; }

		// The user that has been assigned the task.
		public string AssignedUserId { get; set; }

		// Foreign key references
		public virtual TaskState TaskState { get; set; }

		public virtual User CreatedUser { get; set; }

		public virtual User AssignedUser { get; set; }
	}
}