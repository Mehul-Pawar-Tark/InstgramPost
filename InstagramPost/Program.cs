using System;
using InstagramPost.Components;
using InstagramPost.DataBase;

namespace InstagramPosts
{
    class Program
    {
        public static void Main()
        {
            Data data = new Data();
            Application appication=new Application(data);

            appication.StartApplication();

            /*


            Post post1 = new Post("First Post");
            Post post2 = new Post("Second Post");
            Post post3 = new Post("Third Post");
            Post post4 = new Post("fourth Post");
            List<Post> posts = new List<Post>();
            posts.Add(post1);
            posts.Add(post2);


            User user1 = new User(111,"mehul","123456",posts);
            User user2 = new User(123, "jay", "12ji56", posts);
            User user3 = new User(123, "Stavan", "12ji56", posts);

            user1.ShowPosts();
            */
        }
    }
}
