namespace Agent.Web
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;

    public class NewBet_kc : MemberPageBase
    {
        protected DataTable lotteryDT;
        protected string lotteryId = "";
        protected int max_bet_id;
        protected string skin = "";
        protected string sltString = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            agent_userinfo_session model = this.Session[this.Session["user_name"] + "lottery_session_user_info"] as agent_userinfo_session;
            this.skin = model.get_u_skin();
            if (!this.Session["user_type"].ToString().Equals("zj"))
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            base.Permission_Aspx_ZJ(model, "po_1_1");
            this.lotteryId = LSRequest.qq("lid");
            this.lotteryDT = base.GetLotteryList();
            base.IsLotteryExist(this.lotteryId, "u100032", "1", "");
            int num = 0;
            foreach (DataRow row in this.lotteryDT.Rows)
            {
                if ((((Convert.ToInt32(row["id"].ToString()).Equals(0) || Convert.ToInt32(row["id"].ToString()).Equals(1)) || (Convert.ToInt32(row["id"].ToString()).Equals(2) || Convert.ToInt32(row["id"].ToString()).Equals(3))) || ((Convert.ToInt32(row["id"].ToString()).Equals(4) || Convert.ToInt32(row["id"].ToString()).Equals(5)) || (Convert.ToInt32(row["id"].ToString()).Equals(6) || Convert.ToInt32(row["id"].ToString()).Equals(7)))) || ((((Convert.ToInt32(row["id"].ToString()).Equals(9) || Convert.ToInt32(row["id"].ToString()).Equals(8)) || (Convert.ToInt32(row["id"].ToString()).Equals(10) || Convert.ToInt32(row["id"].ToString()).Equals(11))) || ((Convert.ToInt32(row["id"].ToString()).Equals(13) || Convert.ToInt32(row["id"].ToString()).Equals(12)) || (Convert.ToInt32(row["id"].ToString()).Equals(14) || Convert.ToInt32(row["id"].ToString()).Equals(15)))) || (((Convert.ToInt32(row["id"].ToString()).Equals(0x10) || Convert.ToInt32(row["id"].ToString()).Equals(0x11)) || (Convert.ToInt32(row["id"].ToString()).Equals(0x12) || Convert.ToInt32(row["id"].ToString()).Equals(0x13))) || (Convert.ToInt32(row["id"].ToString()).Equals(20) || Convert.ToInt32(row["id"].ToString()).Equals(0x16)))))
                {
                    string str2 = "";
                    if (num > 9)
                    {
                        str2 = "<br />";
                        num = 0;
                    }
                    this.sltString = this.sltString + str2 + string.Format("<label class=\"topLabel\"><input value='{0}' {1} type=\"checkbox\" id=\"chk_{2}\" name=\"chk_{3}\" class=\"chk\" /><span>{4}</span></label>&nbsp;&nbsp;", new object[] { row["id"].ToString(), row["id"].ToString().Equals(this.lotteryId) ? "checked=checked" : "", row["id"].ToString(), row["id"].ToString(), base.GetGameNameByID(row["id"].ToString()) });
                    num++;
                }
            }
        }
    }
}

