using QccHub.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QccHub.Data.Models
{
    public class Order : BaseEntity, ICreationAuditable, ISoftDeletable
    {
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public string ShippingAddress { get; set; } // عنوان الشحن
        [ForeignKey("PaymentStatus")]
        public int PaymentStatusID { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
