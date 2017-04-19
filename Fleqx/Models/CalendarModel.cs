using System.ComponentModel.DataAnnotations;
using System;

namespace Fleqx.Models
{
    public class CalendarModel
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        /// <value>
        /// The start.
        /// </value>
        public string Start { get; set; }
    }
}