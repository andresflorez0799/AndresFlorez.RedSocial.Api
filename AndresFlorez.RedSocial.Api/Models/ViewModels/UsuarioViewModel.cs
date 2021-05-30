using System.ComponentModel.DataAnnotations;

namespace AndresFlorez.RedSocial.Api.Models.ViewModels
{
    public class UsuarioViewModel
    {
        [Required]
        [StringLength(150)]
        [MinLength(2)]
        [Display(Name = "Nombres")]
        public string Nombre { get; set; }
        [StringLength(150)]
        [MinLength(2)]
        [Display(Name = "Apellidos")]
        public string Apellido { get; set; }
        [StringLength(50)]
        [MinLength(2)]
        [Display(Name = "Telefono")]
        public string Telefono { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public string Contrasena { get; set; }
    }
}
