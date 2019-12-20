namespace Agent.Web.BillBackupManage
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;
    using System.IO;
    using System.Web;
    using System.Web.UI.WebControls;

    public class BillBackup : MemberPageBase
    {
        protected Button btn_A;
        protected Button btn_B;
        protected Button btn_C;
        protected DirectoryInfo[] directorys;
        protected DataTable lotteryDT;
        protected string lotteryID = "";
        private DateTime tm_end;

        protected void btn_ABC_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string iD = button.ID;
            if (iD != null)
            {
                if (!(iD == "btn_A"))
                {
                    if (!(iD == "btn_B"))
                    {
                        if (iD == "btn_C")
                        {
                            DateTime time3 = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd") + " " + string.Format("{0:t}", this.tm_end));
                            if (DateTime.Now < Convert.ToDateTime(time3))
                            {
                                base.Response.Write(base.ShowDialogBox("當前不能操作" + string.Format("{0:t}", this.tm_end) + "注單備份！", string.Format("BillBackup.aspx?lid={0}", this.lotteryID), 400));
                                return;
                            }
                            base.Response.Redirect("BillBackup_six.ashx?t=c");
                        }
                        return;
                    }
                }
                else
                {
                    DateTime time = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd") + " 21:20:00");
                    if (DateTime.Now < time)
                    {
                        base.Response.Write(base.ShowDialogBox("當前不能操作21:20注單備份！", string.Format("BillBackup.aspx?lid={0}", this.lotteryID), 400));
                        return;
                    }
                    base.Response.Redirect("BillBackup_six.ashx?t=a");
                    return;
                }
                DateTime time2 = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd") + " 21:30:00");
                if (DateTime.Now < time2)
                {
                    base.Response.Write(base.ShowDialogBox("當前不能操作21:30注單備份！", string.Format("BillBackup.aspx?lid={0}", this.lotteryID), 400));
                }
                else
                {
                    base.Response.Redirect("BillBackup_six.ashx?t=b");
                }
            }
        }

        private void InitData()
        {
            DataRow[] rowArray = this.lotteryDT.Select(string.Format(" id={0} ", this.lotteryID));
            string path = HttpContext.Current.Server.MapPath(string.Format("/BillBackupFolder/{0}/", rowArray[0]["dir_name"].ToString().Trim()));
            if (Directory.Exists(path))
            {
                this.directorys = new DirectoryInfo(path).GetDirectories();
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
            this.lotteryDT = base.GetLotteryList();
            if (string.IsNullOrEmpty(this.lotteryID))
            {
                this.lotteryID = this.lotteryDT.Rows[0]["id"].ToString();
            }
            int num = 100;
            if (this.lotteryID.Equals(num.ToString()))
            {
                cz_phase_six currentPhase = CallBLL.cz_phase_six_bll.GetCurrentPhase();
                this.tm_end = Convert.ToDateTime(currentPhase.get_sn_stop_date().ToString());
                this.btn_C.Text = string.Format("[21:30-{0:t}", this.tm_end) + "]的備份";
            }
            this.InitData();
        }
    }
}

