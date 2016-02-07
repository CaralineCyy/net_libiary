/// <summary>
/// ��˵����Assistant
/// �� �� �ˣ��շ�
/// ��ϵ��ʽ��361983679  
/// ������վ��http://www.sufeinet.com/thread-655-1-1.html
/// </summary>
/** 1. ���ܣ�������������ת��������ת��������ת����ص���
 *  2. ���ߣ������� 
 *  3. �������ڣ�2010-3-19
 *  4. ����޸����ڣ�2010-3-19
**/
using System;
using System.Text;

namespace DotNet.Utilities
{
    /// <summary>
    /// ������������ת��������ת��������ת����ص���
    /// </summary>    
    public sealed class ConvertHelper
    {
        #region ����λ��
        /// <summary>
        /// ָ���ַ����Ĺ̶����ȣ�����ַ���С�ڹ̶����ȣ�
        /// �����ַ�����ǰ�油���㣬�����õĹ̶��������Ϊ9λ
        /// </summary>
        /// <param name="text">ԭʼ�ַ���</param>
        /// <param name="limitedLength">�ַ����Ĺ̶�����</param>
        public static string RepairZero(string text, int limitedLength)
        {
            //����0���ַ���
            string temp = "";

            //����0
            for (int i = 0; i < limitedLength - text.Length; i++)
            {
                temp += "0";
            }

            //����text
            temp += text;

            //���ز���0���ַ���
            return temp;
        }
        #endregion

        #region ����������ת��
        /// <summary>
        /// ʵ�ָ����������ת����ConvertBase("15",10,16)��ʾ��ʮ������15ת��Ϊ16���Ƶ�����
        /// </summary>
        /// <param name="value">Ҫת����ֵ,��ԭֵ</param>
        /// <param name="from">ԭֵ�Ľ���,ֻ����2,8,10,16�ĸ�ֵ��</param>
        /// <param name="to">Ҫת������Ŀ����ƣ�ֻ����2,8,10,16�ĸ�ֵ��</param>
        public static string ConvertBase(string value, int from, int to)
        {
            try
            {
                int intValue = Convert.ToInt32(value, from);  //��ת��10����
                string result = Convert.ToString(intValue, to);  //��ת��Ŀ�����
                if (to == 2)
                {
                    int resultLength = result.Length;  //��ȡ�����Ƶĳ���
                    switch (resultLength)
                    {
                        case 7:
                            result = "0" + result;
                            break;
                        case 6:
                            result = "00" + result;
                            break;
                        case 5:
                            result = "000" + result;
                            break;
                        case 4:
                            result = "0000" + result;
                            break;
                        case 3:
                            result = "00000" + result;
                            break;
                    }
                }
                return result;
            }
            catch
            {

                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
                return "0";
            }
        }
        #endregion

        #region ʹ��ָ���ַ�����stringת����byte[]
        /// <summary>
        /// ʹ��ָ���ַ�����stringת����byte[]
        /// </summary>
        /// <param name="text">Ҫת�����ַ���</param>
        /// <param name="encoding">�ַ�����</param>
        public static byte[] StringToBytes(string text, Encoding encoding)
        {
            return encoding.GetBytes(text);
        }
        #endregion

        #region ʹ��ָ���ַ�����byte[]ת����string
        /// <summary>
        /// ʹ��ָ���ַ�����byte[]ת����string
        /// </summary>
        /// <param name="bytes">Ҫת�����ֽ�����</param>
        /// <param name="encoding">�ַ�����</param>
        public static string BytesToString(byte[] bytes, Encoding encoding)
        {
            return encoding.GetString(bytes);
        }
        #endregion

        #region ��byte[]ת����int
        /// <summary>
        /// ��byte[]ת����int
        /// </summary>
        /// <param name="data">��Ҫת����������byte����</param>
        public static int BytesToInt32(byte[] data)
        {
            //���������ֽ����鳤��С��4,�򷵻�0
            if (data.Length < 4)
            {
                return 0;
            }

            //����Ҫ���ص�����
            int num = 0;

            //���������ֽ����鳤�ȴ���4,��Ҫ���д���
            if (data.Length >= 4)
            {
                //����һ����ʱ������
                byte[] tempBuffer = new byte[4];

                //��������ֽ������ǰ4���ֽڸ��Ƶ���ʱ������
                Buffer.BlockCopy(data, 0, tempBuffer, 0, 4);

                //����ʱ��������ֵת����������������num
                num = BitConverter.ToInt32(tempBuffer, 0);
            }

            //��������
            return num;
        }
        #endregion


    }
}
