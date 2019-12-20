namespace Agent.Web.AutoLet
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;

    public class AutoLet_Show_six : MemberPageBase
    {
        protected DataTable Autosale_DT;
        protected cz_users cz_users_model;
        protected DataTable lotteryDT;
        protected string lotteryId = "";
        protected string mId = "";
        protected DataRow[] play_Row;
        protected string skin = "";
        protected string sltString = "";
        protected string uid = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Session["user_name"].ToString();
            agent_userinfo_session _session = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            this.skin = _session.get_u_skin();
            this.lotteryDT = base.GetLotteryList();
            this.lotteryId = LSRequest.qq("lid");
            this.mId = LSRequest.qq("mid");
            this.uid = LSRequest.qq("uid");
            int num = 1;
            if (!this.mId.Equals(num.ToString()))
            {
                base.Response.Redirect(string.Format("/AutoLet/AutoLet_Show_kc.aspx?uid={0}&mid={1}", this.uid, this.mId), true);
            }
            this.cz_users_model = CallBLL.cz_users_bll.GetUserInfoByUID(this.uid);
            if (this.cz_users_model == null)
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100038&url=&issuccess=1&isback=0&isopen=1");
                base.Response.End();
            }
            if (!this.cz_users_model.get_allow_sale().Equals(1))
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100037&url=&issuccess=1&isback=0&isopen=1");
                base.Response.End();
            }
            if (!_session.get_u_type().Equals("zj") && !base.IsUpperLowerLevels(this.cz_users_model.get_u_name(), _session.get_u_type(), _session.get_u_name()))
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0&isopen=1");
                base.Response.End();
            }
            DataTable play = null;
            if (!FileCacheHelper.get_IsShowLM_B())
            {
                play = CallBLL.cz_play_six_bll.GetPlay("91060,91061,91062,91063,91064,91065");
            }
            else
            {
                play = CallBLL.cz_play_six_bll.GetPlay();
            }
            if (play != null)
            {
                this.play_Row = play.Select(" play_id <>91002 and play_id <>91015 ");
            }
            DataSet autosaleByUserName = null;
            if (!FileCacheHelper.get_IsShowLM_B())
            {
                autosaleByUserName = CallBLL.cz_autosale_six_bll.GetAutosaleByUserName(this.cz_users_model.get_u_name(), "91060,91061,91062,91063,91064,91065");
            }
            else
            {
                autosaleByUserName = CallBLL.cz_autosale_six_bll.GetAutosaleByUserName(this.cz_users_model.get_u_name());
            }
            if (((autosaleByUserName != null) && (autosaleByUserName.Tables.Count > 0)) && (autosaleByUserName.Tables[0].Rows.Count > 0))
            {
                this.Autosale_DT = autosaleByUserName.Tables[0];
            }
        }
    }
}

