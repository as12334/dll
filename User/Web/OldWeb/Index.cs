namespace User.Web.OldWeb
{
    using LotterySystem.Common;
    using LotterySystem.Model;
    using LotterySystem.WebPageBase;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Text;
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

        protected string GetLotteryWebSiteHtml()
        {
            StringBuilder builder = new StringBuilder();
            foreach (DataRow row in this.lotteryDT.Rows)
            {
                int num = 100;
                if (!row["id"].ToString().Equals(num.ToString()))
                {
                    string iframeHeaderLotteryHttp = base.GetIframeHeaderLotteryHttp(row["id"].ToString());
                    builder.Append("<tr>");
                    builder.Append("<td class=\"old_list_caption\" colspan=\"2\">");
                    num = 0x12;
                    if (row["id"].ToString().Equals(num.ToString()))
                    {
                        builder.AppendFormat("<a href=\"javascript:;\" id=\"vrcarVideoBtn2\" title=\"【{0}】開獎視頻\" >“{1}”開獎网</a>&nbsp;</td>", row["lottery_name"].ToString(), row["lottery_name"].ToString());
                    }
                    else
                    {
                        num = 0x13;
                        if (row["id"].ToString().Equals(num.ToString()))
                        {
                            builder.AppendFormat("<a href=\"javascript:;\" id=\"vrsscVideoBtn2\" title=\"【{0}】開獎視頻\" >“{1}”開獎网</a>&nbsp;</td>", row["lottery_name"].ToString(), row["lottery_name"].ToString());
                        }
                        else
                        {
                            builder.AppendFormat("<a href=\"javascript:void(0);\" onclick=\"window.open('{0}','{1}','width=687,height=464,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,toolbar=no,location=no');\">“{2}”開獎网</a>&nbsp;</td>", iframeHeaderLotteryHttp, row["lottery_name"].ToString(), row["lottery_name"].ToString());
                        }
                    }
                    builder.Append("</tr>");
                }
            }
            return builder.ToString();
        }

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

