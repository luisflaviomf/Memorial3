using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Memorial3.Models
{
    public class Media
    {
        public int Id { get; set; }

        [Required]
        public int MemorialId { get; set; }

        [ForeignKey("MemorialId")]
        public Memorial Memorial { get; set; }

        [Required]
        public string FilePath { get; set; }

        [Required]
        public string FileType { get; set; }
    }
}
