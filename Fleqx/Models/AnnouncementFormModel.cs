using System.ComponentModel.DataAnnotations;

namespace Fleqx.Models
{
	public class AnnouncementFormModel
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
		[DataType(DataType.MultilineText)]
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
		/// Gets or sets the user identifier.
		/// </summary>
		/// <value>
		/// The user identifier.
		/// </value>
		[Required]
		[DataType(DataType.Text)]
		public string UserId { get; set; }
	}
}