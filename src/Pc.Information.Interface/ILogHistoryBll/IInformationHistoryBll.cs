using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pc.Information.Model.InformationLog;

namespace Pc.Information.Interface.ILogHistoryBll
{
    /// <summary>
    /// Deal user chat info.
    /// </summary>
    public interface IInformationHistoryBll
    {
        /// <summary>
        /// Add chat info
        /// </summary>
        /// <param name="chatModel"></param>
        /// <returns></returns>
        long AddChatInfo(PiFInformationLogModel chatModel);
    }
}
