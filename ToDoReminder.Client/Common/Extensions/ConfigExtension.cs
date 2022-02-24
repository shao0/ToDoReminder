using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoReminder.Client.Common.Extensions
{
    public static class ConfigExtension
    {
        //依据连接串名字connectionName返回数据连接字符串  
        public static string GetConnectionStringsConfig(this string key)
        {
            if(string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
            var value = ConfigurationManager.AppSettings[key];
            return value;
        }
    }
}
