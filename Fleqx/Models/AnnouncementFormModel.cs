using System.ComponentModel.DataAnnotations;

namespace Fleqx.Models
{
	public class AnnouncementFormModel
	{
		// The announcement ID (PK).
		[Required]
		public int AnnouncementID { get; set; }

		// The announcement title.
		[Required]
		[DataType(DataType.MultilineText)]
		public string AnnouncementTitle { get; set; }

		// The announcement content.
		[Required]
		[DataType(DataType.Text)]
		public string AnnouncementContent { get; set; }

		[Required]
		// The announcement importance.
		public int AnnouncementImportance { get; set; }

		[Required]
		[DataType(DataType.Text)]
		// The user that posted the announcement.
		public string UserId { get; set; }
	}
}