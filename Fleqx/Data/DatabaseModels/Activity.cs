using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fleqx.Data.DatabaseModels
{
    public class Activity
    {
        // The activity ID (PK).
        public int ActivityId { get; set; }

        // The activity performed.
        public string ActivityContent { get; set; }

        // The date the activity happened.
        public DateTime Date { get; set; }

        // The user that posted the announcement.
        [Required]
        [DataType(DataType.Text)]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}