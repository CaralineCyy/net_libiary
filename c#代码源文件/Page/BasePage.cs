/// <summary>
/// ��˵����Assistant
/// �� �� �ˣ��շ�
/// ��ϵ��ʽ��361983679  
/// ������վ��http://www.sufeinet.com/thread-655-1-1.html
/// </summary>
using System;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

namespace DotNet.Utilities
{
    public class BasePage : System.Web.UI.Page
    {
        public BasePage()
        {
            //
            //TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        public static string Title = "����";
        public static string keywords = "�ؼ���";
        public static string description = "��վ����";

        protected override void OnInit(EventArgs e)
        {
            if (Session["admin"] == null || Session["admin"].ToString().Trim() == "")
            {
                Response.Redirect("login.aspx");
            }
            base.OnInit(e);
        }

        protected void ExportData(string strContent, string FileName)
        {

            FileName = FileName + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();

            Response.Clear();
            Response.Charset = "gb2312";
            Response.ContentType = "application/ms-excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            //this.Page.EnableViewState = false; 
            // ���ͷ��Ϣ��Ϊ"�ļ�����/���Ϊ"�Ի���ָ��Ĭ���ļ��� 
            Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ".xls");
            // ���ļ������͵��ͻ��� 
            Response.Write("<html><head><meta http-equiv=Content-Type content=\"text/html; charset=utf-8\">");
            Response.Write(strContent);
            Response.Write("</body></html>");
            // ֹͣҳ���ִ�� 
            //Response.End();
        }

        /// <summary>
        /// ����Excel
        /// </summary>
        /// <param name="obj"></param>
        public void ExportData(GridView obj)
        {
            try
            {
                string style = "";
                if (obj.Rows.Count > 0)
                {
                    style = @"<style> .text { mso-number-format:\@; } </script> ";
                }
                else
                {
                    style = "no data.";
                }

                Response.ClearContent();
                DateTime dt = DateTime.Now;
                string filename = dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString();
                Response.AddHeader("content-disposition", "attachment; filename=ExportData" + filename + ".xls");
                Response.ContentType = "application/ms-excel";
                Response.Charset = "GB2312";
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                obj.RenderControl(htw);
                Response.Write(style);
                Response.Write(sw.ToString());
                Response.End();
            }
            catch
            {
            }
        }
    }
}
