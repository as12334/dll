namespace User.Web
{
    using LotterySystem.Common;
    using LotterySystem.Model;
    using LotterySystem.WebPageBase;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Web;
    using User.Web.WebBase;

    public class Index : MemberPageBase
    {
        protected string ajaxErrorLogSwitch = "";
        protected string browserCode = "";
        protected string firstLotteryId = "";
        protected string kc_credit = "";
        protected string kc_iscash = "0";
        protected string kc_usable_credit = "";
        protected DataTable lotteryDT = null;
        protected string masterids = "";
        protected string menuCfg = "";
        protected string six_credit = "";
        protected string six_iscash = "0";
        protected string six_usable_credit = "";
        protected string skin = "Blue";
        protected string sysName = PageBase.get_GetLottorySystemName();
        protected cz_userinfo_session uModel;
        protected string useNowMenuId = "0";
        protected string userName = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ajaxErrorLogSwitch = FileCacheHelper.get_AjaxErrorLogSwitch();
            string str = ConfigurationManager.AppSettings["CloseIndexRefresh"];
            if ((str != "true") && PageBase.IsNeedPopBrower())
            {
                this.Session.Abandon();
                base.Response.Write("<script>top.location.href='/'</script>");
                base.Response.End();
            }
            this.lotteryDT = base.GetLotteryList();
            this.masterids = base.GetLotteryMasterID(this.lotteryDT);
            int num = 0;
            foreach (DataRow row in this.lotteryDT.Rows)
            {
                if (num.Equals(0))
                {
                    this.firstLotteryId = row["id"].ToString();
                }
                num++;
                break;
            }
            if (FileCacheHelper.get_GetDefaultLottery() != "n")
            {
                bool flag = false;
                foreach (DataRow row in this.lotteryDT.Rows)
                {
                    if (row["id"].ToString().Equals(FileCacheHelper.get_GetDefaultLottery()))
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    this.firstLotteryId = FileCacheHelper.get_GetDefaultLottery();
                }
                this.useNowMenuId = "1";
            }
            if (this.lotteryDT != null)
            {
                Dictionary<string, object> lotteryMenuCfg = base.GetLotteryMenuCfg(this.lotteryDT);
                this.menuCfg = JsonHandle.ObjectToJson(lotteryMenuCfg);
            }
            if (HttpContext.Current.Session["user_name"] == null)
            {
                base.Response.End();
            }
            this.userName = HttpContext.Current.Session["user_name"].ToString();
            this.uModel = HttpContext.Current.Session[this.userName + "lottery_session_user_info"] as cz_userinfo_session;
            this.skin = this.uModel.get_u_skin();
            if (str != "true")
            {
                this.browserCode = Utils.Number(4);
                PageBase.SetBrowerFlag(this.browserCode);
            }
            DataTable table = CallBLL.cz_users_bll.GetCredit(this.userName, this.uModel.get_su_type()).Tables[0];
            this.six_credit = string.Format("{0:F0}", double.Parse(table.Rows[0]["six_credit"].ToString()));
            this.kc_credit = string.Format("{0:F0}", double.Parse(table.Rows[0]["kc_credit"].ToString()));
            this.six_usable_credit = string.Format("{0:F0}", double.Parse(table.Rows[0]["six_usable_credit"].ToString()));
            this.kc_usable_credit = string.Format("{0:F0}", double.Parse(table.Rows[0]["kc_usable_credit"].ToString()));
            this.six_iscash = table.Rows[0]["six_iscash"].ToString();
            this.kc_iscash = table.Rows[0]["kc_iscash"].ToString();
            CacheHelper.RemoveAllCache("LmGroupCount_Cache");
        }
    }
}

