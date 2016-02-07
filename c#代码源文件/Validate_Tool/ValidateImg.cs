/// <summary>
/// ��˵����Assistant
/// �� �� �ˣ��շ�
/// ��ϵ��ʽ��361983679  
/// ������վ��http://www.sufeinet.com/thread-655-1-1.html
/// </summary>
using System;
using System.Drawing;

namespace DotNet.Utilities
{
    /// <summary>
    /// ��֤�� �̳� System.Web.UI.Page ��Session["xk_validate_code"]
    /// </summary>
    public class ValidateImg : System.Web.UI.Page
    {
        private void Page_Load(object sender, EventArgs e)
        {
            char[] chars = "023456789".ToCharArray();
            System.Random random = new Random();

            string validateCode = string.Empty;
            for (int i = 0; i < 4; i++)
            {
                char rc = chars[random.Next(0, chars.Length)];
                if (validateCode.IndexOf(rc) > -1)
                {
                    i--;
                    continue;
                }
                validateCode += rc;
            }
            Session["xk_validate_code"] = validateCode;
            CreateImage(validateCode);
        }
        /// <summary>
        /// ����ͼƬ
        /// </summary>
        /// <param name="checkCode"></param>
        private void CreateImage(string checkCode)
        {
            int iwidth = (int)(checkCode.Length * 11);
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(iwidth, 19);
            Graphics g = Graphics.FromImage(image);
            g.Clear(Color.White);
            //������ɫ
            Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Chocolate, Color.Brown, Color.DarkCyan, Color.Purple };
            Random rand = new Random();

            //�����ͬ�������ɫ����֤���ַ�
            for (int i = 0; i < checkCode.Length; i++)
            {
                int cindex = rand.Next(7);
                Font f = new System.Drawing.Font("Microsoft Sans Serif", 11);
                Brush b = new System.Drawing.SolidBrush(c[cindex]);
                g.DrawString(checkCode.Substring(i, 1), f, b, (i * 10) + 1, 0, StringFormat.GenericDefault);
            }
            //��һ���߿�
            g.DrawRectangle(new Pen(Color.Black, 0), 0, 0, image.Width - 1, image.Height - 1);

            //����������
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            Response.ClearContent();
            Response.ContentType = "image/Jpeg";
            Response.BinaryWrite(ms.ToArray());
            g.Dispose();
            image.Dispose();
        }
    }
}
