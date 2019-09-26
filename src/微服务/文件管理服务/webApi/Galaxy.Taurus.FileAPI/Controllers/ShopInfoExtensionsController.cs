using Galaxy.Taurus.FileAPI.Entitys;
using Galaxy.Taurus.FileAPI.IBusiness;
using Microsoft.AspNetCore.Mvc;

namespace Galaxy.Taurus.FileAPI.Controllers
{
    [Route("fileAPI/[controller]")]
    public class ShopInfoExtensionsController : Controller
    {
        private IShopInfoExtensionsBusiness shopInfoExtensionsBusiness;

        /// <summary>
        /// 扩展信息
        /// </summary>
        /// <param name="shopInfoExtensionsBusiness"></param>
        public ShopInfoExtensionsController(IShopInfoExtensionsBusiness shopInfoExtensionsBusiness)
        {
            this.shopInfoExtensionsBusiness = shopInfoExtensionsBusiness;
        }

        /// <summary>
        /// 获取
        /// </summary>
        [HttpGet("Get/{shopId}")]
        public ShopInfoExtensions Get(string shopId)
        {
            return shopInfoExtensionsBusiness.Get(shopId);
        }
    }
}
