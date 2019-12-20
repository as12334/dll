namespace Agent.Web
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;

    public class ViewFgsOptOdssLog : MemberPageBase
    {
        protected string cloneName = "";
        protected int dataCount;
        protected DataTable dataTable;
        protected string[] FiledName = new string[] { "lid", "phaseNum" };
        protected string[] FiledValue = new string[0];
        protected bool isCloneUser;
        protected DataTable lotteryDT;
        protected string lotteryId = "";
        protected string page = "1";
        protected int pageCount;
        protected int pageSize = 20;
        protected string phaseNum = "";
        protected DataTable phaseNumTable;
        protected string strLi = "";
        protected string strPhaseHtml = "";
        protected string u_type = "";
        protected string url = "";

        private void initSelect(agent_userinfo_session userModel)
        {
            if (this.lotteryDT != null)
            {
                foreach (DataRow row in this.lotteryDT.Rows)
                {
                    string str = row["id"].ToString();
                    int num = 100;
                    if (str == num.ToString())
                    {
                        if (userModel.get_six_op_odds().Equals(1))
                        {
                            this.strLi = this.strLi + string.Format("<option value='{0}' {1}>{2}</option>", str, (str == this.lotteryId) ? "selected=selected" : "", base.GetGameNameByID(str));
                        }
                    }
                    else if (userModel.get_kc_op_odds().Equals(1))
                    {
                        this.strLi = this.strLi + string.Format("<option value='{0}' {1}>{2}</option>", str, (str == this.lotteryId) ? "selected=selected" : "", base.GetGameNameByID(str));
                    }
                }
            }
            if (this.lotteryId != "")
            {
                if (this.phaseNum == "")
                {
                    this.strPhaseHtml = this.strPhaseHtml + string.Format("<option value='{0}' {1}>{2}</option>", "", "selected=selected", "全部");
                }
                else
                {
                    this.strPhaseHtml = this.strPhaseHtml + string.Format("<option value='{0}' {1}>{2}</option>", "", "", "全部");
                }
                foreach (DataRow row2 in this.phaseNumTable.Rows)
                {
                    string str2 = row2["l_phase"].ToString();
                    string str3 = row2["l_phase"].ToString();
                    if (str3.Equals(this.phaseNum))
                    {
                        this.strPhaseHtml = this.strPhaseHtml + string.Format("<option value='{0}' {1}>{2}</option>", str3, "selected=selected", "第" + str2 + "期");
                    }
                    else
                    {
                        this.strPhaseHtml = this.strPhaseHtml + string.Format("<option value='{0}' {1}>{2}</option>", str3, "", "第" + str2 + "期");
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Session["user_name"].ToString();
            agent_userinfo_session model = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (!model.get_u_type().Equals("fgs"))
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            if (model.get_u_type().Equals("fgs") && (model.get_six_op_odds().Equals(1) || model.get_kc_op_odds().Equals(1)))
            {
                base.Permission_Aspx_DL(model, "po_5_3");
            }
            else
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            string str2 = model.get_u_name();
            this.u_type = model.get_u_type().Trim();
            if (this.Session["child_user_name"] != null)
            {
                this.isCloneUser = true;
                this.cloneName = this.Session["child_user_name"].ToString();
            }
            this.lotteryId = LSRequest.qq("lid");
            if (string.IsNullOrEmpty(this.lotteryId))
            {
                this.lotteryId = "";
            }
            this.phaseNum = LSRequest.qq("phaseNum");
            if (string.IsNullOrEmpty(this.phaseNum))
            {
                this.phaseNum = "";
            }
            this.lotteryDT = base.GetLotteryList();
            base.IsLotteryExistForSysLog(this.lotteryId, "u100032", "1", "");
            if (this.lotteryId == "")
            {
                if (model.get_six_op_odds().Equals(1))
                {
                    this.lotteryId = 100.ToString();
                }
                else
                {
                    this.lotteryId = this.lotteryDT.Select(string.Format(" id<>{0} ", 100))[0]["id"].ToString();
                }
            }
            else
            {
                if (!model.get_six_op_odds().Equals(1))
                {
                    int num2 = 100;
                    if (this.lotteryId == num2.ToString())
                    {
                        this.lotteryId = this.lotteryDT.Select(string.Format(" id<>{0} ", 100))[0]["id"].ToString();
                    }
                }
                if (!model.get_kc_op_odds().Equals(1))
                {
                    int num3 = 100;
                    if (this.lotteryId != num3.ToString())
                    {
                        this.lotteryId = 100.ToString();
                    }
                }
            }
            this.page = LSRequest.qq("page");
            if (string.IsNullOrEmpty(this.page))
            {
                this.page = "1";
            }
            if (int.Parse(this.page) < 1)
            {
                this.page = "1";
            }
            if (this.lotteryId != "")
            {
                this.phaseNumTable = CallBLL.cz_fgs_opt_log_bll.GetPhaseNumByLottery(this.lotteryId, str2);
            }
            this.initSelect(model);
            DataSet set = CallBLL.cz_fgs_opt_log_bll.GetFgsOptLogByPage(Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, (this.lotteryId == "") ? "" : this.lotteryId, this.phaseNum, str2);
            this.dataTable = set.Tables[0];
            this.FiledValue = new string[] { this.lotteryId, this.phaseNum };
        }
    }
}

