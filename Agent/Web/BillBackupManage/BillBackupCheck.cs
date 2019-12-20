namespace Agent.Web.BillBackupManage
{
    using Agent.Web.Handler;
    using Agent.Web.WebBase;
    using LotterySystem.Model;
    using System;
    using System.Data;
    using System.Data.OleDb;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Web;

    public class BillBackupCheck : BaseHandler
    {
        public override void ProcessRequest(HttpContext context)
        {
            string str8;
            context.Response.ContentType = "text/html";
            if (!context.Session["user_type"].ToString().Equals("zj"))
            {
                context.Response.End();
            }
            string str = context.Request["lid"];
            string s = context.Request["folder"];
            s = base.Server.HtmlDecode(s);
            DateTime now = DateTime.Now;
            string str3 = now.ToString("yyyy-MM-dd");
            string str4 = Convert.ToDateTime(s).AddDays(1.0).ToString("yyyy-MM-dd");
            if (int.Parse(now.ToString("HH")) < Convert.ToInt32(base.get_BetTime_KC()))
            {
                now = now.AddDays(-1.0);
            }
            string str5 = now.ToString("yyyy-MM-dd");
            DataRow[] rowArray = base.GetLotteryList().Select(string.Format(" id={0} ", str));
            string path = HttpContext.Current.Server.MapPath(string.Format("/BillBackupFolder/{0}/{1}/", rowArray[0]["dir_name"].ToString().Trim(), s));
            string str7 = "<span style='display:none;'></span>";
            StreamReader reader = new StreamReader(Path.Combine(base.Server.MapPath("."), "ProgressBar.html"), Encoding.UTF8);
            string str10 = reader.ReadToEnd();
            reader.Close();
            context.Response.Write(str10);
            if (str5 == s)
            {
                int num = int.Parse(now.Hour.ToString());
                int num2 = int.Parse(now.Minute.ToString());
                if (((str3 != str4) || (num < 2)) || (num2 < 0x16))
                {
                    str8 = "<script>gotoMessage('u100085'); </script>";
                    context.Response.Write(str8);
                    context.Response.Flush();
                    return;
                }
            }
            int num12 = 100;
            if (!str.Equals(num12.ToString()))
            {
                if ((now >= Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 08:30:00")) || (now <= Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 06:30:00")))
                {
                    str8 = "<script>gotoMessage('u100083'); </script>";
                    context.Response.Write(str8);
                    context.Response.Flush();
                    return;
                }
            }
            else if (CallBLL.cz_phase_six_bll.GetCurrentPhase().get_is_opendata().Equals(0))
            {
                str8 = "<script>gotoMessage('u100084'); </script>";
                context.Response.Write(str8);
                context.Response.Flush();
                return;
            }
            context.Response.Flush();
            Thread.Sleep(200);
            string trString = "";
            string str12 = "";
            double length = 0.0;
            int num4 = 0;
            if (Directory.Exists(path))
            {
                FileInfo[] files = new DirectoryInfo(path).GetFiles("*.aspx");
                string[] strArray = new string[files.Length];
                double num5 = 0.0;
                length = strArray.Length;
                int num6 = 0;
                double num7 = 0.0;
                double num8 = 0.0;
                string str13 = "";
                DataTable dataByDay = null;
                int num13 = 100;
                if (str.Equals(num13.ToString()))
                {
                    dataByDay = CallBLL.cz_betold_six_bll.GetDataByDay(s);
                }
                else
                {
                    dataByDay = CallBLL.cz_betold_kc_bll.GetDataByDay(str, s, Convert.ToInt32(base.get_BetTime_KC()));
                }
                if (dataByDay == null)
                {
                    str12 = string.Format("<tr><td colspan='11' align=center>{0}</td></tr>", "[" + s + "] 無注單數據核對！");
                    context.Response.Write(this.TableString(str12, str));
                    str8 = "<script>hText(); </script>";
                    context.Response.Write(str8);
                    context.Response.Flush();
                    return;
                }
                foreach (FileInfo info2 in files)
                {
                    str13 = path + "/" + info2.Name.ToString();
                    OleDbConnection selectConnection = null;
                    selectConnection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + str13);
                    selectConnection.Open();
                    string selectCommandText = "select * from betdata order by bet_id";
                    DataSet dataSet = new DataSet();
                    new OleDbDataAdapter(selectCommandText, selectConnection).Fill(dataSet);
                    selectConnection.Close();
                    double num9 = 0.0;
                    int count = dataSet.Tables[0].Rows.Count;
                    string str15 = "<!--dadioshjiddddddddddddddddddddddddddddddjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddggggggggggggggggggggggssssssssssssssssssssssssssssddddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjdadioshjiddddddddddddddddddddddddddddddddddddddddaijojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjj-->";
                    double num15 = num5 / length;
                    if ((((num5 / length) * 100.0) >= 1.0) && (Math.Round((double) ((num5 / length) * 100.0)) > num7))
                    {
                        Thread.Sleep(1);
                        num7 = Math.Round((double) ((num5 / length) * 100.0));
                        str8 = string.Concat(new object[] { str15, "<script>SetPorgressBar('已核對", num5, "','", num7.ToString(), "'); </script>", str7 });
                        context.Response.Write(str8);
                        context.Response.Flush();
                    }
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        num8++;
                        num6 = 0;
                        str12 = "";
                        DataRow[] rowArray2 = dataByDay.Select(string.Format(" bet_id={0} ", row["bet_id"].ToString()));
                        if (rowArray2.Length > 0)
                        {
                            string str16 = rowArray2[0]["u_name"].ToString();
                            string str17 = rowArray2[0]["order_num"].ToString();
                            string str18 = rowArray2[0]["checkcode"].ToString();
                            string str19 = rowArray2[0]["phase"].ToString();
                            string str20 = rowArray2[0]["category"].ToString();
                            string str21 = rowArray2[0]["play_name"].ToString();
                            string str22 = rowArray2[0]["odds_id"].ToString();
                            string str23 = rowArray2[0]["bet_val"].ToString();
                            string str24 = rowArray2[0]["odds"].ToString();
                            string str25 = rowArray2[0]["amount"].ToString();
                            string str26 = rowArray2[0]["kind"].ToString();
                            str12 = string.Format("<tr>", new object[0]);
                            if (!row["u_name"].ToString().Trim().Equals(str16.Trim()))
                            {
                                str12 = str12 + string.Format("<td>原:{0}&nbsp;變:<font color=red>{1}</font></td>", row["u_name"].ToString().Trim(), str16.Trim());
                                num6 = 1;
                            }
                            else
                            {
                                str12 = str12 + string.Format("<td>{0}</td>", row["u_name"].ToString().Trim());
                            }
                            if (!row["order_num"].ToString().Trim().Equals(str17.Trim()))
                            {
                                str12 = str12 + string.Format("<td>原:{0}&nbsp;變:<font color=red>{1}</font></td>", row["order_num"].ToString().Trim(), str17.Trim());
                                num6 = 1;
                            }
                            else
                            {
                                str12 = str12 + string.Format("<td>{0}</td>", row["order_num"].ToString().Trim());
                            }
                            if (!row["checkcode"].ToString().Trim().Equals(str18.Trim()))
                            {
                                str12 = str12 + string.Format("<td>原:{0}&nbsp;變:<font color=red>{1}</font></td>", row["checkcode"].ToString().Trim(), str18.Trim());
                                num6 = 1;
                            }
                            else
                            {
                                str12 = str12 + string.Format("<td>{0}</td>", row["checkcode"].ToString().Trim());
                            }
                            if (!row["phase"].ToString().Trim().Equals(str19.Trim()))
                            {
                                str12 = str12 + string.Format("<td>原:{0}&nbsp;變:<font color=red>{1}</font></td>", row["phase"].ToString().Trim(), str19.Trim());
                                num6 = 1;
                            }
                            else
                            {
                                str12 = str12 + string.Format("<td>{0}</td>", row["phase"].ToString().Trim());
                            }
                            if (!row["category"].ToString().Trim().Equals(str20.Trim()))
                            {
                                str12 = str12 + string.Format("<td>原:{0}&nbsp;變:<font color=red>{1}</font></td>", row["category"].ToString().Trim(), str20.Trim());
                                num6 = 1;
                            }
                            else
                            {
                                str12 = str12 + string.Format("<td>{0}</td>", row["category"].ToString().Trim());
                            }
                            if (!row["play_name"].ToString().Trim().Equals(str21.Trim()))
                            {
                                str12 = str12 + string.Format("<td>原:{0}&nbsp;變:<font color=red>{1}</font></td>", row["play_name"].ToString().Trim(), str21.Trim());
                                num6 = 1;
                            }
                            else
                            {
                                str12 = str12 + string.Format("<td>{0}</td>", row["play_name"].ToString().Trim());
                            }
                            if (!row["odds_id"].ToString().Trim().Equals(str22.Trim()))
                            {
                                str12 = str12 + string.Format("<td>原:{0}&nbsp;變:<font color=red>{1}</font></td>", row["odds_id"].ToString().Trim(), str22.Trim());
                                num6 = 1;
                            }
                            else
                            {
                                str12 = str12 + string.Format("<td>{0}</td>", row["odds_id"].ToString().Trim());
                            }
                            if (!row["bet_val"].ToString().Trim().Equals(str23.Trim()))
                            {
                                str12 = str12 + string.Format("<td>原:{0}&nbsp;變:<font color=red>{1}</font></td>", row["bet_val"].ToString().Trim(), str23.Trim());
                                num6 = 1;
                            }
                            else
                            {
                                str12 = str12 + string.Format("<td>{0}</td>", row["bet_val"].ToString().Trim());
                            }
                            if (!row["odds"].ToString().Trim().Equals(str24.Trim()))
                            {
                                str12 = str12 + string.Format("<td>原:{0}&nbsp;變:<font color=red>{1}</font></td>", row["odds"].ToString().Trim(), str24.Trim());
                                num6 = 1;
                            }
                            else
                            {
                                str12 = str12 + string.Format("<td>{0}</td>", row["odds"].ToString().Trim());
                            }
                            num9 = double.Parse(row["amount"].ToString().Trim()) - double.Parse(str25.Trim());
                            if (Math.Abs(num9) > 0.5)
                            {
                                str12 = str12 + string.Format("<td>原:{0}&nbsp;變:<font color=red>{1}</font></td>", row["amount"].ToString().Trim(), str25.Trim());
                                num6 = 1;
                            }
                            else
                            {
                                str12 = str12 + string.Format("<td>{0}</td>", row["amount"].ToString().Trim());
                            }
                            if (!row["kind"].ToString().Trim().Equals(str26.Trim()))
                            {
                                str12 = str12 + string.Format("<td>原:{0}&nbsp;變:<font color=red>{1}</font></td>", row["kind"].ToString().Trim(), str26.Trim());
                                num6 = 1;
                            }
                            else
                            {
                                str12 = str12 + string.Format("<td>{0}</td>", row["kind"].ToString().Trim());
                            }
                            str12 = str12 + "</tr>";
                        }
                        else
                        {
                            str12 = string.Format("<tr><td>{0}</td><td colspan='10' align=center>單號為[{1}]沒有找到注單或注單已被刪除！！</td></tr>", row["u_name"].ToString(), row["order_num"].ToString().Trim());
                            num6 = 1;
                        }
                        if (num6 > 0)
                        {
                            trString = trString + str12;
                            num4++;
                        }
                    }
                    num5++;
                }
            }
            trString = trString + string.Format("<tr><td colspan='11' align=center><b>核對完成,共  <font color='red'>{0}</font> 份備份記錄</b></td></tr>", length.ToString());
            if (num4 <= 0)
            {
                trString = trString + string.Format("<tr><td colspan='11' align=center><b>無異常注單！！</b></td></tr>", new object[0]);
            }
            context.Response.Write(this.TableString(trString, str));
            str8 = "<script>hText(); </script>";
            context.Response.Write(str8);
            context.Response.Flush();
        }

        public string TableString(string trString, string lotteryID)
        {
            StringBuilder builder = new StringBuilder();
            string str = HttpContext.Current.Session["user_name"].ToString();
            agent_userinfo_session _session = HttpContext.Current.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            builder.Append(string.Format(" <link rel=\"stylesheet\" id=\"Iframe_skin\" href=\"../Styles/{0}/skin.css\">", _session.get_u_skin()));
            builder.Append(" <link rel=\"stylesheet\" href=\"../Styles/global.css\">");
            builder.Append(" <link rel=\"stylesheet\" href=\"../Styles/BallCss/ball_all.css\">");
            builder.Append(" <link rel=\"stylesheet\" href=\"../Styles/ui-dialog.css\">");
            builder.Append(" <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" bgcolor=\"#FFFFFF\">");
            builder.Append(" <tr>");
            builder.Append(" <td class=\"topLeftBg1\"></td>");
            builder.Append(" <td class=\"topBoxBg1\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" class=\"tm2\">");
            builder.Append(" <tr>");
            builder.Append(" <td width=\"26\" align=\"center\"><div class=\"topArr\"></div></td>");
            builder.AppendFormat(" <td align=\"left\"><b><a href=\"BillBackup.aspx?lid={0}\">{1}</a> -> 注單核對</b></td> ", lotteryID, base.GetGameNameByID(lotteryID));
            builder.Append(" <td>&nbsp;</td>");
            builder.Append(" <td width=\"120\">");
            builder.Append(" </td>");
            builder.Append(" </tr>");
            builder.Append(" </table></td>");
            builder.Append(" <td class=\"topRightBg1\"></td>");
            builder.Append(" </tr>");
            builder.Append(" <tr>");
            builder.Append(" <td class=\"centerLeftBg\"></td>");
            builder.Append(" <td valign=\"top\">");
            builder.Append(" <table class=\"t_list\"> ");
            builder.Append(" <tr> ");
            builder.Append(" <th>用戶名</th> ");
            builder.Append(" <th>單號</th> ");
            builder.Append(" <th>驗證號</th> ");
            builder.Append(" <th>獎期</th> ");
            builder.Append(" <th>玩法分類</th> ");
            builder.Append(" <th>玩法</th> ");
            builder.Append(" <th>玩法標示</th> ");
            builder.Append(" <th>下注值</th> ");
            builder.Append(" <th>賠率</th> ");
            builder.Append(" <th>下注額</th> ");
            builder.Append(" <th>盤口</th> ");
            builder.Append(" </tr> ");
            builder.Append(trString);
            builder.Append(" </table> ");
            builder.Append(" </td>");
            builder.Append(" <td class=\"centerRightBg\"></td>");
            builder.Append(" </tr>");
            builder.Append(" <tr>");
            builder.Append(" <td class=\"bottomLeftBg\"></td>");
            builder.Append(" <td class=\"bottomBoxBg\" align=\"center\">&nbsp;");
            builder.Append(" </td>");
            builder.Append(" <td class=\"bottomRightBg\"></td>");
            builder.Append(" </tr>");
            builder.Append(" </table>");
            return builder.ToString();
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

