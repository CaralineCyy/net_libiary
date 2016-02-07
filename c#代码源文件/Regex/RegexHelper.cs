/// <summary>
/// ��˵����Assistant
/// �� �� �ˣ��շ�
/// ��ϵ��ʽ��361983679  
/// ������վ��http://www.sufeinet.com/thread-655-1-1.html
/// </summary>
using System.Text.RegularExpressions;

namespace DotNet.Utilities
{
    /// <summary>
    /// ����������ʽ�Ĺ�����
    /// </summary>    
    public class RegexHelper
    {
        #region ��֤�����ַ����Ƿ���ģʽ�ַ���ƥ��
        /// <summary>
        /// ��֤�����ַ����Ƿ���ģʽ�ַ���ƥ�䣬ƥ�䷵��true
        /// </summary>
        /// <param name="input">�����ַ���</param>
        /// <param name="pattern">ģʽ�ַ���</param>        
        public static bool IsMatch(string input, string pattern)
        {
            return IsMatch(input, pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// ��֤�����ַ����Ƿ���ģʽ�ַ���ƥ�䣬ƥ�䷵��true
        /// </summary>
        /// <param name="input">������ַ���</param>
        /// <param name="pattern">ģʽ�ַ���</param>
        /// <param name="options">ɸѡ����</param>
        public static bool IsMatch(string input, string pattern, RegexOptions options)
        {
            return Regex.IsMatch(input, pattern, options);
        }
        #endregion
    }
}
