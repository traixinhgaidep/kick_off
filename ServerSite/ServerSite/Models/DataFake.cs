using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSite
{
    public class DataFake
    {

        public List<User> GetDataUser()
        {
            List<User> lstsUser = new List<User>()
            {
                new User()
                {
                    Id = 1,
                    UserName = "Phongnv1",
                    Password = "12345",
                    Permission = Permission.Admin,
                    FullName = "FullName1"
                },
                 new User()
                {
                     Id = 2,
                    UserName = "Phongnv2",
                    Password = "12345",
                    Permission = Permission.User,
                    FullName = "FullName2"
                },
                new User()
                {
                    Id = 3,
                    UserName = "Phongn3",
                    Password = "12345",
                    Permission = Permission.User,
                    FullName = "FullName3"
                }
            };

            return lstsUser;
        }
    }
}
