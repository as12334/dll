namespace Agent.Web.BillBackupManage
{
    using Agent.Web.Handler;
    using Agent.Web.WebBase;
    using LotterySystem.DBUtility;
    using LotterySystem.Model;
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.OleDb;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Web;

    public class BillBackup_six : BaseHandler
    {
        public void ExecuteSqlTran(ArrayList SQLStringList, string bakname, HttpContext context)
        {
            string str3;
            StreamReader reader = new StreamReader(Path.Combine(base.Server.MapPath("."), "ProgressBar.html"), Encoding.GetEncoding("gb2312"));
            string s = reader.ReadToEnd();
            reader.Close();
            context.Response.Write(s);
            context.Response.Flush();
            Thread.Sleep(200);
            OleDbConnection connection = null;
            connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + bakname);
            connection.Open();
            OleDbCommand command = new OleDbCommand {
                Connection = connection,
                Transaction = connection.BeginTransaction()
            };
            try
            {
                double num = 0.0;
                double count = SQLStringList.Count;
                double num3 = 0.0;
                for (int i = 0; i < SQLStringList.Count; i++)
                {
                    num++;
                    if ((((num / count) * 100.0) >= 1.0) && (Math.Round((double) ((num / count) * 100.0)) > num3))
                    {
                        Thread.Sleep(1);
                        num3 = Math.Round((double) ((num / count) * 100.0));
                        str3 = "<script>SetPorgressBar('已备份" + num.ToString() + "','" + num3.ToString() + "'); </script>";
                        context.Response.Write(str3);
                        context.Response.Flush();
                    }
                    string str4 = SQLStringList[i].ToString();
                    if (str4.Trim().Length > 1)
                    {
                        command.CommandText = str4;
                        command.ExecuteNonQuery();
                    }
                }
                command.Transaction.Commit();
            }
            catch (Exception exception)
            {
                command.Transaction.Rollback();
                throw new Exception(exception.Message);
            }
            command.Cancel();
            connection.Close();
            command.Dispose();
            connection.Dispose();
            str3 = "<script>hText(); </script>";
            context.Response.Write(str3);
            context.Response.Flush();
            context.Response.Write(base.ShowDialogBox("備份成功！", string.Format("/BillBackupManage/BillBackup.aspx?lid={0}", 100), 0));
        }

        public override void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            if (!context.Session["user_type"].ToString().Equals("zj"))
            {
                context.Response.End();
            }
            string text1 = context.Request["lid"];
            string s = context.Request["folder"];
            s = base.Server.HtmlDecode(s);
            DateTime now = DateTime.Now;
            this.zdbak_Click(context);
        }

        protected void zdbak_Click(HttpContext context)
        {
            string str4;
            cz_phase_six currentPhase = CallBLL.cz_phase_six_bll.GetCurrentPhase();
            if (currentPhase == null)
            {
                context.Response.Write(base.ShowDialogBox("獎期無數據！", string.Format("/BillBackupManage/BillBackup.aspx?lid={0}", 100), 0));
                context.Response.End();
                return;
            }
            DateTime time = Convert.ToDateTime(currentPhase.get_sn_stop_date());
            string str = "";
            if (context.Request["t"] != null)
            {
                str = context.Request["t"].ToString().Trim();
            }
            string str2 = "";
            string str3 = "";
            string str19 = str;
            if (str19 != null)
            {
                if (!(str19 == "a"))
                {
                    if (str19 == "b")
                    {
                        DateTime time3 = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd") + " 21:30:00");
                        if (DateTime.Now < time3)
                        {
                            context.Response.Write(base.ShowDialogBox("當前不能操作21:30注單備份！", string.Format("/BillBackupManage/BillBackup.aspx?lid={0}", 100), 0));
                            context.Response.End();
                            return;
                        }
                        str2 = "betdata_" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "_" + DateTime.Now.ToLongTimeString().Replace(":", "-") + "_2120and2030.aspx";
                        str3 = "select  bet_id, u_name, order_num, checkcode, phase, odds_id, category, play_name, bet_val, odds, amount, kind from cz_bet_six with(NOLOCK)  where bet_time  >='" + DateTime.Now.Date.ToString("yyyy-MM-dd") + " 21:20:00' and  bet_time <'" + DateTime.Now.Date.ToString("yyyy-MM-dd") + " 21:30:00'";
                        goto Label_048A;
                    }
                    if (str19 == "c")
                    {
                        DateTime time4 = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd") + " " + string.Format("{0:t}", time));
                        if (DateTime.Now < Convert.ToDateTime(time4))
                        {
                            context.Response.Write(base.ShowDialogBox("當前不能操作" + string.Format("{0:t}", time) + "注單備份！", string.Format("/BillBackupManage/BillBackup.aspx?lid={0}", 100), 0));
                            context.Response.End();
                            return;
                        }
                        str2 = "betdata_" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "_" + DateTime.Now.ToLongTimeString().Replace(":", "-") + "_after2130.aspx";
                        str3 = "select bet_id, u_name, order_num, checkcode, phase, odds_id, category, play_name, bet_val, odds, amount, kind from cz_bet_six with(NOLOCK)  where bet_time>='" + DateTime.Now.Date.ToString("yyyy-MM-dd") + " 21:30:00'";
                        goto Label_048A;
                    }
                }
                else
                {
                    DateTime time2 = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd") + " 21:20:00");
                    if (DateTime.Now < time2)
                    {
                        context.Response.Write(base.ShowDialogBox("當前不能操作21:20注單備份！", string.Format("/BillBackupManage/BillBackup.aspx?lid={0}", 100), 0));
                        context.Response.End();
                        return;
                    }
                    str2 = "betdata_" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "_" + DateTime.Now.ToLongTimeString().Replace(":", "-") + "_before2120.aspx";
                    str3 = "select  bet_id, u_name, order_num, checkcode, phase, odds_id, category, play_name, bet_val, odds, amount, kind from cz_bet_six with(NOLOCK)  where bet_time<'" + DateTime.Now.Date.ToString("yyyy-MM-dd") + " 21:20:00'";
                    goto Label_048A;
                }
            }
            context.Response.Write(base.ShowDialogBox("请重新登陆！", "/Default.aspx", 0));
            context.Response.End();
        Label_048A:
            str4 = DateTime.Now.ToString("yyyy-MM-dd");
            DataRow[] rowArray = base.GetLotteryList().Select(string.Format(" id={0} ", 100));
            string path = HttpContext.Current.Server.MapPath(string.Format("/BillBackupFolder/{0}/{1}/", rowArray[0]["dir_name"].ToString().Trim(), str4));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            File.Copy(base.Server.MapPath("/BillBackupFolder/DBTemplate.mdb"), path + str2, true);
            ArrayList sQLStringList = new ArrayList();
            string str6 = null;
            DataTable table2 = DbHelperSQL.Query(str3, null).Tables[0];
            foreach (DataRow row in table2.Rows)
            {
                string str7 = "";
                string str8 = "";
                string str9 = "";
                string str10 = "";
                string str11 = "";
                string str12 = "";
                string str13 = "";
                string str14 = "";
                string str15 = "";
                string str16 = "";
                string str17 = "";
                string str18 = "";
                str7 = row["bet_id"].ToString().Trim();
                str8 = row["u_name"].ToString().Trim();
                str9 = row["order_num"].ToString().Trim();
                str10 = row["checkcode"].ToString().Trim();
                str11 = row["phase"].ToString().Trim();
                str12 = row["odds_id"].ToString().Trim();
                str13 = row["category"].ToString().Trim();
                str14 = row["play_name"].ToString().Trim();
                str15 = row["bet_val"].ToString().Trim();
                str16 = row["odds"].ToString().Trim();
                str17 = row["amount"].ToString().Trim();
                str18 = row["kind"].ToString().Trim();
                str6 = "insert into betdata(bet_id, u_name, order_num, checkcode, phase, odds_id, category, play_name, bet_val, odds, amount, kind) ";
                string str20 = str6;
                sQLStringList.Add((str20 + " values('" + str7 + "','" + str8 + "','" + str9 + "','" + str10 + "','" + str11 + "','" + str12 + "','" + str13 + "','" + str14 + "','" + str15 + "','" + str16 + "','" + str17 + "','" + str18 + "')").ToString());
            }
            this.ExecuteSqlTran(sQLStringList, path + str2, context);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}

