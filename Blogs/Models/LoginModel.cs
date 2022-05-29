using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blogs.Models
{
    public class LoginModel
    {
        [Required]
        [StringLength(20)]
        [Key]
        public string UserName { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
        [Required]
        public string RePassword { get; set; }

        public string CaptchaCode { get; set; }

    }
}