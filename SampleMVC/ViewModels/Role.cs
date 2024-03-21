using System.Collections.Generic;
using APISolution.Domain;

namespace SampleMVC.ViewModels
{
    public class Role
    {
        public Role()
        {
            this.Users = new List<User>();
        }

        public int RoleID { get; set; }
        public string RoleName { get; set; }

        public IEnumerable<User> Users { get; set; }
    }
}
