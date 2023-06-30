using EmpleadosSharepoint;
using EmployeeApi.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApi.Infraestructure.Authenticate
{
    public class UserService: IUser
    {
        List<User> users = new List<User>()
        {
            new User(){ Username = "josetest", Password = "123456" },
            new User(){ Username = "prueba2", Password = "123456" }
        };

        public bool IsUser(string userName, string pass) =>
            users.Where(d => d.Username == userName && d.Password == pass).Count() > 0;
    }
}
