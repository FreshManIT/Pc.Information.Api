using System;
using Microsoft.AspNetCore.Mvc;
using Pc.Information.Interface.ILogHistoryBll;
using Pc.Information.Model.InformationLog;

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
    }
}
