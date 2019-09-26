using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Galaxy.Taurus.AuthorizationAPI.Entitys
{
    [Table("UserInfo")]
    public class UserInfo
    {
        /// <summary>
        /// 32位GUID
        /// </summary>
        [Key]
        [MaxLength(32)]
        public string Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string PhoneNumber { get; set; }

        [MaxLength(30)]
        public string UserName { get; set; }

        [MaxLength(32)]
        public string Psw { get; set; }
    }
}
