using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Pc.Information.Interface.ILogHistoryBll;
using Pc.Information.Model.InformationLog;
using Pc.Information.Model.User;

namespace Pc.Information.Api.Controllers
{
    /// <summary>
    /// Chat info history info controller.
    /// </summary>
    public class ChatInfoHistoryController : BaseController
    {
        /// <summary>
        /// Interface user info server.
        /// </summary>
        private IInformationHistoryBll ChatHistoryInfoBll { get; set; }

        /// <summary>
        /// Constructed function.
        /// </summary>
        /// <param name="chatHistoryInfoBll"></param>
        public ChatInfoHistoryController(IInformationHistoryBll chatHistoryInfoBll)
        {
            ChatHistoryInfoBll = chatHistoryInfoBll;
        }

        /// <summary>
        /// Add chat info.
        /// </summary>
        /// <param name="fromId">from id</param>
        /// <param name="toId">send to id</param>
        /// <param name="groupId">group id</param>
        /// <param name="contentType">content type id(0:text;1:image)</param>
        /// <param name="content">sent content</param>
        /// <returns>stock data resulte</returns>
        [HttpGet]
        [HttpPost]
        [Route("AddChatInfo")]
        public ApiResultModel<long> AddChatInfo(int fromId, int toId, int groupId, int contentType, string content)
        {
            var info = 0L;
            if (fromId < 1 || toId < 1 || string.IsNullOrEmpty(content)) return ResponseDataApi(info);
            var chatModel = new PiFInformationLogModel
            {
                PiFFromId = fromId,
                PiFToId = toId,
                PiFToGroupId = groupId,
                PiFContentType = contentType,
                PiFContent = content,
                PiFSendTime = DateTime.Now
            };
            info = ChatHistoryInfoBll.AddChatInfo(chatModel);
            return ResponseDataApi(info);
        }

        /// <summary>
        /// Add new online user info.
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="userName">user name</param>
        /// <param name="ruleType">rule type:0:common user;1:server provider;2:administrator.</param>
        /// <returns>Online user count</returns>
        [HttpGet]
        [Route("AddOnlineUser")]
        public ApiResultModel<long> AddOnlineUser(int userId, string userName, int ruleType)
        {
            if (userId < 1 || string.IsNullOrEmpty(userName) || ruleType < 0) return ResponseDataApi(0L);
            var newUserModel = new OnlineUserModel { UserId = userId, UserName = userName, RuleType = ruleType };
            var onlineUserCount = ChatHistoryInfoBll.AddOnlineUser(newUserModel);
            return ResponseDataApi(onlineUserCount);
        }

        /// <summary>
        /// Remove off online user info.
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="userName">user name</param>
        /// <param name="ruleType">rule type:1:usually user;2:teacher user.</param>
        /// <returns>Online user count</returns>
        [HttpGet]
        [Route("RemoveOnlineUser")]
        public ApiResultModel<long> RemoveOnlineUser(int userId, string userName, int ruleType)
        {
            if (userId < 1 || string.IsNullOrEmpty(userName) || ruleType < 0) return ResponseDataApi(0L);
            var newUserModel = new OnlineUserModel { UserId = userId, UserName = userName, RuleType = ruleType };
            var onlineUserCount = ChatHistoryInfoBll.RemoveOnlineUser(newUserModel);
            return ResponseDataApi(onlineUserCount);
        }

        /// <summary>
        /// Get list range info.
        /// </summary>
        /// <param name="ruleType">rule type:1:usually user;2:teacher user.</param>
        /// <param name="start">start index,begin is zore.</param>
        /// <param name="fail">the fail of list.</param>
        /// <returns>rule type online user list</returns>
        [HttpGet]
        [Route("GetAllOnlineUserModels")]
        public ApiResultModel<List<OnlineUserModel>> GetAllOnlineUserModels(int ruleType = -1, long start = 0,
            long fail = -1)
        {
            var userList = ChatHistoryInfoBll.GetAllOnlineUserModels(ruleType, start, fail);
            return ResponseDataApi(userList);
        }
    }
}
