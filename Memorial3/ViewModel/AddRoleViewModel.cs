using System.ComponentModel.DataAnnotations;

namespace Memorial3.ViewModel
{
    public class AddRoleViewModel
    {
        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}
