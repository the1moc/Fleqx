using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fleqx.Data.DatabaseModels
{
    public class ActivityModel
    {
        // The activity performed.
        public string ActivityContent { get; set; }

        // The user name.
        public string UserName { get; set; }

        // Date the action took place.
        public string Date { get; set; }
    }
}