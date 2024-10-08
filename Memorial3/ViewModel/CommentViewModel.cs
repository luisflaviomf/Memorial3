﻿using System.Collections.Generic;
using Memorial3.Models;

namespace Memorial3.ViewModels
{
    public class CommentViewModel
    {
        public int MemorialId { get; set; }
        public string Content { get; set; }
        public List<Comment> Comments { get; set; }
        public string CurrentUserId { get; set; }
        public bool IsAdmin { get; set; }
    }
}
