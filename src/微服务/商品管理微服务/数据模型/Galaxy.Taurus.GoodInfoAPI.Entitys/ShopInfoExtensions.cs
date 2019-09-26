using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Galaxy.Taurus.GoodInfoAPI.Entitys
{
    [Table("ShopInfoExtensions")]
    public class ShopInfoExtensions
    {
        /// <summary>
        /// 店铺Id
        /// </summary>
        [Key]
        [MaxLength(32)]
        public string ShopId { get; set; }

        /// <summary>
        /// 最大商品分类数
        /// </summary>
        [Required]
        public int LimitCategoryNum { get; set; }

        /// <summary>
        /// 商品分类数据版本，用于实现乐观锁
        /// </summary>
        [MaxLength(32)]
        public string CategoryDataVersion { get; set; }

        /// <summary>
        /// 最大商品数
        /// </summary>
        [Required]
        public int LimitGoodNum { get; set; }

        /// <summary>
        /// 商品数据版本，用于实现乐观锁
        /// </summary>
        [MaxLength(32)]
        public string GoodDataVersion { get; set; }
    }
}
