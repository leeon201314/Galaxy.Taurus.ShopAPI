using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Galaxy.Taurus.GoodInfoAPI.Entitys
{
    [Table("GoodsCategory")]
    public class GoodsCategory
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        [MaxLength(32)]
        public string Id { get; set; }

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
        /// 显示顺序
        /// </summary>
        public int ShowIndex { get; set; }
    }
}
