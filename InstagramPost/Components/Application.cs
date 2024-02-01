using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstagramPost.DataBase;
using InstagramPost.Features;

namespace InstagramPost.Components
{
    internal class Application
    {
        Data data;
        List<User> Users;
        

        public Application(Data data) 
        {  
            Users = new List<User>();
            this.data = data;
        }

        public void AddUser(User user) { Users.Add(user);}

        public void StartApplication()
        {
            Login login = new Login();

            User user=login.Authentication(data);

            if(user.UserName != "No") 
            {
                user.ShowPosts();
                StartApplication();
            }

        }
            
    }
}
