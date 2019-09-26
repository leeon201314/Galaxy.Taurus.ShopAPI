using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Galaxy.Taurus.FileAPI.Entitys
{
    [Table("FileData")]
    public class FileData
    {
        /// <summary>
        /// 32位GUID
        /// </summary>
        [Key]
        [MaxLength(32)]
        public string FileId { get; set; }

        /// <summary>
        /// 文件类型 例如：jpg|jpeg|png|gif等
        /// </summary>
        [Required]
        [MaxLength(12)]
        public string FileType { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Required]
        public DateTime VersionDateTime { get; set; }

        /// <summary>
        /// 店铺Id
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string ShopId { get; set; }

        /// <summary>
        /// 虚拟分组Id
        /// </summary>
        [MaxLength(32)]
        public string GroupId { get; set; }

        /// <summary>
        /// 网络路径
        /// </summary>
        [NotMapped]
        public string URL { get; set; }
    }
}
