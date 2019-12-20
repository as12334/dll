namespace User.Web.OldWeb
{
    using LotterySystem.Common;
    using System;
    using System.Data;
    using System.Text;
    using User.Web.WebBase;

    public class Result : MemberPageBase
    {
        protected DataTable car168DataTable = null;
        protected DataTable cqscDataTable = null;
        protected int dataCount = 0;
        protected string[] FiledName = new string[] { "mid", "id", "tabletype" };
        protected string[] FiledValue = null;
        protected DataTable happycarDataTable = null;
        protected DataTable jscarDataTable = null;
        protected DataTable jscqscDataTable = null;
        protected DataTable jsft2DataTable = null;
        protected DataTable jsk3DataTable = null;
        protected DataTable jspk10DataTable = null;
        protected DataTable jssfcDataTable = null;
        protected DataTable k8scDataTable = null;
        protected DataTable kl10DataTable = null;
        protected DataTable kl8DataTable = null;
        protected DataTable lotteryDT = null;
        protected string lotteryID = "";
        protected string mID = "";
        protected string numTable = "";
        protected int page = 1;
        protected int pageCount = 0;
        protected int pageSize = 20;
        protected DataTable pcddDataTable = null;
        protected DataTable pk10DataTable = null;
        protected DataTable pkbjlDataTable = null;
        protected DataTable sixDataTable = null;
        protected DataTable speed5DataTable = null;
        protected DataTable ssc168DataTable = null;
        protected DataTable vrcarDataTable = null;
        protected DataTable vrsscDataTable = null;
        protected DataTable xyft5DataTable = null;
        protected DataTable xyftoaDataTable = null;
        protected DataTable xyftsgDataTable = null;
        protected DataTable xyncDataTable = null;

        protected string get_cl_dsh(string d_c, string s_c)
        {
            string str = "";
            int num = int.Parse(d_c);
            int num2 = int.Parse(s_c);
            if (num == num2)
            {
                return "<label class=\"green\">和</label>";
            }
            if (num > num2)
            {
                return "<label class=\"blue\">單(多)</label>";
            }
            if (num < num2)
            {
                str = "<label class=\"orange\">雙(多)</label>";
            }
            return str;
        }

        protected string get_cl_qhh(string q_c, string h_c)
        {
            string str = "";
            int num = int.Parse(q_c);
            int num2 = int.Parse(h_c);
            if (num == num2)
            {
                return "<label class=\"green\">和</label>";
            }
            if (num > num2)
            {
                return "<label class=\"blue\">前(多)</label>";
            }
            if (num < num2)
            {
                str = "<label class=\"orange\">後(多)</label>";
            }
            return str;
        }

        protected string get_cl_wh(string xz_str)
        {
            string str = "";
            int num = int.Parse(xz_str.ToString().Trim());
            if ((num >= 210) && (num <= 0x2b7))
            {
                return "金";
            }
            if ((num >= 0x2b8) && (num <= 0x2fb))
            {
                return "木";
            }
            if ((num >= 0x2fc) && (num <= 0x357))
            {
                return "水";
            }
            if ((num >= 0x358) && (num <= 0x39b))
            {
                return "火";
            }
            if ((num >= 0x39c) && (num <= 0x582))
            {
                str = "土";
            }
            return str;
        }

        protected string get_cqsc_str(string numstr)
        {
            string[] strArray = numstr.Split(new char[] { ',' });
            int num = int.Parse(strArray[0]);
            int num2 = int.Parse(strArray[1]);
            int num3 = int.Parse(strArray[2]);
            if ((num == num2) && (num2 == num3))
            {
                return "豹子";
            }
            if (((((((((num2 - num) == 1) && ((num3 - num2) == 1)) || (((num - num2) == 1) && ((num2 - num3) == 1))) || ((((num - num3) == 1) && ((num2 - num) == 1)) || (((num3 - num2) == 1) && ((num - num3) == 1)))) || (((((num3 - num) == 1) && ((num - num2) == 1)) || (((num2 - num3) == 1) && ((num3 - num) == 1))) || ((((num == 0) && (num2 == 9)) && (num3 == 8)) || (((num == 8) && (num2 == 9)) && (num3 == 0))))) || ((((((num == 0) && (num2 == 8)) && (num3 == 9)) || (((num == 8) && (num2 == 0)) && (num3 == 9))) || ((((num == 9) && (num2 == 8)) && (num3 == 0)) || (((num == 9) && (num2 == 0)) && (num3 == 8)))) || (((((num == 1) && (num2 == 2)) && (num3 == 0)) || (((num == 9) && (num2 == 0)) && (num3 == 1))) || ((((num == 0) && (num2 == 1)) && (num3 == 9)) || (((num == 0) && (num2 == 9)) && (num3 == 1)))))) || ((((num == 1) && (num2 == 0)) && (num3 == 9)) || (((num == 1) && (num2 == 9)) && (num3 == 0)))) || (((num == 9) && (num2 == 1)) && (num3 == 0)))
            {
                return "順子";
            }
            if ((((num - num2) == 0) || ((num - num3) == 0)) || ((num2 - num3) == 0))
            {
                return "對子";
            }
            if ((((((((num2 - num) == 1) || ((num3 - num2) == 1)) || (((num - num3) == 1) || ((num - num2) == 1))) || ((((num2 - num3) == 1) || ((num3 - num) == 1)) || ((num2 == 9) && (num3 == 0)))) || ((((num2 == 0) && (num3 == 9)) || ((num == 1) && (num3 == 0))) || (((num == 9) && (num2 == 0)) || ((num == 0) && (num2 == 9))))) || ((num3 == 9) && (num == 0))) || ((num3 == 0) && (num == 9)))
            {
                return "半順";
            }
            return "雜六";
        }

        protected string get_jscqsc_str(string numstr)
        {
            string[] strArray = numstr.Split(new char[] { ',' });
            int num = int.Parse(strArray[0]);
            int num2 = int.Parse(strArray[1]);
            int num3 = int.Parse(strArray[2]);
            if ((num == num2) && (num2 == num3))
            {
                return "豹子";
            }
            if (((((((((num2 - num) == 1) && ((num3 - num2) == 1)) || (((num - num2) == 1) && ((num2 - num3) == 1))) || ((((num - num3) == 1) && ((num2 - num) == 1)) || (((num3 - num2) == 1) && ((num - num3) == 1)))) || (((((num3 - num) == 1) && ((num - num2) == 1)) || (((num2 - num3) == 1) && ((num3 - num) == 1))) || ((((num == 0) && (num2 == 9)) && (num3 == 8)) || (((num == 8) && (num2 == 9)) && (num3 == 0))))) || ((((((num == 0) && (num2 == 8)) && (num3 == 9)) || (((num == 8) && (num2 == 0)) && (num3 == 9))) || ((((num == 9) && (num2 == 8)) && (num3 == 0)) || (((num == 9) && (num2 == 0)) && (num3 == 8)))) || (((((num == 1) && (num2 == 2)) && (num3 == 0)) || (((num == 9) && (num2 == 0)) && (num3 == 1))) || ((((num == 0) && (num2 == 1)) && (num3 == 9)) || (((num == 0) && (num2 == 9)) && (num3 == 1)))))) || ((((num == 1) && (num2 == 0)) && (num3 == 9)) || (((num == 1) && (num2 == 9)) && (num3 == 0)))) || (((num == 9) && (num2 == 1)) && (num3 == 0)))
            {
                return "順子";
            }
            if ((((num - num2) == 0) || ((num - num3) == 0)) || ((num2 - num3) == 0))
            {
                return "對子";
            }
            if ((((((((num2 - num) == 1) || ((num3 - num2) == 1)) || (((num - num3) == 1) || ((num - num2) == 1))) || ((((num2 - num3) == 1) || ((num3 - num) == 1)) || ((num2 == 9) && (num3 == 0)))) || ((((num2 == 0) && (num3 == 9)) || ((num == 1) && (num3 == 0))) || (((num == 9) && (num2 == 0)) || ((num == 0) && (num2 == 9))))) || ((num3 == 9) && (num == 0))) || ((num3 == 0) && (num == 9)))
            {
                return "半順";
            }
            return "雜六";
        }

        protected string get_k8sc_str(string numstr)
        {
            string[] strArray = numstr.Split(new char[] { ',' });
            int num = int.Parse(strArray[0]);
            int num2 = int.Parse(strArray[1]);
            int num3 = int.Parse(strArray[2]);
            if ((num == num2) && (num2 == num3))
            {
                return "豹子";
            }
            if (((((((((num2 - num) == 1) && ((num3 - num2) == 1)) || (((num - num2) == 1) && ((num2 - num3) == 1))) || ((((num - num3) == 1) && ((num2 - num) == 1)) || (((num3 - num2) == 1) && ((num - num3) == 1)))) || (((((num3 - num) == 1) && ((num - num2) == 1)) || (((num2 - num3) == 1) && ((num3 - num) == 1))) || ((((num == 0) && (num2 == 9)) && (num3 == 8)) || (((num == 8) && (num2 == 9)) && (num3 == 0))))) || ((((((num == 0) && (num2 == 8)) && (num3 == 9)) || (((num == 8) && (num2 == 0)) && (num3 == 9))) || ((((num == 9) && (num2 == 8)) && (num3 == 0)) || (((num == 9) && (num2 == 0)) && (num3 == 8)))) || (((((num == 1) && (num2 == 2)) && (num3 == 0)) || (((num == 9) && (num2 == 0)) && (num3 == 1))) || ((((num == 0) && (num2 == 1)) && (num3 == 9)) || (((num == 0) && (num2 == 9)) && (num3 == 1)))))) || ((((num == 1) && (num2 == 0)) && (num3 == 9)) || (((num == 1) && (num2 == 9)) && (num3 == 0)))) || (((num == 9) && (num2 == 1)) && (num3 == 0)))
            {
                return "順子";
            }
            if ((((num - num2) == 0) || ((num - num3) == 0)) || ((num2 - num3) == 0))
            {
                return "對子";
            }
            if ((((((((num2 - num) == 1) || ((num3 - num2) == 1)) || (((num - num3) == 1) || ((num - num2) == 1))) || ((((num2 - num3) == 1) || ((num3 - num) == 1)) || ((num2 == 9) && (num3 == 0)))) || ((((num2 == 0) && (num3 == 9)) || ((num == 1) && (num3 == 0))) || (((num == 9) && (num2 == 0)) || ((num == 0) && (num2 == 9))))) || ((num3 == 9) && (num == 0))) || ((num3 == 0) && (num == 9)))
            {
                return "半順";
            }
            return "雜六";
        }

        protected string get_speed5_str(string numstr)
        {
            string[] strArray = numstr.Split(new char[] { ',' });
            int num = int.Parse(strArray[0]);
            int num2 = int.Parse(strArray[1]);
            int num3 = int.Parse(strArray[2]);
            if ((num == num2) && (num2 == num3))
            {
                return "豹子";
            }
            if (((((((((num2 - num) == 1) && ((num3 - num2) == 1)) || (((num - num2) == 1) && ((num2 - num3) == 1))) || ((((num - num3) == 1) && ((num2 - num) == 1)) || (((num3 - num2) == 1) && ((num - num3) == 1)))) || (((((num3 - num) == 1) && ((num - num2) == 1)) || (((num2 - num3) == 1) && ((num3 - num) == 1))) || ((((num == 0) && (num2 == 9)) && (num3 == 8)) || (((num == 8) && (num2 == 9)) && (num3 == 0))))) || ((((((num == 0) && (num2 == 8)) && (num3 == 9)) || (((num == 8) && (num2 == 0)) && (num3 == 9))) || ((((num == 9) && (num2 == 8)) && (num3 == 0)) || (((num == 9) && (num2 == 0)) && (num3 == 8)))) || (((((num == 1) && (num2 == 2)) && (num3 == 0)) || (((num == 9) && (num2 == 0)) && (num3 == 1))) || ((((num == 0) && (num2 == 1)) && (num3 == 9)) || (((num == 0) && (num2 == 9)) && (num3 == 1)))))) || ((((num == 1) && (num2 == 0)) && (num3 == 9)) || (((num == 1) && (num2 == 9)) && (num3 == 0)))) || (((num == 9) && (num2 == 1)) && (num3 == 0)))
            {
                return "順子";
            }
            if ((((num - num2) == 0) || ((num - num3) == 0)) || ((num2 - num3) == 0))
            {
                return "對子";
            }
            if ((((((((num2 - num) == 1) || ((num3 - num2) == 1)) || (((num - num3) == 1) || ((num - num2) == 1))) || ((((num2 - num3) == 1) || ((num3 - num) == 1)) || ((num2 == 9) && (num3 == 0)))) || ((((num2 == 0) && (num3 == 9)) || ((num == 1) && (num3 == 0))) || (((num == 9) && (num2 == 0)) || ((num == 0) && (num2 == 9))))) || ((num3 == 9) && (num == 0))) || ((num3 == 0) && (num == 9)))
            {
                return "半順";
            }
            return "雜六";
        }

        protected string get_ssc168_str(string numstr)
        {
            string[] strArray = numstr.Split(new char[] { ',' });
            int num = int.Parse(strArray[0]);
            int num2 = int.Parse(strArray[1]);
            int num3 = int.Parse(strArray[2]);
            if ((num == num2) && (num2 == num3))
            {
                return "豹子";
            }
            if (((((((((num2 - num) == 1) && ((num3 - num2) == 1)) || (((num - num2) == 1) && ((num2 - num3) == 1))) || ((((num - num3) == 1) && ((num2 - num) == 1)) || (((num3 - num2) == 1) && ((num - num3) == 1)))) || (((((num3 - num) == 1) && ((num - num2) == 1)) || (((num2 - num3) == 1) && ((num3 - num) == 1))) || ((((num == 0) && (num2 == 9)) && (num3 == 8)) || (((num == 8) && (num2 == 9)) && (num3 == 0))))) || ((((((num == 0) && (num2 == 8)) && (num3 == 9)) || (((num == 8) && (num2 == 0)) && (num3 == 9))) || ((((num == 9) && (num2 == 8)) && (num3 == 0)) || (((num == 9) && (num2 == 0)) && (num3 == 8)))) || (((((num == 1) && (num2 == 2)) && (num3 == 0)) || (((num == 9) && (num2 == 0)) && (num3 == 1))) || ((((num == 0) && (num2 == 1)) && (num3 == 9)) || (((num == 0) && (num2 == 9)) && (num3 == 1)))))) || ((((num == 1) && (num2 == 0)) && (num3 == 9)) || (((num == 1) && (num2 == 9)) && (num3 == 0)))) || (((num == 9) && (num2 == 1)) && (num3 == 0)))
            {
                return "順子";
            }
            if ((((num - num2) == 0) || ((num - num3) == 0)) || ((num2 - num3) == 0))
            {
                return "對子";
            }
            if ((((((((num2 - num) == 1) || ((num3 - num2) == 1)) || (((num - num3) == 1) || ((num - num2) == 1))) || ((((num2 - num3) == 1) || ((num3 - num) == 1)) || ((num2 == 9) && (num3 == 0)))) || ((((num2 == 0) && (num3 == 9)) || ((num == 1) && (num3 == 0))) || (((num == 9) && (num2 == 0)) || ((num == 0) && (num2 == 9))))) || ((num3 == 9) && (num == 0))) || ((num3 == 0) && (num == 9)))
            {
                return "半順";
            }
            return "雜六";
        }

        protected string get_tz(string hm)
        {
            int num = 0;
            if (hm.Trim() == "49")
            {
                return "和";
            }
            num = int.Parse(hm) % 4;
            if (num == 0)
            {
                return "4";
            }
            int num2 = int.Parse(hm) % 4;
            return num2.ToString();
        }

        protected string get_vrssc_str(string numstr)
        {
            string[] strArray = numstr.Split(new char[] { ',' });
            int num = int.Parse(strArray[0]);
            int num2 = int.Parse(strArray[1]);
            int num3 = int.Parse(strArray[2]);
            if ((num == num2) && (num2 == num3))
            {
                return "豹子";
            }
            if (((((((((num2 - num) == 1) && ((num3 - num2) == 1)) || (((num - num2) == 1) && ((num2 - num3) == 1))) || ((((num - num3) == 1) && ((num2 - num) == 1)) || (((num3 - num2) == 1) && ((num - num3) == 1)))) || (((((num3 - num) == 1) && ((num - num2) == 1)) || (((num2 - num3) == 1) && ((num3 - num) == 1))) || ((((num == 0) && (num2 == 9)) && (num3 == 8)) || (((num == 8) && (num2 == 9)) && (num3 == 0))))) || ((((((num == 0) && (num2 == 8)) && (num3 == 9)) || (((num == 8) && (num2 == 0)) && (num3 == 9))) || ((((num == 9) && (num2 == 8)) && (num3 == 0)) || (((num == 9) && (num2 == 0)) && (num3 == 8)))) || (((((num == 1) && (num2 == 2)) && (num3 == 0)) || (((num == 9) && (num2 == 0)) && (num3 == 1))) || ((((num == 0) && (num2 == 1)) && (num3 == 9)) || (((num == 0) && (num2 == 9)) && (num3 == 1)))))) || ((((num == 1) && (num2 == 0)) && (num3 == 9)) || (((num == 1) && (num2 == 9)) && (num3 == 0)))) || (((num == 9) && (num2 == 1)) && (num3 == 0)))
            {
                return "順子";
            }
            if ((((num - num2) == 0) || ((num - num3) == 0)) || ((num2 - num3) == 0))
            {
                return "對子";
            }
            if ((((((((num2 - num) == 1) || ((num3 - num2) == 1)) || (((num - num3) == 1) || ((num - num2) == 1))) || ((((num2 - num3) == 1) || ((num3 - num) == 1)) || ((num2 == 9) && (num3 == 0)))) || ((((num2 == 0) && (num3 == 9)) || ((num == 1) && (num3 == 0))) || (((num == 9) && (num2 == 0)) || ((num == 0) && (num2 == 9))))) || ((num3 == 9) && (num == 0))) || ((num3 == 0) && (num == 9)))
            {
                return "半順";
            }
            return "雜六";
        }

        protected string GetZodiacNameList(string n1, string n2, string n3, string n4, string n5, string n6)
        {
            string zodiacName = base.GetZodiacName(n1);
            string str2 = base.GetZodiacName(n2);
            string str3 = base.GetZodiacName(n3);
            string str4 = base.GetZodiacName(n4);
            string str5 = base.GetZodiacName(n5);
            string str6 = base.GetZodiacName(n6);
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("{0}&nbsp;{1}&nbsp;{2}&nbsp;{3}&nbsp;{4}&nbsp;{5}", new object[] { zodiacName, str2, str3, str4, str5, str6 });
            return builder.ToString();
        }

        protected string GetZodiacNameString(string sn)
        {
            string zodiacName = base.GetZodiacName(sn);
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("{0}", zodiacName);
            return builder.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.lotteryID = base.q("id");
            this.mID = base.q("mid");
            this.numTable = base.q("tabletype");
            if (string.IsNullOrEmpty(this.numTable))
            {
                string cookie = CookieHelper.GetCookie("PKBJL_TableType_Record_Current_User");
                if (string.IsNullOrEmpty(cookie))
                {
                    cookie = "p1";
                }
                this.numTable = Utils.GetPKBJL_NumTable(cookie);
            }
            this.lotteryDT = base.GetLotteryList();
            if (!string.IsNullOrEmpty(base.q("page")))
            {
                this.page = Convert.ToInt32(base.q("page"));
            }
            if (this.page <= 0)
            {
                this.page = 1;
            }
            this.FiledValue = new string[] { this.mID, this.lotteryID, this.numTable };
            int num = 100;
            if (this.lotteryID == num.ToString())
            {
                this.sixDataTable = CallBLL.cz_phase_six_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            num = 0;
            if (this.lotteryID == num.ToString())
            {
                this.kl10DataTable = CallBLL.cz_phase_kl10_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            num = 1;
            if (this.lotteryID == num.ToString())
            {
                this.cqscDataTable = CallBLL.cz_phase_cqsc_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            num = 2;
            if (this.lotteryID == num.ToString())
            {
                this.pk10DataTable = CallBLL.cz_phase_pk10_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            num = 3;
            if (this.lotteryID == num.ToString())
            {
                this.xyncDataTable = CallBLL.cz_phase_xync_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            num = 4;
            if (this.lotteryID == num.ToString())
            {
                this.jsk3DataTable = CallBLL.cz_phase_jsk3_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            num = 5;
            if (this.lotteryID == num.ToString())
            {
                this.kl8DataTable = CallBLL.cz_phase_kl8_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            num = 6;
            if (this.lotteryID == num.ToString())
            {
                this.k8scDataTable = CallBLL.cz_phase_k8sc_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            num = 7;
            if (this.lotteryID == num.ToString())
            {
                this.pcddDataTable = CallBLL.cz_phase_pcdd_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            num = 9;
            if (this.lotteryID == num.ToString())
            {
                this.xyft5DataTable = CallBLL.cz_phase_xyft5_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            num = 8;
            if (this.lotteryID == num.ToString())
            {
                this.pkbjlDataTable = CallBLL.cz_phase_pkbjl_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.numTable).Tables[0];
            }
            num = 10;
            if (this.lotteryID == num.ToString())
            {
                this.jscarDataTable = CallBLL.cz_phase_jscar_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            num = 11;
            if (this.lotteryID == num.ToString())
            {
                this.speed5DataTable = CallBLL.cz_phase_speed5_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            num = 13;
            if (this.lotteryID == num.ToString())
            {
                this.jscqscDataTable = CallBLL.cz_phase_jscqsc_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            num = 12;
            if (this.lotteryID == num.ToString())
            {
                this.jspk10DataTable = CallBLL.cz_phase_jspk10_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            num = 14;
            if (this.lotteryID == num.ToString())
            {
                this.jssfcDataTable = CallBLL.cz_phase_jssfc_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            num = 15;
            if (this.lotteryID == num.ToString())
            {
                this.jsft2DataTable = CallBLL.cz_phase_jsft2_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            num = 0x10;
            if (this.lotteryID == num.ToString())
            {
                this.car168DataTable = CallBLL.cz_phase_car168_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            num = 0x11;
            if (this.lotteryID == num.ToString())
            {
                this.ssc168DataTable = CallBLL.cz_phase_ssc168_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            num = 0x12;
            if (this.lotteryID == num.ToString())
            {
                this.vrcarDataTable = CallBLL.cz_phase_vrcar_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            num = 0x13;
            if (this.lotteryID == num.ToString())
            {
                this.vrsscDataTable = CallBLL.cz_phase_vrssc_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            num = 20;
            if (this.lotteryID == num.ToString())
            {
                this.xyftoaDataTable = CallBLL.cz_phase_xyftoa_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            num = 0x15;
            if (this.lotteryID == num.ToString())
            {
                this.xyftsgDataTable = CallBLL.cz_phase_xyftsg_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            num = 0x16;
            if (this.lotteryID == num.ToString())
            {
                this.happycarDataTable = CallBLL.cz_phase_happycar_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
        }

        public string Week(DateTime rq_str)
        {
            string[] strArray = new string[] { "日", "一", "二", "三", "四", "五", "六" };
            return strArray[Convert.ToInt32(rq_str.DayOfWeek)];
        }
    }
}

