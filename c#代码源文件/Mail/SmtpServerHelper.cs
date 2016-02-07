 using System;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Collections;

namespace DotNet.Utilities_Xofly
{
    public enum MailFormat { Text, HTML };
    public enum MailPriority { Low = 1, Normal = 3, High = 5 };

    /// <summary>
    /// ��Ӹ���
    /// </summary>
    public class MailAttachments
    {
        #region ���캯��
        public MailAttachments()
        {
            _Attachments = new ArrayList();
        }
        #endregion

        #region ˽���ֶ�
        private IList _Attachments;
        private const int MaxAttachmentNum = 10;
        #endregion

        #region ������
        public string this[int index]
        {
            get { return (string)_Attachments[index]; }
        }
        #endregion

        #region ��������
        /// <summary>
        /// ����ʼ�����
        /// </summary>
        /// <param name="FilePath">�����ľ���·��</param>
        public void Add(params string[] filePath)
        {
            if (filePath == null)
            {
                throw (new ArgumentNullException("�Ƿ��ĸ���"));
            }
            else
            {
                for (int i = 0; i < filePath.Length; i++)
                {
                    Add(filePath[i]);
                }
            }
        }

        /// <summary>
        /// ���һ������,��ָ���ĸ���������ʱ�����Ըø������������쳣��
        /// </summary>
        /// <param name="filePath">�����ľ���·��</param>
        public void Add(string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                if (_Attachments.Count < MaxAttachmentNum)
                {
                    _Attachments.Add(filePath);
                }
            }
        }

        /// <summary>
        /// ������и���
        /// </summary>
        public void Clear()
        {
            _Attachments.Clear();
        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        public int Count
        {
            get { return _Attachments.Count; }
        }
        #endregion
    }

    /// <summary>
    /// �ʼ���Ϣ
    /// </summary>
    public class MailMessage
    {
        #region ���캯��
        public MailMessage()
        {
            _Recipients = new ArrayList();        //�ռ����б�
            _Attachments = new MailAttachments(); //����
            _BodyFormat = MailFormat.HTML;        //ȱʡ���ʼ���ʽΪHTML
            _Priority = MailPriority.Normal;
            _Charset = "GB2312";
        }
        #endregion

        #region ˽���ֶ�
        private int _MaxRecipientNum = 30;
        private string _From;      //�����˵�ַ
        private string _FromName;  //����������
        private IList _Recipients; //�ռ���
        private MailAttachments _Attachments;//����
        private string _Body;      //����
        private string _Subject;   //����
        private MailFormat _BodyFormat;     //�ʼ���ʽ
        private string _Charset = "GB2312"; //�ַ������ʽ
        private MailPriority _Priority;     //�ʼ����ȼ�
        #endregion

        #region ��������
        /// <summary>
        /// �趨���Դ��룬Ĭ���趨ΪGB2312���粻��Ҫ������Ϊ""
        /// </summary>
        public string Charset
        {
            get { return _Charset; }
            set { _Charset = value; }
        }

        /// <summary>
        /// ����ռ���
        /// </summary>
        public int MaxRecipientNum
        {
            get { return _MaxRecipientNum; }
            set { _MaxRecipientNum = value; }
        }

        /// <summary>
        /// �����˵�ַ
        /// </summary>
        public string From
        {
            get { return _From; }
            set { _From = value; }
        }

