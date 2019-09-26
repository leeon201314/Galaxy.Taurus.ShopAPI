using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Galaxy.Taurus.GoodInfoAPI.Entitys
{
    [Table("GoodInfo")]
    public class GoodInfo
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        [MaxLength(32)]
        public string Id { get; set; }

        [MaxLength(32)]
        public string CategoryId { get; set; }

        [MaxLength(32)]
        [Required]
        public string ShopId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(60)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 图片（列表展示的小图）
        /// </summary>
        public string Pic { get; set; }

        /// <summary>
        /// 状态：是否上线
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 状态：是否推荐商品
        /// </summary>
        public bool RecommendStatus { get; set; }

        /// <summary>
        /// 原价
        /// </summary>
        public double OriginalPrice { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 排序顺序
        /// </summary>
        public int ShowIndex { get; set; }
    }
}
