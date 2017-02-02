using System;
using System.ComponentModel.DataAnnotations;
using Fleqx.Data.DatabaseModels;

namespace Fleqx.Models
{
	public class AnnouncementModel
	{
		// The announcement ID (PK).
		[Required]
		public int AnnouncementID { get; set; }

		// The announcement title.
		[Required]
		[DataType(DataType.Text)]
		public string AnnouncementTitle { get; set; }

		// The announcement content.
		[Required]
		[DataType(DataType.Text)]
		public string AnnouncementContent { get; set; }

		// The announcement importance.
		[Required]
		public int AnnouncementImportance { get; set; }

		// When the announcement was created.
		[Required]
		[DataType(DataType.DateTime)]
		public DateTime Created { get; set; }

		// The user that posted the announcement.
		[Required]
		public User User { get; set; }
	}
}