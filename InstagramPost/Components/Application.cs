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
        #region properties
        Data data;
        List<User> Users;
        #endregion

        #region Constructor
        public Application(Data data) 
        {  
            Users = new List<User>();
            this.data = data;
        }
        #endregion

        #region Adduser
        public void AddUser(User user) { Users.Add(user);}
        #endregion

        #region StartApplication
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
        #endregion
    }
}
