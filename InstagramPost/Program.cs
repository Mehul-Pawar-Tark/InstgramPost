using System;
using InstagramPost.Components;

namespace InstagramPosts
{
    class Program
    {
        public static void Main()
        {
            Application appication=new Application();



            Post post = new Post("First Post");

            post.DisplayPost();

        }
    }
}
