using System.ComponentModel.DataAnnotations;

namespace Galaxy.Taurus.GoodInfoAPI.ViewModel
{
    /// <summary>
    /// 商品SKU DTO
    /// </summary>
    public class GoodInfoSKUDTO
    {
        /// <summary>
        /// SKUId
        /// </summary>
        [MaxLength(32)]
        public string SKUId { get; set; }

        /// <summary>
        /// 商品Id
        /// </summary>
        [MaxLength(32)]
        public string GoodInfoId { get; set; }

        /// <summary>
        /// 店铺Id
        /// </summary>
        [MaxLength(32)]
        public string ShopId { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public float Price { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public float Stock { get; set; }

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
        /// 显示顺序
        /// </summary>
        public int? ShowIndex { get; set; }
    }
}
