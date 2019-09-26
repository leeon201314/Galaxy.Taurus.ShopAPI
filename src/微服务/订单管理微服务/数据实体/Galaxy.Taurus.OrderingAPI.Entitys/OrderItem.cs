using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Galaxy.Taurus.OrderingAPI.Entitys
{
    [Table("OrderItem")]
    public class OrderItem
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        [MaxLength(32)]
        public string OrderId { get; set; }

        /// <summary>
        /// 店铺Id
        /// </summary>
        [MaxLength(32)]
        [Required]
        public string ShopId { get; set; }

        /// <summary>
        /// 商品Id
        /// </summary>
        [MaxLength(32)]
        [Required]
        public string GoodId { get; set; }

        /// <summary>
        /// 规格1
        /// </summary>
        public string Specification1 { get; set; }

        /// <summary>
        /// 规格值1
        /// </summary>
        public string SpecificationValue1 { get; set; }

        /// <summary>
        /// 规格2
        /// </summary>
        public string Specification2 { get; set; }

        /// <summary>
        /// 规格值2
        /// </summary>
        public string SpecificationValue2 { get; set; }

        /// <summary>
        /// 规格3
        /// </summary>
        public string Specification3 { get; set; }

        /// <summary>
        /// 规格值3
        /// </summary>
        public string SpecificationValue3 { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }
    }
}
