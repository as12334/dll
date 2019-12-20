namespace Agent.Web.m.Account
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using System.Web.UI.HtmlControls;

    public class filluser_add : MemberPageBase_Mobile
    {
        protected string checkredio = "";
        protected HtmlForm form1;
        protected DataTable lotteryDT;
        protected string lottrty_kc = "";
        protected string lottrty_six = "";
        protected string upuname = "";
        protected agent_userinfo_session userModel;
        protected string utypeTxt = "";

        private void AddUser()
        {
            string str = LSRequest.qq("userName").ToLower().Trim();
            string str2 = LSRequest.qq("userNicker");
            string str3 = LSRequest.qq("userKind_six");
            string message = "";
            if (!base.ValidParamByUserAdd("filluser", ref message, null, this.lottrty_six, this.lottrty_kc))
            {
                base.noRightOptMsg(message);
            }
            if ((!str3.ToUpper().Equals("A") && !str3.ToUpper().Equals("B")) && !str3.ToUpper().Equals("C"))
            {
                base.Response.End();
            }
            cz_saleset_six model = new cz_saleset_six();
            model.set_u_id(Guid.NewGuid().ToString());
            model.set_u_name(str.Trim());
            model.set_u_nicker(str2.Trim());
            model.set_six_kind(str3.Trim().ToUpper());
            model.set_add_date(new DateTime?(DateTime.Now));
            string str5 = "";
            Dictionary<string, string> data = new Dictionary<string, string>();
            if (CallBLL.cz_saleset_six_bll.AddUser(model, ref str5))
            {
                CallBLL.cz_saleset_six_bll.GetSaleSetUser();
                string str6 = "";
                str6 = str6 + "{" + "\"saleuser\": {";
                DataTable saleSetUser = CallBLL.cz_saleset_six_bll.GetSaleSetUser();
                if (saleSetUser != null)
                {
                    for (int i = 0; i < saleSetUser.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            str6 = str6 + string.Format("\"{0}\": {1}", saleSetUser.Rows[i]["u_name"], saleSetUser.Rows[i]["flag"]);
                        }
                        else
                        {
                            str6 = str6 + string.Format(",\"{0}\": {1}", saleSetUser.Rows[i]["u_name"], saleSetUser.Rows[i]["flag"]);
                        }
                    }
                }
                str6 = str6 + "}}";
                base.user_add_fill_log(model);
                string text1 = "/Account/filluser_drawback.aspx?uid=" + str5 + "&isadd=1";
                StringBuilder builder = new StringBuilder();
                builder.Append("<script>");
                builder.AppendFormat(" top.aAllowSaleUserName = {0};", str6);
                builder.Append("</script>");
                data.Add("uname", model.get_u_name());
                data.Add("uid", model.get_u_id());
                base.successOptMsg("添加外補會員成功！", data);
            }
            else
            {
                base.noRightOptMsg("添加外補會員失敗！");
            }
        }

        private void getMemberDetail(ref string strResult)
        {
            base.checkLoginByHandler(0);
            new ReturnResult();
            new Dictionary<string, object>();
            new List<object>();
            string str = this.Session["user_name"].ToString();
            this.userModel = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (!this.userModel.get_u_type().Trim().Equals("zj"))
            {
                base.Response.End();
            }
            base.Permission_Aspx_ZJ_Mobile("po_2_3");
            this.lotteryDT = base.GetLotteryList();
            DataTable table = this.lotteryDT.DefaultView.ToTable(true, new string[] { "master_id" });
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (Convert.ToInt32(table.Rows[i][0]).Equals(1))
                {
                    this.lottrty_six = table.Rows[i][0].ToString();
                }
                else if (Convert.ToInt32(table.Rows[i][0]).Equals(2))
                {
                    this.lottrty_kc = table.Rows[i][0].ToString();
                }
            }
            string str2 = LSRequest.qq("memberId");
            string str3 = LSRequest.qq("submitType");
            if (str3 != "add")
            {
                base.Response.End();
            }
            if (str2 != "sales")
            {
                base.Response.End();
            }
            if (str3 == "add")
            {
                this.AddUser();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str3;
            base.Response.Expires = 0;
            base.Response.CacheControl = "no-cache";
            string str = LSRequest.qq("action").Trim();
            string strResult = "";
            if (((str3 = str) != null) && (str3 == "getMemberDetail"))
            {
                this.getMemberDetail(ref strResult);
            }
            base.OutJson(strResult);
        }
    }
}

