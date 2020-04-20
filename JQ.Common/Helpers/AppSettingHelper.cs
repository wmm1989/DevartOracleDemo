using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JQ.Common.Helpers
{
    public class AppSettingHelper
    {
        static IConfiguration Configuration { get; set; }
        static AppSettingHelper()
        {
            //ReloadOnChange=true   当appsetting.json修改过后自动加载
            Configuration = new ConfigurationBuilder()
                .Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
                .Build();
        }
        /// <summary>
        /// 按照层级获取
        /// </summary>
        /// <param name="sections"></param>
        /// <returns></returns>
        public static string App(params string[] sections)
        {
            try
            {
                var val = string.Empty;
                for (int i = 0; i < sections.Length; i++)
                {
                    val += sections[i] + ":";
                }

                return Configuration[val.TrimEnd(':')];
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string App(string section)
        {
            string[] sections = section.Split(":");
            return App(sections);
        }

        /// <summary>
        /// 只能获取123层，且支持最后一个加密解密处理
        /// </summary>
        /// <param name="sections"></param>
        /// <returns></returns>
        public static string App(string p1, string p2, string p3 = "")
        {
            string[] sections = new string[] { p1, p2, p3 };

            string val =  App(sections);

            return val;
            
        }
    }
}
