namespace Blogs.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Post
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Post()
        {
            Comments = new HashSet<Comment>();
            Tags = new HashSet<Tag>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Column("Short Description")]
        [Required]
        [StringLength(1024)]
        public string Short_Description { get; set; }

        public string Meta { get; set; }

        [StringLength(255)]
        public string UrlSlug { get; set; }

        public bool Published { get; set; }

        [Column("Posted On")]
        public DateTime Posted_On { get; set; }

        public DateTime? Modified { get; set; }

        public int CategoryId { get; set; }

        public int? ViewCount { get; set; }

        public int? RateCount { get; set; }

        public int? TotalRate { get; set; }

        [Column(TypeName = "ntext")]
        public string PostContent { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
