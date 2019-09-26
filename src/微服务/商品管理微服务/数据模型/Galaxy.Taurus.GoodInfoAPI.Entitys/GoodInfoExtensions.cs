using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Galaxy.Taurus.GoodInfoAPI.Entitys
{
    /// <summary>
    /// 商品扩展信息
    /// </summary>
    [Table("GoodInfoExtensions")]
    public class GoodInfoExtensions
    {
        /// <summary>
        /// 商品Id
        /// </summary>
        [MaxLength(32)]
        [Key]
        public string GoodInfoId { get; set; }

        /// <summary>
        /// 店铺Id
        /// </summary>
        [MaxLength(32)]
        [Required]
        public string ShopId { get; set; }

        /// <summary>
        /// 横幅的图片或视频，json存储
        /// </summary>
        public string Banner { get; set; }

        /// <summary>
        /// 详细信息图片或视频，使用json存储
        /// </summary>
        public string DescMedia { get; set; }

        /// <summary>
        /// 产品规格，使用json存储
        /// </summary>
        public string Specification { get; set; }
    }
}
