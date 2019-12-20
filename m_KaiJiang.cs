using LotterySystem.Common;
using System;
using System.Data;
using User.Web.WebBase;

public class m_KaiJiang : MemberPageBase_Mobile
{
    protected DataTable data_Table = null;
    protected DataTable date_Table = null;
    protected DataTable lotteryTable = null;
    private int nDate = 30;
    private int nTop = 30;
    protected string numTable = "";
    protected string sltDate = "";
    protected string sltType = "";

    protected string ChangeDate(object str)
    {
        return Convert.ToDateTime(str.ToString()).ToString("yyyy-MM-dd");
    }

    protected string get_cl_dsh(string d_c, string s_c)
    {
        string str = "";
        int num = int.Parse(d_c);
        int num2 = int.Parse(s_c);
        if (num == num2)
        {
            return "<span class=\"green\">和</span>";
        }
        if (num > num2)
        {
            return "<span class=\"blue\">單(多)</span>";
        }
        if (num < num2)
        {
            str = "<span class=\"orange\">雙(多)</span>";
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
            return "<span class=\"green\">和</span>";
        }
        if (num > num2)
        {
            return "<span class=\"blue\">前(多)</span>";
        }
        if (num < num2)
        {
            str = "<span class=\"orange\">後(多)</span>";
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

    protected string GetTypeName(string k_lb)
    {
        if (k_lb.Equals("0"))
        {
            return "北京快8";
        }
        return "加拿大午夜盤";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.Response.Expires = 0;
        base.IsUserLoginByMobile();
        this.sltType = LSRequest.qq("sltType");
        this.sltDate = LSRequest.qq("sltDate");
        this.lotteryTable = base.GetLotteryList();
        if (string.IsNullOrEmpty(this.sltType))
        {
            this.sltType = base.LotteryTypeSave();
        }
        if (string.IsNullOrEmpty(this.sltDate))
        {
            this.sltDate = DateTime.Now.AddHours((double) -int.Parse(base.get_BetTime_KC())).ToString("yyyy-MM-dd");
        }
        switch (Convert.ToInt32(this.sltType))
        {
            case 0:
                this.date_Table = CallBLL.cz_phase_kl10_bll.GetPhaseDate(this.nDate);
                this.data_Table = CallBLL.cz_phase_kl10_bll.GetPhaseByQueryDate(this.sltDate, this.nTop);
                break;

            case 1:
                this.date_Table = CallBLL.cz_phase_cqsc_bll.GetPhaseDate(this.nDate);
                this.data_Table = CallBLL.cz_phase_cqsc_bll.GetPhaseByQueryDate(this.sltDate, this.nTop);
                break;

            case 2:
                this.date_Table = CallBLL.cz_phase_pk10_bll.GetPhaseDate(this.nDate);
                this.data_Table = CallBLL.cz_phase_pk10_bll.GetPhaseByQueryDate(this.sltDate, this.nTop);
                break;

            case 3:
                this.date_Table = CallBLL.cz_phase_xync_bll.GetPhaseDate(this.nDate);
                this.data_Table = CallBLL.cz_phase_xync_bll.GetPhaseByQueryDate(this.sltDate, this.nTop);
                break;

            case 4:
                this.date_Table = CallBLL.cz_phase_jsk3_bll.GetPhaseDate(this.nDate);
                this.data_Table = CallBLL.cz_phase_jsk3_bll.GetPhaseByQueryDate(this.sltDate, this.nTop);
                break;

            case 5:
                this.date_Table = CallBLL.cz_phase_kl8_bll.GetPhaseDate(this.nDate);
                this.data_Table = CallBLL.cz_phase_kl8_bll.GetPhaseByQueryDate(this.sltDate, this.nTop);
                break;

            case 6:
                this.date_Table = CallBLL.cz_phase_k8sc_bll.GetPhaseDate(this.nDate);
                this.data_Table = CallBLL.cz_phase_k8sc_bll.GetPhaseByQueryDate(this.sltDate, this.nTop);
                break;

            case 7:
                this.date_Table = CallBLL.cz_phase_pcdd_bll.GetPhaseDate(this.nDate);
                this.data_Table = CallBLL.cz_phase_pcdd_bll.GetPhaseByQueryDate(this.sltDate, this.nTop);
                break;

            case 8:
                this.numTable = LSRequest.qq("tabletype");
                if (string.IsNullOrEmpty(this.numTable))
                {
                    string cookie = CookieHelper.GetCookie("PKBJL_TableType_Record_Current_User");
                    if (string.IsNullOrEmpty(cookie))
                    {
                        cookie = "p1";
                    }
                    this.numTable = Utils.GetPKBJL_NumTable(cookie);
                }
                this.date_Table = CallBLL.cz_phase_pkbjl_bll.GetPhaseDate(this.nDate, this.numTable);
                this.data_Table = CallBLL.cz_phase_pkbjl_bll.GetPhaseByQueryDate(this.sltDate, this.nTop, this.numTable);
                break;

            case 9:
                this.date_Table = CallBLL.cz_phase_xyft5_bll.GetPhaseDate(this.nDate);
                this.data_Table = CallBLL.cz_phase_xyft5_bll.GetPhaseByQueryDate(this.sltDate, this.nTop);
                break;

            case 10:
                this.date_Table = CallBLL.cz_phase_jscar_bll.GetPhaseDate(this.nDate);
                this.data_Table = CallBLL.cz_phase_jscar_bll.GetPhaseByQueryDate(this.sltDate, this.nTop);
                break;

            case 11:
                this.date_Table = CallBLL.cz_phase_speed5_bll.GetPhaseDate(this.nDate);
                this.data_Table = CallBLL.cz_phase_speed5_bll.GetPhaseByQueryDate(this.sltDate, this.nTop);
                break;

            case 12:
                this.date_Table = CallBLL.cz_phase_jspk10_bll.GetPhaseDate(this.nDate);
                this.data_Table = CallBLL.cz_phase_jspk10_bll.GetPhaseByQueryDate(this.sltDate, this.nTop);
                break;

            case 13:
                this.date_Table = CallBLL.cz_phase_jscqsc_bll.GetPhaseDate(this.nDate);
                this.data_Table = CallBLL.cz_phase_jscqsc_bll.GetPhaseByQueryDate(this.sltDate, this.nTop);
                break;

            case 14:
                this.date_Table = CallBLL.cz_phase_jssfc_bll.GetPhaseDate(this.nDate);
                this.data_Table = CallBLL.cz_phase_jssfc_bll.GetPhaseByQueryDate(this.sltDate, this.nTop);
                break;

            case 15:
                this.date_Table = CallBLL.cz_phase_jsft2_bll.GetPhaseDate(this.nDate);
                this.data_Table = CallBLL.cz_phase_jsft2_bll.GetPhaseByQueryDate(this.sltDate, this.nTop);
                break;

            case 0x10:
                this.date_Table = CallBLL.cz_phase_car168_bll.GetPhaseDate(this.nDate);
                this.data_Table = CallBLL.cz_phase_car168_bll.GetPhaseByQueryDate(this.sltDate, this.nTop);
                break;

            case 0x11:
                this.date_Table = CallBLL.cz_phase_ssc168_bll.GetPhaseDate(this.nDate);
                this.data_Table = CallBLL.cz_phase_ssc168_bll.GetPhaseByQueryDate(this.sltDate, this.nTop);
                break;

            case 0x12:
                this.date_Table = CallBLL.cz_phase_vrcar_bll.GetPhaseDate(this.nDate);
                this.data_Table = CallBLL.cz_phase_vrcar_bll.GetPhaseByQueryDate(this.sltDate, this.nTop);
                break;

            case 0x13:
                this.date_Table = CallBLL.cz_phase_vrssc_bll.GetPhaseDate(this.nDate);
                this.data_Table = CallBLL.cz_phase_vrssc_bll.GetPhaseByQueryDate(this.sltDate, this.nTop);
                break;

            case 20:
                this.date_Table = CallBLL.cz_phase_xyftoa_bll.GetPhaseDate(this.nDate);
                this.data_Table = CallBLL.cz_phase_xyftoa_bll.GetPhaseByQueryDate(this.sltDate, this.nTop);
                break;

            case 0x15:
                this.date_Table = CallBLL.cz_phase_xyftsg_bll.GetPhaseDate(this.nDate);
                this.data_Table = CallBLL.cz_phase_xyftsg_bll.GetPhaseByQueryDate(this.sltDate, this.nTop);
                break;

            case 0x16:
                this.date_Table = CallBLL.cz_phase_happycar_bll.GetPhaseDate(this.nDate);
                this.data_Table = CallBLL.cz_phase_happycar_bll.GetPhaseByQueryDate(this.sltDate, this.nTop);
                break;

            case 100:
                this.data_Table = CallBLL.cz_phase_six_bll.GetTopPhase(this.nTop).Tables[0];
                break;

            default:
                base.Response.End();
                break;
        }
    }
}

