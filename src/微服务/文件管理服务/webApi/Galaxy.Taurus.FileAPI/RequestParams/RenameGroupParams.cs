using Galaxy.Taurus.FileAPI.ViewModels;

namespace Galaxy.Taurus.FileAPI.RequestParams
{
    /// <summary>
    /// 重命名分组的参数
    /// </summary>
    public class RenameGroupParams
    {
        /// <summary>
        /// 分组Id
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// 分组名
        /// </summary>
        public string GroupName { get; set; }
    }

    /// <summary>
    /// 参数验证
    /// </summary>
    public class RenameGroupParamsValidator
    {
        /// <summary>
        /// 验证参数
        /// </summary>
        public static MessageViewModel Validate(RenameGroupParams renameGroupParams)
        {
            if (renameGroupParams == null)
                return new MessageViewModel { Code = MessageCode.ParamsError, Message = "参数不允许空" };
            else if (string.IsNullOrEmpty(renameGroupParams.GroupId))
                return new MessageViewModel { Code = MessageCode.ParamsError, Message = "分组Id不允许空" };
            else if (string.IsNullOrEmpty(renameGroupParams.GroupName))
                return new MessageViewModel { Code = MessageCode.ParamsError, Message = "分组名称不允许空" };
            else if (renameGroupParams.GroupName.Length > 8)
                return new MessageViewModel { Code = MessageCode.ParamsError, Message = "分组名称最多8个字符" };
            else
                return new MessageViewModel { Code = MessageCode.Success };
        }
    }
}
