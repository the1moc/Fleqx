using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fleqx.Data.DatabaseModels
{
	public class ChatMessage
	{
		// The chat message ID (PK).
		[Required]
		public int ChatMessageID { get; set; }

		// The chat message.
		[DataType(DataType.Text)]
		public string ChatContent { get; set; }

		// The time the message was created.
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