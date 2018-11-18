using System;

namespace MyAbp.Attributes
{
    public class CommentAttribute:Attribute
    {

        public CommentAttribute(string comments)
        {
            Comments = comments;
        }

        public string Comments { get; set; }
    }
}
