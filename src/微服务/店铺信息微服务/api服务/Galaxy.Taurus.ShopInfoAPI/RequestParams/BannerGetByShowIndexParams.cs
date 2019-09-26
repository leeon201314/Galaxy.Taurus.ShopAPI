using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxy.Taurus.ShopInfoAPI.RequestParams
{
    public class BannerGetByShowIndexParams
    {
        /// <summary>
        /// 店铺ID
        /// </summary>
        public string ShopId { get; set; }

        /// <summary>
        ///显示顺序
        /// </summary>
        public int ShowIndex { get; set; }
    }
}
