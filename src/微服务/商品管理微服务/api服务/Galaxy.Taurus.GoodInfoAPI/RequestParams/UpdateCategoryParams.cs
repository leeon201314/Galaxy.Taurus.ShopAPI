using Galaxy.Taurus.GoodInfoAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxy.Taurus.GoodInfoAPI.RequestParams
{
    public class UpdateCategoryParams
    {
        /// <summary>
        /// 分组Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 分组名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int ShowIndex { get; set; }
    }

    public class UpdateCategoryParamsValidator
    {
        /// <summary>
        /// 验证参数
        /// </summary>
        public static ResultViewModel Validate(UpdateCategoryParams updateCategoryParams)
        {
            if (updateCategoryParams == null)
                return new ResultViewModel { Code = ResultCode.ParamsError, Message = "参数不允许空" };
            else if (string.IsNullOrEmpty(updateCategoryParams.Id))
                return new ResultViewModel { Code = ResultCode.ParamsError, Message = "分组Id不允许空" };
            else if (string.IsNullOrEmpty(updateCategoryParams.Name))
                return new ResultViewModel { Code = ResultCode.ParamsError, Message = "分组名称不允许空" };
            else if (updateCategoryParams.Name.Length > 8)
                return new ResultViewModel { Code = ResultCode.ParamsError, Message = "分组名称最多8个字符" };
            else
                return new ResultViewModel { Code = ResultCode.Success };
        }
    }
}
