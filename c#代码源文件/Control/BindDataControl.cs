/// <summary>
/// ��˵����Assistant
/// �� �� �ˣ��շ�
/// ��ϵ��ʽ��361983679  
/// ������վ��http://www.sufeinet.com/thread-655-1-1.html
/// </summary>
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;

namespace DotNet.Utilities
{
    /// <summary>
    /// ����չʾ�ؼ� ��������
    /// </summary>
    public class BindDataControl
    {
        #region �󶨷��������ݿؼ� �򵥰�DataList
        /// <summary>
        /// �򵥰�DataList
        /// </summary>
        /// <param name="ctrl">�ؼ�ID</param>
        /// <param name="mydv">������ͼ</param>
        public static void BindDataList(Control ctrl, DataView mydv)
        {
            ((DataList)ctrl).DataSourceID = null;
            ((DataList)ctrl).DataSource = mydv;
            ((DataList)ctrl).DataBind();
        }
        #endregion

        #region �󶨷��������ݿؼ� SqlDataReader�򵥰�DataList
        /// <summary>
        /// SqlDataReader�򵥰�DataList
        /// </summary>
        /// <param name="ctrl">�ؼ�ID</param>
        /// <param name="mydv">������ͼ</param>
        public static void BindDataReaderList(Control ctrl, SqlDataReader mydv)
        {
            ((DataList)ctrl).DataSourceID = null;
            ((DataList)ctrl).DataSource = mydv;
            ((DataList)ctrl).DataBind();
        }
        #endregion

        #region �󶨷��������ݿؼ� �򵥰�GridView
        /// <summary>
        /// �򵥰�GridView
        /// </summary>
        /// <param name="ctrl">�ؼ�ID</param>
        /// <param name="mydv">������ͼ</param>
        public static void BindGridView(Control ctrl, DataView mydv)
        {
            ((GridView)ctrl).DataSourceID = null;
            ((GridView)ctrl).DataSource = mydv;
            ((GridView)ctrl).DataBind();
        }
        #endregion

        /// <summary>
        /// �󶨷������ؼ� �򵥰�Repeater
        /// </summary>
        /// <param name="ctrl">�ؼ�ID</param>
        /// <param name="mydv">������ͼ</param>
        public static void BindRepeater(Control ctrl, DataView mydv)
        {
            ((Repeater)ctrl).DataSourceID = null;
            ((Repeater)ctrl).DataSource = mydv;
            ((Repeater)ctrl).DataBind();
        }
    }
}
