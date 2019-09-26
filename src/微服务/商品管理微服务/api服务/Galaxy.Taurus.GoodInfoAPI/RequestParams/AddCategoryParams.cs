using Galaxy.Taurus.GoodInfoAPI.ViewModel;

namespace Galaxy.Taurus.GoodInfoAPI.RequestParams
{
    /// <summary>
    /// 添加分组的参数
    /// </summary>
    public class AddCategoryParams
    {
        /// <summary>
        /// 分组名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int ShowIndex { get; set; }
    }

    /// <summary>
    /// 参数验证
    /// </summary>
    public class AddCategoryParamsValidator
    {
        /// <summary>
        /// 验证参数
        /// </summary>
        public static ResultViewModel Validate(AddCategoryParams addCategoryParams)
        {
            if (addCategoryParams == null)
                return new ResultViewModel { Code = ResultCode.ParamsError, Message = "参数不允许空" };
            else if (string.IsNullOrEmpty(addCategoryParams.Name))
                return new ResultViewModel { Code = ResultCode.ParamsError, Message = "分组名称不允许空" };
            else if (addCategoryParams.Name.Length > 8)
                return new ResultViewModel { Code = ResultCode.ParamsError, Message = "分组名称最多8个字符" };
            else
                return new ResultViewModel { Code = ResultCode.Success };
        }
    }
}
