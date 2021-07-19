using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomIdentityWebApi.Res.UserRes
{
    public class CreateUserRes
    {
        [Required]
        [StringLength(1000)]
        public string FullName { get; set; }

        public bool Active { get; set; }

        [Required]
        [StringLength(256)]
        public string UserName { get; set; }

        [Required]
        [StringLength(256)]
        public string Password { get; set; }

        [Required]
        [StringLength(256)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public long RoleId { get; set; }
    }
}
