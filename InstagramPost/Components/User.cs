using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstagramPost.Components
{
    internal class User
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public List<Post> posts { get; set; }
        public User() { posts = new List<Post>(); }

        public User(int userId, string userName, string password)
        {
            UserId = userId;
            UserName = userName;
            Password = password;
            posts=new List<Post>();
        }

        public void ShowPosts()
        {
            Console.WriteLine("UserName : "+UserName);

            foreach(Post post in posts) { post.DisplayPost(); }
        }

        public void AddPost(Post post)
        { posts.Add(post); }
    }
}
