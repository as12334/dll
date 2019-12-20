namespace Agent.Web.ReportSearch
{
    using Agent.Web.WebBase;
    using LotterySystem.BLL;
    using LotterySystem.Common;
    using LotterySystem.DBUtility;
    using LotterySystem.Model;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class ReportList_T_kcNew : MemberPageBase
    {
        protected cz_users g_UserModel;
        protected string uid = "";
        protected agent_userinfo_session uModel;

        protected string GetReportOpenDate()
        {
            return new cz_system_set_kc_exBLL().GetReportOpenDate();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.uModel = this.Session[this.Session["user_name"] + "lottery_session_user_info"] as agent_userinfo_session;
            base.Permission_Aspx_ZJ(this.uModel, "po_4_2");
            base.Permission_Aspx_DL(this.uModel, "po_7_2");
            this.uid = LSRequest.qq("uid");
            if (!string.IsNullOrEmpty(this.uid))
            {
                this.g_UserModel = CallBLL.cz_users_bll.GetUserInfoByUID(this.uid);
                if ((!this.uModel.get_u_type().Equals("fgs") && !this.uModel.get_allow_view_report().Equals(1)) && !base.IsUpperLowerLevels(this.g_UserModel.get_u_name(), this.uModel.get_u_type(), this.uModel.get_u_name()))
                {
                    base.Response.Redirect("../MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0&isopen=0");
                    base.Response.End();
                }
            }
            string str = base.q("BeginDate");
            string str2 = base.q("EndDate");
            DateTime time = Convert.ToDateTime(str);
            DateTime time2 = Convert.ToDateTime(str2);
            if (time > Convert.ToDateTime(DateTime.Now.ToString("d")))
            {
                str = DateTime.Now.ToString("d");
            }
            if (time2 > Convert.ToDateTime(DateTime.Now.ToString("d")))
            {
                str2 = DateTime.Now.ToString("d");
            }
            if (time > time2)
            {
                base.Response.End();
            }
            string str3 = "select  *  from R_stat where R_date  between @bet_time1 and @bet_time2";
            SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@bet_time1", SqlDbType.DateTime), new SqlParameter("@bet_time2", SqlDbType.DateTime) };
            parameterArray[0].Value = str;
            parameterArray[1].Value = str2;
            DataTable table = DbHelperSQL.Query(str3, parameterArray).Tables[0];
            if (table.Rows.Count == 0)
            {
                base.Server.Transfer("ReportList_T_kc.aspx", true);
                base.Response.End();
            }
            LSRequest.AddReportCookies();
        }
    }
}

