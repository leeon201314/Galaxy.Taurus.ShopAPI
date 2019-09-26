using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Galaxy.Taurus.ShopInfoAPI.Entitys
{
    [Table("HomeBanner")]
    public class HomeBanner
    {
        /// <summary>
        /// ShopId
        /// </summary>
        [MaxLength(32)]
        [Key, Column(Order = 1)]
        public string ShopId { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [Key, Column(Order = 2)]
        public int ShowIndex { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        [MaxLength(255)]
        [Required]
        public string PicUrl { get; set; }
    }
}
