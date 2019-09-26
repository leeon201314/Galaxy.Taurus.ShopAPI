using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Galaxy.Taurus.AuthorizationAPI.Entitys
{
    [Table("R_User_Role")]
    public class UserRoleRelation
    {
        [MaxLength(32)]
        [Key, Column(Order = 0)]
        public string UserId { get; set; }

        [MaxLength(32)]
        [Key, Column(Order = 1)]
        public string ShopId { get; set; }

        [MaxLength(32)]
        [Key, Column(Order = 2)]
        public string RoleName { get; set; }
    }
}
