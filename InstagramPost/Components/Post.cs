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

        int AuthorId { get; set; }

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
            Console.WriteLine("0. Exit               1. Add Comment          2. Add Reply");
            Console.WriteLine("3. Like Post          4.DisLike Post          5. DeleteComment");
            Console.WriteLine("6. Like Comment       7.DisLike Comment       8. EditComment");
            Console.Write("Select Your PostReaction Decision [0-5] : ");

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

            if (decisionIndex < 0 || decisionIndex > 8)
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
                        AddComment(GenerateComment(0));   break;
                case 2:
                        AddReply(); break;
                case 3:
                        Like();     break;
                case 4:
                        DisLike();  break;
                case 5:
                        DeleteComment();   break;
                case 6:
                    LikeComment(); break;
                case 7:
                    DisLikeComment(); break;
                case 8:
                    EditComment(); break;
            }

            if (decisionIndex != 0)
            {
                Console.Clear();
                DisplayPost();
            }

        }
        #endregion

        #region Like Comment
        public void LikeComment()
        { 
            Comment comment = GetComment();
            comment.Like();
        }
        #endregion

        #region DisLike Comment
        public void DisLikeComment()
        {
            Comment comment = GetComment();
            comment.DisLike();
        }
        #endregion

        #region Edit Comment
        public void EditComment()
        {
            Comment comment = GetComment();
            comment.EditComment();
        }
        #endregion

        #region GetComment
        public Comment GetComment()
        {
            int CommentId = 0;

            bool IsValidId = true;
            do
            {
                Console.Write("Enter CommentId : ");
                try
                {
                    CommentId = Int32.Parse(Console.ReadLine());
                    IsValidId = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    IsValidId = false;
                }

            } while (!IsValidId);

            
            if (CommentsWithID.ContainsKey(CommentId))
            {
                return CommentsWithID[CommentId];
            }
            else
            {
                Console.WriteLine("this Comment is not Exist");     //error msg
                GetComment();
            }

            return new Comment();
        }
        #endregion

        #region Add Reply    
        public void AddReply()
        {
                Comment comment = GetComment();

                int Hierarchy = comment.GetHierarchy() + 1;

                if(Hierarchy > 4) 
                {
                    Console.WriteLine("you can't add hirarchycal 6th comment...");  //error msg
                    Console.ReadLine();
                }
                else if (!comment.GetIsDeleted()) 
                {
                    comment.ReplyofComment(GenerateComment(Hierarchy));
                    comment.SetHasReply(true);
                }
                else
                {
                    Console.WriteLine("This Comment is Deleted you cant Reply it");   //error msg
                    Console.ReadLine();
                }     
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
                        comment.SetIsDeleted(true);
                        TotalComments--;
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

        #region GenerateComment
        public Comment GenerateComment(int Hierarchy)
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

            Comment comment= new Comment(CommentText, this, Hierarchy);
            CommentsWithID.Add(comment.GetCommentID(), comment);

            return comment;

        }
        #endregion
        
        #region AddComment
        public void AddComment(Comment comment)
        {
            this.Comments.Add(comment);
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
