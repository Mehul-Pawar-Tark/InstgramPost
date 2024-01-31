using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstagramPost.Components
{
    internal class Application
    {
        List<User> Users;

        public Application() {  Users = new List<User>(); }

        public void AddUser(User user) { Users.Add(user);}
            
    }
}
