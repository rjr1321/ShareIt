using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Core.Application
{
    public class RegisterViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Debe colocar el nombre del usuario")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Debe colocar el apellido del usuario")]
        public string LastName { get; set; }

       
        [Required(ErrorMessage = "Debe colocar un telefono")]
        public string PhoneNumber { get; set; }

       
        [Required(ErrorMessage = "Debe colocar un correo")]
        public string Email { get; set; }

        
        [Required(ErrorMessage = "Debe colocar un nombre de usuario")]
        public string Username { get; set;}

        [Required(ErrorMessage = "Debe colocar una contraseña")]
        public string Password { get; set;}

        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coiciden")]
        [Required(ErrorMessage = "Debe colocar una contraseña")]
        public string ConfirmPassword { get; set; }
        public IFormFile Photo { get; set; }

        public string? PhotoURL { get; set; }

      
    }
}
