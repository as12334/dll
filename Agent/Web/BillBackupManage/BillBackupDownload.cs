namespace Agent.Web.BillBackupManage
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using System;
    using System.Data;
    using System.Web;

    public class BillBackupDownload : MemberPageBase
    {
        private string fileName = "";
        private string folderName = "";
        private DataTable lotteryDT;
        private string lotteryID = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Session["user_type"].ToString().Equals("zj"))
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100014&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            this.lotteryDT = base.GetLotteryList();
            this.lotteryID = LSRequest.qq("lid");
            this.folderName = base.Server.HtmlDecode(LSRequest.qq("folder"));
            this.fileName = base.Server.HtmlDecode(base.Request["file"]);
            DataRow[] rowArray = this.lotteryDT.Select(string.Format(" id={0} ", this.lotteryID));
            string filename = HttpContext.Current.Server.MapPath(string.Format("/BillBackupFolder/{0}/{1}/{2}", rowArray[0]["dir_name"].ToString().Trim(), this.folderName, this.fileName.Replace(".dat", ".aspx")));
            base.Response.Clear();
            base.Response.ContentType = "application/octet-stream";
            base.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(this.fileName.Replace(".dat", ".mdb")));
            base.Response.WriteFile(filename);
            base.Response.End();
        }
    }
}

