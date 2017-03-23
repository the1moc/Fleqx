using System.Collections.Generic;
using System.Web.Mvc;
using Fleqx.Controllers;

namespace Fleqx.Data.DatabaseModels
{
    public static class ListControlHelper
    {
        // The Roles available.
        public static readonly IEnumerable<SelectListItem> Roles = new List<SelectListItem>
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

        // The Teams available.
        public static readonly IEnumerable<SelectListItem> Teams = new List<SelectListItem>
        {
            new SelectListItem
            {
                Value = "1",
                Text = "Group One"
            },
            new SelectListItem
            {
                Value = "2",
                Text = "Group Two"
            },
            new SelectListItem
            {
                Value = "3",
                Text = "Group Three"
            }
        };

        // The importance levels.
        public static readonly IEnumerable<SelectListItem> Importance = new List<SelectListItem>
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
            },
            new SelectListItem
            {
                Value = "6",
                Text = "All"
            }
        };

        // The importance levels.
        public readonly static IEnumerable<SelectListItem> TaskPriority = new List<SelectListItem>
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
            },
            new SelectListItem
            {
                Value = "6",
                Text = "All"
            }
        };

        // The Task states.
        public static readonly IEnumerable<SelectListItem> TaskStates = new List<SelectListItem>
        {
            new SelectListItem
            {
                Value = "1",
                Text = "Open"
            },
            new SelectListItem
            {
                Value = "2",
                Text = "Active"
            },
            new SelectListItem
            {
                Value = "3",
                Text = "Closed"
            }
        };

        /// <summary>
        /// Gets the role according to the passed in role number.
        /// </summary>
        /// <param name="roleNumber">The role number.</param>
        /// <returns></returns>
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