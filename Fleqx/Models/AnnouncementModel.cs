using System;
using System.ComponentModel.DataAnnotations;
using Fleqx.Data.DatabaseModels;

namespace Fleqx.Models
{
	public class AnnouncementModel
	{
		/// <summary>
		/// Gets or sets the announcement identifier.
		/// </summary>
		/// <value>
		/// The announcement identifier.
		/// </value>
		[Required]
		public int AnnouncementID { get; set; }

		/// <summary>
		/// Gets or sets the announcement title.
		/// </summary>
		/// <value>
		/// The announcement title.
		/// </value>
		[Required]
		[DataType(DataType.Text)]
		public string AnnouncementTitle { get; set; }

		/// <summary>
		/// Gets or sets the content of the announcement.
		/// </summary>
		/// <value>
		/// The content of the announcement.
		/// </value>
		[Required]
		[DataType(DataType.Text)]
		public string AnnouncementContent { get; set; }

		/// <summary>
		/// Gets or sets the announcement importance.
		/// </summary>
		/// <value>
		/// The announcement importance.
		/// </value>
		[Required]
		public int AnnouncementImportance { get; set; }

		/// <summary>
		/// Gets or sets the created.
		/// </summary>
		/// <value>
		/// The created.
		/// </value>
		[Required]
		[DataType(DataType.DateTime)]
		public DateTime Created { get; set; }

		/// <summary>
		/// Gets or sets the user.
		/// </summary>
		/// <value>
		/// The user.
		/// </value>
		[Required]
		public User User { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can be edited by the user.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can edit; otherwise, <c>false</c>.
        /// </value>
        public bool CanEdit { get; set; }
	}
}