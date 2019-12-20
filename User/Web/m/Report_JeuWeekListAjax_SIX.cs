namespace User.Web.m
{
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;
    using User.Web.WebBase;

    public class Report_JeuWeekListAjax_SIX : MemberPageBase_Mobile
    {
        protected string count_order = "0";
        protected string cssdisplay = "";
        protected int dataCount = 0;
        protected DataTable dataTable = null;
        protected string dataTitle = "";
        protected string findDate = "";
        protected string lid = "";
        protected int page = 1;
        protected int pageCount = 0;
        protected int pageSize = 10;
        protected string sum_money = "0";
        protected string u_drawback = "";
        protected cz_userinfo_session uModel = null;
        protected string userName = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Response.Buffer = true;
            base.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1.0);
            base.Response.Expires = 0;
            base.Response.CacheControl = "no-cache";
            base.Response.AddHeader("Pragma", "No-Cache");
            base.IsUserLoginByMobile();
            base.IsUserLoginByMobileForAjax();
            this.findDate = LSRequest.qq("findDate");
            if (!string.IsNullOrEmpty(base.q("page")))
            {
                this.page = Convert.ToInt32(base.q("page"));
            }
            this.userName = this.Session["user_name"].ToString();
            this.uModel = base.GetUserModelInfo;
            string str = this.uModel.get_su_type().ToString().Trim();
            if (str != null)
            {
                if (!(str == "dl"))
                {
                    if (str == "zd")
                    {
                        this.u_drawback = "dl_drawback";
                    }
                    else if (str == "gd")
                    {
                        this.u_drawback = "zd_drawback";
                    }
                    else if (str == "fgs")
                    {
                        this.u_drawback = "gd_drawback";
                    }
                }
                else
                {
                    this.u_drawback = "hy_drawback";
                }
            }
            this.dataTable = CallBLL.cz_betold_six_bll.Phone_GetReportByPageList(this.userName, this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.findDate + " 08:00:00", this.findDate + " 23:59:59").Tables[0];
        }
    }
}

