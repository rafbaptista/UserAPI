using System.ComponentModel.DataAnnotations;
using WevoTest.Domain.Interfaces;

namespace WevoTest.Domain.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Name { get; set; } 

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Cellphone { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Gender { get; set; }      

    }
}
