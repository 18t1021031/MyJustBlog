namespace Blogs.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        public int PostId { get; set; }

        [Required]
        [StringLength(255)]
        public string CommentHeader { get; set; }

        public string CommentText { get; set; }

        public DateTime CommentTime { get; set; }

        public virtual Post Post { get; set; }
    }
}
