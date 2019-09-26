namespace Galaxy.Taurus.OrderingAPI.ViewModel
{
    public class MemberOrderQueryParams
    {
        /// <summary>
        /// 订单状态：0->待付款；1->待发货；2->已发货；3->已完成；4->已关闭；5->无效订单
        /// </summary>
        public int OrderStatus { get; set; }

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
