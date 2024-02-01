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

        #region Authentication
        public User Authentication(Data data)
        {
            bool IsValidUsernamePassword = true;
            do
            {
                try
                {
                    Console.WriteLine("Enter Username = No for exit from Login page ");
                    Console.Write("Enter Username : ");
                    String UserName = Console.ReadLine();

                    if (UserName != "No")
                    {
                        Console.Write("Enter Password : ");
                        String Password = Console.ReadLine();

                        foreach (User user in data.GetUsers())
                        {
                            if (user.UserName == UserName && user.Password == Password)
                            {
                                Console.Clear();
                                return user;
                            }
                        }

                        IsValidUsernamePassword = false;
                        Console.WriteLine("Invalid User Name or Password  !!!!");
                        Console.ReadLine();
                        Console.Clear();
                    }
                    else
                        IsValidUsernamePassword = true;
                }
                catch (Exception ex)
                {
                    IsValidUsernamePassword = false;
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                    Console.Clear();
                }
            } while (!IsValidUsernamePassword);
            return new User("No");
        }
        #endregion

    }
}
