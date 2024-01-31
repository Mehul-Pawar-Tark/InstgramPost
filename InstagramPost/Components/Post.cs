using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InstagramPost.Components
{
    internal class Post
    {
        String Link { get; set; }

        String Media { get; set; }

        int Likes { get; set; }
        bool IsLiked { get; set; }
        bool IsDisLiked { get; set; }

        int TotalComments { get; set; }

        int DisLikes { get; set; }

        List<Comment> Comments { get; set; }

        Dictionary<int,Comment> CommentsWithID { get; set; }

        public Post()
        {
            Comments = new List<Comment>();
            Likes = 0;
            DisLikes = 0;
            IsLiked = false;
            TotalComments = 0;
        }

        public Post(String media)
        {
            this.Media = media;
            Likes = 0;
            IsLiked=false;  IsDisLiked = false;
            Comments = new List<Comment>();
            CommentsWithID=new Dictionary<int,Comment>();
            TotalComments = 0;
        }

        #region DisplayPost
        public void DisplayPost()
        {
            Console.WriteLine("----------------------------------------------------------------------------------------------------\n\n");
            Console.WriteLine("\t\t\t\tPost : " + this.Media);
            Console.WriteLine("\t\t\t\tTotal Likes : " + this.Likes);
            Console.WriteLine("\t\t\t\tTotal DisLikes : " + this.DisLikes);
            Console.WriteLine("\n\nComments : " + TotalComments + "\n");

            foreach (Comment comment in Comments)
            {
                comment.PrintComment();
            }

            Console.WriteLine("----------------------------------------------------------------------------------------------------");

            Reaction();

        }
        #endregion

        #region Decision for Reaction
        public int Decision()
        {
            Console.WriteLine("0. Exit\t 1. Add Comment\t 2. Add Reply");
            Console.WriteLine("3. Like Post\t 4.DesLike Post\t5. DeleteComment");
            Console.Write("Select Your Decision [0-5] : ");

            int decisionIndex = -1;
            try
            {
                decisionIndex = Int32.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n"+ex.Message+"\n");
                return Decision();
            }

            if (decisionIndex < 0 || decisionIndex > 5)
            {
                    Console.WriteLine("\nYou Enter Wrong Choice, try again......\n");
                    return Decision();
            }


            return decisionIndex;
        }
        #endregion

        #region Reaction
        public void Reaction()
        {
            int decisionIndex = Decision();

            switch (decisionIndex)
            {
                case 1:
                        AddComment(GenerateComment());   break;
                case 2:
                        AddReply(); break;
                case 3:
                        Like();     break;
                case 4:
                        DisLike();  break;
                case 5:
                        DeleteComment();   break;
            }

            if (decisionIndex != 0)
            {
                Console.Clear();
                DisplayPost();
            }

        }
        #endregion

        #region Add Reply
        
        public void AddReply()
        {
            int CommentId = 0;

            Console.Write("Enter CommentId : ");
            try
            {
                CommentId = Int32.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                AddReply();
            }

            Comment comment=new Comment();
            if(comment==null)
            {
                Console.WriteLine("hello");
            }
            Console.ReadLine();
        }
        #endregion

        #region DeleteComment

        public void DeleteComment()
        {
            int CommentId = 0;

            Console.Write("Enter CommentId : ");
            try
            {
                CommentId = Int32.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                DeleteComment();
            }
                
                if (!(CommentsWithID.ContainsKey(CommentId))) { Console.WriteLine("Comment Not Exist."); Console.ReadLine(); }
                else
                {
                
                    Comment comment = CommentsWithID[CommentId];

                    if(comment.GetIsDeleted())
                    { 
                        Console.WriteLine("Comment allready Deleted");
                        Console.ReadLine();
                    }
             
                    else if(comment.GetHasReply()) 
                    {
                        comment.DeleteValues();                       
                    }
                    else
                    {
                        Comments.Remove(comment);
                        TotalComments--;
                    }

                }

        }

        #endregion

        #region Get value Methods
        public int GetTotalComments()
        {
            return this.TotalComments;
        }
        #endregion

        #region FindCommentIndex 
        public int FindCommentIndex(int CommentId)
        {
           for(int index=0; index < Comments.Count; index++) 
            {
                if (Comments[index].GetCommentID()==CommentId)
                {
                    return index;
                }
            }

            return -1;
        }
        #endregion

        #region GenerateComment
        public Comment GenerateComment()
        {
            String CommentText = "Initial Comment";
            Console.Write("Your Comment : ");

            try
            {
                CommentText = Console.ReadLine();
                TotalComments++;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new Comment(CommentText, this, 1);

        }
        #endregion
        
        #region AddComment
        public void AddComment(Comment comment)
        {
            this.Comments.Add(comment);
            CommentsWithID.Add(comment.GetCommentID(), comment);
        }
        #endregion

        #region Like Post
        public void Like()  
        {
            //Likes++;
            
            if (!IsLiked)
            {
                Likes++;
                IsLiked = true;

            } 

            if(IsDisLiked)
            {
                DisLikes--;
                IsDisLiked = false;
            }
            
        
        }
        #endregion

        #region DisLike Post
        public void DisLike() 
        {
            //DisLikes++;
            
            if (!IsDisLiked)
            {
                DisLikes++;
                IsDisLiked = true;
            }
            if(IsLiked)
            {
                Likes--;
                IsLiked = false;

            }  
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
