namespace Agent.Web.NewsManage
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Web;

    public class news_add : MemberPageBase
    {
        private string clone = "";
        protected int lottery_master_id;
        protected agent_userinfo_session userModel;

        private void AddUser()
        {
            string str = LSRequest.qq("txtTitle");
            string str2 = LSRequest.qq("txtContent");
            string str3 = LSRequest.qq("sltUType");
            string str4 = LSRequest.qq("rdoShow");
            string s = LSRequest.qq("rdoTop");
            string str6 = LSRequest.qq("rdoMasterID");
            if ((s != "0") && (s != "1"))
            {
                s = "0";
            }
            if (string.IsNullOrEmpty(str))
            {
                base.Response.Write(base.GetAlert("請輸入標題！"));
                base.Response.End();
            }
            if (str.Length > 50)
            {
                base.Response.Write(base.GetAlert("輸入標題需小於50字符！"));
                base.Response.End();
            }
            if (string.IsNullOrEmpty(str2))
            {
                base.Response.Write(base.GetAlert("請輸入內容！"));
                base.Response.End();
            }
            cz_ad _ad = new cz_ad();
            _ad.set_u_name(this.userModel.get_u_name());
            if (this.clone != "")
            {
                _ad.set_u_name(_ad.get_u_name() + "-" + this.clone);
            }
            _ad.set_allow_show(new int?(Convert.ToInt32(str4)));
            _ad.set_type(new int?(Convert.ToInt32(str3)));
            _ad.set_post_date(new DateTime?(DateTime.Now));
            _ad.set_title(str);
            _ad.set_ad_content(str2);
            _ad.set_is_top(new int?(int.Parse(s)));
            _ad.set_lottery_master_id(new int?(int.Parse(str6)));
            if (CallBLL.cz_ad_bll.AddAd(_ad))
            {
                string url = "news_list.aspx";
                base.Response.Write(base.ShowDialogBox("添加站內消息成功！", url, 1));
                base.Response.End();
            }
            else
            {
                base.Response.Write(base.ShowDialogBox("添加站內消息失敗！", "", 1));
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
            this.lottery_master_id = base.get_current_master_id();
            if (LSRequest.qq("hdnadd").Equals("hdnadd"))
            {
                this.AddUser();
            }
        }
    }
}

