using System;
using System.ComponentModel.DataAnnotations;

namespace Fleqx.Models
{
    public class AnnouncementFilterModel
    {
        /// <summary>
        /// Gets or sets the start filter date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        [Required]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end filter date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        [Required]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the announcement importance.
        /// </summary>
        /// <value>
        /// The importance of the announcements.
        /// </value>
        [Required]
        public int AnnouncementImportance { get; set; }
    }
}