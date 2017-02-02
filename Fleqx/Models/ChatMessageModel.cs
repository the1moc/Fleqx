using System;
using System.ComponentModel.DataAnnotations;
using Fleqx.Data.DatabaseModels;

namespace Fleqx.Models
{
	public class ChatMessageModel
	{
		// The chat message ID.
		[Required]
		public int ChatMessageID { get; set; }

		// The chat content.
		[Required]
		[DataType(DataType.Text)]
		public string ChatMessageContent { get; set; }

		// When the message was created.
		[Required]
		[DataType(DataType.DateTime)]
		public DateTime Created { get; set; }

		// The user that posted the announcement.
		public User User { get; set; }
	}
}