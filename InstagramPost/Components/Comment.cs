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
        String CommentText {  get; set; }

        int CommentID { get; set; }
        int Hierarchy {  get; set; }
        int Like {  get; set; }
        int DisLike { get; set; }

        bool IsDeleted { get; set; }
        bool HasReply { get; set; }

        List<Comment> Replies { get; set; }

        #endregion

        public Comment() 
        { 
            IsDeleted = false;
            HasReply = false;
            Like = 0;   
            DisLike = 0;
            Replies=new List<Comment>();
            
        }    

       
        public Comment(String CommentText)
        {
            this.CommentText = CommentText;
            IsDeleted = false;
            HasReply = false;
            Like = 0;
            DisLike = 0;
            Replies = new List<Comment>();
        }

        public Comment(String CommentText,Post OriginalPost,int Hierarchy)
        {
            this.CommentText = CommentText;
            this.OriginalPost = OriginalPost;   
            IsDeleted = false;
            HasReply = false;
            Like = 0;
            DisLike = 0;
            this.Hierarchy = Hierarchy;
            Replies = new List<Comment>();
            CommentID = OriginalPost.GetTotalComments()-1;
        }

        #region Get Value mathods
        public int GetCommentID() { return CommentID; }

        public bool GetHasReply() {  return HasReply; }

        public bool GetIsDeleted() { return IsDeleted; }
        #endregion

        #region PrintComment
        public void PrintComment() 
        {
            
            for (int i = 0; i < Hierarchy; i++) 
                 Console.Write("\t"); 

            if(IsDeleted)
            {
                String DeleteText = "This comment is Deleted";

                Console.WriteLine(DeleteText);
            }
            else
            {
                Console.WriteLine(String.Format("({0}) {1} \t\t\t Like : {2} \tDisLike : {3}", CommentID, CommentText, this.Like, this.DisLike));
            }
        }
        #endregion

        #region Delete values ofComment
        public void DeleteValues() 
        { 
            Like=0; DisLike=0; IsDeleted=true; CommentText = null;
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
