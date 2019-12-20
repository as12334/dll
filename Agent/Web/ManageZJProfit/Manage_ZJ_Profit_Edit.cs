namespace Agent.Web.ManageZJProfit
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;

    public class Manage_ZJ_Profit_Edit : MemberPageBase
    {
        private string clone = "";
        protected DataTable dataTable;
        protected string flag = "edit";
        protected string id = "";
        protected string id_ccms = "";
        protected string lottery_id = "";
        protected int lottery_master_id;
        protected string maxRate = "";
        protected string minRate = "";
        protected string openTime = "";
        protected string systemRate = "100";
        protected string u_name = "";
        protected string u_type = "";
        protected agent_userinfo_session userModel;
        protected string zjName = "";

        private void Edit()
        {
            string str = LSRequest.qq("hdnid");
            string str2 = LSRequest.qq("sltUType");
            string str3 = LSRequest.qq("txtUName");
            string str4 = LSRequest.qq("txtMinRate");
            string str5 = LSRequest.qq("txtMaxRate");
            string str6 = LSRequest.qq("txtOpenTime");
            if ((string.IsNullOrEmpty(str2) || string.IsNullOrEmpty(str4)) || string.IsNullOrEmpty(str5))
            {
                base.Response.Write(base.ShowDialogBox("參數錯誤!", "", 400));
                base.Response.End();
            }
            if (!str2.Equals("zj") && string.IsNullOrEmpty(str3))
            {
                base.Response.Write(base.ShowDialogBox("參數錯誤!", "", 400));
                base.Response.End();
            }
            if (!Utils.IsNumeric(str4))
            {
                base.Response.Write(base.ShowDialogBox("最小 總監盈利成數错误!", "", 400));
                base.Response.End();
            }
            if (!Utils.IsNumeric(str5))
            {
                base.Response.Write(base.ShowDialogBox("最大 總監盈利成數错误!", "", 400));
                base.Response.End();
            }
            if ((int.Parse(str4) < FileCacheHelper.get_GetZJProfitMinRate()) || (int.Parse(str5) > 100))
            {
                base.Response.Write(base.ShowDialogBox("最小 總監盈利成數错误!", "", 400));
                base.Response.End();
            }
            if ((int.Parse(str4) < FileCacheHelper.get_GetZJProfitMinRate()) || (int.Parse(str5) > 100))
            {
                base.Response.Write(base.ShowDialogBox("最大 總監盈利成數错误!", "", 400));
                base.Response.End();
            }
            if (int.Parse(str4) > int.Parse(str5))
            {
                base.Response.Write(base.ShowDialogBox("總監盈利成數错误! 最小不能大於最大值!", "", 400));
                base.Response.End();
            }
            if (!string.IsNullOrEmpty(str6) && !Regex.IsMatch(str6, @"^((20|21|22|23|[0-1]?\d):[0-5]?\d)$"))
            {
                base.Response.Write(base.ShowDialogBox("每天開始時間错误!", "", 400));
                base.Response.End();
            }
            if (!str2.Equals("zj") && (CallBLL.cz_users_bll.GetUser(str3, str2) == null))
            {
                base.Response.Write(base.ShowDialogBox("賬號或級別不合法!", "", 400));
                base.Response.End();
            }
            if (!base.IsUserType(str2))
            {
                base.Response.Write(base.ShowDialogBox("級別不合法!", "", 400));
                base.Response.End();
            }
            if (str2.Equals("zj"))
            {
                str3 = "";
            }
            DataTable profitById = CallBLL.cz_zj_profit_set_bll.GetProfitById(str);
            if (CallBLL.cz_zj_profit_set_bll.UpdateById(str, str2, str3, str4, str5, str6))
            {
                DataTable table3 = CallBLL.cz_zj_profit_set_bll.GetProfitById(str);
                if (!Utils.CompareDataTable(profitById, table3))
                {
                    string str7 = profitById.Rows[0]["lottery_id"].ToString();
                    string uTypeText = base.GetUTypeText(profitById.Rows[0]["u_type"].ToString());
                    string str9 = profitById.Rows[0]["u_name"].ToString();
                    string str10 = profitById.Rows[0]["minRate"].ToString();
                    string str11 = profitById.Rows[0]["maxRate"].ToString();
                    profitById.Rows[0]["openTime"].ToString();
                    string str12 = base.GetUTypeText(table3.Rows[0]["u_type"].ToString());
                    string str13 = table3.Rows[0]["u_name"].ToString();
                    string str14 = table3.Rows[0]["minRate"].ToString();
                    string str15 = table3.Rows[0]["maxRate"].ToString();
                    table3.Rows[0]["openTime"].ToString();
                    StringBuilder builder = new StringBuilder();
                    builder.AppendFormat("用戶級別：{0}<br />", uTypeText);
                    builder.AppendFormat("用戶賬戶：{0}<br />", str9);
                    builder.AppendFormat("最小總監盈利成數%({0}至100之間)：{1}<br />", FileCacheHelper.get_GetZJProfitMinRate(), str10);
                    builder.AppendFormat("最大總監盈利成數%({0}至100之間)：{1}<br />", FileCacheHelper.get_GetZJProfitMinRate(), str11);
                    StringBuilder builder2 = new StringBuilder();
                    builder2.AppendFormat("用戶級別：{0}<br />", str12);
                    builder2.AppendFormat("用戶賬戶：{0}<br />", str13);
                    builder2.AppendFormat("最小總監盈利成數%({0}至100之間)：{1}<br />", FileCacheHelper.get_GetZJProfitMinRate(), str14);
                    builder2.AppendFormat("最大總監盈利成數%({0}至100之間)：{1}<br />", FileCacheHelper.get_GetZJProfitMinRate(), str15);
                    CallBLL.cz_zj_profit_set_log_bll.Add(str7, builder.ToString(), builder2.ToString(), this.userModel.get_zjname(), DateTime.Now);
                }
                string url = "Manage_ZJ_Profit.aspx";
                base.Response.Write(base.ShowDialogBox("修改盈利設置成功！", url, 1));
                base.Response.End();
            }
            else
            {
                base.Response.Write(base.ShowDialogBox("修改盈利設置失敗！", "", 1));
                base.Response.End();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Session["user_name"].ToString();
            this.userModel = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (!this.userModel.get_u_type().Trim().Equals("zj"))
            {
                base.Response.Redirect("../Quit.aspx", true);
                base.Response.End();
            }
            if (this.Session["child_user_name"] != null)
            {
                if (this.userModel.get_u_type().Trim().Equals("zj") && !RuleJudge.ChildOperateValid(this.userModel, "po_3_4", HttpContext.Current))
                {
                    base.Response.Redirect("../MessagePage.aspx?code=u100014&url=&issuccess=1&isback=0");
                    base.Response.End();
                }
                this.clone = base.get_children_name();
            }
            if (!FileCacheHelper.get_ManageZJProfit().Equals("1"))
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100014&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            if ((this.userModel.get_users_child_session() != null) && !this.userModel.get_users_child_session().get_is_admin().Equals(1))
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100014&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            DataTable zJInfo = CallBLL.cz_users_bll.GetZJInfo();
            this.zjName = zJInfo.Rows[0]["u_name"].ToString();
            this.lottery_master_id = base.get_current_master_id();
            this.id = LSRequest.qq("id");
            if (!string.IsNullOrEmpty(this.id))
            {
                this.dataTable = CallBLL.cz_zj_profit_set_bll.GetProfitById(this.id);
                if (this.dataTable != null)
                {
                    this.id = this.dataTable.Rows[0]["id"].ToString();
                    this.u_type = this.dataTable.Rows[0]["u_type"].ToString();
                    this.u_name = this.dataTable.Rows[0]["u_name"].ToString();
                    this.minRate = this.dataTable.Rows[0]["minRate"].ToString();
                    this.maxRate = this.dataTable.Rows[0]["maxRate"].ToString();
                    this.openTime = this.dataTable.Rows[0]["openTime"].ToString();
                    this.lottery_id = this.dataTable.Rows[0]["lottery_id"].ToString();
                    this.id_ccms = this.dataTable.Rows[0]["id_ccms"].ToString();
                    this.systemRate = this.dataTable.Rows[0]["systemRate"].ToString();
                }
            }
            if (!string.IsNullOrEmpty(LSRequest.qq("hdnid")))
            {
                if (this.userModel.get_users_child_session() != null)
                {
                    base.Response.Write(base.ShowDialogBox("子賬號不能保存數據!", "", 400));
                    base.Response.End();
                }
                this.Edit();
            }
        }
    }
}

