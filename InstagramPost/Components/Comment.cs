using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstagramPost.Components
{
    internal class Comment
    {
        #region Properties
        Post OriginalPost { get; set; }
        String CommentText { get; set; }

        User User { get; set; }

        int CommentID { get; set; }
        int Hierarchy { get; set; }
        int Likes { get; set; }
        int DisLikes { get; set; }

        bool IsLiked { get; set; }
        bool IsDisLiked { get; set; }
        bool IsDeleted { get; set; }
        bool HasReply { get; set; }

        List<Comment> Replies { get; set; }

        DateTime LastEdit { get; set; }

        #endregion

        public Comment()
        {
            IsDeleted = false;
            HasReply = false;
            Likes = 0;
            DisLikes = 0;
            Replies = new List<Comment>();

        }


        public Comment(String CommentText)
        {
            this.CommentText = CommentText;
            IsDeleted = false;
            HasReply = false;
            Likes = 0;
            DisLikes = 0;
            Replies = new List<Comment>();
        }

        public Comment(String CommentText, Post OriginalPost, int Hierarchy)
        {
            this.CommentText = CommentText;
            this.OriginalPost = OriginalPost;
            IsDeleted = false; IsDisLiked = false;
            HasReply = false;  IsLiked = false;
            Likes = 0;
            DisLikes = 0;
            this.Hierarchy = Hierarchy;
            Replies = new List<Comment>();
            CommentID = OriginalPost.GetTotalComments() - 1;
            LastEdit = DateTime.Now;
            
        }

        #region Edit Comment
        public void EditComment()
        {
            String EditedText = "";
            try 
            {
                 double diffInSeconds = (DateTime.Now - LastEdit).TotalSeconds;

                if(diffInSeconds <= 60) 
                {
                    Console.Write("Enter new Text : ");
                    EditedText= Console.ReadLine();

                    this.CommentText = EditedText;
                    LastEdit = DateTime.Now;
                }
                else
                {
                    Console.WriteLine("comment is older than one minite you can't change it");
                    Console.ReadLine();
                }
            } 
            catch(Exception ex) 
            { 
                Console.WriteLine(ex.Message);   
            }
        }
        #endregion

        #region Get Value mathods
        public int GetCommentID() { return CommentID; }

        public bool GetHasReply() {  return HasReply; }

        public bool GetIsDeleted() { return IsDeleted; }

        public int GetHierarchy() { return Hierarchy; }
        #endregion

        #region set values
        
        public void SetIsDeleted(bool IsDeleted) { this.IsDeleted = IsDeleted; }

        public void SetHasReply(bool HasReply) { this.HasReply = HasReply; }

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

            if (IsDisLiked)
            {
                DisLikes--;
                IsDisLiked = false;
            }


        }
        #endregion

        #region DisLike Comment
        public void DisLike()
        {
            if (!IsDisLiked)
            {
                DisLikes++;
                IsDisLiked = true;
            }
            if (IsLiked)
            {
                Likes--;
                IsLiked = false;

            }
        }
        #endregion

        #region PrintComment
        public void PrintComment() 
        {
            
            for (int i = 0; i < Hierarchy; i++) 
                 Console.Write("\t"); 

            if(IsDeleted)
            {
                String DeleteText = "This comment is Deleted";

                Console.WriteLine(DeleteText+"\n");
            }
            else
            {
                Console.WriteLine(String.Format("({0}) {1} \t\t\t Like : {2} \tDisLike : {3}\n", CommentID, CommentText, this.Likes, this.DisLikes));
            }

            foreach(var Comment in Replies)
            {
                Comment.PrintComment();
            }
        }
        #endregion

        #region Delete values ofComment
        public void DeleteValues() 
        { 
            Likes=0; DisLikes=0; IsDeleted=true; CommentText = null;
        }
        #endregion

        #region Reply of Comment
        public void ReplyofComment(Comment Reply)
        {
            Replies.Add(Reply);
        }


        #endregion
    }
}
