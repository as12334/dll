namespace Agent.Web.ReportBackupManage
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.IO;
    using System.Web;

    public class ReportBackupDownload : MemberPageBase
    {
        private void DownloadFile(string filepath)
        {
            string path = base.Server.MapPath(filepath);
            string filename = "";
            string str = "";
            str = Path.GetFileName(path).Split(new char[] { '#' })[0] + ".rar";
            filename = path;
            base.Response.Clear();
            base.Response.ContentType = "application/octet-stream";
            base.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(str));
            base.Response.WriteFile(filename);
            base.Response.End();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["user_type"].ToString().Equals("zj"))
            {
                string str = LSRequest.qq("id");
                string str2 = LSRequest.qq("lid");
                string str3 = base.Server.HtmlEncode("../ReportBackupManage/ReportBackup.aspx?lid=" + str2);
                if (!string.IsNullOrEmpty(str))
                {
                    cz_reportbackup model = CallBLL.cz_reportbackup_bll.GetModel(str);
                    string str4 = "";
                    string str5 = "";
                    if (model != null)
                    {
                        str4 = model.get_truefilename().ToString().Replace(".db", ".rar");
                        str5 = model.get_folder();
                        this.DownloadFile(string.Format("/ReportBackupFolder/{0}/" + str4, str5));
                    }
                    else
                    {
                        base.Response.Redirect(string.Format("../MessagePage.aspx?code=u100051&url={0}&issuccess=1&isback=1", str3));
                        base.Response.End();
                    }
                }
                else
                {
                    base.Response.Redirect(string.Format("../MessagePage.aspx?code=u100051&url={0}&issuccess=1&isback=1", str3));
                    base.Response.End();
                }
            }
        }
    }
}

