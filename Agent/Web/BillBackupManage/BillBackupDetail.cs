namespace Agent.Web.BillBackupManage
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using System;
    using System.Data;
    using System.IO;
    using System.Web;

    public class BillBackupDetail : MemberPageBase
    {
        protected FileInfo[] files;
        protected string folderName = "";
        protected DataTable lotteryDT;
        protected string lotteryID = "";

        private void InitData()
        {
            DataRow[] rowArray = this.lotteryDT.Select(string.Format(" id={0} ", this.lotteryID));
            string path = HttpContext.Current.Server.MapPath(string.Format("/BillBackupFolder/{0}/{1}/", rowArray[0]["dir_name"].ToString().Trim(), this.folderName));
            if (Directory.Exists(path))
            {
                this.files = new DirectoryInfo(path).GetFiles();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Session["user_type"].ToString().Equals("zj"))
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100014&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            this.lotteryID = LSRequest.qq("lid");
            this.folderName = base.Server.HtmlDecode(LSRequest.qq("folder"));
            this.lotteryDT = base.GetLotteryList();
            if (string.IsNullOrEmpty(this.lotteryID))
            {
                this.lotteryID = this.lotteryDT.Rows[0]["id"].ToString();
            }
            this.InitData();
        }
    }
}

