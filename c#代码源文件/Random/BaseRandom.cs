/// <summary>
/// �� �� �ˣ��շ�
/// ��ϵ��ʽ��361983679  
/// ������վ��http://www.sufeinet.com/thread-655-1-1.html
/// </summary>

using System;

namespace DotNet.Utilities
{
    /// <summary>
    /// BaseRandom
    /// ���������
    /// 
    /// ������������ֵ����Сֵ�����Լ������趨��
    /// </summary>
    public class BaseRandom
    {
        public static int Minimum = 100000;
        public static int Maximal = 999999;
        public static int RandomLength = 6;

        private static string RandomString = "0123456789ABCDEFGHIJKMLNOPQRSTUVWXYZ";
        private static Random Random = new Random(DateTime.Now.Second);

        #region public static string GetRandomString() ��������ַ�
        /// <summary>
        /// ��������ַ�
        /// </summary>
        /// <returns>�ַ���</returns>
        public static string GetRandomString()
        {
            string returnValue = string.Empty;
            for (int i = 0; i < RandomLength; i++)
            {
                int r = Random.Next(0, RandomString.Length - 1);
                returnValue += RandomString[r];
            }
            return returnValue;
        }
        #endregion

        #region public static int GetRandom()
        /// <summary>
        /// ���������
        /// </summary>
        /// <returns>�����</returns>
        public static int GetRandom()
        {
            return Random.Next(Minimum, Maximal);
        }
        #endregion

        #region public static int GetRandom(int minimum, int maximal)
        /// <summary>
        /// ���������
        /// </summary>
        /// <param name="minimum">��Сֵ</param>
        /// <param name="maximal">���ֵ</param>
        /// <returns>�����</returns>
        public static int GetRandom(int minimum, int maximal)
        {
            return Random.Next(minimum, maximal);
        }
        #endregion
    }
}