namespace Agent.Web.Account
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;
    using System.Text;

    public class filluser_list : MemberPageBase
    {
        protected DataTable childTable;
        protected string CurUrl = "";
        protected int dataCount;
        protected string[] FiledName = new string[0];
        protected string[] FiledValue = new string[0];
        protected string page = "1";
        protected int pageCount;
        protected int pageSize = 20;
        protected DataTable userDT;

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Session["user_name"].ToString();
            agent_userinfo_session model = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (!model.get_u_type().Trim().Equals("zj"))
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100035&url=&issuccess=1&isback=0&isopen=1");
                base.Response.End();
            }
            base.Permission_Aspx_ZJ(model, "po_2_3");
            this.page = LSRequest.qq("page");
            if (string.IsNullOrEmpty(this.page))
            {
                this.page = "1";
            }
            string str2 = LSRequest.qq("isdel");
            if (!string.IsNullOrEmpty(str2) && str2.Equals("1"))
            {
                base.Permission_Aspx_ZJ(model, "po_2_3");
                string str3 = LSRequest.qq("uid");
                if (!string.IsNullOrEmpty(str3))
                {
                    string url = string.Format("/Account/filluser_list.aspx?page={0}", this.page);
                    if (CallBLL.cz_saleset_six_bll.GetModel(str3).get_flag().Equals(1))
                    {
                        base.Response.Write(base.ShowDialogBox("該外補會員不允許刪除！", url, 0));
                        base.Response.End();
                    }
                    if (!CallBLL.cz_saleset_six_bll.DeleteUser(str3))
                    {
                        base.Response.Write(base.ShowDialogBox("刪除外補會員失敗！", url, 0));
                        base.Response.End();
                    }
                    else
                    {
                        base.user_del_fill_log(str3);
                        base.Response.Write(base.ShowDialogBox("刪除外補會員成功！", "", 400));
                        CallBLL.cz_saleset_six_bll.GetSaleSetUser();
                        string str5 = "";
                        str5 = str5 + "{" + "\"saleuser\": {";
                        DataTable saleSetUser = CallBLL.cz_saleset_six_bll.GetSaleSetUser();
                        if (saleSetUser != null)
                        {
                            for (int i = 0; i < saleSetUser.Rows.Count; i++)
                            {
                                if (i == 0)
                                {
                                    str5 = str5 + string.Format("\"{0}\": {1}", saleSetUser.Rows[i]["u_name"], saleSetUser.Rows[i]["flag"]);
                                }
                                else
                                {
                                    str5 = str5 + string.Format(",\"{0}\": {1}", saleSetUser.Rows[i]["u_name"], saleSetUser.Rows[i]["flag"]);
                                }
                            }
                        }
                        str5 = str5 + "}}";
                        StringBuilder builder = new StringBuilder();
                        builder.Append("<script>");
                        builder.AppendFormat(" top.aAllowSaleUserName = {0};", str5);
                        builder.Append("</script>");
                        base.Response.Write(builder);
                        base.Response.Write(base.LocationHref(url));
                        base.Response.End();
                    }
                }
            }
            if (int.Parse(this.page) < 1)
            {
                this.page = "1";
            }
            this.childTable = CallBLL.cz_saleset_six_bll.GetSaleSetUserByPage(Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount);
            this.CurUrl = string.Format("/Account/filluser_list.aspx?page={0}", this.page);
        }
    }
}

