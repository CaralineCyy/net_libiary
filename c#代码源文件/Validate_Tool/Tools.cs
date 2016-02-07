/// <summary>
/// ��˵����Assistant
/// �� �� �ˣ��շ�
/// ��ϵ��ʽ��361983679  
/// ������վ��http://www.sufeinet.com/thread-655-1-1.html
/// </summary>
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace DotNet.Utilities
{
    /// <summary>
    /// ���ù�����
    /// </summary>
    public static class Tools
    {
        #region ����û�IP
        /// <summary>
        /// ����û�IP
        /// </summary>
        public static string GetUserIp()
        {
            string ip;
            string[] temp;
            bool isErr = false;
            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_ForWARDED_For"] == null)
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            else
                ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_ForWARDED_For"].ToString();
            if (ip.Length > 15)
                isErr = true;
            else
            {
                temp = ip.Split('.');
                if (temp.Length == 4)
                {
                    for (int i = 0; i < temp.Length; i++)
                    {
                        if (temp[i].Length > 3) isErr = true;
                    }
                }
                else
                    isErr = true;
            }

            if (isErr)
                return "1.1.1.1";
            else
                return ip;
        }
        #endregion

        #region �������ö�ָ���ַ������� MD5 ����
        /// <summary>
        /// �������ö�ָ���ַ������� MD5 ����
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetMD5(string s)
        {
            //md5����
            s = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(s, "md5").ToString();

            return s.ToLower().Substring(8, 16);
        }
        #endregion

        #region �õ��ַ������ȣ�һ�����ֳ���Ϊ2
        /// <summary>
        /// �õ��ַ������ȣ�һ�����ֳ���Ϊ2
        /// </summary>
        /// <param name="inputString">�����ַ���</param>
        /// <returns></returns>
        public static int StrLength(string inputString)
        {
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            int tempLen = 0;
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                    tempLen += 2;
                else
                    tempLen += 1;
            }
            return tempLen;
        }
        #endregion

        #region ��ȡָ�������ַ���
        /// <summary>
        /// ��ȡָ�������ַ���
        /// </summary>
        /// <param name="inputString">Ҫ������ַ���</param>
        /// <param name="len">ָ������</param>
        /// <returns>���ش������ַ���</returns>
        public static string ClipString(string inputString, int len)
        {
            bool isShowFix = false;
            if (len % 2 == 1)
            {
                isShowFix = true;
                len--;
            }
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            int tempLen = 0;
            string tempString = "";
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                    tempLen += 2;
                else
                    tempLen += 1;

                try
                {
                    tempString += inputString.Substring(i, 1);
                }
                catch
                {
                    break;
                }

                if (tempLen > len)
                    break;
            }

            byte[] mybyte = System.Text.Encoding.Default.GetBytes(inputString);
            if (isShowFix && mybyte.Length > len)
                tempString += "��";
            return tempString;
        }
        #endregion

        #region ����������ڵļ��
        /// <summary>
        /// ����������ڵļ��
        /// </summary>
        /// <param name="DateTime1">����һ��</param>
        /// <param name="DateTime2">���ڶ���</param>
        /// <returns>���ڼ��TimeSpan��</returns>
        public static TimeSpan DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            return ts;
        }
        #endregion

        #region ��ʽ������ʱ��
        /// <summary>
        /// ��ʽ������ʱ��
        /// </summary>
        /// <param name="dateTime1">����ʱ��</param>
        /// <param name="dateMode">��ʾģʽ</param>
        /// <returns>0-9��ģʽ������</returns>
        public static string FormatDate(DateTime dateTime1, string dateMode)
        {
            switch (dateMode)
            {
                case "0":
                    return dateTime1.ToString("yyyy-MM-dd");
                case "1":
                    return dateTime1.ToString("yyyy-MM-dd HH:mm:ss");
                case "2":
                    return dateTime1.ToString("yyyy/MM/dd");
                case "3":
                    return dateTime1.ToString("yyyy��MM��dd��");
                case "4":
                    return dateTime1.ToString("MM-dd");
                case "5":
                    return dateTime1.ToString("MM/dd");
                case "6":
                    return dateTime1.ToString("MM��dd��");
                case "7":
                    return dateTime1.ToString("yyyy-MM");
                case "8":
                    return dateTime1.ToString("yyyy/MM");
                case "9":
                    return dateTime1.ToString("yyyy��MM��");
                default:
                    return dateTime1.ToString();
            }
        }
        #endregion

        #region �õ��������
        /// <summary>
        /// �õ��������
        /// </summary>
        /// <param name="time1">��ʼ����</param>
        /// <param name="time2">��������</param>
        /// <returns>�������֮��� �������</returns>
        public static DateTime GetRandomTime(DateTime time1, DateTime time2)
        {
            Random random = new Random();
            DateTime minTime = new DateTime();
            DateTime maxTime = new DateTime();

            System.TimeSpan ts = new System.TimeSpan(time1.Ticks - time2.Ticks);

            // ��ȡ����ʱ�����������
            double dTotalSecontds = ts.TotalSeconds;
            int iTotalSecontds = 0;

            if (dTotalSecontds > System.Int32.MaxValue)
            {
                iTotalSecontds = System.Int32.MaxValue;
            }
            else if (dTotalSecontds < System.Int32.MinValue)
            {
                iTotalSecontds = System.Int32.MinValue;
            }
            else
            {
                iTotalSecontds = (int)dTotalSecontds;
            }


            if (iTotalSecontds > 0)
            {
                minTime = time2;
                maxTime = time1;
            }
            else if (iTotalSecontds < 0)
            {
                minTime = time1;
                maxTime = time2;
            }
            else
            {
                return time1;
            }

            int maxValue = iTotalSecontds;

            if (iTotalSecontds <= System.Int32.MinValue)
                maxValue = System.Int32.MinValue + 1;

            int i = random.Next(System.Math.Abs(maxValue));

            return minTime.AddSeconds(i);
        }
        #endregion

        #region HTMLת�г�TEXT
        /// <summary>
        /// HTMLת�г�TEXT
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string HtmlToTxt(string strHtml)
        {
            string[] aryReg ={
            @"<script[^>]*?>.*?</script>",
            @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
            @"([\r\n])[\s]+",
            @"&(quot|#34);",
            @"&(amp|#38);",
            @"&(lt|#60);",
            @"&(gt|#62);", 
            @"&(nbsp|#160);", 
            @"&(iexcl|#161);",
            @"&(cent|#162);",
            @"&(pound|#163);",
            @"&(copy|#169);",
            @"&#(\d+);",
            @"-->",
            @"<!--.*\n"
            };

            string newReg = aryReg[0];
            string strOutput = strHtml;
            for (int i = 0; i < aryReg.Length; i++)
            {
                Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, string.Empty);
            }

            strOutput.Replace("<", "");
            strOutput.Replace(">", "");
            strOutput.Replace("\r\n", "");


            return strOutput;
        }
        #endregion

        #region �ж϶����Ƿ�Ϊ��
        /// <summary>
        /// �ж϶����Ƿ�Ϊ�գ�Ϊ�շ���true
        /// </summary>
        /// <typeparam name="T">Ҫ��֤�Ķ��������</typeparam>
        /// <param name="data">Ҫ��֤�Ķ���</param>        
        public static bool IsNullOrEmpty<T>(T data)
        {
            //���Ϊnull
            if (data == null)
            {
                return true;
            }

            //���Ϊ""
            if (data.GetType() == typeof(String))
            {
                if (string.IsNullOrEmpty(data.ToString().Trim()))
                {
                    return true;
                }
            }

            //���ΪDBNull
            if (data.GetType() == typeof(DBNull))
            {
                return true;
            }

            //��Ϊ��
            return false;
        }

        /// <summary>
        /// �ж϶����Ƿ�Ϊ�գ�Ϊ�շ���true
        /// </summary>
        /// <param name="data">Ҫ��֤�Ķ���</param>
        public static bool IsNullOrEmpty(object data)
        {
            //���Ϊnull
            if (data == null)
            {
                return true;
            }

            //���Ϊ""
            if (data.GetType() == typeof(String))
            {
                if (string.IsNullOrEmpty(data.ToString().Trim()))
                {
                    return true;
                }
            }

            //���ΪDBNull
            if (data.GetType() == typeof(DBNull))
            {
                return true;
            }

            //��Ϊ��
            return false;
        }
        #endregion

        #region ��֤IP��ַ�Ƿ�Ϸ�
        /// <summary>
        /// ��֤IP��ַ�Ƿ�Ϸ�
        /// </summary>
        /// <param name="ip">Ҫ��֤��IP��ַ</param>        
        public static bool IsIP(string ip)
        {
            //���Ϊ�գ���Ϊ��֤�ϸ�
            if (IsNullOrEmpty(ip))
            {
                return true;
            }

            //���Ҫ��֤�ַ����еĿո�
            ip = ip.Trim();

            //ģʽ�ַ���
            string pattern = @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$";

            //��֤
            return RegexHelper.IsMatch(ip, pattern);
        }
        #endregion

        #region  ��֤EMail�Ƿ�Ϸ�
        /// <summary>
        /// ��֤EMail�Ƿ�Ϸ�
        /// </summary>
        /// <param name="email">Ҫ��֤��Email</param>
        public static bool IsEmail(string email)
        {
            //���Ϊ�գ���Ϊ��֤���ϸ�
            if (IsNullOrEmpty(email))
            {
                return false;
            }

            //���Ҫ��֤�ַ����еĿո�
            email = email.Trim();

            //ģʽ�ַ���
            string pattern = @"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$";

            //��֤
            return RegexHelper.IsMatch(email, pattern);
        }
        #endregion

        #region ��֤�Ƿ�Ϊ����
        /// <summary>
        /// ��֤�Ƿ�Ϊ���� ���Ϊ�գ���Ϊ��֤���ϸ� ����false
        /// </summary>
        /// <param name="number">Ҫ��֤������</param>        
        public static bool IsInt(string number)
        {
            //���Ϊ�գ���Ϊ��֤���ϸ�
            if (IsNullOrEmpty(number))
            {
                return false;
            }

            //���Ҫ��֤�ַ����еĿո�
            number = number.Trim();

            //ģʽ�ַ���
            string pattern = @"^[0-9]+[0-9]*$";

            //��֤
            return RegexHelper.IsMatch(number, pattern);
        }
        #endregion

        #region ��֤�Ƿ�Ϊ����
        /// <summary>
        /// ��֤�Ƿ�Ϊ����
        /// </summary>
        /// <param name="number">Ҫ��֤������</param>        
        public static bool IsNumber(string number)
        {
            //���Ϊ�գ���Ϊ��֤���ϸ�
            if (IsNullOrEmpty(number))
            {
                return false;
            }

            //���Ҫ��֤�ַ����еĿո�
            number = number.Trim();

            //ģʽ�ַ���
            string pattern = @"^[0-9]+[0-9]*[.]?[0-9]*$";

            //��֤
            return RegexHelper.IsMatch(number, pattern);
        }
        #endregion

        #region ��֤�����Ƿ�Ϸ�
        /// <summary>
        /// ��֤�����Ƿ�Ϸ�,�Բ���������˼򵥴���
        /// </summary>
        /// <param name="date">����</param>
        public static bool IsDate(ref string date)
        {
            //���Ϊ�գ���Ϊ��֤�ϸ�
            if (IsNullOrEmpty(date))
            {
                return true;
            }

            //���Ҫ��֤�ַ����еĿո�
            date = date.Trim();

            //�滻\
            date = date.Replace(@"\", "-");
            //�滻/
            date = date.Replace(@"/", "-");

            //������ҵ�����"��",����Ϊ�ǵ�ǰ����
            if (date.IndexOf("��") != -1)
            {
                date = DateTime.Now.ToString();
            }

            try
            {
                //��ת�������Ƿ�Ϊ����������ַ�
                date = Convert.ToDateTime(date).ToString("d");
                return true;
            }
            catch
            {
                //��������ַ����д��ڷ����֣��򷵻�false
                if (!IsInt(date))
                {
                    return false;
                }

                #region �Դ����ֽ��н���
                //��8λ�����ֽ��н���
                if (date.Length == 8)
                {
                    //��ȡ������
                    string year = date.Substring(0, 4);
                    string month = date.Substring(4, 2);
                    string day = date.Substring(6, 2);

                    //��֤�Ϸ���
                    if (Convert.ToInt32(year) < 1900 || Convert.ToInt32(year) > 2100)
                    {
                        return false;
                    }
                    if (Convert.ToInt32(month) > 12 || Convert.ToInt32(day) > 31)
                    {
                        return false;
                    }

                    //ƴ������
                    date = Convert.ToDateTime(year + "-" + month + "-" + day).ToString("d");
                    return true;
                }

                //��6λ�����ֽ��н���
                if (date.Length == 6)
                {
                    //��ȡ����
                    string year = date.Substring(0, 4);
                    string month = date.Substring(4, 2);

                    //��֤�Ϸ���
                    if (Convert.ToInt32(year) < 1900 || Convert.ToInt32(year) > 2100)
                    {
                        return false;
                    }
                    if (Convert.ToInt32(month) > 12)
                    {
                        return false;
                    }

                    //ƴ������
                    date = Convert.ToDateTime(year + "-" + month).ToString("d");
                    return true;
                }

                //��5λ�����ֽ��н���
                if (date.Length == 5)
                {
                    //��ȡ����
                    string year = date.Substring(0, 4);
                    string month = date.Substring(4, 1);

                    //��֤�Ϸ���
                    if (Convert.ToInt32(year) < 1900 || Convert.ToInt32(year) > 2100)
                    {
                        return false;
                    }

                    //ƴ������
                    date = year + "-" + month;
                    return true;
                }

                //��4λ�����ֽ��н���
                if (date.Length == 4)
                {
                    //��ȡ��
                    string year = date.Substring(0, 4);

                    //��֤�Ϸ���
                    if (Convert.ToInt32(year) < 1900 || Convert.ToInt32(year) > 2100)
                    {
                        return false;
                    }

                    //ƴ������
                    date = Convert.ToDateTime(year).ToString("d");
                    return true;
                }
                #endregion

                return false;
            }
        }
        #endregion

        #region ��֤���֤�Ƿ�Ϸ�
        /// <summary>
        /// ��֤���֤�Ƿ�Ϸ�
        /// </summary>
        /// <param name="idCard">Ҫ��֤�����֤</param>        
        public static bool IsIdCard(string idCard)
        {
            //���Ϊ�գ���Ϊ��֤�ϸ�
            if (IsNullOrEmpty(idCard))
            {
                return true;
            }

            //���Ҫ��֤�ַ����еĿո�
            idCard = idCard.Trim();

            //ģʽ�ַ���
            StringBuilder pattern = new StringBuilder();
            pattern.Append(@"^(11|12|13|14|15|21|22|23|31|32|33|34|35|36|37|41|42|43|44|45|46|");
            pattern.Append(@"50|51|52|53|54|61|62|63|64|65|71|81|82|91)");
            pattern.Append(@"(\d{13}|\d{15}[\dx])$");

            //��֤
            return RegexHelper.IsMatch(idCard, pattern.ToString());
        }
        #endregion

        #region ���ͻ����������Ƿ���Σ���ַ���
        /// <summary>
        /// ���ͻ�������ַ����Ƿ���Ч,����ԭʼ�ַ����޸�Ϊ��Ч�ַ�������ַ�����
        /// ����⵽�ͻ����������й�����Σ���ַ���,�򷵻�false,��Ч����true��
        /// </summary>
        /// <param name="input">Ҫ�����ַ���</param>
        public static bool IsValidInput(ref string input)
        {
            try
            {
                if (IsNullOrEmpty(input))
                {
                    //����ǿ�ֵ,������
                    return true;
                }
                else
                {
                    //�滻������
                    input = input.Replace("'", "''").Trim();

                    //��⹥����Σ���ַ���
                    string testString = "and |or |exec |insert |select |delete |update |count |chr |mid |master |truncate |char |declare ";
                    string[] testArray = testString.Split('|');
                    foreach (string testStr in testArray)
                    {
                        if (input.ToLower().IndexOf(testStr) != -1)
                        {
                            //��⵽�����ַ���,��մ����ֵ
                            input = "";
                            return false;
                        }
                    }

                    //δ��⵽�����ַ���
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
