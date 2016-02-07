using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace DotNet.Utilities.�����ļ�������
{
    public class ConfigHelper_sufei
    {
        /// <summary>
        /// ����KeyȡValueֵ
        /// </summary>
        /// <param name="key"></param>
        public static string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key].ToString().Trim();
        }

        /// <summary>
        /// ����Key�޸�Value
        /// </summary>
        /// <param name="key">Ҫ�޸ĵ�Key</param>
        /// <param name="value">Ҫ�޸�Ϊ��ֵ</param>
        public static void SetValue(string key, string value)
        {
            ConfigurationManager.AppSettings.Set(key, value);
        }

        /// <summary>
        /// ����µ�Key ��Value��ֵ��
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        public static void Add(string key, string value)
        {
            ConfigurationManager.AppSettings.Add(key, value);
        }

        /// <summary>
        /// ����Keyɾ����
        /// </summary>
        /// <param name="key">Key</param>
        public static void Remove(string key)
        {
            ConfigurationManager.AppSettings.Remove(key);
        }
    }
}
