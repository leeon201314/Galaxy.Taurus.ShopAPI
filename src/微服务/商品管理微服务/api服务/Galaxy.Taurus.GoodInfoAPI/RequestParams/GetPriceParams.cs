namespace Galaxy.Taurus.GoodInfoAPI.RequestParams
{
    /// <summary>
    /// 根据规格值获取商品价格的参数
    /// </summary>
    public class GetGoodPriceParams
    {
        /// <summary>
        /// 店铺Id
        /// </summary>
        public string ShopId { get; set; }

        /// <summary>
        /// 商品Id
        /// </summary>
        public string GoodInfoId { get; set; }

        /// <summary>
        /// 规格1
        /// </summary>
        public string Specification1 { get; set; }

        /// <summary>
        /// 规格值1
        /// </summary>
        public string SpecificationValue1 { get; set; }

        /// <summary>
        /// 规格2
        /// </summary>
        public string Specification2 { get; set; }

        /// <summary>
        /// 规格值2
        /// </summary>
        public string SpecificationValue2 { get; set; }

        /// <summary>
        /// 规格3
        /// </summary>
        public string Specification3 { get; set; }

        /// <summary>
        /// 规格值3
        /// </summary>
        public string SpecificationValue3 { get; set; }
    }
}
