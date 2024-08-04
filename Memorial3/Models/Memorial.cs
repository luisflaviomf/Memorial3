using System.ComponentModel.DataAnnotations;
using System;

namespace Memorial3.Models
{
    public class Memorial
    {
        public int Id { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]
        public string Historia { get; set; }
        [DataType(DataType.Date),
            Display(Name = "Data de Nascimento ")]
        public DateTime DataNascimento { get; set; }
        [Required,
            DataType(DataType.Date),
            Display(Name = "Data de Falecimento")]
        public DateTime DataFalecimento { get; set; }


        public string Formacao { get; set; }

        public string Religiao { get; set; }

        public string Hobbies { get; set; }
        [Display(Name = "Image URL")]
        public string MemorialPictureURL { get; set; }
        public string Coletivo { get; set; }

    }
}
