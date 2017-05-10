using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fleqx.Data.DatabaseModels
{
    public class DataFile
    {
        // The file ID (PK).
        public int DataFileId { get; set; }

        // The file path.
        public string FilePath { get; set; }

        // The file name.
        public string FileName { get; set; }

        // The user that posted the file.
        [Required]
        [DataType(DataType.Text)]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}