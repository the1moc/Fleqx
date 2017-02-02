using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fleqx.Data.DatabaseModels
{
	public class Announcement
	{
		// The announcement ID (PK).
		public int AnnouncementID { get; set; }

		// The announcement title.
		[DataType(DataType.Text)]
		[Required]
		public string AnnouncementTitle { get; set; }

		// The announcement content.
		[DataType(DataType.MultilineText)]
		[Required]
		public string AnnouncementContent { get; set; }

		// The announcement importance.
		[Required]
		public int AnnouncementImportance { get; set; }

		// The time the announcement was created.
		[Required]
		[DataType(DataType.DateTime)]
		public DateTime Created { get; set; }

		// The user that posted the announcement.
		[Required]
		[DataType(DataType.Text)]
		public string UserId { get; set; }

		[ForeignKey("UserId")]
		public virtual User User { get; set; }
	}
}