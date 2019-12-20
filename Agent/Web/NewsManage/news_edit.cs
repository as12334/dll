namespace Agent.Web.NewsManage
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Web;

    public class news_edit : MemberPageBase
    {
        protected string ad_id = "";
        protected string allowshow = "";
        protected string content = "";
        protected string is_top = "";
        protected int? lottery_master_id = 0;
        protected string title = "";
        protected string type = "";
        protected agent_userinfo_session userModel;

        private void EditUser()
        {
            string str = LSRequest.qq("txtTitle");
            string str2 = LSRequest.qq("txtContent");
            string str3 = LSRequest.qq("sltUType");
            string str4 = LSRequest.qq("rdoShow");
            string s = LSRequest.qq("rdoTop");
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
            _ad.set_ad_id(Convert.ToInt32(LSRequest.qq("adid")));
            _ad.set_allow_show(new int?(Convert.ToInt32(str4)));
            _ad.set_type(new int?(Convert.ToInt32(str3)));
            _ad.set_title(str);
            _ad.set_ad_content(str2);
            _ad.set_is_top(new int?(int.Parse(s)));
            _ad.set_lottery_master_id(this.lottery_master_id);
            if (CallBLL.cz_ad_bll.UpdateAd(_ad))
            {
                string url = "news_list.aspx?page=" + LSRequest.qq("page");
                base.Response.Write(base.ShowDialogBox("修改站內消息成功！", url, 1));
                base.Response.End();
            }
            else
            {
                base.Response.Write(base.ShowDialogBox("修改站內消息失敗！", "", 400));
                base.Response.End();
            }
        }

        private void InitData()
        {
            cz_ad adModel = CallBLL.cz_ad_bll.GetAdModel(Convert.ToInt32(this.ad_id));
            if (adModel == null)
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100034&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            this.title = adModel.get_title();
            this.content = adModel.get_ad_content();
            this.type = adModel.get_type().ToString();
            this.allowshow = adModel.get_allow_show().ToString();
            this.is_top = adModel.get_is_top().ToString();
            this.lottery_master_id = adModel.get_lottery_master_id();
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
            if (((this.Session["child_user_name"] != null) && this.userModel.get_u_type().Trim().Equals("zj")) && !RuleJudge.ChildOperateValid(this.userModel, "po_3_4", HttpContext.Current))
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100014&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            this.ad_id = LSRequest.qq("adid");
            if (string.IsNullOrEmpty(this.ad_id))
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100034&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            this.InitData();
            if (LSRequest.qq("hdnadd").Equals("hdnadd"))
            {
                this.EditUser();
            }
        }
    }
}

