/// <summary>
/// ��˵����Assistant
/// �� �� �ˣ��շ�
/// ��ϵ��ʽ��361983679  
/// ������վ��http://www.sufeinet.com/thread-655-1-1.html
/// </summary>
using System;
using System.Media;

namespace DotNet.Utilities
{
    /// <summary>
    /// �����ý��Ĺ�����
    /// </summary>
    public class MediaHandler
    {
        #region ͬ������wav�ļ�
        /// <summary>
        /// ��ͬ����ʽ����wav�ļ�
        /// </summary>
        /// <param name="sp">SoundPlayer����</param>
        /// <param name="wavFilePath">wav�ļ���·��</param>
        public static void SyncPlayWAV(SoundPlayer sp, string wavFilePath)
        {
            try
            {
                //����wav�ļ���·�� 
                sp.SoundLocation = wavFilePath;

                //ʹ���첽��ʽ����wav�ļ�
                sp.LoadAsync();

                //ʹ��ͬ����ʽ����wav�ļ�
                if (sp.IsLoadCompleted)
                {
                    sp.PlaySync();
                }
            }
            catch (Exception ex)
            {
                string errStr = ex.Message;
                throw ex;
            }
        }

        /// <summary>
        /// ��ͬ����ʽ����wav�ļ�
        /// </summary>
        /// <param name="wavFilePath">wav�ļ���·��</param>
        public static void SyncPlayWAV(string wavFilePath)
        {
            try
            {
                //����һ��SoundPlaryer�࣬������wav�ļ���·��
                SoundPlayer sp = new SoundPlayer(wavFilePath);

                //ʹ���첽��ʽ����wav�ļ�
                sp.LoadAsync();

                //ʹ��ͬ����ʽ����wav�ļ�
                if (sp.IsLoadCompleted)
                {
                    sp.PlaySync();
                }
            }
            catch (Exception ex)
            {
                string errStr = ex.Message;
                throw ex;
            }
        }
        #endregion

        #region �첽����wav�ļ�
        /// <summary>
        /// ���첽��ʽ����wav�ļ�
        /// </summary>
        /// <param name="sp">SoundPlayer����</param>
        /// <param name="wavFilePath">wav�ļ���·��</param>
        public static void ASyncPlayWAV(SoundPlayer sp, string wavFilePath)
        {
            try
            {
                //����wav�ļ���·�� 
                sp.SoundLocation = wavFilePath;

                //ʹ���첽��ʽ����wav�ļ�
                sp.LoadAsync();

                //ʹ���첽��ʽ����wav�ļ�
                if (sp.IsLoadCompleted)
                {
                    sp.Play();
                }
            }
            catch (Exception ex)
            {
                string errStr = ex.Message;
                throw ex;
            }
        }

        /// <summary>
        /// ���첽��ʽ����wav�ļ�
        /// </summary>
        /// <param name="wavFilePath">wav�ļ���·��</param>
        public static void ASyncPlayWAV(string wavFilePath)
        {
            try
            {
                //����һ��SoundPlaryer�࣬������wav�ļ���·��
                SoundPlayer sp = new SoundPlayer(wavFilePath);

                //ʹ���첽��ʽ����wav�ļ�
                sp.LoadAsync();

                //ʹ���첽��ʽ����wav�ļ�
                if (sp.IsLoadCompleted)
                {
                    sp.Play();
                }
            }
            catch (Exception ex)
            {
                string errStr = ex.Message;
                throw ex;
            }
        }
        #endregion

        #region ֹͣ����wav�ļ�
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sp">SoundPlayer����</param>
        public static void StopWAV(SoundPlayer sp)
        {
            sp.Stop();
        }
        #endregion
    }
}
