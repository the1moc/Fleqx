using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using Fleqx.Data.DatabaseModels;

namespace Fleqx.Models
{
    public class TaskModel
    {
        /// <summary>
        /// Gets or sets the task identifier.
        /// </summary>
        /// <value>
        /// The task identifier.
        /// </value>
        public int TaskID { get; set; }

        /// <summary>
        /// Gets or sets the task title.
        /// </summary>
        /// <value>
        /// The task title.
        /// </value>
        [Required]
        public string TaskTitle { get; set; }

        /// <summary>
        /// Gets or sets the task description.
        /// </summary>
        /// <value>
        /// The task description.
        /// </value>
        public string TaskDescription { get; set; }

        /// <summary>
        /// Gets or sets the task priority.
        /// </summary>
        /// <value>
        /// The task priority.
        /// </value>
        [Required]
        public int TaskPriority { get; set; }

        /// <summary>
        /// Gets or sets the original creation date.
        /// </summary>
        /// <value>
        /// The original creation date.
        /// </value>
        public DateTime OriginalCreationDate { get; set; }

        /// <summary>
        /// Gets or sets the original starting date for the task.
        /// </summary>
        /// <value>
        /// The original creation date.
        /// </value>
        public DateTime TaskStartedDate { get; set; }

        /// <summary>
        /// Gets or sets the last renewal date.
        /// </summary>
        /// <value>
        /// The last renewal date.
        /// </value>
        [Required]
        public DateTime LastRenewalDate { get; set; }

        /// <summary>
        /// Gets or sets the critical finish date.
        /// </summary>
        /// <value>
        /// The critical finish date.
        /// </value>
        [Required]
        public DateTime CriticalFinishDate { get; set; }

        /// <summary>
        /// Gets or sets the actual finish date.
        /// </summary>
        /// <value>
        /// The actual finish date.
        /// </value>
        public DateTime ActualFinishDate { get; set; }

        /// <summary>
        /// Gets or sets the estimated days.
        /// </summary>
        /// <value>
        /// The estimated days.
        /// </value>
        [Required]
        public int EstimatedDaysTaken { get; set; }

        /// <summary>
        /// Gets or sets the estimated days.
        /// </summary>
        /// <value>
        /// The estimated days.
        /// </value>
        [Required]
        public int ActualDaysTaken { get; set; }

        /// <summary>
        /// Gets or sets the created user identifier.
        /// </summary>
        /// <value>
        /// The created user identifier.
        /// </value>
        public string CreatedUserId { get; set; }

        /// <summary>
        /// Gets or sets the task state identifier.
        /// </summary>
        /// <value>
        /// The task state identifier.
        /// </value>
        [Required(ErrorMessage = "Required")]
        public int TaskStateId { get; set; }

        /// <summary>
        /// Gets or sets the assigned user identifier.
        /// </summary>
        /// <value>
        /// The assigned user identifier.
        /// </value>
        [Required(ErrorMessage = "Required")]
        public string AssignedUserId { get; set; }

        /// <summary>
        /// Gets or sets all users of the system.
        /// </summary>
        /// <value>
        /// All users.
        /// </value>
        public List<User> AllUsers { get; set; }

        /// <summary>
        /// Foreign key references.
        /// </summary>
        public virtual TaskState TaskState { get; set; }
        public virtual User CreatedUser { get; set; }
        public virtual User AssignedUser { get; set; }
    }
}