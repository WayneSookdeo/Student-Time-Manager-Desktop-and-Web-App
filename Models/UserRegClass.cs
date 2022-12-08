using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace demo5.Models
{
    public class UserRegClass
    {
        public string Username { get; set; }
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Please Enter Username")]
        public string HashedPassword { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please Enter Password")]
        public string Salt { get; set; }
    }
}
