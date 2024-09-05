using System.ComponentModel.DataAnnotations;
using System;

namespace Memorial3.Models
{
    public class Memorial
    {
        public int Id { get; set; }
        [Required,
            Display(Name = "Nome")]

        public string Name { get; set; }
        [Required,
            Display(Name = "História")]
        public string Historia { get; set; }
        [DataType(DataType.Date),
            Display(Name = "Data de Nascimento ")]
        public DateTime DataNascimento { get; set; }
        [Required,
            DataType(DataType.Date),
            Display(Name = "Data de Falecimento")]
        public DateTime DataFalecimento { get; set; }

        [Required,
            Display(Name = "Formação")]
        public string Formacao { get; set; }
        [Display(Name = "Religião")]
        public string Religiao { get; set; }
        [Required,
            Display(Name = "Hobbies")]
        public string Hobbies { get; set; }
        [Display(Name = "Link da foto principal do memorial")]
        public string MemorialPictureURL { get; set; }
        [Required]
        public string Coletivo { get; set; }

    }
}
