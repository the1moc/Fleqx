using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Fleqx.Data;
using Fleqx.Data.DatabaseModels;
using Fleqx.Models;

namespace Fleqx.Controllers
{
	/// <summary>
	/// The controller that resides over the chat component of the application.
	/// </summary>
	/// <seealso cref="Fleqx.Controllers.BaseController" />
	public class ChatController : BaseController
	{
		/// <summary>
		/// Display the chat view.
		/// </summary>
		/// <returns></returns>
		[ChildActionOnly]
		public ActionResult Chat()
		{
			return View();
		}

		/// <summary>
		/// Return all the chat messages.
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult ChatMessages()
		{
			using (var dbContext = GetDatabaseContext())
			{
				List<ChatMessage> chatMessages = dbContext.ChatMessages.ToList();

				// Create the view models to be passed to the announcements view
				List<ChatMessageModel> models = chatMessages.OrderBy(chatMessage => chatMessage.Created).Select(chatMessage =>
				{
					return new ChatMessageModel
					{
						ChatMessageID = chatMessage.ChatMessageID,
						ChatMessageContent = chatMessage.ChatContent,
						Created = chatMessage.Created,
						User = chatMessage.User
					};
				}).ToList();

				return PartialView("_ChatMessages", models);
			}
		}
	}
}