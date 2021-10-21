using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace shopping.Authentication
{
    public class registerModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Enter Valid Email")]
        public string Email { get; set; }
    }
}
