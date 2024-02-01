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

        }
    }
}
