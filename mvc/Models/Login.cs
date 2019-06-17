using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mvc.Models
{
   
    public partial class Login
    {
        [Key]
        [Required(ErrorMessage = "Please enter User Name")]
        [Display(Name = "Username")]       
        public string UserName { get; set; } 
        [Required(ErrorMessage = "Please enter Password")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}