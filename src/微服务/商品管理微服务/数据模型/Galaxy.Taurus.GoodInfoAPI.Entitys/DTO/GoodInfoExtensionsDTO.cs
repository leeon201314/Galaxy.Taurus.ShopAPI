using System.Collections.Generic;

namespace Galaxy.Taurus.GoodInfoAPI.Entitys.DTO
{
    /// <summary>
    /// 商品详细扩展信息DTO
    /// </summary>
    public class GoodInfoExtensionsDTO
    {
        /// <summary>
        /// 商品Id
        /// </summary>
        public string GoodInfoId { get; set; }

        /// <summary>
        /// 店铺Id
        /// </summary>
        public string ShopId { get; set; }

        /// <summary>
        /// 横幅的图片或视频
        /// </summary>
        public List<MediaItem> BannerList { get; set; }

        /// <summary>
        /// 详细信息图片或视频
        /// </summary>
        public List<MediaItem> DescMediaList { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public List<GoodInfoSpecificationItem> SpecificationList { get; set; }
    }

    /// <summary>
    /// 信息图片或视频信息
    /// </summary>
    public class MediaItem
    {
        /// <summary>
        /// 显示位置
        /// </summary>
        public int ShowIndex { get; set; }

        /// <summary>
        /// 图片或视频URL
        /// </summary>
        public string MediaUrl { get; set; }

        /// <summary>
        /// 类型：1:图片 2:视频
        /// </summary>
        public int MediaType { get; set; }
    }

    /// <summary>
    /// 商品规格信息
    /// </summary>
    public class GoodInfoSpecificationItem
    {
        /// <summary>
        /// 显示位置
        /// </summary>
        public int ShowIndex { get; set; }

        /// <summary>
        /// 规格名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 规格值,使用下划线连接，例子：红_白_蓝
        /// </summary>
        public List<SpecificationValueItem> Values { get; set; }
    }

    /// <summary>
    /// 规格属性值
    /// </summary>
    public class SpecificationValueItem
    {

        /// <summary>
        /// 显示位置
        /// </summary>
        public int ShowIndex { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
