using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fleqx.Models
{
    public class ChartDataModel
    {
        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        /// <value>
        /// The labels.
        /// </value>
        public List<string> Labels { get; set; }

        /// <summary>
        /// Gets or sets the dataset.
        /// </summary>
        /// <value>
        /// The dataset.
        /// </value>
        public List<ChartDatasetModel> Datasets { get; set; }


    }
}