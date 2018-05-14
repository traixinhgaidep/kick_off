using Ss.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ss.Data.Models
{
    public class User : BaseEntity
    {
        public User()
            : base("Code")
        {

        }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public Permission Permission { get; set; }
    }
}
