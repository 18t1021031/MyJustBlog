namespace Blogs.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderID { get; set; }

        public int? CustomerID { get; set; }

        public DateTime OrderTime { get; set; }

        public int? EmployeeID { get; set; }

        public DateTime? AcceptTime { get; set; }

        public int? ShipperID { get; set; }

        public DateTime? ShippedTime { get; set; }

        public DateTime? FinishedTime { get; set; }

        public int? Status { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Employee Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual OrderStatu OrderStatu { get; set; }

        public virtual Shipper Shipper { get; set; }
    }
}
