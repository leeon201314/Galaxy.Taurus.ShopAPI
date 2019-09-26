namespace Galaxy.Taurus.GoodInfoAPI.RequestParams
{
    /// <summary>
    /// 商品信息查询参数
    /// </summary>
    public class GoodInfoQueryParams
    {
        /// <summary>
        /// 店铺ID
        /// </summary>
        public string ShopId { get; set; }

        /// <summary>
        /// 分类ID
        /// </summary>
        public string CategoryId { get; set; }

        /// <summary>
        /// 状态：是否上线 0:下线  1：上线 其他：所有
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 推荐状态：是否推荐 0:未推荐  1：推荐 其他：所有
        /// </summary>
        public int recommendStatus { get; set; }

        /// <summary>
        /// 分页大小，0时不分页获取所有
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 第几页：从1开始
        /// </summary>
        public int PageIndex { get; set; }
    }
}
