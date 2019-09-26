using Galaxy.Taurus.FileAPI.Entitys;
using Galaxy.Taurus.FileAPI.IBusiness;
using Galaxy.Taurus.FileAPI.ServiceConfig;
using Galaxy.Taurus.FileAPI.Util;
using Galaxy.Taurus.FileAPI.ViewModels;
using Galaxy.Taurus.ShopInfoAPI.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Galaxy.Taurus.FileAPI.Controllers
{
    /// <summary>
    /// 文件信息API
    /// </summary>
    [Route("fileAPI/[controller]")]
    [ApiController]
    public class FileDataController : ControllerBase
    {
        /// <summary>
        /// 文件信息业务处理
        /// </summary>
        public IFileDataBusiness fileDataBusiness;

        /// <summary>
        /// 文件分组业务处理
        /// </summary>
        public IFileGroupBusiness fileGroupBusiness;

        /// <summary>
        /// 构造
        /// </summary>
        public FileDataController(IFileDataBusiness fileDataBusiness, IFileGroupBusiness fileGroupBusiness)
        {
            this.fileDataBusiness = fileDataBusiness;
            this.fileGroupBusiness = fileGroupBusiness;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        [HttpPost("delete/{fileId}")]
        [Authorize("FileInfoManage")]
        public MessageViewModel Delete(string fileId)
        {
            MessageViewModel msg = new MessageViewModel();
            var res = fileDataBusiness.Delete(this.CurrentAuthShopId(), fileId);
            msg.Code = res ? MessageCode.Success : MessageCode.Fail;
            return msg;
        }

        /// <summary>
        /// 修改所在分组
        /// </summary>
        [HttpGet("UpdateGroup/{fileId}/{groupId}")]
        [Authorize("FileInfoManage")]
        public MessageViewModel UpdateGroup(string fileId, string groupId)
        {
            if (string.IsNullOrEmpty(fileId))
                return new MessageViewModel { Code = MessageCode.ParamsError, Message = "文件Id为空！" };

            MessageViewModel msg = new MessageViewModel();
            var data = fileDataBusiness.UpdateGroup(this.CurrentAuthShopId(), fileId, groupId);
            msg.Code = data != null ? MessageCode.Success : MessageCode.Fail;
            msg.Data = data;
            return msg;
        }

        /// <summary>
        /// 添加文件
        /// </summary>
        [HttpPost("Add/{groupId}")]
        [Authorize("FileInfoManage")]
        public async Task<MessageViewModel> Add([FromForm]IFormCollection formCollection, string groupId)
        {
            if (formCollection == null || formCollection.Files.Count <= 0)
                return new MessageViewModel { Code = MessageCode.ParamsError, Message = "文件为空！" };

            IFormFile fileItem = formCollection.Files[0];//只接受第一个

            if (fileItem.Length > 1024 * 500)
                return new MessageViewModel { Code = MessageCode.ParamsError, Message = "只能上传jpg/png文件，且不超过500kb" };

            if (!(fileItem.FileName.EndsWith(".png") || fileItem.FileName.EndsWith(".jpg") || fileItem.FileName.EndsWith(".jpeg")))
                return new MessageViewModel { Code = MessageCode.ParamsError, Message = "只能上传jpg/png文件，且不超过500kb" };

            FileData fileData = new FileData();
            fileData.FileId = Guid.NewGuid().ToString("N");
            fileData.GroupId = string.IsNullOrEmpty(groupId) ? "" : groupId;
            fileData.ShopId = this.CurrentAuthShopId();
            fileData.VersionDateTime = DateTime.Now;

            int index = fileItem.FileName.LastIndexOf(".");
            fileData.FileType = (index > 0 && index < fileItem.FileName.Length) ? fileItem.FileName.Substring(index + 1, fileItem.FileName.Length - index - 1) : "temp";
            DataResult dataResult = await fileDataBusiness.Add(fileData);

            if (dataResult.Code == DataResultCode.Success)
            {
                // string directoryPath = $"E://leeon201314/{fileData.ShopId}";
                string directoryPath = $"{ServiceConfigInfo.Single.MainDirectory}/{fileData.ShopId}";
                string fullPath = $"{directoryPath}/{fileData.FileId}.{fileData.FileType}";

                if (!Directory.Exists(directoryPath))//如果不存在就创建文件夹
                    Directory.CreateDirectory(directoryPath);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    fileItem.CopyTo(stream);
                }
            }

            return AutoMapperUtil.Singleton.Map<DataResult, MessageViewModel>(dataResult);
        }

        /// <summary>
        /// 通过Id获取文件
        /// </summary>
        [HttpGet("Get/{fileId}")]
        public FileData Get(string fileId)
        {
            var res = fileDataBusiness.Get(this.CurrentAuthShopId(), fileId);

            if (res != null)
            {
                InitFileDataUrl(res);
            }

            return res;
        }

        /// <summary>
        /// 通过groupId获取文件,分页从1开始
        /// </summary>
        [HttpGet("GetByGroup/{groupId}/{pageSize}/{pageIndex}")]
        [Authorize("FileInfoManage")]
        public async Task<MessageViewModel> GetByGroup(string groupId, int pageSize, int pageIndex)
        {
            if (pageIndex <= 0)
                pageIndex = 1;

            if (pageSize >= 200)
                pageSize = 200;
            else if (pageSize <= 0)
                pageSize = 20;

            int total;
            var res = await fileDataBusiness.GetByGroup(this.CurrentAuthShopId(), groupId, out total, pageSize, pageIndex);


            if (res != null)
            {
                foreach (var item in res)
                {
                    InitFileDataUrl(item);
                }
            }

            PageDataViewModel pageDataViewModel = new PageDataViewModel { Total = total, Data = res };
            return new MessageViewModel { Code = MessageCode.Success, Data = pageDataViewModel, Message = "获取成功！" };
        }

        /// <summary>
        /// 填充网络Url
        /// </summary>
        private void InitFileDataUrl(FileData fileData)
        {
            fileData.URL = $"{ServiceConfigInfo.Single.BaseUrl}/{fileData.ShopId}/{fileData.FileId}.{fileData.FileType}";
        }
    }
}
