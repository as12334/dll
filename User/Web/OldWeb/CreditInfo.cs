namespace User.Web.OldWeb
{
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;
    using System.Web;
    using User.Web.WebBase;

    public class CreditInfo : MemberPageBase
    {
        protected DataTable car168DataTable = null;
        protected DataTable cqscDataTable = null;
        protected DataTable happycarDataTable = null;
        protected DataTable jscarDataTable = null;
        protected DataTable jscqscDataTable = null;
        protected DataTable jsft2DataTable = null;
        protected DataTable jsk3DataTable = null;
        protected DataTable jspk10DataTable = null;
        protected DataTable jssfcDataTable = null;
        protected DataTable k8scDataTable = null;
        protected decimal kc_credit = 0.0M;
        protected string kc_iscash = "0";
        protected string kc_kind = null;
        protected decimal kc_usable_credit = 0.0M;
        protected DataTable kl10DataTable = null;
        protected DataTable kl8DataTable = null;
        protected DataTable lotteryDT = null;
        protected string lotteryID = "";
        protected string mID = "";
        protected DataTable pcddDataTable = null;
        protected DataTable pk10DataTable = null;
        protected DataTable pkbjlDataTable = null;
        protected DataRow[] rowKC = null;
        protected DataRow[] rowSix = null;
        protected decimal six_credit = 0.0M;
        protected string six_iscash = "0";
        protected string six_kind = null;
        protected decimal six_usable_credit = 0.0M;
        protected DataTable sixDataTable = null;
        protected DataTable speed5DataTable = null;
        protected DataTable ssc168DataTable = null;
        protected string userName = null;
        protected DataTable vrcarDataTable = null;
        protected DataTable vrsscDataTable = null;
        protected DataTable xyft5DataTable = null;
        protected DataTable xyftoaDataTable = null;
        protected DataTable xyftsgDataTable = null;
        protected DataTable xyncDataTable = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            int num;
            this.lotteryID = base.q("id");
            this.mID = base.q("mid");
            this.userName = HttpContext.Current.Session["user_name"].ToString();
            cz_users _users = CallBLL.cz_users_bll.UserLogin(this.userName);
            this.six_kind = _users.get_six_kind().Trim();
            this.six_credit = Convert.ToDecimal(_users.get_six_credit());
            this.six_usable_credit = Convert.ToDecimal(_users.get_six_usable_credit());
            this.six_iscash = _users.get_six_iscash().ToString();
            this.kc_kind = _users.get_kc_kind().Trim();
            this.kc_credit = Convert.ToDecimal(_users.get_kc_credit());
            this.kc_usable_credit = Convert.ToDecimal(_users.get_kc_usable_credit());
            this.kc_iscash = _users.get_kc_iscash().ToString();
            this.lotteryDT = base.GetLotteryList();
            this.rowSix = this.lotteryDT.Select(string.Format(" master_id={0} ", 1));
            this.rowKC = this.lotteryDT.Select(string.Format(" master_id={0} ", 2));
            if (this.mID == string.Empty)
            {
                num = 100;
                if (this.lotteryID == num.ToString())
                {
                    num = 1;
                    this.mID = num.ToString();
                }
                else
                {
                    this.mID = 2.ToString();
                }
            }
            num = 1;
            if (this.mID.Equals(num.ToString()))
            {
                num = 100;
                if (this.lotteryID == num.ToString())
                {
                    if (!FileCacheHelper.get_IsShowLM_B())
                    {
                        this.sixDataTable = CallBLL.cz_drawback_six_bll.User_GetDrawBackList(HttpContext.Current.Session["user_name"].ToString(), "91060,91061,91062,91063,91064,91065").Tables[0];
                    }
                    else
                    {
                        this.sixDataTable = CallBLL.cz_drawback_six_bll.GetDrawBackList(HttpContext.Current.Session["user_name"].ToString()).Tables[0];
                    }
                }
            }
            else
            {
                foreach (DataRow row in this.lotteryDT.Rows)
                {
                    string str2;
                    string str = row["id"].ToString();
                    num = 0;
                    if (str == num.ToString())
                    {
                        str2 = "86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,123,124,125,126,127,128,129,130,131,132,133,134,135,136";
                        this.kl10DataTable = CallBLL.cz_drawback_kl10_bll.GetDrawbackByPlayId(this.userName, str2).Tables[0];
                    }
                    num = 1;
                    if (str == num.ToString())
                    {
                        this.cqscDataTable = CallBLL.cz_drawback_cqsc_bll.GetDrawbackList(this.userName, "4,5,6,7,8,9,10,11,12,13,14,15").Tables[0];
                    }
                    num = 2;
                    if (str == num.ToString())
                    {
                        this.pk10DataTable = CallBLL.cz_drawback_pk10_bll.GetDrawbackList(this.userName, "1,2,3,4,36,37,38").Tables[0];
                    }
                    num = 3;
                    if (str == num.ToString())
                    {
                        this.xyncDataTable = CallBLL.cz_drawback_xync_bll.GetDrawbackList(this.userName, "86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,123,124,125,126,127,128,129,130,131,132,133,134,135,136").Tables[0];
                    }
                    num = 4;
                    if (str == num.ToString())
                    {
                        this.jsk3DataTable = CallBLL.cz_drawback_jsk3_bll.GetDrawbackList(this.userName).Tables[0];
                    }
                    num = 5;
                    if (str == num.ToString())
                    {
                        this.kl8DataTable = CallBLL.cz_drawback_kl8_bll.GetDrawbackList(this.userName).Tables[0];
                    }
                    num = 6;
                    if (str == num.ToString())
                    {
                        this.k8scDataTable = CallBLL.cz_drawback_k8sc_bll.GetDrawbackList(this.userName, "4,5,6,7,8,9,10,11,12,13,14,15").Tables[0];
                    }
                    num = 7;
                    if (str == num.ToString())
                    {
                        this.pcddDataTable = CallBLL.cz_drawback_pcdd_bll.GetDrawbackList(this.userName, "71009,71010,71011,71012").Tables[0];
                    }
                    num = 9;
                    if (str == num.ToString())
                    {
                        this.xyft5DataTable = CallBLL.cz_drawback_xyft5_bll.GetDrawbackList(this.userName, "1,2,3,4,36,37,38").Tables[0];
                    }
                    num = 8;
                    if (str == num.ToString())
                    {
                        this.pkbjlDataTable = CallBLL.cz_drawback_pkbjl_bll.GetDrawbackList(this.userName, "81001,81002,81003,81004").Tables[0];
                    }
                    num = 10;
                    if (str == num.ToString())
                    {
                        this.jscarDataTable = CallBLL.cz_drawback_jscar_bll.GetDrawbackList(this.userName, "1,2,3,4,36,37,38").Tables[0];
                    }
                    num = 11;
                    if (str == num.ToString())
                    {
                        this.speed5DataTable = CallBLL.cz_drawback_speed5_bll.GetDrawbackList(this.userName, "4,5,6,7,8,9,10,11,12,13,14,15").Tables[0];
                    }
                    num = 12;
                    if (str == num.ToString())
                    {
                        this.jspk10DataTable = CallBLL.cz_drawback_jspk10_bll.GetDrawbackList(this.userName, "1,2,3,4,36,37,38").Tables[0];
                    }
                    num = 13;
                    if (str == num.ToString())
                    {
                        this.jscqscDataTable = CallBLL.cz_drawback_jscqsc_bll.GetDrawbackList(this.userName, "4,5,6,7,8,9,10,11,12,13,14,15").Tables[0];
                    }
                    num = 14;
                    if (str == num.ToString())
                    {
                        str2 = "86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,123,124,125,126,127,128,129,130,131,132,133,134,135,136";
                        this.jssfcDataTable = CallBLL.cz_drawback_jssfc_bll.GetDrawbackByPlayId(this.userName, str2).Tables[0];
                    }
                    num = 15;
                    if (str == num.ToString())
                    {
                        this.jsft2DataTable = CallBLL.cz_drawback_jsft2_bll.GetDrawbackList(this.userName, "1,2,3,4,36,37,38").Tables[0];
                    }
                    num = 0x10;
                    if (str == num.ToString())
                    {
                        this.car168DataTable = CallBLL.cz_drawback_car168_bll.GetDrawbackList(this.userName, "1,2,3,4,36,37,38").Tables[0];
                    }
                    num = 0x11;
                    if (str == num.ToString())
                    {
                        this.ssc168DataTable = CallBLL.cz_drawback_ssc168_bll.GetDrawbackList(this.userName, "4,5,6,7,8,9,10,11,12,13,14,15").Tables[0];
                    }
                    num = 0x12;
                    if (str == num.ToString())
                    {
                        this.vrcarDataTable = CallBLL.cz_drawback_vrcar_bll.GetDrawbackList(this.userName, "1,2,3,4,36,37,38").Tables[0];
                    }
                    num = 0x13;
                    if (str == num.ToString())
                    {
                        this.vrsscDataTable = CallBLL.cz_drawback_vrssc_bll.GetDrawbackList(this.userName, "4,5,6,7,8,9,10,11,12,13,14,15").Tables[0];
                    }
                    num = 20;
                    if (str == num.ToString())
                    {
                        this.xyftoaDataTable = CallBLL.cz_drawback_xyftoa_bll.GetDrawbackList(this.userName, "1,2,3,4,36,37,38").Tables[0];
                    }
                    num = 0x15;
                    if (str == num.ToString())
                    {
                        this.xyftsgDataTable = CallBLL.cz_drawback_xyftsg_bll.GetDrawbackList(this.userName, "1,2,3,4,36,37,38").Tables[0];
                    }
                    num = 0x16;
                    if (str == num.ToString())
                    {
                        this.happycarDataTable = CallBLL.cz_drawback_happycar_bll.GetDrawbackList(this.userName, "1,2,3,4,36,37,38").Tables[0];
                    }
                }
            }
        }
    }
}

