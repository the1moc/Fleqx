using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fleqx.Data.DatabaseModels
{
	public class Task
	{
		// The task ID (PK).
		public int TaskID { get; set; }

		// The task title.
		[DataType(DataType.Text)]
		[Required]
		public string TasktTitle { get; set; }

		// The task description.
		[DataType(DataType.MultilineText)]
		[Required]
		public string TaskDescription { get; set; }

		// The task importance.
		[Required]
		public int TaskPriority { get; set; }

		// The time the task was created.
		[Required]
		[DataType(DataType.DateTime)]
		public DateTime OriginalCreationDate { get; set; }

		// The last time the task was renewed.
		[Required]
		[DataType(DataType.DateTime)]
		public DateTime LastRenewalDate { get; set; }

		// The time the task has to be done.
		[Required]
		[DataType(DataType.DateTime)]
		public DateTime CriticalFinishDate { get; set; }

		// The estimated number of days it will take to complete.
		[Required]
		public int EstimatedDays { get; set; }

		// The user that created the task.
		[Required]
		[DataType(DataType.Text)]
		public string CreatedUserId { get; set; }

		// The current state of the task.
		[Required]
		public int TaskStateId { get; set; }

		// The user that has been assigned the task.
		[Required]
		[DataType(DataType.Text)]
		public string AssignedUserId { get; set; }

		// Foreign key references
		[ForeignKey("TaskStateId")]
		public virtual TaskState TaskState { get; set; }

		[ForeignKey("CreatedUserId")]
		public virtual User CreatedUser { get; set; }

		[ForeignKey("AssignedUserId")]
		public virtual User AssignedUser { get; set; }
	}
}