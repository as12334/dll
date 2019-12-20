namespace Agent.Web
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;

    public class HistoryLottery : MemberPageBase
    {
        protected DataTable car168DataTable;
        protected DataTable cqscDataTable;
        protected int dataCount;
        protected string[] FiledName = new string[] { "lid", "tabletype" };
        protected string[] FiledValue;
        protected DataTable happycarDataTable;
        protected DataTable jscarDataTable;
        protected DataTable jscqscDataTable;
        protected DataTable jsft2DataTable;
        protected DataTable jsk3DataTable;
        protected DataTable jspk10DataTable;
        protected DataTable jssfcDataTable;
        protected DataTable k8scDataTable;
        protected DataTable kl10DataTable;
        protected DataTable kl8DataTable;
        protected DataTable lotteryDT;
        protected string lotteryID = "";
        protected string numTable = "";
        protected int page = 1;
        protected int pageCount;
        protected int pageSize = 20;
        protected DataTable pcddDataTable;
        protected DataTable pk10DataTable;
        protected DataTable pkbjlDataTable;
        protected DataTable sixDataTable;
        protected string skin = "";
        protected DataTable speed5DataTable;
        protected DataTable ssc168DataTable;
        protected DataTable vrcarDataTable;
        protected DataTable vrsscDataTable;
        protected DataTable xyft5DataTable;
        protected DataTable xyftoaDataTable;
        protected DataTable xyftsgDataTable;
        protected DataTable xyncDataTable;

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
                str = "<label class=\"red\">雙(多)</label>";
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
                str = "<label class=\"red\">後(多)</label>";
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
            if (((((((num2 - num) == 1) && ((num3 - num2) == 1)) || (((num - num2) == 1) && ((num2 - num3) == 1))) || ((((num - num3) == 1) && ((num2 - num) == 1)) || (((num3 - num2) == 1) && ((num - num3) == 1)))) || (((((num3 - num) == 1) && ((num - num2) == 1)) || (((num2 - num3) == 1) && ((num3 - num) == 1))) || ((((num == 0) && (num2 == 9)) && (num3 == 8)) || (((num == 8) && (num2 == 9)) && (num3 == 0))))) || ((((((num == 0) && (num2 == 8)) && (num3 == 9)) || (((num == 8) && (num2 == 0)) && (num3 == 9))) || ((((num == 9) && (num2 == 8)) && (num3 == 0)) || (((num == 9) && (num2 == 0)) && (num3 == 8)))) || ((((((num == 1) && (num2 == 2)) && (num3 == 0)) || (((num == 9) && (num2 == 0)) && (num3 == 1))) || ((((num == 0) && (num2 == 1)) && (num3 == 9)) || (((num == 0) && (num2 == 9)) && (num3 == 1)))) || (((((num == 1) && (num2 == 0)) && (num3 == 9)) || (((num == 1) && (num2 == 9)) && (num3 == 0))) || (((num == 9) && (num2 == 1)) && (num3 == 0))))))
            {
                return "順子";
            }
            if ((((num - num2) == 0) || ((num - num3) == 0)) || ((num2 - num3) == 0))
            {
                return "對子";
            }
            if ((((((((num2 - num) == 1) || ((num3 - num2) == 1)) || (((num - num3) == 1) || ((num - num2) == 1))) || ((((num2 - num3) == 1) || ((num3 - num) == 1)) || ((num2 == 9) && (num3 == 0)))) || ((num2 == 0) && (num3 == 9))) || (((num == 1) && (num3 == 0)) || ((num == 9) && (num2 == 0)))) || ((((num == 0) && (num2 == 9)) || ((num3 == 9) && (num == 0))) || ((num3 == 0) && (num == 9))))
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
            if (((((((num2 - num) == 1) && ((num3 - num2) == 1)) || (((num - num2) == 1) && ((num2 - num3) == 1))) || ((((num - num3) == 1) && ((num2 - num) == 1)) || (((num3 - num2) == 1) && ((num - num3) == 1)))) || (((((num3 - num) == 1) && ((num - num2) == 1)) || (((num2 - num3) == 1) && ((num3 - num) == 1))) || ((((num == 0) && (num2 == 9)) && (num3 == 8)) || (((num == 8) && (num2 == 9)) && (num3 == 0))))) || ((((((num == 0) && (num2 == 8)) && (num3 == 9)) || (((num == 8) && (num2 == 0)) && (num3 == 9))) || ((((num == 9) && (num2 == 8)) && (num3 == 0)) || (((num == 9) && (num2 == 0)) && (num3 == 8)))) || ((((((num == 1) && (num2 == 2)) && (num3 == 0)) || (((num == 9) && (num2 == 0)) && (num3 == 1))) || ((((num == 0) && (num2 == 1)) && (num3 == 9)) || (((num == 0) && (num2 == 9)) && (num3 == 1)))) || (((((num == 1) && (num2 == 0)) && (num3 == 9)) || (((num == 1) && (num2 == 9)) && (num3 == 0))) || (((num == 9) && (num2 == 1)) && (num3 == 0))))))
            {
                return "順子";
            }
            if ((((num - num2) == 0) || ((num - num3) == 0)) || ((num2 - num3) == 0))
            {
                return "對子";
            }
            if ((((((((num2 - num) == 1) || ((num3 - num2) == 1)) || (((num - num3) == 1) || ((num - num2) == 1))) || ((((num2 - num3) == 1) || ((num3 - num) == 1)) || ((num2 == 9) && (num3 == 0)))) || ((num2 == 0) && (num3 == 9))) || (((num == 1) && (num3 == 0)) || ((num == 9) && (num2 == 0)))) || ((((num == 0) && (num2 == 9)) || ((num3 == 9) && (num == 0))) || ((num3 == 0) && (num == 9))))
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
            if (((((((num2 - num) == 1) && ((num3 - num2) == 1)) || (((num - num2) == 1) && ((num2 - num3) == 1))) || ((((num - num3) == 1) && ((num2 - num) == 1)) || (((num3 - num2) == 1) && ((num - num3) == 1)))) || (((((num3 - num) == 1) && ((num - num2) == 1)) || (((num2 - num3) == 1) && ((num3 - num) == 1))) || ((((num == 0) && (num2 == 9)) && (num3 == 8)) || (((num == 8) && (num2 == 9)) && (num3 == 0))))) || ((((((num == 0) && (num2 == 8)) && (num3 == 9)) || (((num == 8) && (num2 == 0)) && (num3 == 9))) || ((((num == 9) && (num2 == 8)) && (num3 == 0)) || (((num == 9) && (num2 == 0)) && (num3 == 8)))) || ((((((num == 1) && (num2 == 2)) && (num3 == 0)) || (((num == 9) && (num2 == 0)) && (num3 == 1))) || ((((num == 0) && (num2 == 1)) && (num3 == 9)) || (((num == 0) && (num2 == 9)) && (num3 == 1)))) || (((((num == 1) && (num2 == 0)) && (num3 == 9)) || (((num == 1) && (num2 == 9)) && (num3 == 0))) || (((num == 9) && (num2 == 1)) && (num3 == 0))))))
            {
                return "順子";
            }
            if ((((num - num2) == 0) || ((num - num3) == 0)) || ((num2 - num3) == 0))
            {
                return "對子";
            }
            if ((((((((num2 - num) == 1) || ((num3 - num2) == 1)) || (((num - num3) == 1) || ((num - num2) == 1))) || ((((num2 - num3) == 1) || ((num3 - num) == 1)) || ((num2 == 9) && (num3 == 0)))) || ((num2 == 0) && (num3 == 9))) || (((num == 1) && (num3 == 0)) || ((num == 9) && (num2 == 0)))) || ((((num == 0) && (num2 == 9)) || ((num3 == 9) && (num == 0))) || ((num3 == 0) && (num == 9))))
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
            if (((((((num2 - num) == 1) && ((num3 - num2) == 1)) || (((num - num2) == 1) && ((num2 - num3) == 1))) || ((((num - num3) == 1) && ((num2 - num) == 1)) || (((num3 - num2) == 1) && ((num - num3) == 1)))) || (((((num3 - num) == 1) && ((num - num2) == 1)) || (((num2 - num3) == 1) && ((num3 - num) == 1))) || ((((num == 0) && (num2 == 9)) && (num3 == 8)) || (((num == 8) && (num2 == 9)) && (num3 == 0))))) || ((((((num == 0) && (num2 == 8)) && (num3 == 9)) || (((num == 8) && (num2 == 0)) && (num3 == 9))) || ((((num == 9) && (num2 == 8)) && (num3 == 0)) || (((num == 9) && (num2 == 0)) && (num3 == 8)))) || ((((((num == 1) && (num2 == 2)) && (num3 == 0)) || (((num == 9) && (num2 == 0)) && (num3 == 1))) || ((((num == 0) && (num2 == 1)) && (num3 == 9)) || (((num == 0) && (num2 == 9)) && (num3 == 1)))) || (((((num == 1) && (num2 == 0)) && (num3 == 9)) || (((num == 1) && (num2 == 9)) && (num3 == 0))) || (((num == 9) && (num2 == 1)) && (num3 == 0))))))
            {
                return "順子";
            }
            if ((((num - num2) == 0) || ((num - num3) == 0)) || ((num2 - num3) == 0))
            {
                return "對子";
            }
            if ((((((((num2 - num) == 1) || ((num3 - num2) == 1)) || (((num - num3) == 1) || ((num - num2) == 1))) || ((((num2 - num3) == 1) || ((num3 - num) == 1)) || ((num2 == 9) && (num3 == 0)))) || ((num2 == 0) && (num3 == 9))) || (((num == 1) && (num3 == 0)) || ((num == 9) && (num2 == 0)))) || ((((num == 0) && (num2 == 9)) || ((num3 == 9) && (num == 0))) || ((num3 == 0) && (num == 9))))
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
            if (((((((num2 - num) == 1) && ((num3 - num2) == 1)) || (((num - num2) == 1) && ((num2 - num3) == 1))) || ((((num - num3) == 1) && ((num2 - num) == 1)) || (((num3 - num2) == 1) && ((num - num3) == 1)))) || (((((num3 - num) == 1) && ((num - num2) == 1)) || (((num2 - num3) == 1) && ((num3 - num) == 1))) || ((((num == 0) && (num2 == 9)) && (num3 == 8)) || (((num == 8) && (num2 == 9)) && (num3 == 0))))) || ((((((num == 0) && (num2 == 8)) && (num3 == 9)) || (((num == 8) && (num2 == 0)) && (num3 == 9))) || ((((num == 9) && (num2 == 8)) && (num3 == 0)) || (((num == 9) && (num2 == 0)) && (num3 == 8)))) || ((((((num == 1) && (num2 == 2)) && (num3 == 0)) || (((num == 9) && (num2 == 0)) && (num3 == 1))) || ((((num == 0) && (num2 == 1)) && (num3 == 9)) || (((num == 0) && (num2 == 9)) && (num3 == 1)))) || (((((num == 1) && (num2 == 0)) && (num3 == 9)) || (((num == 1) && (num2 == 9)) && (num3 == 0))) || (((num == 9) && (num2 == 1)) && (num3 == 0))))))
            {
                return "順子";
            }
            if ((((num - num2) == 0) || ((num - num3) == 0)) || ((num2 - num3) == 0))
            {
                return "對子";
            }
            if ((((((((num2 - num) == 1) || ((num3 - num2) == 1)) || (((num - num3) == 1) || ((num - num2) == 1))) || ((((num2 - num3) == 1) || ((num3 - num) == 1)) || ((num2 == 9) && (num3 == 0)))) || ((num2 == 0) && (num3 == 9))) || (((num == 1) && (num3 == 0)) || ((num == 9) && (num2 == 0)))) || ((((num == 0) && (num2 == 9)) || ((num3 == 9) && (num == 0))) || ((num3 == 0) && (num == 9))))
            {
                return "半順";
            }
            return "雜六";
        }

        protected string get_tz(string hm)
        {
            if (hm.Trim() == "49")
            {
                return "和";
            }
            if ((int.Parse(hm) % 4) == 0)
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
            if (((((((num2 - num) == 1) && ((num3 - num2) == 1)) || (((num - num2) == 1) && ((num2 - num3) == 1))) || ((((num - num3) == 1) && ((num2 - num) == 1)) || (((num3 - num2) == 1) && ((num - num3) == 1)))) || (((((num3 - num) == 1) && ((num - num2) == 1)) || (((num2 - num3) == 1) && ((num3 - num) == 1))) || ((((num == 0) && (num2 == 9)) && (num3 == 8)) || (((num == 8) && (num2 == 9)) && (num3 == 0))))) || ((((((num == 0) && (num2 == 8)) && (num3 == 9)) || (((num == 8) && (num2 == 0)) && (num3 == 9))) || ((((num == 9) && (num2 == 8)) && (num3 == 0)) || (((num == 9) && (num2 == 0)) && (num3 == 8)))) || ((((((num == 1) && (num2 == 2)) && (num3 == 0)) || (((num == 9) && (num2 == 0)) && (num3 == 1))) || ((((num == 0) && (num2 == 1)) && (num3 == 9)) || (((num == 0) && (num2 == 9)) && (num3 == 1)))) || (((((num == 1) && (num2 == 0)) && (num3 == 9)) || (((num == 1) && (num2 == 9)) && (num3 == 0))) || (((num == 9) && (num2 == 1)) && (num3 == 0))))))
            {
                return "順子";
            }
            if ((((num - num2) == 0) || ((num - num3) == 0)) || ((num2 - num3) == 0))
            {
                return "對子";
            }
            if ((((((((num2 - num) == 1) || ((num3 - num2) == 1)) || (((num - num3) == 1) || ((num - num2) == 1))) || ((((num2 - num3) == 1) || ((num3 - num) == 1)) || ((num2 == 9) && (num3 == 0)))) || ((num2 == 0) && (num3 == 9))) || (((num == 1) && (num3 == 0)) || ((num == 9) && (num2 == 0)))) || ((((num == 0) && (num2 == 9)) || ((num3 == 9) && (num == 0))) || ((num3 == 0) && (num == 9))))
            {
                return "半順";
            }
            return "雜六";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.skin = (this.Session[this.Session["user_name"].ToString() + "lottery_session_user_info"] as agent_userinfo_session).get_u_skin();
            this.lotteryID = base.q("lid");
            this.numTable = base.q("tabletype");
            if (string.IsNullOrEmpty(this.numTable))
            {
                string cookie = CookieHelper.GetCookie("PKBJL_TableType_Record_Current_Agent");
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
            else
            {
                this.page = 1;
            }
            if (this.page < 1)
            {
                this.page = 1;
            }
            this.FiledValue = new string[] { this.lotteryID, this.numTable };
            int num = 100;
            if (this.lotteryID == num.ToString())
            {
                this.sixDataTable = CallBLL.cz_phase_six_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            int num2 = 0;
            if (this.lotteryID == num2.ToString())
            {
                this.kl10DataTable = CallBLL.cz_phase_kl10_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            int num3 = 1;
            if (this.lotteryID == num3.ToString())
            {
                this.cqscDataTable = CallBLL.cz_phase_cqsc_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            int num4 = 2;
            if (this.lotteryID == num4.ToString())
            {
                this.pk10DataTable = CallBLL.cz_phase_pk10_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            int num5 = 3;
            if (this.lotteryID == num5.ToString())
            {
                this.xyncDataTable = CallBLL.cz_phase_xync_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            int num6 = 4;
            if (this.lotteryID == num6.ToString())
            {
                this.jsk3DataTable = CallBLL.cz_phase_jsk3_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            int num7 = 5;
            if (this.lotteryID == num7.ToString())
            {
                this.kl8DataTable = CallBLL.cz_phase_kl8_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            int num8 = 6;
            if (this.lotteryID == num8.ToString())
            {
                this.k8scDataTable = CallBLL.cz_phase_k8sc_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            int num9 = 7;
            if (this.lotteryID == num9.ToString())
            {
                this.pcddDataTable = CallBLL.cz_phase_pcdd_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            int num10 = 9;
            if (this.lotteryID == num10.ToString())
            {
                this.xyft5DataTable = CallBLL.cz_phase_xyft5_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            int num11 = 8;
            if (this.lotteryID == num11.ToString())
            {
                this.pkbjlDataTable = CallBLL.cz_phase_pkbjl_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.numTable).Tables[0];
            }
            int num12 = 10;
            if (this.lotteryID == num12.ToString())
            {
                this.jscarDataTable = CallBLL.cz_phase_jscar_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            int num13 = 11;
            if (this.lotteryID == num13.ToString())
            {
                this.speed5DataTable = CallBLL.cz_phase_speed5_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            int num14 = 13;
            if (this.lotteryID == num14.ToString())
            {
                this.jscqscDataTable = CallBLL.cz_phase_jscqsc_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            int num15 = 12;
            if (this.lotteryID == num15.ToString())
            {
                this.jspk10DataTable = CallBLL.cz_phase_jspk10_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            int num16 = 14;
            if (this.lotteryID == num16.ToString())
            {
                this.jssfcDataTable = CallBLL.cz_phase_jssfc_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            int num17 = 15;
            if (this.lotteryID == num17.ToString())
            {
                this.jsft2DataTable = CallBLL.cz_phase_jsft2_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            int num18 = 0x10;
            if (this.lotteryID == num18.ToString())
            {
                this.car168DataTable = CallBLL.cz_phase_car168_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            int num19 = 0x11;
            if (this.lotteryID == num19.ToString())
            {
                this.ssc168DataTable = CallBLL.cz_phase_ssc168_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            int num20 = 0x12;
            if (this.lotteryID == num20.ToString())
            {
                this.vrcarDataTable = CallBLL.cz_phase_vrcar_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            int num21 = 0x13;
            if (this.lotteryID == num21.ToString())
            {
                this.vrsscDataTable = CallBLL.cz_phase_vrssc_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            int num22 = 20;
            if (this.lotteryID == num22.ToString())
            {
                this.xyftoaDataTable = CallBLL.cz_phase_xyftoa_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            int num23 = 0x15;
            if (this.lotteryID == num23.ToString())
            {
                this.xyftsgDataTable = CallBLL.cz_phase_xyftsg_bll.GetPhaseByPage(this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount).Tables[0];
            }
            int num24 = 0x16;
            if (this.lotteryID == num24.ToString())
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

