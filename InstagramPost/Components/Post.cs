using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstagramPost.Components
{
    internal class Post
    {
        String Link { get; set; }

        String Media {  get; set; }

        int Likes { get; set; }

        int TotalComment {  get; set; } 
        
        int DisLikes { get; set; }

        List<Comment> Comments { get; set; }

        public Post() 
        {
           Comments = new List<Comment>(); 
           Likes = 0;
            DisLikes = 0;
        }

        public Post(String media)
        {
            this.Media = media;
            Likes = 0;
            Comments = new List<Comment>();
        }

        #region DisplayPost
        public void DisplayPost()
        {
            Console.WriteLine("--------------------------------------------------------------\n");
            Console.WriteLine("\tPost : "+this.Media);
            Console.WriteLine("\tTotal Likes : "+this.Likes);
            Console.WriteLine("\tTotal DisLikes : " + this.DisLikes);
            Console.WriteLine("\nComments : "+TotalComment+"\n");

            foreach (Comment comment in Comments) 
            { 
                comment.PrintComment();
            }

            Console.WriteLine("--------------------------------------------------------------");


            Console.Write("Add Comment : ");
            try
            {
               String comment = Console.ReadLine();
               if(comment != "") 
               {
                    AddComment(GenerateComment(comment));
                    TotalComment++;
                    Console.Clear();
                    DisplayPost(); 
               }
            }
            catch(Exception e) 
            { 
                Console.WriteLine(e.Message);
            }

        }
        #endregion

        #region GenerateComment
        public Comment GenerateComment(String CommentText)
        {
            return new Comment(CommentText);
        }
        #endregion

        #region AddComment
        public void AddComment(Comment comment)
        {
            this.Comments.Add(comment);
        }
        #endregion

        #region Share
        public void share()
        {
          // not implimented
        }
        #endregion

        #region Save
        public void Save()
        {
            //not implimented
        }
        #endregion

    }
}
