namespace Blogs.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductPhotos")]
    public partial class ProductPhoto
    {
        [Key]
        public long PhotoID { get; set; }

        public int ProductID { get; set; }

        [Required]
        [StringLength(255)]
        public string Photo { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsHidden { get; set; }

        public virtual Product Product { get; set; }
    }
}
