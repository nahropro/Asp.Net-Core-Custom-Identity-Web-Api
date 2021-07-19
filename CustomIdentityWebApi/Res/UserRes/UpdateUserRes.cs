using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomIdentityWebApi.Res.UserRes
{
    public class UpdateUserRes
    {
        [Required]
        [StringLength(1000)]
        public string FullName { get; set; }

        public bool Active { get; set; }

        [Required]
        [StringLength(256)]
        public string UserName { get; set; }

        public long RoleId { get; set; }
    }
}
