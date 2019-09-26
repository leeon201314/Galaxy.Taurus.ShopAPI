using System.Collections.Generic;

namespace Galaxy.Taurus.AuthorizationAPI.Entitys
{
    /// <summary>
    /// 权限操作
    /// </summary>
    public class Operations
    {
        public static readonly Operation ShopInfoManage = new Operation("ShopInfoManage", "店铺信息管理");

        public static readonly Operation GoodInfoManage = new Operation("GoodInfoManage", "商品信息管理");

        public static readonly Operation OrderManage = new Operation("OrderManage", "订单信息管理");

        public static readonly Operation FileInfoManage = new Operation("FileInfoManage", "文件管理");

        public static readonly Operation SubmitOrder = new Operation("SubmitOrder", "提交订单");

        public static List<Operation> All()
        {
            List<Operation> opList = new List<Operation>();
            opList.Add(Operations.ShopInfoManage);
            opList.Add(Operations.GoodInfoManage);
            opList.Add(Operations.FileInfoManage);
            opList.Add(Operations.OrderManage);
            opList.Add(Operations.SubmitOrder);
            return opList;
        }
    }

    /// <summary>
    /// 权限操作
    /// </summary>
    public class Operation
    {
        public string Name { get; private set; }

        public string OpDesc { get; private set; }

        public Operation(string name, string opDesc)
        {
            Name = name;
            OpDesc = opDesc;
        }
    }
}
