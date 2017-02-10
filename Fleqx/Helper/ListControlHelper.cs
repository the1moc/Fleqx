using System.Collections.Generic;
using System.Web.Mvc;
using Fleqx.Controllers;

namespace Fleqx.Data.DatabaseModels
{
	public static class ListControlHelper
	{
		// The Roles available.
		public static IEnumerable<SelectListItem> Roles = new List<SelectListItem>
		{
			new SelectListItem
			{
				Value = "1",
				Text = "Admin"
			},
			new SelectListItem
			{
				Value = "2",
				Text = "Standard"
			},
			new SelectListItem
			{
				Value = "3",
				Text = "Guest"
			}
		};

		// The importance levels.
		public static IEnumerable<SelectListItem> Importance = new List<SelectListItem>
		{
			new SelectListItem
			{
				Value = "1",
				Text = "Very Low"
			},
			new SelectListItem
			{
				Value = "2",
				Text = "Low"
			},
			new SelectListItem
			{
				Value = "3",
				Text = "Medium"
			},
			new SelectListItem
			{
				Value = "4",
				Text = "High"
			},
			new SelectListItem
			{
				Value = "5",
				Text = "Very high"
			}
		};

		// The importance levels.
		public static IEnumerable<SelectListItem> TaskPriority = new List<SelectListItem>
		{
			new SelectListItem
			{
				Value = "1",
				Text = "Very Low"
			},
			new SelectListItem
			{
				Value = "2",
				Text = "Low"
			},
			new SelectListItem
			{
				Value = "3",
				Text = "Medium"
			},
			new SelectListItem
			{
				Value = "4",
				Text = "High"
			},
			new SelectListItem
			{
				Value = "5",
				Text = "Very high"
			}
		};

		// Return the correct role.
		public static string GetRole(int roleNumber)
		{
			switch (roleNumber)
			{
				case 1:
					return "Admin";
				case 2:
					return "Standard";
				default:
					return "Guest";
			}
		}
	}
}