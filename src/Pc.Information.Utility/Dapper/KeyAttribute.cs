using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pc.Information.Utility.Dapper
{
    /// <summary>
    /// Optional Key attribute.
    /// You can use the System.ComponentModel.DataAnnotations version in its place to specify the Primary Key of a poco
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class KeyAttribute : Attribute
    {
    }
}
