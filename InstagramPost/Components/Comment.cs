using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        int UserId { get; set; }
        int CommentID { get; set; }
        int Hierarchy { get; set; }
        int likes { get; set; }
        HashSet<int> Likes { get; set; }

        HashSet<int> DisLikes { get; set; }
        int disLikes { get; set; }

        bool IsDeleted { get; set; }
        bool HasReply { get; set; }

        List<Comment> Replies { get; set; }

        DateTime LastEdit { get; set; }

        #endregion

        public Comment(int CommentId)
        {
            this.CommentID = CommentId;
            IsDeleted = false;
            HasReply = false;
            this.Likes = new HashSet<int>();
            this.DisLikes = new HashSet<int>();
            Replies = new List<Comment>();

        }

        public Comment(String CommentText)
        {
            this.CommentText = CommentText;
            IsDeleted = false;
            HasReply = false;
            this.Likes = new HashSet<int>();
            this.DisLikes = new HashSet<int>();
            Replies = new List<Comment>();
        }

        public Comment(String CommentText, int CommentId, int Hierarchy,int UserId)
        {
            this.CommentText = CommentText;
            this.OriginalPost = OriginalPost;
            IsDeleted = false;
            HasReply = false; 
            this.Likes = new HashSet<int>();
            this.DisLikes = new HashSet<int>();
            this.UserId = UserId;
            this.Hierarchy = Hierarchy;
            Replies = new List<Comment>();
            CommentID =CommentId;
            LastEdit = DateTime.Now;
            
        }

        #region Edit Comment
        public void EditComment(int UserId)
        {
            String EditedText = "";
            try 
            {
                double diffInSeconds = (DateTime.Now - LastEdit).TotalSeconds;
                if(UserId!=this.UserId)
                {
                    Console.WriteLine("You can't edit other users Comment");
                    Console.ReadLine();
                }
                else if(diffInSeconds <= 300) 
                {
                    Console.Write("Enter new Text : ");
                    EditedText= Console.ReadLine();

                    this.CommentText = EditedText;
                    LastEdit = DateTime.Now;
                }
                else
                {
                    Console.WriteLine("comment is older than 5 minite you can't change it");
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
        public int GetUserId() { return UserId; }
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
        public void Like(int UserId)
        {
       
            if (!Likes.Contains(UserId))
                Likes.Add(UserId);

            DisLikes.Remove(UserId);


        }
        #endregion

        #region DisLike Comment
        public void DisLike(int UserId)
        {
            if (!DisLikes.Contains(UserId))    
                DisLikes.Add(UserId);
            
            Likes.Remove(UserId);
        }
        #endregion

        #region PrintComment
        public void PrintComment() 
        {
            
            for (int i = 0; i < Hierarchy; i++) 
                 Console.Write("     "); 

            if(IsDeleted)
            {
                String DeleteText = "This comment is Deleted";

                Console.WriteLine(DeleteText+"\n");
            }
            else
            {
                String SpaceText = "                                             ";
                String IdText = String.Format("({0}){1}", CommentID, CommentText);

                IdText += SpaceText.Substring(0,(45- (IdText.Length+ (5*Hierarchy) ))) ;

                Console.WriteLine(String.Format("{0} \t\t Like : {1} \tDisLike : {2}\n", IdText, Likes.Count, DisLikes.Count));
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
            Likes.Clear(); DisLikes.Clear();
            IsDeleted=true; CommentText = null;
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
