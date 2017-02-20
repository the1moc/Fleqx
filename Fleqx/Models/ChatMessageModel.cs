using System;
using System.ComponentModel.DataAnnotations;
using Fleqx.Data.DatabaseModels;

namespace Fleqx.Models
{
	public class ChatMessageModel
	{
		/// <summary>
		/// Gets or sets the chat message identifier.
		/// </summary>
		/// <value>
		/// The chat message identifier.
		/// </value>
		[Required]
		public int ChatMessageID { get; set; }

		/// <summary>
		/// Gets or sets the content of the chat message.
		/// </summary>
		/// <value>
		/// The content of the chat message.
		/// </value>
		[Required]
		[DataType(DataType.Text)]
		public string ChatMessageContent { get; set; }

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
		public User User { get; set; }
	}
}