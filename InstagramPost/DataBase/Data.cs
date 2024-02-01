using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstagramPost.Components;

namespace InstagramPost.DataBase
{
    internal class Data
    {
        #region properties
        List<User> Users = new List<User>();
        #endregion

        #region Constructors
        public Data() 
        {
            Post post1 = new Post("First Post");
            Post post2 = new Post("Second Post");
            Post post3 = new Post("Third Post");
            Post post4 = new Post("fourth Post");

            List<Post> posts = new List<Post>();
            
            posts.Add(post1); 
            posts.Add(post2); 
            posts.Add(post3);
            

            User user1 = new User(111, "mehul", "123456", posts);
            User user2 = new User(123, "jay", "654321", posts);
            User user3 = new User(123, "Stavan", "123654", posts);


            Users.Add(user1);
            Users.Add(user2);
            Users.Add(user3);
        }
        #endregion

        #region GetValues
        public List<User> GetUsers() { return Users; }
        #endregion

    }
}
