using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace DotNet.Utilities
{
    /// <summary>
    /// �ʼ�������
    /// </summary>
    public class MailHelper
    {
        /// <summary>
        /// ��ȡEmail��½��ַ
        /// </summary>
        /// <param name="email">email��ַ</param>
        /// <returns></returns>
        public static string GetEMailLoginUrl(string email)
        {
            if ((email == string.Empty) || (email.IndexOf("@") <= 0))
            {
                return string.Empty;
            }
            int index = email.IndexOf("@");
            email = "http://mail." + email.Substring(index + 1);
            return email;
        }
        /// <summary>
        /// �����ʼ�
        /// </summary>
        /// <param name="mailSubjct">�ʼ�����</param>
        /// <param name="mailBody">�ʼ�����</param>
        /// <param name="mailFrom">������</param>
        /// <param name="mailAddress">�ʼ���ַ�б�</param>
        /// <param name="HostIP">����IP</param>
        /// <returns></returns>
        public static string sendMail(string mailSubjct, string mailBody, string mailFrom, List<string> mailAddress, string HostIP)
        {
            string str = "";
            try
            {
                MailMessage message = new MailMessage
                {
                    IsBodyHtml = false,
                    Subject = mailSubjct,
                    Body = mailBody,
                    From = new MailAddress(mailFrom)
                };
                for (int i = 0; i < mailAddress.Count; i++)
                {
                    message.To.Add(mailAddress[i]);
                }
                new SmtpClient { UseDefaultCredentials = false, DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis, Host = HostIP, Port = (char)0x19 }.Send(message);
            }
            catch (Exception exception)
            {
                str = exception.Message;
            }
            return str;
        }
        /// <summary>
        /// �����ʼ���Ҫ���½��
        /// </summary>
        /// <param name="mailSubjct">�ʼ�����</param>
        /// <param name="mailBody">�ʼ�����</param>
        /// <param name="mailFrom">������</param>
        /// <param name="mailAddress">���յ�ַ�б�</param>
        /// <param name="HostIP">����IP</param>
        /// <param name="username">�û���</param>
        /// <param name="password">����</param>
        /// <returns></returns>
        public static bool sendMail(string mailSubjct, string mailBody, string mailFrom, List<string> mailAddress, string HostIP, string username, string password)
        {
            bool flag;
            string str = sendMail(mailSubjct, mailBody, mailFrom, mailAddress, HostIP, 0x19, username, password, false, string.Empty, out flag);
            return flag;
        }
        /// <summary>
        /// �����ʼ�
        /// </summary>
        /// <param name="mailSubjct">�ʼ�����</param>
        /// <param name="mailBody">�ʼ�����</param>
        /// <param name="mailFrom">������</param>
        /// <param name="mailAddress">���յ�ַ�б�</param>
        /// <param name="HostIP">����IP</param>
        /// <param name="filename">������</param>
        /// <param name="username">�û���</param>
        /// <param name="password">����</param>
        /// <param name="ssl">��������</param>
        /// <returns></returns>
        public static string sendMail(string mailSubjct, string mailBody, string mailFrom, List<string> mailAddress, string HostIP, string filename, string username, string password, bool ssl)
        {
            string str = "";
            try
            {
                MailMessage message = new MailMessage
                {
                    IsBodyHtml = false,
                    Subject = mailSubjct,
                    Body = mailBody,

                    From = new MailAddress(mailFrom)
                };
                for (int i = 0; i < mailAddress.Count; i++)
                {
                    message.To.Add(mailAddress[i]);
                }
                if (System.IO.File.Exists(filename))
                {
                    message.Attachments.Add(new Attachment(filename));
                }
                SmtpClient client = new SmtpClient
                {
                    EnableSsl = ssl,
                    UseDefaultCredentials = false
                };
                NetworkCredential credential = new NetworkCredential(username, password);
                client.Credentials = credential;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Host = HostIP;
                client.Port = 0x19;
                client.Send(message);
            }
            catch (Exception exception)
            {
                str = exception.Message;
            }
            return str;
        }
        /// <summary>
        /// �����ʼ�
        /// </summary>
        /// <param name="mailSubjct"></param>
        /// <param name="mailBody"></param>
        /// <param name="mailFrom"></param>
        /// <param name="mailAddress"></param>
        /// <param name="HostIP"></param>
        /// <param name="port"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="ssl"></param>
        /// <param name="replyTo"></param>
        /// <param name="sendOK"></param>
        /// <returns></returns>
        public static string sendMail(string mailSubjct, string mailBody, string mailFrom, List<string> mailAddress, string HostIP, int port, string username, string password, bool ssl, string replyTo, out bool sendOK)
        {
            sendOK = true;
            string str = "";
            try
            {
                MailMessage message = new MailMessage
                {
                    IsBodyHtml = false,
                    Subject = mailSubjct,
                    Body = mailBody,
                    From = new MailAddress(mailFrom)
                };
                if (replyTo != string.Empty)
                {
                    MailAddress address = new MailAddress(replyTo);
                    message.ReplyTo = address;
                }
                Regex regex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                for (int i = 0; i < mailAddress.Count; i++)
                {
                    if (regex.IsMatch(mailAddress[i]))
                    {
                        message.To.Add(mailAddress[i]);
                    }
                }
                if (message.To.Count == 0)
                {
                    return string.Empty;
                }
                SmtpClient client = new SmtpClient
                {
                    EnableSsl = ssl,
                    UseDefaultCredentials = false
                };
                NetworkCredential credential = new NetworkCredential(username, password);
                client.Credentials = credential;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Host = HostIP;
                client.Port = port;
                client.Send(message);
            }
            catch (Exception exception)
            {
                str = exception.Message;
                sendOK = false;
            }
            return str;
        }
    }
}
