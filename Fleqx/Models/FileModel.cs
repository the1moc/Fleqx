using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fleqx.Models
{
    public class FileModel
    {
        // The file ID (PK).
        public int FileId { get; set; }

        // The file path.
        public string FilePath { get; set; }

        // The file name.
        public string FileName { get; set; }

        // The name of the user.
        public string Username { get; set; }
    }
}