        /// <summary>
        /// ����������
        /// </summary>
        public string FromName
        {
            get { return _FromName; }
            set { _FromName = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string Body
        {
            get { return _Body; }
            set { _Body = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public MailAttachments Attachments
        {
            get { return _Attachments; }
            set { _Attachments = value; }
        }

        /// <summary>
        /// ����Ȩ
        /// </summary>
        public MailPriority Priority
        {
            get { return _Priority; }
            set { _Priority = value; }
        }

        /// <summary>
        /// �ռ���
        /// </summary>
        public IList Recipients
        {
            get { return _Recipients; }
        }

        /// <summary>
        /// �ʼ���ʽ
        /// </summary>
        public MailFormat BodyFormat
        {
            set { _BodyFormat = value; }
            get { return _BodyFormat; }
        }
        #endregion

        #region ��������
        /// <summary>
        /// ����һ���ռ��˵�ַ
        /// </summary>
        /// <param name="recipient">�ռ��˵�Email��ַ</param>
        public void AddRecipients(string recipient)
        {
            if (_Recipients.Count < MaxRecipientNum)
            {
                _Recipients.Add(recipient);
            }
        }

        /// <summary>
        /// ���Ӷ���ռ��˵�ַ
        /// </summary>
        /// <param name="recipient">�ռ��˵�Email��ַ����</param>
        public void AddRecipients(params string[] recipient)
        {
            if (recipient == null)
            {
                throw (new ArgumentException("�ռ��˲���Ϊ��."));
            }
            else
            {
                for (int i = 0; i < recipient.Length; i++)
                {
                    AddRecipients(recipient[i]);
                }
            }
        }
        #endregion
    }

    /// <summary>
    /// �ʼ�����
    /// </summary>
    public class SmtpServerHelper
    {
        #region ���캯������������
        public SmtpServerHelper()
        {
            SMTPCodeAdd();
        }

        ~SmtpServerHelper()
        {
            networkStream.Close();
            tcpClient.Close();
        }
        #endregion

        #region ˽���ֶ�
        /// <summary>
        /// �س�����
        /// </summary>
        private string CRLF = "\r\n";

        /// <summary>
        /// ������Ϣ����
        /// </summary>
        private string errmsg;

        /// <summary>
        /// TcpClient�����������ӷ�����
        /// </summary> 
        private TcpClient tcpClient;

        /// <summary>
        /// NetworkStream����
        /// </summary> 
        private NetworkStream networkStream;

        /// <summary>
        /// ������������¼
        /// </summary>
        private string logs = "";

        /// <summary>
        /// SMTP��������ϣ��
        /// </summary>
        private Hashtable ErrCodeHT = new Hashtable();

        /// <summary>
        /// SMTP��ȷ�����ϣ��
        /// </summary>
        private Hashtable RightCodeHT = new Hashtable();
        #endregion

        #region ��������
        /// <summary>
        /// ������Ϣ����
        /// </summary>
        public string ErrMsg
        {
            set { errmsg = value; }
            get { return errmsg; }
        }
        #endregion

        #region ˽�з���
        /// <summary>
        /// ���ַ�������ΪBase64�ַ���
        /// </summary>
        /// <param name="str">Ҫ������ַ���</param>
        private string Base64Encode(string str)
        {
            byte[] barray;
            barray = Encoding.Default.GetBytes(str);
            return Convert.ToBase64String(barray);
        }

        /// <summary>
        /// ��Base64�ַ�������Ϊ��ͨ�ַ���
        /// </summary>
        /// <param name="str">Ҫ������ַ���</param>
        private string Base64Decode(string str)
        {
            byte[] barray;
            barray = Convert.FromBase64String(str);
            return Encoding.Default.GetString(barray);
        }

        /// <summary>
        /// �õ��ϴ��������ļ���
        /// </summary>
        /// <param name="FilePath">�����ľ���·��</param>
        private string GetStream(string FilePath)
        {
            System.IO.FileStream FileStr = new System.IO.FileStream(FilePath, System.IO.FileMode.Open);
            byte[] by = new byte[System.Convert.ToInt32(FileStr.Length)];
            FileStr.Read(by, 0, by.Length);
            FileStr.Close();
            return (System.Convert.ToBase64String(by));
        }

        /// <summary>
        /// SMTP��Ӧ�����ϣ��
        /// </summary>
        private void SMTPCodeAdd()
        {
            ErrCodeHT.Add("421", "����δ�������رմ����ŵ�");
            ErrCodeHT.Add("432", "��Ҫһ������ת��");
            ErrCodeHT.Add("450", "Ҫ����ʼ�����δ��ɣ����䲻���ã����磬����æ��");
            ErrCodeHT.Add("451", "����Ҫ��Ĳ�������������г���");
            ErrCodeHT.Add("452", "ϵͳ�洢���㣬Ҫ��Ĳ���δִ��");
            ErrCodeHT.Add("454", "��ʱ��֤ʧ��");
            ErrCodeHT.Add("500", "�����ַ����");
            ErrCodeHT.Add("501", "������ʽ����");
            ErrCodeHT.Add("502", "�����ʵ��");
            ErrCodeHT.Add("503", "��������ҪSMTP��֤");
            ErrCodeHT.Add("504", "�����������ʵ��");
            ErrCodeHT.Add("530", "��Ҫ��֤");
            ErrCodeHT.Add("534", "��֤���ƹ��ڼ�");
            ErrCodeHT.Add("538", "��ǰ�������֤������Ҫ����");
            ErrCodeHT.Add("550", "Ҫ����ʼ�����δ��ɣ����䲻���ã����磬����δ�ҵ����򲻿ɷ��ʣ�");
            ErrCodeHT.Add("551", "�û��Ǳ��أ��볢��<forward-path>");
            ErrCodeHT.Add("552", "�����Ĵ洢���䣬Ҫ��Ĳ���δִ��");
            ErrCodeHT.Add("553", "�����������ã�Ҫ��Ĳ���δִ�У����������ʽ����");
            ErrCodeHT.Add("554", "����ʧ��");

            RightCodeHT.Add("220", "�������");
            RightCodeHT.Add("221", "����رմ����ŵ�");
            RightCodeHT.Add("235", "��֤�ɹ�");
            RightCodeHT.Add("250", "Ҫ����ʼ��������");
            RightCodeHT.Add("251", "�Ǳ����û�����ת����<forward-path>");
            RightCodeHT.Add("334", "��������Ӧ��֤Base64�ַ���");
            RightCodeHT.Add("354", "��ʼ�ʼ����룬��<CRLF>.<CRLF>����");
        }

        /// <summary>
        /// ����SMTP����
        /// </summary> 
        private bool SendCommand(string str)
        {
            byte[] WriteBuffer;
            if (str == null || str.Trim() == String.Empty)
            {
                return true;
            }
            logs += str;
            WriteBuffer = Encoding.Default.GetBytes(str);
            try
            {
                networkStream.Write(WriteBuffer, 0, WriteBuffer.Length);
            }
            catch
            {
                errmsg = "�������Ӵ���";
                return false;
            }
            return true;
        }

        /// <summary>
        /// ����SMTP��������Ӧ
        /// </summary>
        private string RecvResponse()
        {
            int StreamSize;
            string Returnvalue = String.Empty;
            byte[] ReadBuffer = new byte[1024];
            try
            {
                StreamSize = networkStream.Read(ReadBuffer, 0, ReadBuffer.Length);
            }
            catch
            {
                errmsg = "�������Ӵ���";
                return "false";
            }

            if (StreamSize == 0)
            {
                return Returnvalue;
            }
            else
            {
                Returnvalue = Encoding.Default.GetString(ReadBuffer).Substring(0, StreamSize);
                logs += Returnvalue + this.CRLF;
                return Returnvalue;
            }
        }

        /// <summary>
        /// �����������������һ��������ջ�Ӧ��
        /// </summary>
        /// <param name="str">һ��Ҫ���͵�����</param>
        /// <param name="errstr">�������Ҫ��������Ϣ</param>
        private bool Dialog(string str, string errstr)
        {
            if (str == null || str.Trim() == string.Empty)
            {
                return true;
            }
            if (SendCommand(str))
            {
                string RR = RecvResponse();
                if (RR == "false")
                {
                    return false;
                }

                //��鷵�صĴ��룬����[RFC 821]���ش���Ϊ3λ���ִ�����220
                string RRCode = RR.Substring(0, 3);
                if (RightCodeHT[RRCode] != null)
                {
                    return true;
                }
                else
                {
                    if (ErrCodeHT[RRCode] != null)
                    {
                        errmsg += (RRCode + ErrCodeHT[RRCode].ToString());
                        errmsg += CRLF;
                    }
                    else
                    {
                        errmsg += RR;
                    }
                    errmsg += errstr;
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// �����������������һ��������ջ�Ӧ��
        /// </summary>
        private bool Dialog(string[] str, string errstr)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (!Dialog(str[i], ""))
                {
                    errmsg += CRLF;
                    errmsg += errstr;
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// ���ӷ�����
        /// </summary>
        private bool Connect(string smtpServer, int port)
        {
            try
            {
                tcpClient = new TcpClient(smtpServer, port);
            }
            catch (Exception e)
            {
                errmsg = e.ToString();
                return false;
            }
            networkStream = tcpClient.GetStream();

            if (RightCodeHT[RecvResponse().Substring(0, 3)] == null)
            {
                errmsg = "��������ʧ��";
                return false;
            }
            return true;
        }

        /// <summary>
        /// ��ȡ���ȼ�
        /// </summary>
        /// <param name="mailPriority">���ȼ�</param>
        private string GetPriorityString(MailPriority mailPriority)
        {
            string priority = "Normal";
            if (mailPriority == MailPriority.Low)
            {
                priority = "Low";
            }
            else if (mailPriority == MailPriority.High)
            {
                priority = "High";
            }
            return priority;
        }

        /// <summary>
        /// ���͵����ʼ�
        /// </summary>
        /// <param name="smtpServer">����SMTP������</param>
        /// <param name="port">�˿ڣ�Ĭ��Ϊ25</param>
        /// <param name="username">�����������ַ</param>
        /// <param name="password">��������������</param>
        /// <param name="mailMessage">�ʼ�����</param>
        private bool SendEmail(string smtpServer, int port, bool ESmtp, string username, string password, MailMessage mailMessage)
        {
            if (Connect(smtpServer, port) == false) return false;

            string priority = GetPriorityString(mailMessage.Priority);

            bool Html = (mailMessage.BodyFormat == MailFormat.HTML);

            string[] SendBuffer;
            string SendBufferstr;

            //����SMTP��֤
            if (ESmtp)
            {
                SendBuffer = new String[4];
                SendBuffer[0] = "EHLO " + smtpServer + CRLF;
                SendBuffer[1] = "AUTH LOGIN" + CRLF;
                SendBuffer[2] = Base64Encode(username) + CRLF;
                SendBuffer[3] = Base64Encode(password) + CRLF;
                if (!Dialog(SendBuffer, "SMTP��������֤ʧ�ܣ���˶��û��������롣")) return false;
            }
            else
            {
                SendBufferstr = "HELO " + smtpServer + CRLF;
                if (!Dialog(SendBufferstr, "")) return false;
            }

            //�����˵�ַ
            SendBufferstr = "MAIL FROM:<" + username + ">" + CRLF;
            if (!Dialog(SendBufferstr, "�����˵�ַ���󣬻���Ϊ��")) return false;

            //�ռ��˵�ַ
            SendBuffer = new string[mailMessage.Recipients.Count];
            for (int i = 0; i < mailMessage.Recipients.Count; i++)
            {
                SendBuffer[i] = "RCPT TO:<" + (string)mailMessage.Recipients[i] + ">" + CRLF;
            }
            if (!Dialog(SendBuffer, "�ռ��˵�ַ����")) return false;

            SendBufferstr = "DATA" + CRLF;
            if (!Dialog(SendBufferstr, "")) return false;

            //����������
            SendBufferstr = "From:" + mailMessage.FromName + "<" + mailMessage.From + ">" + CRLF;

            if (mailMessage.Recipients.Count == 0)
            {
                return false;
            }
            else
            {
                SendBufferstr += "To:=?" + mailMessage.Charset.ToUpper() + "?B?" + Base64Encode((string)mailMessage.Recipients[0]) + "?=" + "<" + (string)mailMessage.Recipients[0] + ">" + CRLF;
            }
            SendBufferstr += ((mailMessage.Subject == String.Empty || mailMessage.Subject == null) ? "Subject:" : ((mailMessage.Charset == "") ? ("Subject:" + mailMessage.Subject) : ("Subject:" + "=?" + mailMessage.Charset.ToUpper() + "?B?" + Base64Encode(mailMessage.Subject) + "?="))) + CRLF;
            SendBufferstr += "X-Priority:" + priority + CRLF;
            SendBufferstr += "X-MSMail-Priority:" + priority + CRLF;
            SendBufferstr += "Importance:" + priority + CRLF;
            SendBufferstr += "X-Mailer: Lion.Web.Mail.SmtpMail Pubclass [cn]" + CRLF;
            SendBufferstr += "MIME-Version: 1.0" + CRLF;
            if (mailMessage.Attachments.Count != 0)
            {
                SendBufferstr += "Content-Type: multipart/mixed;" + CRLF;
                SendBufferstr += " boundary=\"=====" + (Html ? "001_Dragon520636771063_" : "001_Dragon303406132050_") + "=====\"" + CRLF + CRLF;
            }
            if (Html)
            {
                if (mailMessage.Attachments.Count == 0)
                {
                    SendBufferstr += "Content-Type: multipart/alternative;" + CRLF; //���ݸ�ʽ�ͷָ���
                    SendBufferstr += " boundary=\"=====003_Dragon520636771063_=====\"" + CRLF + CRLF;
                    SendBufferstr += "This is a multi-part message in MIME format." + CRLF + CRLF;
                }
                else
                {
                    SendBufferstr += "This is a multi-part message in MIME format." + CRLF + CRLF;
                    SendBufferstr += "--=====001_Dragon520636771063_=====" + CRLF;
                    SendBufferstr += "Content-Type: multipart/alternative;" + CRLF; //���ݸ�ʽ�ͷָ���
                    SendBufferstr += " boundary=\"=====003_Dragon520636771063_=====\"" + CRLF + CRLF;
                }
                SendBufferstr += "--=====003_Dragon520636771063_=====" + CRLF;
                SendBufferstr += "Content-Type: text/plain;" + CRLF;
                SendBufferstr += ((mailMessage.Charset == "") ? (" charset=\"iso-8859-1\"") : (" charset=\"" + mailMessage.Charset.ToLower() + "\"")) + CRLF;
                SendBufferstr += "Content-Transfer-Encoding: base64" + CRLF + CRLF;
                SendBufferstr += Base64Encode("�ʼ�����ΪHTML��ʽ����ѡ��HTML��ʽ�鿴") + CRLF + CRLF;

                SendBufferstr += "--=====003_Dragon520636771063_=====" + CRLF;

                SendBufferstr += "Content-Type: text/html;" + CRLF;
                SendBufferstr += ((mailMessage.Charset == "") ? (" charset=\"iso-8859-1\"") : (" charset=\"" + mailMessage.Charset.ToLower() + "\"")) + CRLF;
                SendBufferstr += "Content-Transfer-Encoding: base64" + CRLF + CRLF;
                SendBufferstr += Base64Encode(mailMessage.Body) + CRLF + CRLF;
                SendBufferstr += "--=====003_Dragon520636771063_=====--" + CRLF;
            }
            else
            {
                if (mailMessage.Attachments.Count != 0)
                {
                    SendBufferstr += "--=====001_Dragon303406132050_=====" + CRLF;
                }
                SendBufferstr += "Content-Type: text/plain;" + CRLF;
                SendBufferstr += ((mailMessage.Charset == "") ? (" charset=\"iso-8859-1\"") : (" charset=\"" + mailMessage.Charset.ToLower() + "\"")) + CRLF;
                SendBufferstr += "Content-Transfer-Encoding: base64" + CRLF + CRLF;
                SendBufferstr += Base64Encode(mailMessage.Body) + CRLF;
            }
            if (mailMessage.Attachments.Count != 0)
            {
                for (int i = 0; i < mailMessage.Attachments.Count; i++)
                {
                    string filepath = (string)mailMessage.Attachments[i];
                    SendBufferstr += "--=====" + (Html ? "001_Dragon520636771063_" : "001_Dragon303406132050_") + "=====" + CRLF;
                    SendBufferstr += "Content-Type: text/plain;" + CRLF;
                    SendBufferstr += " name=\"=?" + mailMessage.Charset.ToUpper() + "?B?" + Base64Encode(filepath.Substring(filepath.LastIndexOf("\\") + 1)) + "?=\"" + CRLF;
                    SendBufferstr += "Content-Transfer-Encoding: base64" + CRLF;
                    SendBufferstr += "Content-Disposition: attachment;" + CRLF;
                    SendBufferstr += " filename=\"=?" + mailMessage.Charset.ToUpper() + "?B?" + Base64Encode(filepath.Substring(filepath.LastIndexOf("\\") + 1)) + "?=\"" + CRLF + CRLF;
                    SendBufferstr += GetStream(filepath) + CRLF + CRLF;
                }
                SendBufferstr += "--=====" + (Html ? "001_Dragon520636771063_" : "001_Dragon303406132050_") + "=====--" + CRLF + CRLF;
            }
            SendBufferstr += CRLF + "." + CRLF;
            if (!Dialog(SendBufferstr, "�����ż���Ϣ")) return false;

            SendBufferstr = "QUIT" + CRLF;
            if (!Dialog(SendBufferstr, "�Ͽ�����ʱ����")) return false;

            networkStream.Close();
            tcpClient.Close();
            return true;
        }
        #endregion

        #region ���з���
        /// <summary>
        /// ���͵����ʼ�,SMTP����������Ҫ�����֤
        /// </summary>
        /// <param name="smtpServer">����SMTP������</param>
        /// <param name="port">�˿ڣ�Ĭ��Ϊ25</param>
        /// <param name="mailMessage">�ʼ�����</param>
        public bool SendEmail(string smtpServer, int port, MailMessage mailMessage)
        {
            return SendEmail(smtpServer, port, false, "", "", mailMessage);
        }

        /// <summary>
        /// ���͵����ʼ�,SMTP��������Ҫ�����֤
        /// </summary>
        /// <param name="smtpServer">����SMTP������</param>
        /// <param name="port">�˿ڣ�Ĭ��Ϊ25</param>
        /// <param name="username">�����������ַ</param>
        /// <param name="password">��������������</param>
        /// <param name="mailMessage">�ʼ�����</param>
        public bool SendEmail(string smtpServer, int port, string username, string password, MailMessage mailMessage)
        {
            return SendEmail(smtpServer, port, true, username, password, mailMessage);
        }
        #endregion
    }

    /// <summary>
    /// �����ʼ�
    /// </summary>
    //--------------------����-----------------------
    //MailAttachments ma=new MailAttachments();
    //ma.Add(@"������ַ");
    //MailMessage mail = new MailMessage();
    //mail.Attachments=ma;
    //mail.Body="���";
    //mail.AddRecipients("<a class="__cf_email__" href="/cdn-cgi/l/email-protection" data-cfemail="760c1c0f4f4f404e4244404e364740455815191b">[email&nbsp;protected]</a><script cf-hash='f9e31' type="text/javascript">
/* <![CDATA[ */!function(){try{var t="currentScript"in document?document.currentScript:function(){for(var t=document.getElementsByTagName("script"),e=t.length;e--;)if(t[e].getAttribute("cf-hash"))return t[e]}();if(t&&t.previousSibling){var e,r,n,i,c=t.previousSibling,a=c.getAttribute("data-cfemail");if(a){for(e="",r=parseInt(a.substr(0,2),16),n=2;a.length-n;n+=2)i=parseInt(a.substr(n,2),16)^r,e+=String.fromCharCode(i);e=document.createTextNode(e),c.parentNode.replaceChild(e,c)}}}catch(u){}}();/* ]]> */</script>");
    //mail.From="<a class="__cf_email__" href="/cdn-cgi/l/email-protection" data-cfemail="2359495a1a1a151b1711151b631215100d404c4e">[email&nbsp;protected]</a><script cf-hash='f9e31' type="text/javascript">
/* <![CDATA[ */!function(){try{var t="currentScript"in document?document.currentScript:function(){for(var t=document.getElementsByTagName("script"),e=t.length;e--;)if(t[e].getAttribute("cf-hash"))return t[e]}();if(t&&t.previousSibling){var e,r,n,i,c=t.previousSibling,a=c.getAttribute("data-cfemail");if(a){for(e="",r=parseInt(a.substr(0,2),16),n=2;a.length-n;n+=2)i=parseInt(a.substr(n,2),16)^r,e+=String.fromCharCode(i);e=document.createTextNode(e),c.parentNode.replaceChild(e,c)}}}catch(u){}}();/* ]]> */</script>";
    //mail.FromName="zjy";
    //mail.Subject="Hello";
    //SmtpClient sp = new SmtpClient();
    //sp.SmtpServer = "smtp.163.com";
    //sp.Send(mail, "<a class="__cf_email__" href="/cdn-cgi/l/email-protection" data-cfemail="512b3b286868676965636769116067627f323e3c">[email&nbsp;protected]</a><script cf-hash='f9e31' type="text/javascript">
/* <![CDATA[ */!function(){try{var t="currentScript"in document?document.currentScript:function(){for(var t=document.getElementsByTagName("script"),e=t.length;e--;)if(t[e].getAttribute("cf-hash"))return t[e]}();if(t&&t.previousSibling){var e,r,n,i,c=t.previousSibling,a=c.getAttribute("data-cfemail");if(a){for(e="",r=parseInt(a.substr(0,2),16),n=2;a.length-n;n+=2)i=parseInt(a.substr(n,2),16)^r,e+=String.fromCharCode(i);e=document.createTextNode(e),c.parentNode.replaceChild(e,c)}}}catch(u){}}();/* ]]> */</script>", "123456");
    //------------------------------------------------
    public class SmtpClient
    {
        #region ���캯��
        public SmtpClient()
        { }

        public SmtpClient(string _smtpServer)
        {
            _SmtpServer = _smtpServer;
        }
        #endregion

        #region ˽���ֶ�
        private string errmsg;
        private string _SmtpServer;
        #endregion

        #region ��������
        /// <summary>
        /// ������Ϣ����
        /// </summary>
        public string ErrMsg
        {
            get { return errmsg; }
        }

        /// <summary>
        /// �ʼ�������
        /// </summary>
        public string SmtpServer
        {
            set { _SmtpServer = value; }
            get { return _SmtpServer; }
        }
        #endregion

        public bool Send(MailMessage mailMessage, string username, string password)
        {
            SmtpServerHelper helper = new SmtpServerHelper();
            if (helper.SendEmail(_SmtpServer, 25, username, password, mailMessage))
                return true;
            else
            {
                errmsg = helper.ErrMsg;
                return false;
            }
        }
    }

    /// <summary>
    /// �������������ʼ�
    /// </summary>
    public class SmtpMail
    {
        public SmtpMail()
        { }

        #region �ֶ�
        private StreamReader sr;
        private StreamWriter sw;
        private TcpClient tcpClient;
        private NetworkStream networkStream;
        #endregion

        #region ˽�з���
        /// <summary>
        /// �������������Ϣ
        /// </summary>
        private bool SendDataToServer(string str)
        {
            try
            {
                sw.WriteLine(str);
                sw.Flush();
                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }

        /// <summary>
        /// ���������ж�ȡ���������͵���Ϣ
        /// </summary>
        private string ReadDataFromServer()
        {
            string str = null;
            try
            {
                str = sr.ReadLine();
                if (str[0] == '-')
                {
                    str = null;
                }
            }
            catch (Exception err)
            {
                str = err.Message;
            }
            return str;
        }
        #endregion

        #region ��ȡ�ʼ���Ϣ
        /// <summary>
        /// ��ȡ�ʼ���Ϣ
        /// </summary>
        /// <param name="uid">�����˺�</param>
        /// <param name="pwd">��������</param>
        /// <returns>�ʼ���Ϣ</returns>
        public ArrayList ReceiveMail(string uid, string pwd)
        {
            ArrayList EmailMes = new ArrayList();
            string str;
            int index = uid.IndexOf('@');
            string pop3Server = "pop3." + uid.Substring(index + 1);
            tcpClient = new TcpClient(pop3Server, 110);
            networkStream = tcpClient.GetStream();
            sr = new StreamReader(networkStream);
            sw = new StreamWriter(networkStream);

            if (ReadDataFromServer() == null) return EmailMes;
            if (SendDataToServer("USER " + uid) == false) return EmailMes;
            if (ReadDataFromServer() == null) return EmailMes;
            if (SendDataToServer("PASS " + pwd) == false) return EmailMes;
            if (ReadDataFromServer() == null) return EmailMes;
            if (SendDataToServer("LIST") == false) return EmailMes;
            if ((str = ReadDataFromServer()) == null) return EmailMes;

            string[] splitString = str.Split(' ');
            int count = int.Parse(splitString[1]);
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if ((str = ReadDataFromServer()) == null) return EmailMes;
                    splitString = str.Split(' ');
                    EmailMes.Add(string.Format("��{0}���ʼ���{1}�ֽ�", splitString[0], splitString[1]));
                }
                return EmailMes;
            }
            else
            {
                return EmailMes;
            }
        }
        #endregion

        #region ��ȡ�ʼ�����
        /// <summary>
        /// ��ȡ�ʼ�����
        /// </summary>
        /// <param name="mailMessage">�ڼ���</param>
        /// <returns>����</returns>
        public string ReadEmail(string str)
        {
            string state = "";
            if (SendDataToServer("RETR " + str) == false)
                state = "Error";
            else
            {
                state = sr.ReadToEnd();
            }
            return state;
        }
        #endregion

        #region ɾ���ʼ�
        /// <summary>
        /// ɾ���ʼ�
        /// </summary>
        /// <param name="str">�ڼ���</param>
        /// <returns>������Ϣ</returns>
        public string DeleteEmail(string str)
        {
            string state = "";
            if (SendDataToServer("DELE " + str) == true)
            {
                state = "�ɹ�ɾ��";
            }
            else
            {
                state = "Error";
            }
            return state;
        }
        #endregion

        #region �رշ���������
        /// <summary>
        /// �رշ���������
        /// </summary>
        public void CloseConnection()
        {
            SendDataToServer("QUIT");
            sr.Close();
            sw.Close();
            networkStream.Close();
            tcpClient.Close();
        }
        #endregion
    }

}

