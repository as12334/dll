namespace Agent.Web.ReportBackupManage
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;

    public class ReportBackup : MemberPageBase
    {
        protected DataTable lotteryDT;
        protected string lotteryId = "";
        protected string lotteryMasterID = "";
        protected string lotteryName = "";
        protected DataTable reportTable;
        protected string sltString = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            agent_userinfo_session model = this.Session[this.Session["user_name"] + "lottery_session_user_info"] as agent_userinfo_session;
            if (!this.Session["user_type"].ToString().Equals("zj"))
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100014&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            base.Permission_Aspx_ZJ(model, "po_4_1");
            this.lotteryId = LSRequest.qq("lid");
            this.lotteryDT = base.GetLotteryList();
            base.IsLotteryExist(this.lotteryId, "u100032", "1", "");
            DataRow[] rowArray = this.lotteryDT.Select(string.Format(" id={0} ", 100));
            DataRow[] rowArray2 = this.lotteryDT.Select(string.Format(" id<>{0} ", 100));
            int num = 100;
            if (this.lotteryId.Equals(num.ToString()))
            {
                if (rowArray.Length > 0)
                {
                    this.sltString = this.sltString + string.Format("<option value='{0}' selected=selected>{1}</option>", 100, "香港⑥合彩");
                }
                if (rowArray2.Length > 0)
                {
                    this.sltString = this.sltString + string.Format("<option value='{0}'>{1}</option>", 0, "快彩");
                }
                this.lotteryName = "香港⑥合彩";
                this.lotteryMasterID = 1.ToString();
            }
            else
            {
                if (rowArray.Length > 0)
                {
                    this.sltString = this.sltString + string.Format("<option value='{0}'>{1}</option>", 100, "香港⑥合彩");
                }
                if (rowArray2.Length > 0)
                {
                    this.sltString = this.sltString + string.Format("<option value='{0}' selected=selected>{1}</option>", 0, "快彩");
                }
                this.lotteryName = "快彩";
                this.lotteryMasterID = 2.ToString();
            }
            this.reportTable = CallBLL.cz_reportbackup_bll.GetList(Convert.ToInt32(this.lotteryMasterID));
        }
    }
}

