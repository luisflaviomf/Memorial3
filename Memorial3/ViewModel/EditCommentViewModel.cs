using System.Collections.Generic;
using Memorial3.Models;

namespace Memorial3.ViewModels
{
    public class EditCommentViewModel
    {
        public int Id { get; set; }
        public int MemorialId { get; set; }
        public string Content { get; set; }
        public string CurrentUserId { get; set; }
        public bool IsAdmin { get; set; }
    }
}
