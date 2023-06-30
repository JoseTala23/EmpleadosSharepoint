using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpleadosSharepoint
{
    public interface IUser
    {
        public bool IsUser(string user, string pass);

    }
}
