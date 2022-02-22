using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Shared.Authentication
{
    public class UserLogin
    {
        public UserLogin(string name,string password)
        {
            Name = name;
            Password = password;
        }
        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(17, MinimumLength = 5, ErrorMessage = "Пароль не соответсвует размеру")]
        public string Password { get; set; }
    }
}
