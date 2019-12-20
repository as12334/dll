namespace Agent.Web
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BillSearch : MemberPageBase
    {
        protected string jsonLottery = "";
        protected string kc_list = "<option value='' selected=selected>全部</option>";
        protected DataTable lotteryDT;
        protected string lotteryId = "";
        protected string six_list = "";
        protected string skin = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Session["user_name"].ToString();
            agent_userinfo_session model = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            this.skin = model.get_u_skin();
            if (!model.get_u_type().Equals("zj"))
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            base.Permission_Aspx_ZJ(model, "po_3_5");
            this.lotteryId = LSRequest.qq("lid");
            this.lotteryDT = base.GetLotteryList();
            base.IsLotteryExist(this.lotteryId, "u100032", "1", "");
            Dictionary<string, Dictionary<string, string>> dictionary = new Dictionary<string, Dictionary<string, string>>();
            DataRow[] rowArray = this.lotteryDT.Select(string.Format(" master_id={0} ", 1));
            DataRow[] rowArray2 = this.lotteryDT.Select(string.Format(" master_id={0} ", 2));
            if ((rowArray != null) && (rowArray.Length > 0))
            {
                string str2 = "";
                Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
                foreach (DataRow row in rowArray)
                {
                    dictionary2.Add(row["id"].ToString(), row["lottery_name"].ToString());
                    str2 = row["name"].ToString();
                    this.six_list = this.six_list + string.Format("<option value='{0}' {1}>{2}</option>", row["id"].ToString(), "selected=selected", row["lottery_name"].ToString());
                }
                dictionary.Add(1.ToString() + "," + str2, dictionary2);
            }
            if ((rowArray2 != null) && (rowArray2.Length > 0))
            {
                string str3 = "";
                Dictionary<string, string> dictionary3 = new Dictionary<string, string>();
                foreach (DataRow row2 in rowArray2)
                {
                    dictionary3.Add(row2["id"].ToString(), row2["lottery_name"].ToString());
                    str3 = row2["name"].ToString();
                    this.kc_list = this.kc_list + string.Format("<option value='{0}'>{1}</option>", row2["id"].ToString(), row2["lottery_name"].ToString());
                }
                dictionary.Add(2.ToString() + "," + str3, dictionary3);
            }
            this.jsonLottery = JsonHandle.ObjectToJson(dictionary);
        }
    }
}

