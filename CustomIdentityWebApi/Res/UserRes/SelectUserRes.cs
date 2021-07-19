using CustomIdentityWebApi.Res.RoleRes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomIdentityWebApi.Res.UserRes
{
    public class SelectUserRes
    {
        public long Id { get; set; }

        public string FullName { get; set; }

        public bool Active { get; set; }

        public string UserName { get; set; }

        public SelectRoleRes Role { get; set; }
    }
}
