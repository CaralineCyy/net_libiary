/// <summary>
/// �� �� �ˣ��շ�
/// ��ϵ��ʽ��361983679  
/// ������վ��http://www.sufeinet.com/thread-655-1-1.html
/// </summary>
using System;

namespace DotNet.Utilities
{
    /// <summary>
    /// ʹ��Random������α�����
    /// </summary>
    public class RandomHelper
    {
        //���������
        private Random _random;

        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public RandomHelper()
        {
            //Ϊ���������ֵ
            this._random = new Random();
        }
        #endregion

        #region ����һ��ָ����Χ���������
        /// <summary>
        /// ����һ��ָ����Χ��������������������Χ������Сֵ�������������ֵ
        /// </summary>
        /// <param name="minNum">��Сֵ</param>
        /// <param name="maxNum">���ֵ</param>
        public int GetRandomInt(int minNum, int maxNum)
        {
            return this._random.Next(minNum, maxNum);
        }
        #endregion

        #region ����һ��0.0��1.0�����С��
        /// <summary>
        /// ����һ��0.0��1.0�����С��
        /// </summary>
        public double GetRandomDouble()
        {
            return this._random.NextDouble();
        }
        #endregion

        #region ��һ����������������
        /// <summary>
        /// ��һ����������������
        /// </summary>
        /// <typeparam name="T">���������</typeparam>
        /// <param name="arr">��Ҫ������������</param>
        public void GetRandomArray<T>(T[] arr)
        {
            //������������������㷨:���ѡ������λ�ã�������λ���ϵ�ֵ����

            //�����Ĵ���,����ʹ������ĳ�����Ϊ��������
            int count = arr.Length;

            //��ʼ����
            for (int i = 0; i < count; i++)
            {
                //�������������λ��
                int randomNum1 = GetRandomInt(0, arr.Length);
                int randomNum2 = GetRandomInt(0, arr.Length);

                //������ʱ����
                T temp;

                //�������������λ�õ�ֵ
                temp = arr[randomNum1];
                arr[randomNum1] = arr[randomNum2];
                arr[randomNum2] = temp;
            }
        }


        // һ��������ɲ��ظ������ַ���  
        private int rep = 0;
        public string GenerateCheckCodeNum(int codeCount)
        {
            string str = string.Empty;
            long num2 = DateTime.Now.Ticks + this.rep;
            this.rep++;
            Random random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> this.rep)));
            for (int i = 0; i < codeCount; i++)
            {
                int num = random.Next();
                str = str + ((char)(0x30 + ((ushort)(num % 10)))).ToString();
            }
            return str;
        }

        //����������������ַ��������ֺ���ĸ��ͣ�
        public string GenerateCheckCode(int codeCount)
        {
            string str = string.Empty;
            long num2 = DateTime.Now.Ticks + this.rep;
            this.rep++;
            Random random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> this.rep)));
            for (int i = 0; i < codeCount; i++)
            {
                char ch;
                int num = random.Next();
                if ((num % 2) == 0)
                {
                    ch = (char)(0x30 + ((ushort)(num % 10)));
                }
                else
                {
                    ch = (char)(0x41 + ((ushort)(num % 0x1a)));
                }
                str = str + ch.ToString();
            }
            return str;
        }

        #region

        /// <summary>
        /// ���ַ���������õ����涨�������ַ���.
        /// </summary>
        /// <param name="allChar"></param>
        /// <param name="CodeCount"></param>
        /// <returns></returns>
        private string GetRandomCode(string allChar, int CodeCount)
        {
            //string allChar = "1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,i,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z"; 
            string[] allCharArray = allChar.Split(',');
            string RandomCode = "";
            int temp = -1;
            Random rand = new Random();
            for (int i = 0; i < CodeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(temp * i * ((int)DateTime.Now.Ticks));
                }

                int t = rand.Next(allCharArray.Length - 1);

                while (temp == t)
                {
                    t = rand.Next(allCharArray.Length - 1);
                }

                temp = t;
                RandomCode += allCharArray[t];
            }
            return RandomCode;
        }

        #endregion
        #endregion
    }
}
