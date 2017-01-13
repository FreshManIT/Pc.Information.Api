using System;
using Pc.Information.Model.Enum;

namespace Pc.Information.Model.ErrorInfoLog
{
    /// <summary>
    /// Error Info log model
    /// </summary>
    public class ErrorInfoLogModel
    {
        /// <summary>
        /// construct function.
        /// </summary>
        public ErrorInfoLogModel()
        {
            ErrorTime = DateTime.Now;
        }

        /// <summary>
        /// Primary key.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Request contentType
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// error message
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Inner message.
        /// </summary>
        public string InnerErrorMessage { get; set; }

        /// <summary>
        /// Error type full name
        /// </summary>
        public string ErrorTypeFullName { get; set; }

        /// <summary>
        /// Error stack trace.
        /// </summary>
        public string StackTrace { get; set; }

        /// <summary>
        /// product error time
        /// </summary>
        public DateTime ErrorTime { get; set; }

        /// <summary>
        /// 异常枚举类型
        /// </summary>
        public ErrorTypeEnum ErrorTyp { get; set; }
    }
}
