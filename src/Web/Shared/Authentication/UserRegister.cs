using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Shared.Authentication
{
    public class UserRegister
    {
        public UserRegister(string name, string password, string email)
        {
            Name = name;
            Password = password;
            Email = email;
        }
        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(17, MinimumLength = 5,ErrorMessage ="Пароль не соответсвует размеру")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Введите свою почту")]
        public string Email { get; set; }
    }
}
