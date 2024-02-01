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
        public User(String UserName) 
        { 
            this.UserName=UserName; 
            posts = new List<Post>(); 
        }

       
        public User(int userId, string userName, string password, List<Post> posts)
        {
            UserId = userId;
            UserName = userName;
            Password = password;
            this.posts=posts;

        }

        public void ShowPosts()
        {
            Console.WriteLine("UserName : "+UserName);

            for(int i=0; i<posts.Count; i++) 
            {
                posts[i].DisplayPost(i); 
            }

            bool validIndex=true;
            do
            {
                try
                {
                        Console.Write("On which post you want to react : [0-0] : ");
                        int PostIndex = Int32.Parse(Console.ReadLine());

                        if (PostIndex < posts.Count && PostIndex >= 0)
                        {
                            posts[PostIndex].Reaction(PostIndex,UserId);
                            ShowPosts();
                            validIndex = true;
                        }
                        else if(PostIndex==-1)
                        {
                            validIndex = true;
                        }
                        else
                        { 
                            Console.WriteLine("You Enter Invalid index !!!! "); 
                            validIndex = false;
                        }

                }
                catch(Exception e) 
                {
                    Console.WriteLine(e.Message);
                    validIndex = false;
                }
            } while (!validIndex);
        }

        public void AddPost(Post post)
        { posts.Add(post); }

        public int GetUserId()
        {
            return this.UserId;
        }
    }
}
