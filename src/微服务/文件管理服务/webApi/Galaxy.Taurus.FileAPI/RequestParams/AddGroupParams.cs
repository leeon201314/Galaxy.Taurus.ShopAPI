using Galaxy.Taurus.FileAPI.ViewModels;

namespace Galaxy.Taurus.FileAPI.RequestParams
{
    /// <summary>
    /// 添加分组的参数
    /// </summary>
    public class AddGroupParams
    {
        /// <summary>
        /// 分组名
        /// </summary>
        public string GroupName { get; set; }
    }

    /// <summary>
    /// 参数验证
    /// </summary>
    public class AddGroupParamsValidator
    {
        /// <summary>
        /// 验证参数
        /// </summary>
        public static MessageViewModel Validate(AddGroupParams addGroupParams)
        {
            if (addGroupParams == null)
                return new MessageViewModel { Code = MessageCode.ParamsError, Message = "参数不允许空" };
            else if (string.IsNullOrEmpty(addGroupParams.GroupName))
                return new MessageViewModel { Code = MessageCode.ParamsError, Message = "分组名称不允许空" };
            else if (addGroupParams.GroupName.Length > 8)
                return new MessageViewModel { Code = MessageCode.ParamsError, Message = "分组名称最多8个字符" };
            else
                return new MessageViewModel { Code = MessageCode.Success };
        }
    }
}
