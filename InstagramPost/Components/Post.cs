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
        #region properties
        String Link { get; set; }

        int AuthorId { get; set; }

        String Media { get; set; }

        HashSet<int> Likes { get; set; }
        
        HashSet<int> Dislikes { get; set; } 
        
        int TotalComments { get; set; }
        
        int CommentId { get; set; }

        List<Comment> Comments { get; set; }

        Dictionary<int,Comment> CommentsWithID { get; set; }
        #endregion

        #region Constructors
        public Post()
        {
            Comments = new List<Comment>();
            Likes=new HashSet<int>();
            Dislikes=new HashSet<int>();
            TotalComments = 0;
            CommentId = 0;
        }

        public Post(String media)
        {
            this.Media = media;
            Comments = new List<Comment>();
            CommentsWithID=new Dictionary<int,Comment>();
            TotalComments = 0; CommentId = 0;
            Likes=new HashSet<int>(); Dislikes = new HashSet<int>();
        }
        #endregion

        #region Add Reply    
        public void AddReply(int UserId)
        {
            Comment comment = GetComment();

            if (comment.GetCommentID() != -1)
            {
                int Hierarchy = comment.GetHierarchy() + 1;
                if (Hierarchy > 4)
                {
                    Console.WriteLine("you can't add hierarchical 6th comment...");  //error msg
                    Console.ReadLine();
                }
                else if (!comment.GetIsDeleted())
                {
                    comment.AddReplyofComment(GenerateComment(Hierarchy, UserId));
                    comment.SetHasReply(true);
                }
                else
                {
                    Console.WriteLine("This Comment is Deleted you cant Reply it");   //error msg
                    Console.ReadLine();
                }
            }
        }
        #endregion

        #region AddComment
        public void AddComment(Comment comment)
        {
            this.Comments.Add(comment);
        }
        #endregion

        #region Decision
        public int Decision()
        {
            Console.WriteLine("0. Exit               1. Add Comment          2. Add Reply");
            Console.WriteLine("3. Like Post          4.DisLike Post          5. DeleteComment");
            Console.WriteLine("6. Like Comment       7.DisLike Comment       8. EditComment");
            Console.Write("Select Your PostReaction Decision [0-8] : ");

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

        #region DeleteComment
        public void DeleteComment(int UserId)
        {
            int CommentId = 0;
            bool IsValidCommentId = true;
            do
            {
                Console.Write("Enter CommentId : ");
                try
                {
                    CommentId = Int32.Parse(Console.ReadLine());
                    IsValidCommentId = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    IsValidCommentId= false;
                }
            } while (!IsValidCommentId);
                
                if (!(CommentsWithID.ContainsKey(CommentId))) { Console.WriteLine("Comment Not Exist."); Console.ReadLine(); }
                else
                {
                    Comment comment = CommentsWithID[CommentId];
                    if (comment.GetUserId() == UserId)
                    {
                        if (comment.GetIsDeleted())
                        {
                            Console.WriteLine("Comment allready Deleted");
                            Console.ReadLine();
                        }
                        else if (comment.GetHasReply())
                        {
                            comment.DeleteValuesOfComment();
                            comment.SetIsDeleted(true);
                            TotalComments--;
                        }
                        else
                        {
                            Comments.Remove(comment);
                            TotalComments--;
                        }
                    }
                    else
                    {
                        Console.WriteLine("This is Not Your Comment so you can't Delete it");
                        Console.ReadLine();
                    }
                }

        }

        #endregion

        #region DisLike Comment
        public void DisLikeComment(int UserId)
        {
            Comment comment = GetComment();
            comment.DisLike(UserId);
        }
        #endregion

        #region DisLike Post
        public void DisLikePost(int UserId) 
        {
            if (!Dislikes.Contains(UserId))
                Dislikes.Add(UserId);

            Likes.Remove(UserId);
        }
        #endregion

        #region Display Post
        public void DisplayPost(int PostIndex)
        {
            Console.WriteLine("-------------------------------------- PostIndex : "+PostIndex+" -----------------------------------------------\n\n");
            Console.WriteLine("\t\t\t\tPost : " + this.Media);
            Console.WriteLine("\t\t\t\tTotal Likes : " + Likes.Count);
            Console.WriteLine("\t\t\t\tTotal DisLikes : " + Dislikes.Count);
            Console.WriteLine("\n\nComments : " + TotalComments + "\n");

            foreach (Comment comment in Comments)
            {
                comment.PrintComment();
            }

            Console.WriteLine("----------------------------------------------------------------------------------------------------");

        }
        #endregion

        #region Edit Comment
        public void EditComment(int UserId)
        {
            Comment comment = GetComment();

            if(comment.GetCommentID()!=-1)
                comment.EditComment(UserId);
        }
        #endregion

        #region GenerateComment

        public Comment GenerateComment(int Hierarchy,int UserId)
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

            Comment comment= new Comment(CommentText, CommentId++, Hierarchy,UserId);
            CommentsWithID.Add(comment.GetCommentID(), comment);

            return comment;

        }
        #endregion
       
        #region Get Comment
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
            else if(CommentId!=-1)
            {
                Console.WriteLine("this Comment is not Exist");     //error msg
                GetComment();
            }

            return new Comment(-1);
        }
        #endregion
        
        #region GetTotalComments
        public int GetTotalComments()
        {
            return this.TotalComments;
        }
        #endregion

        #region Like Comment
        public void LikeComment(int UserId)
        { 
            Comment comment = GetComment();
            comment.LikeComment(UserId);
        }
        #endregion
        
        #region Like Post
        public void LikePost(int UserId)  
        {
            if(!Likes.Contains(UserId))
                Likes.Add(UserId);
            
            Dislikes.Remove(UserId);
        }
        #endregion
     
        #region Reaction
        public void Reaction(int PostIndex,int UserId)
        {
            int decisionIndex = Decision();

            switch (decisionIndex)
            {
                case 0: 
                        Console.Clear(); break;
                case 1:
                        AddComment(GenerateComment(0,UserId));   break;
                case 2:
                        AddReply(UserId); break;
                case 3:
                        LikePost(UserId);     break;
                case 4:
                        DisLikePost(UserId);  break;
                case 5:
                        DeleteComment(UserId);   break;
                case 6:
                        LikeComment(UserId); break;
                case 7:
                        DisLikeComment(UserId); break;
                case 8:
                        EditComment(UserId); break;
            }

            if (decisionIndex != 0)
            {
                Console.Clear();
            }
        }
        #endregion

    }
}
