using System;
using Fleqx.Data;
using Fleqx.Data.DatabaseModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;

namespace Fleqx.Hubs
{
	public class ChatHub : Hub
	{
		// Save a new message to the page.
		public void Save(string message)
		{
			using (DatabaseContext dbContext = new DatabaseContext())
			{
				dbContext.ChatMessages.Add(new ChatMessage
				{
					ChatContent = message,
					Created     = DateTime.Now,
					UserId      = Context.User.Identity.GetUserId()
				});

				dbContext.SaveChanges();
			}

			// Call the update method for the chat clients.
			Clients.All.update();
		}
	}
}