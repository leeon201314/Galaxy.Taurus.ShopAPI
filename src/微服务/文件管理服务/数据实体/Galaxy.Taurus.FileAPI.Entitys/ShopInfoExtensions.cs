using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Galaxy.Taurus.FileAPI.Entitys
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
        /// 最大分组数
        /// </summary>
        [Required]
        public int LimitGroupNum { get; set; }

        /// <summary>
        /// 分组数据版本，用于实现乐观锁
        /// </summary>
        [MaxLength(32)]
        public string GroupDataVersion { get; set; }

        /// <summary>
        /// 最大文件数
        /// </summary>
        [Required]
        public int LimitFileNum { get; set; }

        /// <summary>
        /// 文件数据版本，用于实现乐观锁
        /// </summary>
        [MaxLength(32)]
        public string FileDataVersion { get; set; }
    }
}
