using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Galaxy.Taurus.FileAPI.Entitys
{
    /// <summary>
    /// 文件虚拟分组
    /// </summary>
    [Table("FileGroup")]
    public class FileGroup
    {
        [Key]
        [MaxLength(32)]
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(32)]
        public string ParentId { get; set; }

        [Required]
        [MaxLength(32)]
        public string ShopId { get; set; }
    }
}
