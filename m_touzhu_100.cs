using LotterySystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using User.Web.WebBase;

public class m_touzhu_100 : MemberPageBase_Mobile
{
    protected string caizhong = "";
    protected string data_str = "";
    protected Dictionary<string, double> dictWT = new Dictionary<string, double>();
    protected Dictionary<string, double> dictWT_SX = new Dictionary<string, double>();
    protected Dictionary<string, double> dictWT_WS = new Dictionary<string, double>();
    protected int g_cnt = 0;
    protected string game_type = "";
    protected string group_odds = "";
    protected string jiangqi = "";
    protected string jiangqi_str = "";
    protected string lmtype = null;
    protected string lx_numbers = "";
    protected string numbers = "";
    protected string pk_kind = "";
    protected DataTable plDT = null;
    protected string r_url = "";
    protected int s_count = 0;
    protected int s_momey = 0;
    protected string str_numbers = "";
    protected string txtMomey = "";
    protected cz_userinfo_session uModel = null;
    protected string wanfa = "";
    protected string wt_value = "";

    private void EndTipInfo(string msg)
    {
        string s = string.Format("<script>history.go(-1);alert('{0}');</script>", msg);
        base.Response.Write(s);
        base.Response.End();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.Response.Expires = 0;
        base.IsUserLoginByMobile();
        this.caizhong = base.qq("caizhong");
        this.game_type = base.qq("game_type");
        this.wanfa = base.qq("wanfa");
        this.jiangqi = base.qq("jiangqi");
        this.txtMomey = base.qq("txtMomey");
        this.data_str = base.qq("data");
        this.wt_value = base.qq("uPI_WT");
        this.lmtype = base.qq("lmtype");
        string str = base.qq("ids");
        this.group_odds = base.qq("group_odds");
        this.numbers = base.qq("numbers");
        this.str_numbers = this.numbers;
        this.str_numbers.Replace(',', '、');
        this.lx_numbers = base.qq("zodiac");
        this.lx_numbers.Replace(',', '、');
        string str2 = base.qq("plx");
        if (((((this.caizhong == "") || (this.wanfa == "")) || ((this.jiangqi == "") || (this.data_str == ""))) || (this.txtMomey == "")) || (this.game_type == ""))
        {
            this.EndTipInfo("参数错误!");
        }
        this.r_url = "SIX_" + this.game_type + ".aspx?lottery_type=" + this.caizhong + "&player_type=" + this.wanfa;
        Dictionary<string, object> source = new Dictionary<string, object>();
        string[] strArray = null;
        string str3 = "";
        if (this.lmtype != "0")
        {
            strArray = this.data_str.Split(new char[] { '|' });
            this.s_count = strArray.Length;
            this.s_momey = int.Parse(this.txtMomey) * strArray.Length;
            foreach (string str4 in strArray)
            {
                string[] strArray2 = str4.Split(new char[] { ',' })[0].Split(new char[] { '_' });
                str3 = str3 + strArray2[3].ToString().Trim() + ",";
            }
            str3 = str3.Substring(0, str3.Length - 1);
        }
        else
        {
            string[] strArray3;
            string[] strArray4;
            DataTable wTs;
            int num3;
            int num4;
            DataRow[] rowArray;
            double num5;
            double num6;
            strArray = this.data_str.Split(new char[] { '|' });
            this.s_count = 1;
            foreach (string str4 in strArray)
            {
                string str5 = str4.Split(new char[] { ',' })[0];
                string key = str5.Substring(str5.IndexOf("jeu_p_"));
                string str7 = str4.Split(new char[] { ',' })[1];
                source.Add(key, str7);
            }
            int num = source.Count<KeyValuePair<string, object>>();
            int num2 = 0;
            string str8 = str.Split(new char[] { '_' })[1];
            str3 = str8;
            if (((str8.Equals("92285") || str8.Equals("92286")) || str8.Equals("92638")) || str8.Equals("92639"))
            {
                num2 = ((num * (num - 1)) * (num - 2)) / 6;
                strArray3 = this.numbers.Split(new char[] { ',' });
                strArray4 = str2.Split(new char[] { ',' });
                wTs = null;
                if (str8.Equals("92285"))
                {
                    wTs = CallBLL.cz_wt_3qz_six_bll.GetWTs();
                }
                else if (str8.Equals("92286"))
                {
                    wTs = CallBLL.cz_wt_3z2_six_bll.GetWTs();
                }
                else if (str8.Equals("92638"))
                {
                    wTs = CallBLL.cz_wt_3qz_b_six_bll.GetWTs();
                }
                else if (str8.Equals("92639"))
                {
                    wTs = CallBLL.cz_wt_3z2_b_six_bll.GetWTs();
                }
                if ((wTs != null) && (wTs.Rows.Count > 0))
                {
                    num3 = 0;
                    foreach (string str9 in strArray3)
                    {
                        num4 = int.Parse(str9);
                        rowArray = wTs.Select(string.Format(" number = {0} ", num4));
                        if ((rowArray != null) && (rowArray.Count<DataRow>() > 0))
                        {
                            num5 = double.Parse(rowArray[0]["wt_value"].ToString());
                            num6 = double.Parse(strArray4[num3]);
                            this.dictWT.Add(str9, num5);
                        }
                        num3++;
                    }
                }
            }
            else if ((((str8.Equals("92287") || str8.Equals("92288")) || (str8.Equals("92289") || str8.Equals("92640"))) || str8.Equals("92641")) || str8.Equals("92642"))
            {
                num2 = (num * (num - 1)) / 2;
                strArray3 = this.numbers.Split(new char[] { ',' });
                strArray4 = str2.Split(new char[] { ',' });
                wTs = null;
                if (str8.Equals("92287"))
                {
                    wTs = CallBLL.cz_wt_2qz_six_bll.GetWTs();
                }
                else if (str8.Equals("92288"))
                {
                    wTs = CallBLL.cz_wt_2zt_six_bll.GetWTs();
                }
                else if (str8.Equals("92289"))
                {
                    wTs = CallBLL.cz_wt_tc_six_bll.GetWTs();
                }
                else if (str8.Equals("92640"))
                {
                    wTs = CallBLL.cz_wt_2qz_b_six_bll.GetWTs();
                }
                else if (str8.Equals("92641"))
                {
                    wTs = CallBLL.cz_wt_2zt_b_six_bll.GetWTs();
                }
                else if (str8.Equals("92642"))
                {
                    wTs = CallBLL.cz_wt_tc_b_six_bll.GetWTs();
                }
                if ((wTs != null) && (wTs.Rows.Count > 0))
                {
                    num3 = 0;
                    foreach (string str9 in strArray3)
                    {
                        num4 = int.Parse(str9);
                        rowArray = wTs.Select(string.Format(" number = {0} ", num4));
                        if ((rowArray != null) && (rowArray.Count<DataRow>() > 0))
                        {
                            num5 = double.Parse(rowArray[0]["wt_value"].ToString());
                            num6 = double.Parse(strArray4[num3]);
                            this.dictWT.Add(str9, num5);
                        }
                        num3++;
                    }
                }
            }
            else if (str8.Equals("92575") || str8.Equals("92643"))
            {
                num2 = (((num * (num - 1)) * (num - 2)) * (num - 3)) / 0x18;
                strArray3 = this.numbers.Split(new char[] { ',' });
                strArray4 = str2.Split(new char[] { ',' });
                wTs = null;
                if (str8.Equals("92575"))
                {
                    wTs = CallBLL.cz_wt_4z1_six_bll.GetWTs();
                }
                else if (str8.Equals("92643"))
                {
                    wTs = CallBLL.cz_wt_4z1_b_six_bll.GetWTs();
                }
                if ((wTs != null) && (wTs.Rows.Count > 0))
                {
                    num3 = 0;
                    foreach (string str9 in strArray3)
                    {
                        num4 = int.Parse(str9);
                        rowArray = wTs.Select(string.Format(" number = {0} ", num4));
                        if ((rowArray != null) && (rowArray.Count<DataRow>() > 0))
                        {
                            num5 = double.Parse(rowArray[0]["wt_value"].ToString());
                            num6 = double.Parse(strArray4[num3]);
                            this.dictWT.Add(str9, num5);
                        }
                        num3++;
                    }
                }
            }
            else if (str8.Equals("92572"))
            {
                num2 = ((((num * (num - 1)) * (num - 2)) * (num - 3)) * (num - 4)) / 120;
                strArray3 = this.numbers.Split(new char[] { ',' });
                strArray4 = str2.Split(new char[] { ',' });
                wTs = CallBLL.cz_wt_5bz_six_bll.GetWTs();
                if ((wTs != null) && (wTs.Rows.Count > 0))
                {
                    num3 = 0;
                    foreach (string str9 in strArray3)
                    {
                        num4 = int.Parse(str9);
                        rowArray = wTs.Select(string.Format(" number = {0} ", num4));
                        if ((rowArray != null) && (rowArray.Count<DataRow>() > 0))
                        {
                            num5 = double.Parse(rowArray[0]["wt_value"].ToString());
                            num6 = double.Parse(strArray4[num3]);
                            this.dictWT.Add(str9, num5);
                        }
                        num3++;
                    }
                }
            }
            else if (str8.Equals("92588"))
            {
                num2 = (((((num * (num - 1)) * (num - 2)) * (num - 3)) * (num - 4)) * (num - 5)) / 720;
                strArray3 = this.numbers.Split(new char[] { ',' });
                strArray4 = str2.Split(new char[] { ',' });
                wTs = CallBLL.cz_wt_6bz_six_bll.GetWTs();
                if ((wTs != null) && (wTs.Rows.Count > 0))
                {
                    num3 = 0;
                    foreach (string str9 in strArray3)
                    {
                        num4 = int.Parse(str9);
                        rowArray = wTs.Select(string.Format(" number = {0} ", num4));
                        if ((rowArray != null) && (rowArray.Count<DataRow>() > 0))
                        {
                            num5 = double.Parse(rowArray[0]["wt_value"].ToString());
                            num6 = double.Parse(strArray4[num3]);
                            this.dictWT.Add(str9, num5);
                        }
                        num3++;
                    }
                }
            }
            else if (str8.Equals("92589"))
            {
                num2 = ((((((num * (num - 1)) * (num - 2)) * (num - 3)) * (num - 4)) * (num - 5)) * (num - 6)) / 0x13b0;
                strArray3 = this.numbers.Split(new char[] { ',' });
                strArray4 = str2.Split(new char[] { ',' });
                wTs = CallBLL.cz_wt_7bz_six_bll.GetWTs();
                if ((wTs != null) && (wTs.Rows.Count > 0))
                {
                    num3 = 0;
                    foreach (string str9 in strArray3)
                    {
                        num4 = int.Parse(str9);
                        rowArray = wTs.Select(string.Format(" number = {0} ", num4));
                        if ((rowArray != null) && (rowArray.Count<DataRow>() > 0))
                        {
                            num5 = double.Parse(rowArray[0]["wt_value"].ToString());
                            num6 = double.Parse(strArray4[num3]);
                            this.dictWT.Add(str9, num5);
                        }
                        num3++;
                    }
                }
            }
            else if (str8.Equals("92590"))
            {
                num2 = (((((((num * (num - 1)) * (num - 2)) * (num - 3)) * (num - 4)) * (num - 5)) * (num - 6)) * (num - 7)) / 0x9d80;
                strArray3 = this.numbers.Split(new char[] { ',' });
                strArray4 = str2.Split(new char[] { ',' });
                wTs = CallBLL.cz_wt_8bz_six_bll.GetWTs();
                if ((wTs != null) && (wTs.Rows.Count > 0))
                {
                    num3 = 0;
                    foreach (string str9 in strArray3)
                    {
                        num4 = int.Parse(str9);
                        rowArray = wTs.Select(string.Format(" number = {0} ", num4));
                        if ((rowArray != null) && (rowArray.Count<DataRow>() > 0))
                        {
                            num5 = double.Parse(rowArray[0]["wt_value"].ToString());
                            num6 = double.Parse(strArray4[num3]);
                            this.dictWT.Add(str9, num5);
                        }
                        num3++;
                    }
                }
            }
            else if (str8.Equals("92591"))
            {
                num2 = ((((((((num * (num - 1)) * (num - 2)) * (num - 3)) * (num - 4)) * (num - 5)) * (num - 6)) * (num - 7)) * (num - 8)) / 0x58980;
                strArray3 = this.numbers.Split(new char[] { ',' });
                strArray4 = str2.Split(new char[] { ',' });
                wTs = CallBLL.cz_wt_9bz_six_bll.GetWTs();
                if ((wTs != null) && (wTs.Rows.Count > 0))
                {
                    num3 = 0;
                    foreach (string str9 in strArray3)
                    {
                        num4 = int.Parse(str9);
                        rowArray = wTs.Select(string.Format(" number = {0} ", num4));
                        if ((rowArray != null) && (rowArray.Count<DataRow>() > 0))
                        {
                            num5 = double.Parse(rowArray[0]["wt_value"].ToString());
                            num6 = double.Parse(strArray4[num3]);
                            this.dictWT.Add(str9, num5);
                        }
                        num3++;
                    }
                }
            }
            else if (str8.Equals("92592"))
            {
                num2 = (((((((((num * (num - 1)) * (num - 2)) * (num - 3)) * (num - 4)) * (num - 5)) * (num - 6)) * (num - 7)) * (num - 8)) * (num - 9)) / 0x375f00;
                strArray3 = this.numbers.Split(new char[] { ',' });
                strArray4 = str2.Split(new char[] { ',' });
                wTs = CallBLL.cz_wt_10bz_six_bll.GetWTs();
                if ((wTs != null) && (wTs.Rows.Count > 0))
                {
                    num3 = 0;
                    foreach (string str9 in strArray3)
                    {
                        num4 = int.Parse(str9);
                        rowArray = wTs.Select(string.Format(" number = {0} ", num4));
                        if ((rowArray != null) && (rowArray.Count<DataRow>() > 0))
                        {
                            num5 = double.Parse(rowArray[0]["wt_value"].ToString());
                            num6 = double.Parse(strArray4[num3]);
                            this.dictWT.Add(str9, num5);
                        }
                        num3++;
                    }
                }
            }
            else if (str8.Equals("92566") || str8.Equals("92569"))
            {
                num2 = (num * (num - 1)) / 2;
                if (str8.Equals("92569"))
                {
                    this.str_numbers.Replace("、", "尾、");
                }
            }
            else if (str8.Equals("92567") || str8.Equals("92570"))
            {
                num2 = ((num * (num - 1)) * (num - 2)) / 6;
                if (str8.Equals("92570"))
                {
                    this.str_numbers.Replace("、", "尾、");
                }
            }
            else if (str8.Equals("92568") || str8.Equals("92571"))
            {
                num2 = (((num * (num - 1)) * (num - 2)) * (num - 3)) / 0x18;
                if (str8.Equals("92571"))
                {
                    this.str_numbers.Replace("、", "尾、");
                }
            }
            else if (str8.Equals("92636") || str8.Equals("92637"))
            {
                num2 = ((((num * (num - 1)) * (num - 2)) * (num - 3)) * (num - 4)) / 120;
                if (str8.Equals("92637"))
                {
                    this.str_numbers.Replace("、", "尾、");
                }
            }
            else if (str8.Equals("92565"))
            {
                num2 = 1;
            }
            this.g_cnt = num2;
            this.s_momey = this.g_cnt * int.Parse(this.txtMomey);
            if (((str8.Equals("92566") || str8.Equals("92567")) || str8.Equals("92568")) || str8.Equals("92636"))
            {
                strArray3 = this.numbers.Split(new char[] { ',' });
                strArray4 = str2.Split(new char[] { ',' });
                string[] strArray5 = base.qq("zodiac").Split(new char[] { ',' });
                wTs = null;
                wTs = CallBLL.cz_wt_sxlwsl_six_bll.GetWTs(str8);
                if ((wTs != null) && (wTs.Rows.Count > 0))
                {
                    num3 = 0;
                    foreach (string str9 in strArray3)
                    {
                        num4 = int.Parse(str9);
                        rowArray = wTs.Select(string.Format(" number = {0} ", num4));
                        if ((rowArray != null) && (rowArray.Count<DataRow>() > 0))
                        {
                            num5 = double.Parse(rowArray[0]["wt_value"].ToString());
                            num6 = double.Parse(strArray4[num3]);
                            this.dictWT_SX.Add(strArray5[num3], num5);
                        }
                        num3++;
                    }
                }
            }
            else if (((str8.Equals("92569") || str8.Equals("92570")) || str8.Equals("92571")) || str8.Equals("92637"))
            {
                strArray3 = this.numbers.Split(new char[] { ',' });
                strArray4 = str2.Split(new char[] { ',' });
                wTs = null;
                wTs = CallBLL.cz_wt_sxlwsl_six_bll.GetWTs(str8);
                if ((wTs != null) && (wTs.Rows.Count > 0))
                {
                    num3 = 0;
                    foreach (string str9 in strArray3)
                    {
                        num4 = int.Parse(str9);
                        rowArray = wTs.Select(string.Format(" number = {0} ", num4));
                        if ((rowArray != null) && (rowArray.Count<DataRow>() > 0))
                        {
                            num5 = double.Parse(rowArray[0]["wt_value"].ToString());
                            num6 = double.Parse(strArray4[num3]);
                            this.dictWT_WS.Add(str9, num5);
                        }
                        num3++;
                    }
                }
            }
        }
        string str10 = "";
        string str11 = "";
        cz_phase_six currentPhase = CallBLL.cz_phase_six_bll.GetCurrentPhase();
        if (currentPhase != null)
        {
            if (currentPhase.get_is_closed() == 1)
            {
                this.EndTipInfo("停止下注");
                return;
            }
            str10 = currentPhase.get_phase().ToString();
            this.jiangqi_str = str10;
            str11 = currentPhase.get_p_id().ToString();
        }
        else
        {
            this.EndTipInfo("停止下注");
            return;
        }
        DataTable playIDByOddsIds = CallBLL.cz_odds_six_bll.GetPlayIDByOddsIds(str3);
        DateTime now = DateTime.Now;
        DateTime time2 = Convert.ToDateTime(currentPhase.get_stop_date());
        DateTime time3 = Convert.ToDateTime(currentPhase.get_sn_stop_date());
        if (((((playIDByOddsIds.Rows[0]["play_id"].ToString().Trim() == "91001") || (playIDByOddsIds.Rows[0]["play_id"].ToString().Trim() == "91002")) || ((playIDByOddsIds.Rows[0]["play_id"].ToString().Trim() == "91004") || (playIDByOddsIds.Rows[0]["play_id"].ToString().Trim() == "91005"))) || (playIDByOddsIds.Rows[0]["play_id"].ToString().Trim() == "91007")) || (playIDByOddsIds.Rows[0]["play_id"].ToString().Trim() == "91038"))
        {
            if (time3 < now)
            {
                this.EndTipInfo("停止下注");
                return;
            }
        }
        else if (time2 < now)
        {
            this.EndTipInfo("停止下注");
            return;
        }
        DataSet playOddsByID = CallBLL.cz_odds_six_bll.GetPlayOddsByID(str3);
        this.plDT = playOddsByID.Tables[0];
        if (this.plDT.Rows.Count == 0)
        {
            this.EndTipInfo("賠率ID設置错误!");
        }
        else
        {
            string[] strArray6 = str3.Split(new char[] { ',' });
            for (int i = 0; i < strArray6.Length; i++)
            {
                bool flag = false;
                for (int j = 0; j < this.plDT.Rows.Count; j++)
                {
                    if (strArray6[i].Equals(this.plDT.Rows[j]["odds_id"].ToString()))
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    this.EndTipInfo("赔率ID错误!");
                    return;
                }
            }
            this.uModel = base.GetUserModelInfo;
            string str12 = this.Session["user_name"].ToString();
            this.pk_kind = this.uModel.get_six_kind();
        }
    }
}

