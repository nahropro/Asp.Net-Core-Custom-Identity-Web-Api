using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomIdentityWebApi.Models
{
    public class User
    {
        public long Id { get; set; }

        [Required]
        [StringLength(1000)]
        public string FullName { get; set; }

        public bool Active { get; set; }

        [Required]
        [StringLength(256)]
        public string UserName { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string SecurityStamp { get; set; }

        public long RoleId { get; set; }

        public Role Role { get; set; }
    }
}
