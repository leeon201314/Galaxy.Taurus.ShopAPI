using Galaxy.Taurus.GoodInfoAPI.RequestParams;

namespace Galaxy.Taurus.GoodInfoAPI.ViewModel
{
    /// <summary>
    /// 商品价格
    /// </summary>
    public class GoodPriceViewModel: GetGoodPriceParams
    {
        /// <summary>
        /// 价格
        /// </summary>
        public double Price { get; set; }
    }
}
