using Galaxy.Taurus.FileAPI.Entitys;
using Galaxy.Taurus.FileAPI.IBusiness;
using Galaxy.Taurus.FileAPI.RequestParams;
using Galaxy.Taurus.FileAPI.Util;
using Galaxy.Taurus.FileAPI.ViewModels;
using Galaxy.Taurus.ShopInfoAPI.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Galaxy.Taurus.FileAPI.Controllers
{
    /// <summary>
    /// 文件分组信息API
    /// </summary>
    [Route("fileAPI/[controller]")]
    [ApiController]
    public class FileGroupController : Controller
    {
        /// <summary>
        /// 文件分组业务处理
        /// </summary>
        public IFileGroupBusiness fileGroupBusiness;

        /// <summary>
        /// 构造
        /// </summary>
        public FileGroupController(IFileGroupBusiness fileGroupBusiness)
        {
            this.fileGroupBusiness = fileGroupBusiness;
        }

        /// <summary>
        /// 删除分组
        /// </summary>
        [HttpPost("delete/{groupId}")]
        [Authorize("FileInfoManage")]
        public MessageViewModel Delete(string groupId)
        {
            if (string.IsNullOrEmpty(groupId))
                return new MessageViewModel { Code = MessageCode.ParamsError, Message = "分组Id不允许空" };

            fileGroupBusiness.Delete(this.CurrentAuthShopId(), groupId);
            return new MessageViewModel { Code = MessageCode.Success, Message = "删除成功！" };
        }

        /// <summary>
        ///添加分组
        /// </summary>
        [HttpPost("AddGroup")]
        [Authorize("FileInfoManage")]
        public async Task<MessageViewModel> AddGroup([FromBody] AddGroupParams addGroupParams)
        {
            MessageViewModel msg = AddGroupParamsValidator.Validate(addGroupParams);
            if (msg.Code != MessageCode.Success)
                return msg;

            FileGroup fileGroup = new FileGroup();
            fileGroup.ShopId = this.CurrentAuthShopId();
            fileGroup.Name = addGroupParams.GroupName;
            DataResult dataResult = await fileGroupBusiness.Add(fileGroup);

            return AutoMapperUtil.Singleton.Map<DataResult, MessageViewModel>(dataResult);
        }

        /// <summary>
        /// 修改分组
        /// </summary>
        [HttpPost("UpdateName")]
        [Authorize("FileInfoManage")]
        public async Task<MessageViewModel> UpdateName([FromBody] RenameGroupParams renameGroupParams)
        {
            MessageViewModel msg = RenameGroupParamsValidator.Validate(renameGroupParams);
            if (msg.Code != MessageCode.Success)
                return msg;

            FileGroup group = await fileGroupBusiness.Get(this.CurrentAuthShopId(), renameGroupParams.GroupId);

            if (group == null)
                return new MessageViewModel { Code = MessageCode.Fail, Message = "分组不存在" };

            group.Name = renameGroupParams.GroupName;
            fileGroupBusiness.Update(group);
            return new MessageViewModel { Code = MessageCode.Success, Message = "重命名成功" };
        }

        /// <summary>
        /// 通过授权的ShopId查找分组
        /// </summary>
        [HttpGet("GetByShopId")]
        [Authorize("FileInfoManage")]
        public async Task<MessageViewModel> GetByShopId()
        {
            return new MessageViewModel
            {
                Code = MessageCode.Success,
                Data = await fileGroupBusiness.GetByShopId(this.CurrentAuthShopId())
            };
        }
    }
}
