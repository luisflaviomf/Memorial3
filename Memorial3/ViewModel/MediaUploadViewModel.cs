using Memorial3.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Memorial3.ViewModels
{
    public class MediaUploadViewModel
    {
        public int MemorialId { get; set; }
        public IFormFile File { get; set; }
        public string MediaName { get; set; } // Novo campo para o nome da mídia

        public List<Media> MediaList { get; set; }
    }
}
