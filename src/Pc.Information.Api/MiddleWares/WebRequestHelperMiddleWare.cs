using System;
using System.Net.Http;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Pc.Information.Api.MiddleWares
{
    /// <summary>
    /// WebRequest middleware
    /// </summary>
    public class WebRequestHelperMiddleWare
    {
        /// <summary>
        /// Get Querystring or Request.From params,you also can define params in method param use [FromBody]Type params string.
        /// </summary>
        /// <param name="key">params key</param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetStringFromParameters(string key,HttpContext context)
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
