using Microsoft.AspNetCore.Http;

namespace Pc.Information.Utility.Web
{
    /// <summary>
    /// WebRequest extend
    /// </summary>
    public static class WebRequestHelper
    {
        /// <summary>
        /// Get Querystring or Request.From params,you also can define params in method param use [FromBody]Type params string.
        /// </summary>
        /// <param name="context">request context</param>
        /// <param name="key">params key</param>
        /// <returns></returns>
        public static string GetStringFromParameters(this HttpContext context, string key)
        {
            var value = string.Empty;
            if (string.IsNullOrEmpty(key))
            {
                return value;
            }
            if (context != null) value = context.Request?.Query[key];
            return value;
        }
    }
}
