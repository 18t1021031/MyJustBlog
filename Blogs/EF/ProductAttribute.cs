namespace Blogs.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProductAttribute
    {
        [Key]
        public long AttributeID { get; set; }

        public int ProductID { get; set; }

        [Required]
        [StringLength(255)]
        public string AttributeName { get; set; }

        [Required]
        public string AttributeValue { get; set; }

        public int DisplayOrder { get; set; }

        public virtual Product Product { get; set; }
    }
}
