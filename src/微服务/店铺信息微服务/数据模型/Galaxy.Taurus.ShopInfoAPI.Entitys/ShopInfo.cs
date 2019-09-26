using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Galaxy.Taurus.ShopInfoAPI.Entitys
{
    [Table("ShopInfo")]
    public class ShopInfo
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        [MaxLength(32)]
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(60)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string ShopDesc { get; set; }

        /// <summary>
        /// 联系地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string LinkPhone { get; set; }

        /// <summary>
        /// 客服QQ
        /// </summary>
        public string QQ { get; set; }

        /// <summary>
        /// 客服微信
        /// </summary>
        public string WeChat { get; set; }

        /// <summary>
        /// 营业时间
        /// </summary>
        public string OpeningHours { get; set; }
    }
}
