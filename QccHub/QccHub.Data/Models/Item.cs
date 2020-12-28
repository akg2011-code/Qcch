using QccHub.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QccHub.Data.Models
{
    public class Item : BaseEntity, ICreationAuditable, ISoftDeletable
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        [ForeignKey("Supplier")]
        public int SupplierID { get; set; }
        public int Stock { get; set; } // المخزون
        public string Code { get; set; }
        public string PromotionCode { get; set; }
        public double Discount { get; set; }
        public int FixedStock { get; set; } // مقدار مخزون مينفعش يقل عنه
        public DateTime PromotoionExpireDate { get; set; } // معاد انتهاء العرض

        public ApplicationUser Supplier { get; set; }
        public ItemsCategory Category { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
