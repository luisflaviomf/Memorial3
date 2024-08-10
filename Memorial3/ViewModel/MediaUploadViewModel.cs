using Memorial3.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Memorial3.ViewModels
{
    public class MediaUploadViewModel
    {
        public int MemorialId { get; set; }

        [Required]
        public IFormFile File { get; set; }

        public List<Media> MediaList { get; set; }
    }
}
