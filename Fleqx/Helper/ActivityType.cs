using System.Collections.Generic;
using System.Web.Mvc;
using Fleqx.Controllers;

namespace Fleqx.Data.DatabaseModels
{
    public enum ActivityType
    {
        TaskCreated = 1,
        TaskEdited = 2,
        AnnouncementCreated = 3,
        AnnouncementEdited = 4,
        AnnouncementDeleted = 5,
        UserCreated = 6,
        UserEdited = 7
    }
}