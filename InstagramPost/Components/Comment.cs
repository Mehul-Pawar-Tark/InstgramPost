using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstagramPost.Components
{
    internal class Comment
    {
        String CommentText {  get; set; }

        int Like {  get; set; }
        int DisLike { get; set; }

        bool IsDeleted { get; set; }
        bool HasReply { get; set; }

        Comment Reply { get; set; }

        public Comment() 
        { 
            IsDeleted = false;
            HasReply = false;
            Like = 0;   
            DisLike = 0;
        }    

        public Comment(String CommentText)
        {
            this.CommentText = CommentText;
            IsDeleted = false;
            HasReply = false;
            Like = 0;
            DisLike = 0;
        }

        #region PrintComment
        public void PrintComment() 
        {
            Console.WriteLine(CommentText+"\t\t\tLike : "+this.Like+"\tDisLike : "+this.DisLike);
        }
        #endregion
    }
}
