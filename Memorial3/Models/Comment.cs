using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Memorial3.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public DefaultUser User { get; set; }

        [Required]
        public int MemorialId { get; set; }

        [ForeignKey("MemorialId")]
        public Memorial Memorial { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [Required]
        public DateTime DatePosted { get; set; } = DateTime.Now;
    }
}
