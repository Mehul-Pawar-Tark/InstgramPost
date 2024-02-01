using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstagramPost.Components;
using InstagramPost.DataBase;

namespace InstagramPost.Features
{
    internal class Login
    {
        public Login() { }

        public User Authentication(Data data)
        {
            try
            {
                Console.WriteLine("Enter Username = No for exit from Login page ");
                Console.Write("Enter Username : ");
                String UserName = Console.ReadLine();

                if(UserName !="No")
                {
                    Console.Write("Enter Password : ");
                    String Password = Console.ReadLine();

                    foreach(User user in data.GetUsers())
                    {
                        if(user.UserName == UserName && user.Password==Password) 
                        {
                            Console.Clear();
                            return user;
                        }
                    }

                    Console.WriteLine("Youser Not Exist !!!!");
                    Console.ReadLine();
                    Console.Clear();
                }

            }
            catch (Exception ex) { }
            { 
                
            }
            return new User("No");
        }

    }
}
