using LotterySystem.Common;
using LotterySystem.DBUtility;
using LotterySystem.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using User.Web.WebBase;

public class m_L_Confirm_Jeu_six : MemberPageBase_Mobile
{
    private static string comb(int[] a, int n, int m, int M)
    {
        string str = "";
        for (int i = n; i >= m; i--)
        {
            a[m - 1] = i - 1;
            if (m > 1)
            {
                str = str + comb(a, i - 1, m - 1, M);
            }
            else
            {
                for (int j = M - 1; j >= 0; j--)
                {
                    str = str + a[j] + ",";
                }
            }
        }
        return str;
    }

    private static double get_ball_min_pl(DataTable myDT, string nos, double pl)
    {
        if (myDT.Rows.Count > 0)
        {
            List<double> list = new List<double>();
            int[] source = Array.ConvertAll<string, int>(nos.Split(new char[] { ',' }), s => int.Parse(s));
            foreach (DataRow row in myDT.Rows)
            {
                if (source.Contains<int>(int.Parse(row["number"].ToString().Trim())))
                {
                    list.Add(double.Parse(row["wt_value"].ToString()));
                }
            }
            if (list.Count > 0)
            {
                double num = ((IEnumerable<double>) list).Min();
                return (pl + Convert.ToDouble(num));
            }
            return pl;
        }
        return pl;
    }

    private static string get_ball_min_pl(DataTable myDT, string nos, string pl)
    {
        if (myDT.Rows.Count > 0)
        {
            var selector = null;
            Dictionary<string, double> ddDict = new Dictionary<string, double>();
            int[] source = Array.ConvertAll<string, int>(nos.Split(new char[] { ',' }), s => int.Parse(s));
            foreach (DataRow row in myDT.Rows)
            {
                if (source.Contains<int>(int.Parse(row["number"].ToString().Trim())))
                {
                    ddDict.Add(row["number"].ToString().Trim(), double.Parse(row["wt_value"].ToString()));
                }
            }
            if (ddDict.Count > 0)
            {
                if (selector == null)
                {
                    selector = x => new { x = x, y = ddDict[x] };
                }
                string str = (from x in ddDict.Keys.Select(selector)
                    orderby x.y
                    select x).First().x;
                double num = ddDict[str.ToString()];
                if (num == 0.0)
                {
                    return "";
                }
                string str2 = str.ToString();
                if (str2.Length == 1)
                {
                    str2 = "0" + str2;
                }
                if (pl.IndexOf(',') > 0)
                {
                    string[] strArray2 = pl.Split(new char[] { ',' });
                    string str3 = strArray2[0];
                    string str4 = strArray2[1];
                    string str5 = string.Format("{0}", Convert.ToDouble(str3) + num);
                    string str6 = string.Format("{0}", Convert.ToDouble(str4) + num);
                    return (str2 + "|" + str5 + "," + str6);
                }
                double num2 = Convert.ToDouble(pl) + num;
                return (str2 + "|" + num2.ToString());
            }
            return "err";
        }
        return "err";
    }

    private static string get_ball_pl(string playid, double pl, string nos)
    {
        List<string> list = new List<string>();
        DataTable table = CallBLL.cz_wt_kl10_bll.GetWtByPlayID(int.Parse(playid), nos).Tables[0];
        string[] strArray = nos.Split(new char[] { ',' });
        foreach (string str in strArray)
        {
            foreach (DataRow row in table.Rows)
            {
                if (int.Parse(row["number"].ToString().Trim()) == int.Parse(str.Trim()))
                {
                    string s = row["wt_value"].ToString();
                    if (double.Parse(s) != 0.0)
                    {
                        list.Add(row["number"].ToString().Trim() + "~" + ((pl + double.Parse(s))).ToString());
                        break;
                    }
                }
            }
        }
        return string.Join("|", list.ToArray());
    }

    private static string get_sxws_min_pl(DataTable myDT, string nos, string pl, string pl_ID)
    {
        if (myDT.Rows.Count > 0)
        {
            var selector = null;
            Dictionary<string, double> ddDict = new Dictionary<string, double>();
            int[] source = Array.ConvertAll<string, int>(nos.Split(new char[] { ',' }), s => int.Parse(s));
            foreach (DataRow row in myDT.Rows)
            {
                int num = int.Parse(row["number"].ToString().Trim());
                string str = row["odds_id"].ToString().Trim();
                if (source.Contains<int>(num) && (str == pl_ID.Trim()))
                {
                    ddDict.Add(num.ToString(), double.Parse(row["wt_value"].ToString()));
                }
            }
            if (ddDict.Count > 0)
            {
                if (selector == null)
                {
                    selector = x => new { x = x, y = ddDict[x] };
                }
                string str2 = (from x in ddDict.Keys.Select(selector)
                    orderby x.y
                    select x).First().x;
                double num2 = ddDict[str2.ToString()];
                if (num2 == 0.0)
                {
                    return "";
                }
                string str3 = str2.ToString();
                if (str3.Length == 1)
                {
                    str3 = "0" + str3;
                }
                if (pl.IndexOf(',') > 0)
                {
                    string str4 = pl.Split(new char[] { ',' })[0];
                    string str5 = string.Format("{0}", Convert.ToDouble(str4) + num2);
                    return (str3 + "|" + str5);
                }
                return "err";
            }
            return "err";
        }
        return "err";
    }

    private static string getfz(string xz_str, int xz)
    {
        int[] a = new int[10];
        string[] strArray = xz_str.Split(new char[] { ',' });
        string str = "";
        string[] strArray2 = comb(a, strArray.Length, xz, xz).Split(new char[] { ',' });
        int num = 0;
        foreach (string str3 in strArray2)
        {
            num++;
            if (str3.Trim() != "")
            {
                str = str + strArray[int.Parse(str3)];
                if (num == xz)
                {
                    num = 0;
                    str = str + "~";
                }
                else
                {
                    str = str + ",";
                }
            }
        }
        return str.Substring(0, str.Length - 1);
    }

    private static string getlongnum(string ss_str)
    {
        string[] strArray = ss_str.Split(new char[] { ',' });
        string str = "";
        foreach (string str2 in strArray)
        {
            str = str + getnum(str2) + ",";
        }
        return str.Substring(0, str.Length - 1);
    }

    private static string getnum(string ss_num)
    {
        int num = 0;
        num = int.Parse(ss_num);
        if (num < 10)
        {
            return ("0" + num.ToString());
        }
        return num.ToString();
    }

    private static int getzs(string[] a, string[] b)
    {
        int num = 0;
        foreach (string str in a)
        {
            foreach (string str2 in b)
            {
                if (str.Trim() == str2.Trim())
                {
                    num++;
                }
            }
        }
        return ((a.Length * b.Length) - num);
    }

    private static string getzsstr(string a, string b)
    {
        string str = "";
        string[] strArray = a.Split(new char[] { ',' });
        string[] strArray2 = b.Split(new char[] { ',' });
        foreach (string str2 in strArray)
        {
            foreach (string str3 in strArray2)
            {
                if (str2.Trim() != str3.Trim())
                {
                    string str5 = str;
                    str = str5 + str2.Trim() + "," + str3.Trim() + "~";
                }
            }
        }
        return str.Substring(0, str.Length - 1);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.Response.Expires = 0;
        if (!base.IsUserLoginByMobileForAjax())
        {
            this.Session.Abandon();
            base.Response.Write("<script>top.location.href='/m'</script>");
            base.Response.End();
        }
        int num = 100;
        if (!base.IsLotteryExistByPhone(num.ToString()))
        {
            this.Session.Abandon();
            base.Response.Write("<script>top.location.href='/m'</script>");
            base.Response.End();
        }
        string strResult = "";
        this.put_money(ref strResult);
    }

    public void put_money(ref string strResult)
    {
        double num3;
        DateTime time;
        DateTime time2;
        string[] strArray7;
        string[] strArray8;
        string str48;
        string[] strArray9;
        string str49;
        string[] strArray10;
        string[] strArray11;
        int num27;
        DataTable table7;
        int num28;
        string str56;
        string str57;
        string str58;
        DataRow[] rowArray2;
        string str67;
        double playDrawbackValue;
        cz_bet_six _six2;
        int num35;
        cz_system_set_six systemSet;
        double num43;
        double num44;
        double num45;
        double num46;
        double num47;
        double num48;
        cz_system_log _log;
        string str92;
        cz_jp_odds _odds;
        cz_system_log _log2;
        string str95;
        cz_jp_odds _odds2;
        DataTable fgsWTTable;
        int num56;
        string str110;
        double num58;
        if (this.Session["user_name"] == null)
        {
            base.Response.End();
            return;
        }
        string str = this.Session["user_name"].ToString();
        Dictionary<string, object> dictionary = new Dictionary<string, object>();
        string str2 = "";
        cz_userinfo_session getUserModelInfo = base.GetUserModelInfo;
        if (FileCacheHelper.get_RedisStatOnline().Equals(1))
        {
            base.CheckIsOut(getUserModelInfo.get_u_name());
            base.stat_online_redis(getUserModelInfo.get_u_name(), "hy");
        }
        else if (FileCacheHelper.get_RedisStatOnline().Equals(2))
        {
            base.CheckIsOutStack(getUserModelInfo.get_u_name());
            base.stat_online_redisStack(getUserModelInfo.get_u_name(), "hy");
        }
        else
        {
            MemberPageBase.stat_online_bet(getUserModelInfo.get_u_name(), "hy");
        }
        if (!base.BetCreditLock(str))
        {
            str2 = "系統繁忙，請稍後！";
            str2 = string.Format("<script>alert('{0}');</script>", str2);
            base.Response.Write(str2);
            base.Response.End();
        }
        user_six_rate rate = base.GetUserRate_six(getUserModelInfo.get_zjname());
        double num = 0.0;
        double num2 = 0.0;
        if (rate.get_zcyg().Equals("1"))
        {
            num3 = (((100.0 - double.Parse(rate.get_fgszc())) - double.Parse(rate.get_gdzc())) - double.Parse(rate.get_zdzc())) - double.Parse(rate.get_dlzc());
            num = num3;
            num2 = double.Parse(rate.get_fgszc());
        }
        else
        {
            num3 = (((100.0 - double.Parse(rate.get_zjzc())) - double.Parse(rate.get_gdzc())) - double.Parse(rate.get_zdzc())) - double.Parse(rate.get_dlzc());
            num2 = num3;
            num = double.Parse(rate.get_zjzc());
        }
        int num4 = 1;
        if (getUserModelInfo.get_su_type().ToString().Trim() != "dl")
        {
            num4 = -1;
        }
        DataTable table = null;
        DataSet set = CallBLL.cz_users_bll.AccountIsDisabled(rate.get_fgsname(), rate.get_gdname(), rate.get_zdname(), rate.get_dlname(), getUserModelInfo.get_u_name());
        if (set != null)
        {
            table = set.Tables[0];
            if ((table != null) && (table.Rows.Count > 0))
            {
                if (table.Rows[0]["a_state"].ToString().Trim() == "1")
                {
                    base.DeleteCreditLock(str);
                    str2 = "會員或上級已經被凍結,請與上級聯系！";
                    str2 = string.Format("<script>alert('{0}');</script>", str2);
                    base.Response.Write(str2);
                    base.Response.End();
                }
                else
                {
                    base.DeleteCreditLock(str);
                    str2 = "會員或上級已經被停用,請與上級聯系！";
                    str2 = string.Format("<script>alert('{0}');</script>", str2);
                    base.Response.Write(str2);
                    base.Response.End();
                }
            }
        }
        int num5 = 100;
        string str5 = "";
        string s = "";
        string str7 = "";
        string str8 = LSRequest.qq("phaseid");
        string str9 = base.qq("caizhong");
        string str10 = base.qq("wanfa");
        string str11 = base.qq("game_type");
        string str12 = "SIX_" + str11 + ".aspx?lottery_type=" + str9 + "&player_type=" + str10;
        str5 = LSRequest.qq("oddsid");
        s = LSRequest.qq("uPI_P");
        str7 = LSRequest.qq("uPI_M");
        string str13 = LSRequest.qq("uPI_TM");
        string[] strArray = LSRequest.qq("i_index").Split(new char[] { ',' });
        string str14 = LSRequest.qq("JeuValidate");
        string[] source = str5.Split(new char[] { ',' });
        string[] strArray3 = s.Split(new char[] { ',' });
        string[] strArray4 = str7.Split(new char[] { ',' });
        if (source.Distinct<string>().ToList<string>().Count != source.Length)
        {
            base.DeleteCreditLock(str);
            base.Response.End();
        }
        if (source.Length > FileCacheHelper.get_GetSIXMaxGroup())
        {
            base.DeleteCreditLock(str);
            base.Response.Write(string.Concat(new object[] { "<script>alert('您選擇的號碼超過了最大", FileCacheHelper.get_GetSIXMaxGroup(), "組，請重新選擇號碼!');$('#JeuValidate').val('", base.get_JeuValidate(), "');</script>" }));
            base.Response.End();
        }
        if ((this.Session["JeuValidate"].ToString().Trim() != str14) || (this.Session["JeuValidate"].ToString().Trim() == ""))
        {
            base.DeleteCreditLock(str);
            base.Response.Write("<script>alert('下注規則有誤,請重新下注,謝謝合作!');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
            base.Response.End();
        }
        else
        {
            this.Session["JeuValidate"] = "";
        }
        int index = 0;
    Label_06AA:;
        if (index < str7.Split(new char[] { ',' }).Length)
        {
            if (!base.IsNumber(str7.Split(new char[] { ',' })[index]))
            {
                base.DeleteCreditLock(str);
                base.Response.Write("<script>alert($(\"#v_" + strArray[index] + "\").text()+' 下注金額有誤！');$('#m_" + strArray[index] + "').focus();$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                base.Response.End();
                return;
            }
            index++;
            goto Label_06AA;
        }
        if (getUserModelInfo.get_begin_six().Trim() != "yes")
        {
            getUserModelInfo = base.GetRateByUserObject(1, rate);
        }
        string str15 = "";
        string str16 = "";
        cz_phase_six currentPhase = CallBLL.cz_phase_six_bll.GetCurrentPhase();
        if (currentPhase != null)
        {
            if (currentPhase.get_is_closed() == 1)
            {
                base.DeleteCreditLock(str);
                str2 = string.Format("奖期[{0}]已封盘,停止下注！", currentPhase.get_phase().ToString());
                base.Response.Write("<script>alert('" + str2 + "');window.location=\"" + str12 + "\";</script>");
                base.Response.End();
                return;
            }
            str15 = currentPhase.get_phase().ToString();
            num56 = currentPhase.get_p_id();
            str16 = num56.ToString();
            time = Convert.ToDateTime(currentPhase.get_stop_date());
            time2 = Convert.ToDateTime(currentPhase.get_sn_stop_date());
        }
        else
        {
            base.DeleteCreditLock(str);
            base.Response.Write("<script>alert('該獎期已經截止下注！');window.location=\"" + str12 + "\";</script>");
            base.Response.End();
            return;
        }
        DataTable playIDByOddsIds = CallBLL.cz_odds_six_bll.GetPlayIDByOddsIds(str5);
        DateTime now = DateTime.Now;
        DateTime time4 = Convert.ToDateTime(currentPhase.get_stop_date());
        DateTime time5 = Convert.ToDateTime(currentPhase.get_sn_stop_date());
        if (((((playIDByOddsIds.Rows[0]["play_id"].ToString().Trim() == "91001") || (playIDByOddsIds.Rows[0]["play_id"].ToString().Trim() == "91002")) || ((playIDByOddsIds.Rows[0]["play_id"].ToString().Trim() == "91004") || (playIDByOddsIds.Rows[0]["play_id"].ToString().Trim() == "91005"))) || (playIDByOddsIds.Rows[0]["play_id"].ToString().Trim() == "91007")) || (playIDByOddsIds.Rows[0]["play_id"].ToString().Trim() == "91038"))
        {
            if (time5 < now)
            {
                base.DeleteCreditLock(str);
                base.Response.Write("<script>alert('已經截止下注！');window.location=\"" + str12 + "\";</script>");
                base.Response.End();
                return;
            }
        }
        else if (time4 < now)
        {
            base.DeleteCreditLock(str);
            base.Response.Write("<script>alert('已經截止下注！');window.location=\"" + str12 + "\";</script>");
            base.Response.End();
            return;
        }
        string str17 = "";
        string str18 = "";
        double num8 = 0.0;
        DataTable table3 = CallBLL.cz_users_bll.GetUserRate(str, 1).Tables[0];
        str17 = table3.Rows[0]["six_kind"].ToString().Trim().ToUpper();
        str18 = table3.Rows[0]["su_type"].ToString().Trim();
        num8 = double.Parse(table3.Rows[0]["six_usable_credit"].ToString().Trim());
        getUserModelInfo.set_six_kind(str17.ToUpper());
        string str19 = "0";
        string str20 = "0";
        string str21 = "0";
        string str22 = "0";
        string str23 = "0";
        string str24 = "0";
        double num9 = 0.0;
        double num10 = 0.0;
        double num11 = 0.0;
        double num12 = 0.0;
        double num13 = 0.0;
        double num14 = 0.0;
        string str25 = "";
        string str26 = "";
        string diff = "";
        string str28 = "";
        string str29 = "";
        string str30 = "0";
        string str31 = "0";
        DataTable plDT = CallBLL.cz_odds_six_bll.GetPlayOddsByID(str5).Tables[0];
        if (plDT.Rows.Count == 0)
        {
            base.DeleteCreditLock(str);
            base.Response.Write("<script>alert('賠率ID設置错误!');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
            base.Response.End();
        }
        DataTable table5 = CallBLL.cz_bet_six_bll.GetSinglePhaseByBet(str15, str, num5.ToString(), str5).Tables[0];
        for (int i = 0; i < source.Length; i++)
        {
            bool flag2 = false;
            for (int j = 0; j < plDT.Rows.Count; j++)
            {
                if (source[i].Equals(plDT.Rows[j]["odds_id"].ToString()))
                {
                    flag2 = true;
                    break;
                }
            }
            if (!flag2)
            {
                base.DeleteCreditLock(str);
                base.Response.Write("<script>alert('系统错误!');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                base.Response.End();
                return;
            }
        }
        double num17 = 0.0;
        string str32 = "";
        Dictionary<int, string> dictionary2 = new Dictionary<int, string>();
        int num18 = 0;
        foreach (string str33 in source)
        {
            num9 = 0.0;
            foreach (DataRow row in plDT.Rows)
            {
                if (row["odds_id"].ToString().Trim() == str33.Trim())
                {
                    str32 = row["is_open"].ToString().Trim();
                    str26 = row["current_odds"].ToString().Trim();
                    diff = row[str17 + "_diff"].ToString().Trim();
                    str25 = row["play_id"].ToString().Trim();
                    str28 = row["play_name"].ToString().Trim();
                    str29 = row["put_amount"].ToString().Trim();
                    str30 = row["isLM"].ToString().Trim();
                    str31 = row["isDoubleOdds"].ToString().Trim();
                    num14 = double.Parse(row["allow_min_amount"].ToString().Trim());
                    num11 = double.Parse(row["allow_max_amount"].ToString().Trim());
                    num13 = double.Parse(row["allow_max_put_amount"].ToString().Trim());
                    break;
                }
            }
            if (table5.Rows.Count > 0)
            {
                foreach (DataRow row2 in table5.Rows)
                {
                    if (row2["odds_id"].ToString().Trim() == str33.Trim())
                    {
                        num9 = double.Parse(row2["sumbet"].ToString().Trim());
                        break;
                    }
                }
            }
            DataSet drawback = null;
            if (FileCacheHelper.get_GetSixPutMoneyCache() == "1")
            {
                if (CacheHelper.GetCache("six_drawback_FileCacheKey" + str25 + HttpContext.Current.Session["user_name"].ToString()) == null)
                {
                    drawback = CallBLL.cz_drawback_six_bll.GetDrawback(str25, str);
                }
                else
                {
                    drawback = base.GetUserDrawback_six(rate, getUserModelInfo.get_six_kind(), str25);
                }
            }
            else if (CacheHelper.GetCache("six_drawback_FileCacheKey" + HttpContext.Current.Session["user_name"].ToString()) == null)
            {
                drawback = CallBLL.cz_drawback_six_bll.GetDrawback(str25, str);
            }
            else
            {
                drawback = base.GetUserDrawback_six(rate, getUserModelInfo.get_six_kind());
            }
            DataRow[] rowArray = drawback.Tables[0].Select(string.Format(" play_id={0} and u_name='{1}' ", str25, str));
            num10 = double.Parse(rowArray[0]["single_max_amount"].ToString().Trim());
            double num19 = double.Parse(rowArray[0]["single_min_amount"].ToString().Trim());
            num12 = double.Parse(rowArray[0]["single_phase_amount"].ToString().Trim());
            if (num10 > num11)
            {
                num10 = num11;
            }
            if (num12 > num13)
            {
                num12 = num13;
            }
            if (num19 > num14)
            {
                num14 = num19;
            }
            if (str32 != "1")
            {
                base.DeleteCreditLock(str);
                base.Response.Write("<script>alert('" + str28 + "【" + str29 + "】已經停止投注！！');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                base.Response.End();
                return;
            }
            if (str31.Equals("0"))
            {
                string pl = "";
                base.GetOdds_SIX(str33, str26, diff, ref pl);
                num17 = double.Parse(pl);
                if (!(double.Parse(strArray3[num18]) == double.Parse(num17.ToString())))
                {
                    dictionary2.Add(num18 + 1, num17.ToString());
                }
            }
            if (double.Parse(strArray4[num18]) > num10)
            {
                base.DeleteCreditLock(str);
                base.Response.Write("<script>alert($(\"#v_" + strArray[num18] + "\").text()+' 下注金額超出單注最大金額！');$('#m_" + strArray[num18] + "').focus();$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                base.Response.End();
                return;
            }
            if (double.Parse(strArray4[num18]) > num8)
            {
                base.DeleteCreditLock(str);
                base.Response.Write("<script>alert('下注金額超出可用金額!');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                base.Response.End();
                return;
            }
            if (double.Parse(strArray4[num18]) < num14)
            {
                base.DeleteCreditLock(str);
                base.Response.Write("<script>alert($(\"#v_" + strArray[num18] + "\").text()+' 下注金額低過最低金額！');$('#m_" + strArray[num18] + "').focus();$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                base.Response.End();
                return;
            }
            if (!str30.Equals("1") && ((double.Parse(strArray4[num18]) + num9) > num12))
            {
                base.DeleteCreditLock(str);
                str2 = string.Format("{0}【{1}】下注單期金額超出單期最大金額！", str28, str29);
                base.Response.Write("<script>alert('" + str2 + "');$('#m_" + strArray[num18] + "').focus();$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                base.Response.End();
                return;
            }
            num18++;
        }
        if ((dictionary2.Count > 0) && !str30.Equals("1"))
        {
            base.DeleteCreditLock(str);
            base.Response.Write("<script>alert('賠率有變動,請確認後再提交！');</script>");
            foreach (KeyValuePair<int, string> pair in dictionary2)
            {
                base.Response.Write(string.Concat(new object[] { "<script>$('#v_", pair.Key, " ~ span.hong').text('", pair.Value, "');$('#p_", pair.Key, "').val('", pair.Value, "');</script>" }));
            }
            base.Response.Write("<script>$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
            base.Response.End();
            return;
        }
        double num20 = 0.0;
        int num21 = 0;
        foreach (string str35 in source)
        {
            num20 += double.Parse(strArray4[num21].ToString().Trim());
            num21++;
        }
        if (num8 < num20)
        {
            base.DeleteCreditLock(str);
            base.Response.Write("<script>alert('可用餘額不足！');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
            base.Response.End();
            return;
        }
        DataSet ds = null;
        int num22 = 0;
        string str36 = "";
        string str38 = "";
        if (((("92285,92286,92287,92288,92289,92575".IndexOf(str5) <= -1) && ("92638,92639,92640,92641,92642,92643".IndexOf(str5) <= -1)) && ("92565,92566,92567,92568,92569,92570,92571,92636,92637".IndexOf(str5) <= -1)) && ("92572,92588,92589,92590,92591,92592".IndexOf(str5) <= -1))
        {
            bool flag7 = false;
            List<string> successBetList = new List<string>();
            num18 = 0;
            foreach (string str33 in source)
            {
                table7 = CallBLL.cz_odds_six_bll.GetOddsByID(str33).Tables[0];
                num28 = int.Parse(table7.Rows[0]["play_id"].ToString());
                str56 = table7.Rows[0]["o_play_name"].ToString();
                str57 = table7.Rows[0]["put_amount"].ToString();
                str58 = table7.Rows[0]["ratio"].ToString();
                if (FileCacheHelper.get_GetSixPutMoneyCache() == "1")
                {
                    ds = base.GetUserDrawback_six(rate, getUserModelInfo.get_six_kind(), num28.ToString());
                }
                else
                {
                    ds = base.GetUserDrawback_six(rate, getUserModelInfo.get_six_kind());
                }
                if (ds == null)
                {
                    base.DeleteCreditLock(str);
                    base.Response.Write("<script>alert('系統錯誤,請重試！');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                    base.Response.End();
                    return;
                }
                DataTable table8 = ds.Tables[0];
                rowArray2 = table8.Select(string.Format(" play_id={0} ", num28));
                foreach (DataRow row in rowArray2)
                {
                    str110 = row["u_type"].ToString().Trim();
                    if (str110 != null)
                    {
                        if (!(str110 == "zj"))
                        {
                            if (str110 == "fgs")
                            {
                                goto Label_6C64;
                            }
                            if (str110 == "gd")
                            {
                                goto Label_6C88;
                            }
                            if (str110 == "zd")
                            {
                                goto Label_6CAC;
                            }
                            if (str110 == "dl")
                            {
                                goto Label_6CD0;
                            }
                            if (str110 == "hy")
                            {
                                goto Label_6CF4;
                            }
                        }
                        else
                        {
                            str19 = row[str17 + "_drawback"].ToString().Trim();
                        }
                    }
                    continue;
                Label_6C64:
                    str20 = row[str17 + "_drawback"].ToString().Trim();
                    continue;
                Label_6C88:
                    str21 = row[str17 + "_drawback"].ToString().Trim();
                    continue;
                Label_6CAC:
                    str22 = row[str17 + "_drawback"].ToString().Trim();
                    continue;
                Label_6CD0:
                    str23 = row[str17 + "_drawback"].ToString().Trim();
                    continue;
                Label_6CF4:
                    switch (str18)
                    {
                        case "dl":
                            str24 = row[str17 + "_drawback"].ToString().Trim();
                            break;

                        case "zd":
                            str23 = row[str17 + "_drawback"].ToString().Trim();
                            break;

                        case "gd":
                            str22 = row[str17 + "_drawback"].ToString().Trim();
                            break;

                        case "fgs":
                            str21 = row[str17 + "_drawback"].ToString().Trim();
                            break;
                    }
                }
                DateTime time6 = DateTime.Now;
                if (((((playIDByOddsIds.Rows[0]["play_id"].ToString().Trim() == "91001") || (playIDByOddsIds.Rows[0]["play_id"].ToString().Trim() == "91002")) || ((playIDByOddsIds.Rows[0]["play_id"].ToString().Trim() == "91004") || (playIDByOddsIds.Rows[0]["play_id"].ToString().Trim() == "91005"))) || (playIDByOddsIds.Rows[0]["play_id"].ToString().Trim() == "91007")) || (playIDByOddsIds.Rows[0]["play_id"].ToString().Trim() == "91038"))
                {
                    if (time6 >= time2.AddSeconds(-2.0))
                    {
                        flag7 = true;
                        break;
                    }
                }
                else if (time6 >= time.AddSeconds(-2.0))
                {
                    flag7 = true;
                    break;
                }
                num58 = double.Parse(table7.Rows[0]["current_odds"].ToString()) + Convert.ToDouble(table7.Rows[0][str17 + "_diff"].ToString().Trim());
                str38 = num58.ToString();
                str67 = str38;
                base.GetOdds_SIX(str33, table7.Rows[0]["current_odds"].ToString(), table7.Rows[0][str17 + "_diff"].ToString().Trim(), ref str67);
                playDrawbackValue = base.GetPlayDrawbackValue(str20, str58);
                if ((playDrawbackValue != 0.0) && (double.Parse(str38) > playDrawbackValue))
                {
                    base.DeleteCreditLock(str);
                    base.Response.Write("<script>alert('賠率錯誤！');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                    base.Response.End();
                    return;
                }
                _six2 = new cz_bet_six();
                _six2.set_order_num(Utils.GetOrderNumber());
                _six2.set_checkcode(str14);
                _six2.set_u_name(getUserModelInfo.get_u_name());
                _six2.set_u_nicker(getUserModelInfo.get_u_nicker());
                _six2.set_phase_id(new int?(int.Parse(str16)));
                _six2.set_phase(str15);
                _six2.set_bet_time(new DateTime?(DateTime.Now));
                _six2.set_odds_id(new int?(int.Parse(str33)));
                _six2.set_category(table7.Rows[0]["category"].ToString());
                _six2.set_play_id(new int?(int.Parse(table7.Rows[0]["play_id"].ToString())));
                _six2.set_play_name(table7.Rows[0]["o_play_name"].ToString());
                _six2.set_bet_val(table7.Rows[0]["put_amount"].ToString());
                _six2.set_odds(str67);
                _six2.set_amount(new decimal?(decimal.Parse(strArray4[num18])));
                _six2.set_profit(0);
                _six2.set_hy_drawback(new decimal?(decimal.Parse(str24)));
                _six2.set_dl_drawback(new decimal?(decimal.Parse(str23)));
                _six2.set_zd_drawback(new decimal?(decimal.Parse(str22)));
                _six2.set_gd_drawback(new decimal?(decimal.Parse(str21)));
                _six2.set_fgs_drawback(new decimal?(decimal.Parse(str20)));
                _six2.set_zj_drawback(new decimal?(decimal.Parse(str19)));
                _six2.set_dl_rate((float) int.Parse(rate.get_dlzc()));
                _six2.set_zd_rate((float) int.Parse(rate.get_zdzc()));
                _six2.set_gd_rate((float) int.Parse(rate.get_gdzc()));
                _six2.set_fgs_rate((float) int.Parse(num2.ToString()));
                _six2.set_zj_rate((float) int.Parse(num.ToString()));
                _six2.set_is_payment(0);
                _six2.set_dl_name(rate.get_dlname());
                _six2.set_zd_name(rate.get_zdname());
                _six2.set_gd_name(rate.get_gdname());
                _six2.set_fgs_name(rate.get_fgsname());
                _six2.set_m_type(new int?(num4));
                _six2.set_kind(str17);
                _six2.set_ip(LSRequest.GetIP());
                _six2.set_lottery_type(new int?(num5));
                _six2.set_lottery_name(base.GetGameNameByID(_six2.get_lottery_type().ToString()));
                _six2.set_isLM(0);
                _six2.set_ordervalidcode(Utils.GetOrderValidCode(_six2.get_u_name(), _six2.get_order_num(), _six2.get_bet_val(), _six2.get_odds(), _six2.get_kind(), Convert.ToInt32(_six2.get_phase_id()), Convert.ToInt32(_six2.get_odds_id()), Convert.ToDouble(_six2.get_amount())));
                _six2.set_odds_zj(str38);
                _six2.set_isPhone(1);
                num35 = 0;
                if (!(CallBLL.cz_bet_six_bll.AddBet(_six2, decimal.Parse(strArray4[num18]), str, ref num35) && (num35 > 0)))
                {
                    base.DeleteCreditLock(str);
                    base.Response.Write("<script>alert('系統写入錯誤,請重試！');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                    base.Response.End();
                    return;
                }
                successBetList.Add(string.Concat(new object[] { _six2.get_odds_id(), ",", _six2.get_order_num(), ",", _six2.get_play_name(), ",", _six2.get_bet_val(), ",", _six2.get_odds(), ",", _six2.get_amount() }));
                num43 = (double.Parse(strArray4[num18]) * num) / 100.0;
                systemSet = CallBLL.cz_system_set_six_bll.GetSystemSet(100);
                num44 = 0.0;
                num45 = 0.0;
                double num53 = 0.0;
                if (((int.Parse(str33) > 0x16791) && (int.Parse(str33) <= 0x167c2)) && systemSet.get_is_tmab().Equals(1))
                {
                    num56 = int.Parse(str33) - 0x31;
                    DataSet oddsByID = CallBLL.cz_odds_six_bll.GetOddsByID(num56.ToString());
                    CallBLL.cz_odds_six_bll.UpdateGrandTotal(Convert.ToDecimal(num43), int.Parse(str33) - 0x31);
                    num44 = double.Parse(oddsByID.Tables[0].Rows[0]["grand_total"].ToString()) + num43;
                    num45 = double.Parse(oddsByID.Tables[0].Rows[0]["downbase"].ToString());
                    num53 = double.Parse(oddsByID.Tables[0].Rows[0]["down_odds_rate"].ToString());
                }
                else
                {
                    CallBLL.cz_odds_six_bll.UpdateGrandTotal(Convert.ToDecimal(num43), int.Parse(str33));
                    num44 = double.Parse(table7.Rows[0]["grand_total"].ToString()) + num43;
                    num45 = double.Parse(table7.Rows[0]["downbase"].ToString());
                    num53 = double.Parse(table7.Rows[0]["down_odds_rate"].ToString());
                }
                num46 = Math.Floor((double) (num44 / num45));
                if (num46 >= 1.0)
                {
                    int num50;
                    num47 = num53 * num46;
                    num48 = num44 - (num45 * num46);
                    if (((int.Parse(str33) > 0x16791) && (int.Parse(str33) <= 0x167c2)) && systemSet.get_is_tmab().Equals(1))
                    {
                        num50 = CallBLL.cz_odds_six_bll.UpdateGrandTotalCurrentOdds(num47.ToString(), num48.ToString(), str33, true);
                    }
                    else
                    {
                        num50 = CallBLL.cz_odds_six_bll.UpdateGrandTotalCurrentOdds(num47.ToString(), num48.ToString(), str33);
                    }
                    _log = new cz_system_log();
                    _log.set_user_name("系統");
                    _log.set_children_name("");
                    _log.set_category(table7.Rows[0]["category"].ToString());
                    _log.set_play_name(str56);
                    _log.set_put_amount(str57);
                    num56 = 100;
                    _log.set_l_name(base.GetGameNameByID(num56.ToString()));
                    _log.set_l_phase(str15);
                    _log.set_action("降賠率");
                    _log.set_odds_id(int.Parse(str33));
                    str92 = table7.Rows[0]["current_odds"].ToString();
                    _log.set_old_val(str92);
                    num58 = double.Parse(str92) - num47;
                    _log.set_new_val(num58.ToString());
                    _log.set_ip(LSRequest.GetIP());
                    _log.set_add_time(DateTime.Now);
                    _log.set_note("系統自動降賠");
                    _log.set_type_id(Convert.ToInt32((LSEnums.LogTypeID) 0));
                    _log.set_lottery_type(100);
                    CallBLL.cz_system_log_bll.Add(_log);
                    _odds = new cz_jp_odds();
                    _odds.set_add_time(DateTime.Now);
                    _odds.set_odds_id(int.Parse(str33));
                    _odds.set_phase_id(int.Parse(str16));
                    _odds.set_play_name(str56);
                    _odds.set_put_amount(str57);
                    _odds.set_odds(num47.ToString());
                    _odds.set_lottery_type(100);
                    _odds.set_phase(str15);
                    _odds.set_old_odds(str92);
                    num58 = double.Parse(str92) - num47;
                    _odds.set_new_odds(num58.ToString());
                    CallBLL.cz_jp_odds_bll.Add(_odds);
                    if (int.Parse(str33) <= 0x16791)
                    {
                        num56 = Convert.ToInt32(str33) + 0x31;
                        int num54 = CallBLL.cz_odds_six_bll.UpdateCurrentOdds(num47.ToString(), num56.ToString());
                        _log2 = new cz_system_log();
                        _log2.set_user_name("系統");
                        _log2.set_children_name("");
                        _log2.set_category(table7.Rows[0]["category"].ToString());
                        _log2.set_play_name(str56);
                        _log2.set_put_amount(str57);
                        num56 = 100;
                        _log2.set_l_name(base.GetGameNameByID(num56.ToString()));
                        _log2.set_l_phase(str15);
                        _log2.set_action("降賠率");
                        _log2.set_odds_id(int.Parse(str33) + 0x31);
                        str95 = table7.Rows[0]["current_odds"].ToString();
                        _log2.set_old_val(str95);
                        num58 = double.Parse(str95) - num47;
                        _log2.set_new_val(num58.ToString());
                        _log2.set_ip(LSRequest.GetIP());
                        _log2.set_add_time(DateTime.Now);
                        _log2.set_note("系統自動降賠");
                        _log2.set_type_id(Convert.ToInt32((LSEnums.LogTypeID) 0));
                        _log2.set_lottery_type(100);
                        CallBLL.cz_system_log_bll.Add(_log2);
                        _odds2 = new cz_jp_odds();
                        _odds2.set_add_time(DateTime.Now);
                        _odds2.set_odds_id(int.Parse(str33) + 0x31);
                        _odds2.set_phase_id(int.Parse(str16));
                        _odds2.set_play_name(str56);
                        _odds2.set_put_amount(str57);
                        _odds2.set_odds(num47.ToString());
                        _odds2.set_lottery_type(100);
                        _odds2.set_phase(str15);
                        _odds2.set_old_odds(str95);
                        num58 = double.Parse(str95) - num47;
                        _odds2.set_new_odds(num58.ToString());
                        CallBLL.cz_jp_odds_bll.Add(_odds2);
                    }
                    if ((int.Parse(str33) > 0x16791) && (int.Parse(str33) <= 0x167c2))
                    {
                        num56 = Convert.ToInt32(str33) - 0x31;
                        int num55 = CallBLL.cz_odds_six_bll.UpdateCurrentOdds(num47.ToString(), num56.ToString());
                        _log2 = new cz_system_log();
                        _log2.set_user_name("系統");
                        _log2.set_children_name("");
                        _log2.set_category(table7.Rows[0]["category"].ToString());
                        _log2.set_play_name(str56);
                        _log2.set_put_amount(str57);
                        num56 = 100;
                        _log2.set_l_name(base.GetGameNameByID(num56.ToString()));
                        _log2.set_l_phase(str15);
                        _log2.set_action("降賠率");
                        _log2.set_odds_id(int.Parse(str33) - 0x31);
                        str95 = table7.Rows[0]["current_odds"].ToString();
                        _log2.set_old_val(str95);
                        num58 = double.Parse(str95) - num47;
                        _log2.set_new_val(num58.ToString());
                        _log2.set_ip(LSRequest.GetIP());
                        _log2.set_add_time(DateTime.Now);
                        _log2.set_note("系統自動降賠");
                        _log2.set_type_id(Convert.ToInt32((LSEnums.LogTypeID) 0));
                        _log2.set_lottery_type(100);
                        CallBLL.cz_system_log_bll.Add(_log2);
                        _odds2 = new cz_jp_odds();
                        _odds2.set_add_time(DateTime.Now);
                        _odds2.set_odds_id(int.Parse(str33) - 0x31);
                        _odds2.set_phase_id(int.Parse(str16));
                        _odds2.set_play_name(str56);
                        _odds2.set_put_amount(str57);
                        _odds2.set_odds(num47.ToString());
                        _odds2.set_lottery_type(100);
                        _odds2.set_phase(str15);
                        _odds2.set_old_odds(str95);
                        num58 = double.Parse(str95) - num47;
                        _odds2.set_new_odds(num58.ToString());
                        CallBLL.cz_jp_odds_bll.Add(_odds2);
                    }
                }
                fgsWTTable = null;
                if (getUserModelInfo.get_six_op_odds().Equals(1))
                {
                    fgsWTTable = base.GetFgsWTTable(100);
                }
                num56 = 100;
                base.Add_Bet_Lock(num56.ToString(), rate.get_dlname(), str33);
                CallBLL.cz_autosale_six_bll.DLAutoSale(_six2.get_order_num(), str14, getUserModelInfo.get_u_nicker(), str17, table7.Rows[0]["play_id"].ToString(), str33.Trim(), str19, str20, str21, str22, str23, str24, str16, str15, _six2.get_ip(), num5, _six2.get_lottery_name(), getUserModelInfo, rate, fgsWTTable);
                num56 = 100;
                base.Un_Bet_Lock(num56.ToString(), rate.get_dlname(), str33);
                if (getUserModelInfo.get_six_op_odds().Equals(1))
                {
                    fgsWTTable = base.GetFgsWTTable(100);
                }
                num56 = 100;
                base.Add_Bet_Lock(num56.ToString(), rate.get_zdname(), str33);
                CallBLL.cz_autosale_six_bll.ZDAutoSale(_six2.get_order_num(), str14, getUserModelInfo.get_u_nicker(), str17, table7.Rows[0]["play_id"].ToString(), str33.Trim(), str19, str20, str21, str22, str23, str24, str16, str15, _six2.get_ip(), num5, _six2.get_lottery_name(), getUserModelInfo, rate, fgsWTTable);
                num56 = 100;
                base.Un_Bet_Lock(num56.ToString(), rate.get_zdname(), str33);
                if (getUserModelInfo.get_six_op_odds().Equals(1))
                {
                    fgsWTTable = base.GetFgsWTTable(100);
                }
                num56 = 100;
                base.Add_Bet_Lock(num56.ToString(), rate.get_gdname(), str33);
                CallBLL.cz_autosale_six_bll.GDAutoSale(_six2.get_order_num(), str14, getUserModelInfo.get_u_nicker(), str17, table7.Rows[0]["play_id"].ToString(), str33.Trim(), str19, str20, str21, str22, str23, str24, str16, str15, _six2.get_ip(), num5, _six2.get_lottery_name(), getUserModelInfo, rate, fgsWTTable);
                num56 = 100;
                base.Un_Bet_Lock(num56.ToString(), rate.get_gdname(), str33);
                if (getUserModelInfo.get_six_op_odds().Equals(1))
                {
                    fgsWTTable = base.GetFgsWTTable(100);
                }
                num56 = 100;
                base.Add_Bet_Lock(num56.ToString(), rate.get_fgsname(), str33);
                CallBLL.cz_autosale_six_bll.FGSAutoSale(_six2.get_order_num(), str14, getUserModelInfo.get_u_nicker(), str17, table7.Rows[0]["play_id"].ToString(), str33.Trim(), str19, str20, str21, str22, str23, str24, str16, str15, _six2.get_ip(), num5, _six2.get_lottery_name(), getUserModelInfo, rate, fgsWTTable);
                num56 = 100;
                base.Un_Bet_Lock(num56.ToString(), rate.get_fgsname(), str33);
                num18++;
                if (FileCacheHelper.get_GetSixPutMoneyCache() == "1")
                {
                    base.SetUserDrawback_six(ds, num28.ToString());
                }
            }
            base.SetUserRate_six(rate);
            if (FileCacheHelper.get_GetSixPutMoneyCache() != "1")
            {
                base.SetUserDrawback_six(ds);
            }
            getUserModelInfo.set_begin_six("yes");
            base.UserPutBetByPhone(plDT, successBetList, source, strArray3, strArray4);
            base.DeleteCreditLock(str);
            base.Response.Write(string.Format("<script>window.location=\"tj_ok.aspx?lottery_type={0}&player_type={1}&game_type={2}\";</script>", str9, str10, str11));
            base.Response.End();
            return;
        }
        if (("92638,92639,92640,92641,92642,92643".IndexOf(str5) > -1) && (base.IsShowLM_B() == 0))
        {
            base.DeleteCreditLock(str);
            base.Response.Write("<script>alert('下注錯誤！');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
            base.Response.End();
        }
        string str40 = LSRequest.qq("lmtype");
        string str41 = LSRequest.qq("numbers");
        string str42 = LSRequest.qq("uPI_WT");
        string str43 = px(getlongnum(str41.Replace("|", ",")));
        int length = str43.Split(new char[] { ',' }).Length;
        string[] strArray5 = str43.Split(new char[] { ',' });
        int count = strArray5.Distinct<string>().ToList<string>().Count;
        for (index = 0; index < strArray5.Length; index++)
        {
            if (((str5.Equals("92569") || str5.Equals("92570")) || str5.Equals("92571")) || str5.Equals("92637"))
            {
                num56 = int.Parse(strArray5[index]);
                if (num56.Equals(0))
                {
                    strArray5[index] = "10";
                }
            }
        }
        if (count != strArray5.Length)
        {
            base.DeleteCreditLock(str);
            base.Response.Write("<script>alert('下注不能有重複碼！');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
            base.Response.End();
            return;
        }
        if (strArray5.Length != length)
        {
            base.DeleteCreditLock(str);
            base.Response.Write("<script>alert('下注不能有重複碼！');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
            base.Response.End();
            return;
        }
        if (str5.Equals("92565"))
        {
            if (strArray5.Length != 6)
            {
                base.DeleteCreditLock(str);
                base.Response.Write("<script>alert('“六肖”最少必須選擇六個生肖！');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                base.Response.End();
                return;
            }
            if (strArray5.All<string>(p => (int.Parse(p) % 2) == 0))
            {
                base.DeleteCreditLock(str);
                base.Response.Write("<script>alert('“六肖”禁止全單/雙路投注，該注單請投在“特碼單雙”項！');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                base.Response.End();
                return;
            }
            if (strArray5.All<string>(p => (int.Parse(p) % 2) != 0))
            {
                base.DeleteCreditLock(str);
                base.Response.Write("<script>alert('“六肖”禁止全單/雙路投注，該注單請投在“特碼單雙”項！');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                base.Response.End();
                return;
            }
            num22 = 1;
            str36 = px(str43);
        }
        foreach (string str44 in strArray5)
        {
            if (((str5.Equals("92565") || str5.Equals("92566")) || (str5.Equals("92567") || str5.Equals("92568"))) || str5.Equals("92636"))
            {
                if ((int.Parse(str44) > 12) || (int.Parse(str44) < 1))
                {
                    base.DeleteCreditLock(str);
                    base.Response.Write("<script>alert('下注號碼錯誤！');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                    base.Response.End();
                    return;
                }
            }
            else if (((str5.Equals("92569") || str5.Equals("92570")) || str5.Equals("92571")) || str5.Equals("92637"))
            {
                if ((int.Parse(str44) > 10) || (int.Parse(str44) < 1))
                {
                    base.DeleteCreditLock(str);
                    base.Response.Write("<script>alert('下注號碼錯誤！');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                    base.Response.End();
                    return;
                }
            }
            else if ((int.Parse(str44) > 0x31) || (int.Parse(str44) < 1))
            {
                base.DeleteCreditLock(str);
                base.Response.Write("<script>alert('下注號碼錯誤！');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                base.Response.End();
                return;
            }
        }
        string str45 = "";
        str110 = str40;
        if (str110 != null)
        {
            if (!(str110 == "0"))
            {
                if (str110 == "1")
                {
                    base.DeleteCreditLock(str);
                    base.Response.End();
                    string[] strArray6 = str41.Split(new char[] { '|' });
                    str45 = str41.Replace("|", ",");
                    string str46 = "";
                    string str47 = "";
                    int num25 = 0;
                    int num26 = 0;
                Label_219B:;
                    if (num26 < strArray6[0].Split(new char[] { ',' }).Length)
                    {
                        str46 = str46 + getnum(strArray6[0].Split(new char[] { ',' })[num26]) + ",";
                        num25++;
                        num26++;
                        goto Label_219B;
                    }
                    num26 = 0;
                Label_2208:;
                    if (num26 < strArray6[1].Split(new char[] { ',' }).Length)
                    {
                        str47 = str47 + getnum(strArray6[1].Split(new char[] { ',' })[num26]) + ",";
                        num26++;
                        goto Label_2208;
                    }
                    if (num25.Equals(0))
                    {
                        base.Response.End();
                    }
                    if ((("92285".Equals(str5) || "92286".Equals(str5)) || str5.Equals("92638")) || str5.Equals("92639"))
                    {
                        num22 = length - 2;
                        if (!num25.Equals(2))
                        {
                            base.Response.End();
                        }
                    }
                    else if (((("92287".Equals(str5) || "92288".Equals(str5)) || ("92289".Equals(str5) || str5.Equals("92640"))) || str5.Equals("92641")) || str5.Equals("92642"))
                    {
                        num22 = length - 1;
                        if (num25 != 1)
                        {
                            base.Response.End();
                        }
                    }
                    else if ("92566".Equals(str5) || "92569".Equals(str5))
                    {
                        num22 = length - 1;
                        if (num25 != 1)
                        {
                            base.Response.End();
                        }
                    }
                    else if ("92567".Equals(str5) || "92570".Equals(str5))
                    {
                        num22 = length - 2;
                        if (num25 != 2)
                        {
                            base.Response.End();
                        }
                    }
                    else if ("92568".Equals(str5) || "92571".Equals(str5))
                    {
                        num22 = length - 3;
                        if (num25 != 3)
                        {
                            base.Response.End();
                        }
                    }
                    else if ("92636".Equals(str5) || "92637".Equals(str5))
                    {
                        num22 = length - 4;
                        if (num25 != 4)
                        {
                            base.Response.End();
                        }
                    }
                    str47 = str47.Substring(0, str47.Length - 1);
                    str43 = str46 + "|" + str47;
                    strArray6 = str47.Split(new char[] { ',' });
                    str36 = "";
                    foreach (string str33 in strArray6)
                    {
                        if (str33.Trim() != "")
                        {
                            str36 = str36 + str46 + str33.Trim() + "~";
                        }
                    }
                    str36 = str36.Substring(0, str36.Length - 1);
                    goto Label_29C7;
                }
                if (str110 == "2")
                {
                    base.DeleteCreditLock(str);
                    base.Response.End();
                    num22 = 0x10;
                    DataTable table6 = CallBLL.cz_zodiac_six_bll.GetZodiacByID(str43).Tables[0];
                    strArray7 = base.get_ZodiacNumberArray().Split(new char[] { ',' });
                    strArray8 = strArray7[int.Parse(str43.Split(new char[] { ',' })[0]) - 1].Replace("、", ",").Split(new char[] { ',' });
                    str48 = strArray7[int.Parse(str43.Split(new char[] { ',' })[0]) - 1].Replace("、", ",");
                    str45 = str48;
                    if (strArray8.Length > 4)
                    {
                        num22 = 20;
                    }
                    strArray9 = strArray7[int.Parse(str43.Split(new char[] { ',' })[1]) - 1].Replace("、", ",").Split(new char[] { ',' });
                    str49 = strArray7[int.Parse(str43.Split(new char[] { ',' })[1]) - 1].Replace("、", ",");
                    str45 = str45 + "," + str49;
                    if (strArray9.Length > 4)
                    {
                        num22 = 20;
                    }
                    str43 = str48 + "|" + str49;
                    goto Label_29C7;
                }
                if (str110 == "3")
                {
                    base.DeleteCreditLock(str);
                    base.Response.End();
                    if (str43.IndexOf("10") > -1)
                    {
                        num22 = 20;
                    }
                    else
                    {
                        num22 = 0x19;
                    }
                    strArray10 = base.get_WSNumberArray().Split(new char[] { ',' });
                    str48 = strArray10[int.Parse(str43.Split(new char[] { ',' })[0]) - 1].Replace("、", ",");
                    str49 = strArray10[int.Parse(str43.Split(new char[] { ',' })[1]) - 1].Replace("、", ",");
                    str43 = str48 + "|" + str49;
                    str45 = str48 + "|" + str49;
                    goto Label_29C7;
                }
                if (str110 == "4")
                {
                    base.DeleteCreditLock(str);
                    base.Response.End();
                    strArray11 = str43.Split(new char[] { ',' });
                    str48 = "";
                    str49 = "";
                    if (int.Parse(strArray11[0]) > 12)
                    {
                        num27 = (int.Parse(strArray11[0]) - 1) - 12;
                        str48 = base.get_WSNumberArray().Split(new char[] { ',' })[num27].Replace("、", ",");
                    }
                    else
                    {
                        str48 = base.get_ZodiacNumberArray().Split(new char[] { ',' })[int.Parse(strArray11[0]) - 1].Replace("、", ",");
                    }
                    if (int.Parse(strArray11[1]) > 12)
                    {
                        num27 = (int.Parse(strArray11[1]) - 1) - 12;
                        str49 = base.get_WSNumberArray().Split(new char[] { ',' })[num27].Replace("、", ",");
                    }
                    else
                    {
                        str49 = base.get_ZodiacNumberArray().Split(new char[] { ',' })[int.Parse(strArray11[1]) - 1].Replace("、", ",");
                    }
                    str43 = str48 + "|" + str49;
                    str45 = str48 + "," + str49;
                    goto Label_29C7;
                }
            }
            else
            {
                if (((str5.Equals("92285") || str5.Equals("92286")) || str5.Equals("92638")) || str5.Equals("92639"))
                {
                    num22 = ((length * (length - 1)) * (length - 2)) / 6;
                    str36 = getfz(str43, 3);
                }
                else if ((((str5.Equals("92287") || str5.Equals("92288")) || (str5.Equals("92289") || str5.Equals("92640"))) || str5.Equals("92641")) || str5.Equals("92642"))
                {
                    num22 = (length * (length - 1)) / 2;
                    str36 = getfz(str43, 2);
                }
                else if (str5.Equals("92575") || str5.Equals("92643"))
                {
                    num22 = (((length * (length - 1)) * (length - 2)) * (length - 3)) / 0x18;
                    str36 = getfz(str43, 4);
                }
                else if (str5.Equals("92572"))
                {
                    num22 = ((((length * (length - 1)) * (length - 2)) * (length - 3)) * (length - 4)) / 120;
                    str36 = getfz(str43, 5);
                }
                else if (str5.Equals("92588"))
                {
                    num22 = (((((length * (length - 1)) * (length - 2)) * (length - 3)) * (length - 4)) * (length - 5)) / 720;
                    str36 = getfz(str43, 6);
                }
                else if (str5.Equals("92589"))
                {
                    num22 = ((((((length * (length - 1)) * (length - 2)) * (length - 3)) * (length - 4)) * (length - 5)) * (length - 6)) / 0x13b0;
                    str43 = getlongnum(str43);
                    str36 = getfz(str43, 7);
                }
                else if (str5.Equals("92590"))
                {
                    num22 = (((((((length * (length - 1)) * (length - 2)) * (length - 3)) * (length - 4)) * (length - 5)) * (length - 6)) * (length - 7)) / 0x9d80;
                    str43 = getlongnum(str43);
                    str36 = getfz(str43, 8);
                }
                else if (str5.Equals("92591"))
                {
                    num22 = ((((((((length * (length - 1)) * (length - 2)) * (length - 3)) * (length - 4)) * (length - 5)) * (length - 6)) * (length - 7)) * (length - 8)) / 0x58980;
                    str43 = getlongnum(str43);
                    str36 = getfz(str43, 9);
                }
                else if (str5.Equals("92592"))
                {
                    num22 = (((((((((length * (length - 1)) * (length - 2)) * (length - 3)) * (length - 4)) * (length - 5)) * (length - 6)) * (length - 7)) * (length - 8)) * (length - 9)) / 0x375f00;
                    str43 = getlongnum(str43);
                    str36 = getfz(str43, 10);
                }
                else if (str5.Equals("92566") || str5.Equals("92569"))
                {
                    num22 = (length * (length - 1)) / 2;
                    str36 = getfz(str43, 2);
                }
                else if (str5.Equals("92567") || str5.Equals("92570"))
                {
                    num22 = ((length * (length - 1)) * (length - 2)) / 6;
                    str36 = getfz(str43, 3);
                }
                else if (str5.Equals("92568") || str5.Equals("92571"))
                {
                    num22 = (((length * (length - 1)) * (length - 2)) * (length - 3)) / 0x18;
                    str36 = getfz(str43, 4);
                }
                else if (str5.Equals("92636") || str5.Equals("92637"))
                {
                    num22 = ((((length * (length - 1)) * (length - 2)) * (length - 3)) * (length - 4)) / 120;
                    str36 = getfz(str43, 5);
                }
                str45 = str43;
                goto Label_29C7;
            }
        }
        base.DeleteCreditLock(str);
        base.Response.End();
    Label_29C7:
        table7 = CallBLL.cz_odds_six_bll.GetOddsByID(str5).Tables[0];
        str26 = table7.Rows[0]["current_odds"].ToString();
        string str50 = table7.Rows[0]["max_odds"].ToString();
        string str51 = table7.Rows[0]["min_odds"].ToString();
        string str52 = table7.Rows[0]["a_diff"].ToString();
        string str53 = table7.Rows[0]["b_diff"].ToString();
        string str54 = table7.Rows[0]["c_diff"].ToString();
        string str55 = table7.Rows[0][str17 + "_diff"].ToString();
        num28 = int.Parse(table7.Rows[0]["play_id"].ToString());
        str56 = table7.Rows[0]["o_play_name"].ToString();
        str57 = table7.Rows[0]["put_amount"].ToString();
        str58 = table7.Rows[0]["ratio"].ToString();
        string ratio = table7.Rows[0]["ratio2"].ToString();
        if (FileCacheHelper.get_GetSixPutMoneyCache() == "1")
        {
            ds = base.GetUserDrawback_six(rate, getUserModelInfo.get_six_kind(), num28.ToString());
        }
        else
        {
            ds = base.GetUserDrawback_six(rate, getUserModelInfo.get_six_kind());
        }
        if (ds == null)
        {
            base.DeleteCreditLock(str);
            base.Response.Write("<script>alert('系統錯誤,請重試！');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
            base.Response.End();
        }
        else
        {
            double num30;
            string str60;
            string str61;
            string[] strArray12;
            int num31;
            DataTable table9;
            string sXLNameBySXid;
            string[] strArray13;
            string str68;
            string str69;
            string str70;
            string str71;
            List<string> list;
            List<string> list2;
            rowArray2 = ds.Tables[0].Select(string.Format(" play_id={0} ", num28));
            foreach (DataRow row in rowArray2)
            {
                str110 = row["u_type"].ToString().Trim();
                if (str110 != null)
                {
                    if (!(str110 == "zj"))
                    {
                        if (str110 == "fgs")
                        {
                            goto Label_2CE4;
                        }
                        if (str110 == "gd")
                        {
                            goto Label_2D08;
                        }
                        if (str110 == "zd")
                        {
                            goto Label_2D2C;
                        }
                        if (str110 == "dl")
                        {
                            goto Label_2D50;
                        }
                        if (str110 == "hy")
                        {
                            goto Label_2D74;
                        }
                    }
                    else
                    {
                        str19 = row[str17 + "_drawback"].ToString().Trim();
                    }
                }
                continue;
            Label_2CE4:
                str20 = row[str17 + "_drawback"].ToString().Trim();
                continue;
            Label_2D08:
                str21 = row[str17 + "_drawback"].ToString().Trim();
                continue;
            Label_2D2C:
                str22 = row[str17 + "_drawback"].ToString().Trim();
                continue;
            Label_2D50:
                str23 = row[str17 + "_drawback"].ToString().Trim();
                continue;
            Label_2D74:
                switch (str18)
                {
                    case "dl":
                        str24 = row[str17 + "_drawback"].ToString().Trim();
                        break;

                    case "zd":
                        str23 = row[str17 + "_drawback"].ToString().Trim();
                        break;

                    case "gd":
                        str22 = row[str17 + "_drawback"].ToString().Trim();
                        break;

                    case "fgs":
                        str21 = row[str17 + "_drawback"].ToString().Trim();
                        break;
                }
            }
            double num29 = double.Parse(str7) * num22;
            if (str5.Equals("92565"))
            {
                num29 = double.Parse(str7) * 1.0;
                if (double.Parse(str7) > num10)
                {
                    base.DeleteCreditLock(str);
                    base.Response.Write("<script>alert('下註金額超出單註最大金額！');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                    base.Response.End();
                    return;
                }
                if (double.Parse(str7) < num14)
                {
                    base.DeleteCreditLock(str);
                    base.Response.Write("<script>alert('下註金額低過最低金額！');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                    base.Response.End();
                    return;
                }
                num30 = num12 - double.Parse(str7);
                str60 = "";
                str61 = "";
                strArray12 = str36.Replace("00", "10").Split(new char[] { '~' });
                num31 = 0;
                foreach (string str62 in strArray12)
                {
                    if (num31 == 0)
                    {
                        str60 = str60 + "'" + px(str62) + "'";
                        str61 = str61 + px(str62);
                    }
                    else
                    {
                        str60 = str60 + ",'" + px(str62) + "'";
                        str61 = str61 + "|" + px(str62);
                    }
                    num31++;
                }
                table9 = CallBLL.cz_bet_six_bll.DQLMValid(int.Parse(str5), getUserModelInfo.get_u_name(), str61, num30);
                if (table9 != null)
                {
                    sXLNameBySXid = table9.Rows[0][0].ToString().Trim();
                    strArray13 = sXLNameBySXid.Split(new char[] { ',' });
                    for (index = 0; index < strArray13.Length; index++)
                    {
                        if (index == 0)
                        {
                            sXLNameBySXid = FunctionSix.GetSXLNameBySXid(strArray13[index]);
                        }
                        else
                        {
                            sXLNameBySXid = sXLNameBySXid + "、" + FunctionSix.GetSXLNameBySXid(strArray13[index]);
                        }
                    }
                    base.DeleteCreditLock(str);
                    str2 = string.Format("單組【" + sXLNameBySXid + "】單期下註金額超出單期金額[" + num12.ToString() + "]的限制！", new object[0]);
                    base.Response.Write("<script>alert('" + str2 + "');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                    base.Response.End();
                    return;
                }
            }
            else
            {
                if (double.Parse(str7) > num10)
                {
                    base.DeleteCreditLock(str);
                    base.Response.Write("<script>alert('下註金額超出單註最大金額！');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                    base.Response.End();
                    return;
                }
                if (double.Parse(str7) < num14)
                {
                    base.DeleteCreditLock(str);
                    base.Response.Write("<script>alert('下註金額低過最低金額！');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                    base.Response.End();
                    return;
                }
                if (num8 < num29)
                {
                    base.DeleteCreditLock(str);
                    base.Response.Write("<script>alert('可用餘額不足！');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                    base.Response.End();
                    return;
                }
                num30 = num12 - double.Parse(str7);
                str60 = "";
                str61 = "";
                strArray12 = str36.Replace("00", "10").Split(new char[] { '~' });
                num31 = 0;
                foreach (string str62 in strArray12)
                {
                    if (num31 == 0)
                    {
                        str60 = str60 + "'" + px(str62) + "'";
                        str61 = str61 + px(str62);
                    }
                    else
                    {
                        str60 = str60 + ",'" + px(str62) + "'";
                        str61 = str61 + "|" + px(str62);
                    }
                    num31++;
                }
                table9 = CallBLL.cz_bet_six_bll.DQLMValid(int.Parse(str5), getUserModelInfo.get_u_name(), str61, num30);
                if (table9 != null)
                {
                    sXLNameBySXid = table9.Rows[0][0].ToString().Trim();
                    switch (str5)
                    {
                        case "92566":
                        case "92567":
                        case "92568":
                        case "92636":
                            strArray13 = sXLNameBySXid.Split(new char[] { ',' });
                            for (index = 0; index < strArray13.Length; index++)
                            {
                                if (index == 0)
                                {
                                    sXLNameBySXid = FunctionSix.GetSXLNameBySXid(strArray13[index]);
                                }
                                else
                                {
                                    sXLNameBySXid = sXLNameBySXid + "、" + FunctionSix.GetSXLNameBySXid(strArray13[index]);
                                }
                            }
                            break;

                        case "92569":
                        case "92570":
                        case "92571":
                        case "92637":
                            strArray13 = sXLNameBySXid.Split(new char[] { ',' });
                            for (index = 0; index < strArray13.Length; index++)
                            {
                                if (index == 0)
                                {
                                    sXLNameBySXid = FunctionSix.GetWSLNameBySXid(strArray13[index]);
                                }
                                else
                                {
                                    sXLNameBySXid = sXLNameBySXid + "、" + FunctionSix.GetWSLNameBySXid(strArray13[index]);
                                }
                            }
                            break;
                    }
                    base.DeleteCreditLock(str);
                    str2 = string.Format("單組【" + sXLNameBySXid + "】單期下註金額超出單期金額[" + num12.ToString() + "]的限制！", new object[0]);
                    base.Response.Write("<script>alert('" + str2 + "');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                    base.Response.End();
                    return;
                }
            }
            string str64 = "";
            bool isChange = false;
            if (str30.Equals("1"))
            {
                if ((!str40.Equals("2") && !str40.Equals("3")) && !str40.Equals("4"))
                {
                    str64 = base.GetWTList(str5, str43.Replace("|", ",").Replace(",,", ","), str42, ref isChange);
                }
                else
                {
                    string str65 = "";
                    string str66 = LSRequest.qq("numbers");
                    str110 = str40;
                    if (str110 != null)
                    {
                        if (!(str110 == "2"))
                        {
                            if (str110 == "3")
                            {
                                strArray10 = base.get_WSNumberArray().Split(new char[] { ',' });
                                str48 = strArray10[int.Parse(str66.Split(new char[] { ',' })[0]) - 1].Replace("、", ",");
                                str49 = strArray10[int.Parse(str66.Split(new char[] { ',' })[1]) - 1].Replace("、", ",");
                                str65 = str48 + "|" + str49;
                            }
                            else if (str110 == "4")
                            {
                                strArray11 = str66.Split(new char[] { ',' });
                                str48 = "";
                                str49 = "";
                                if (int.Parse(strArray11[0]) > 12)
                                {
                                    num27 = (int.Parse(strArray11[0]) - 1) - 12;
                                    str48 = base.get_WSNumberArray().Split(new char[] { ',' })[num27].Replace("、", ",");
                                }
                                else
                                {
                                    str48 = base.get_ZodiacNumberArray().Split(new char[] { ',' })[int.Parse(strArray11[0]) - 1].Replace("、", ",");
                                }
                                if (int.Parse(strArray11[1]) > 12)
                                {
                                    num27 = (int.Parse(strArray11[1]) - 1) - 12;
                                    str49 = base.get_WSNumberArray().Split(new char[] { ',' })[num27].Replace("、", ",");
                                }
                                else
                                {
                                    str49 = base.get_ZodiacNumberArray().Split(new char[] { ',' })[int.Parse(strArray11[1]) - 1].Replace("、", ",");
                                }
                                str65 = str48 + "," + str49;
                            }
                        }
                        else
                        {
                            strArray7 = base.get_ZodiacNumberArray().Split(new char[] { ',' });
                            strArray8 = strArray7[int.Parse(str66.Split(new char[] { ',' })[0]) - 1].Replace("、", ",").Split(new char[] { ',' });
                            str48 = strArray7[int.Parse(str66.Split(new char[] { ',' })[0]) - 1].Replace("、", ",");
                            strArray9 = strArray7[int.Parse(str66.Split(new char[] { ',' })[1]) - 1].Replace("、", ",").Split(new char[] { ',' });
                            str49 = strArray7[int.Parse(str66.Split(new char[] { ',' })[1]) - 1].Replace("、", ",");
                            str65 = str48 + "|" + str49;
                        }
                    }
                    str64 = base.GetWTList(str5, str65.Replace("|", ","), str42, ref isChange);
                }
            }
            str67 = "";
            if (str30.Equals("1") && str31.Equals("1"))
            {
                str68 = str26.Split(new char[] { ',' })[0];
                str69 = str26.Split(new char[] { ',' })[1];
                str70 = "";
                str71 = "";
                str70 = str55.Split(new char[] { ',' })[0];
                str71 = str70;
                if (str55.Split(new char[] { ',' }).Length > 1)
                {
                    str71 = str55.Split(new char[] { ',' })[1];
                }
                str67 = (Convert.ToDouble(str68) + Convert.ToDouble(str70)) + "," + (Convert.ToDouble(str69) + Convert.ToDouble(str71));
                str38 = (Convert.ToDouble(str68) + Convert.ToDouble(str70)) + "," + (Convert.ToDouble(str69) + Convert.ToDouble(str71));
                base.GetOdds_SIX(str5, str26, table7.Rows[0][str17 + "_diff"].ToString(), ref str67);
                string[] strArray14 = str67.Split(new char[] { ',' });
                if (((double.Parse(s.Split(new char[] { ',' })[0]) != double.Parse(strArray14[0])) || (double.Parse(s.Split(new char[] { ',' })[1]) != double.Parse(strArray14[1]))) || (!string.IsNullOrEmpty(str64) && isChange))
                {
                    base.DeleteCreditLock(str);
                    list = new List<string> { "1" };
                    list2 = new List<string> {
                        str67
                    };
                    if ((dictionary2.Count > 0) || (!string.IsNullOrEmpty(str64) && str30.Equals("1")))
                    {
                        base.Response.Write("<script>alert('賠率有變動,請確認再投註！');</script>");
                    }
                    base.Response.Write("<script>$('#v_" + strArray[0] + " ~ span.hong').text('" + str67.ToString() + "');$('#p_" + strArray[0] + "').val('" + str67.ToString() + "');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                    if (!(string.IsNullOrEmpty(str64) || !str30.Equals("1")))
                    {
                        base.Response.Write("<script>$('#uPI_WT').val('" + str64 + "');</script>");
                    }
                    base.Response.Write("<script>$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                    base.Response.Write("<script>chageLmBetWt();</script>");
                    base.Response.End();
                    return;
                }
                playDrawbackValue = base.GetPlayDrawbackValue(str20, str58);
                if ((playDrawbackValue != 0.0) && (double.Parse(str38.Split(new char[] { ',' })[0]) > playDrawbackValue))
                {
                    base.DeleteCreditLock(str);
                    base.Response.Write("<script>alert('賠率錯誤！');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                    base.Response.End();
                }
                double num33 = base.GetPlayDrawbackValue(str20, ratio);
                if ((num33 != 0.0) && (double.Parse(str38.Split(new char[] { ',' })[1]) > num33))
                {
                    base.DeleteCreditLock(str);
                    base.Response.Write("<script>alert('賠率錯誤！');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                    base.Response.End();
                    return;
                }
            }
            else if (str30.Equals("1") && str31.Equals("0"))
            {
                str68 = str26;
                str70 = str55;
                num58 = Convert.ToDouble(str68) + Convert.ToDouble(str70);
                str67 = num58.ToString();
                num58 = Convert.ToDouble(str68) + Convert.ToDouble(str70);
                str38 = num58.ToString();
                base.GetOdds_SIX(str5, str26, table7.Rows[0][str17 + "_diff"].ToString(), ref str67);
                if ((double.Parse(s) != double.Parse(str67)) || (!string.IsNullOrEmpty(str64) && isChange))
                {
                    base.DeleteCreditLock(str);
                    list = new List<string> { "1" };
                    list2 = new List<string> {
                        str67
                    };
                    if ((dictionary2.Count > 0) || (!string.IsNullOrEmpty(str64) && str30.Equals("1")))
                    {
                        base.Response.Write("<script>alert('賠率有變動,請確認再投註！');</script>");
                    }
                    base.Response.Write("<script>$('#v_" + strArray[0] + " ~ span.hong').text('" + str67.ToString() + "');$('#p_" + strArray[0] + "').val('" + str67.ToString() + "');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                    if (!(string.IsNullOrEmpty(str64) || !str30.Equals("1")))
                    {
                        base.Response.Write("<script>$('#uPI_WT').val('" + str64 + "');</script>");
                    }
                    base.Response.Write("<script>$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                    base.Response.Write("<script>chageLmBetWt();</script>");
                    base.Response.End();
                    return;
                }
                playDrawbackValue = base.GetPlayDrawbackValue(str20, str58);
                if ((playDrawbackValue != 0.0) && (double.Parse(str38) > playDrawbackValue))
                {
                    base.DeleteCreditLock(str);
                    base.Response.Write("<script>alert('賠率錯誤！');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                    base.Response.End();
                    return;
                }
            }
            else
            {
                base.DeleteCreditLock(str);
                base.Response.End();
            }
            string str72 = base.GetLmGroup()[num28 + "_" + 100];
            int num34 = int.Parse(str72.Split(new char[] { ',' })[1]);
            if (str36.Split(new char[] { '~' }).Length > num34)
            {
                base.DeleteCreditLock(str);
                str2 = string.Format("您選擇的號碼組合超過了最大{0}組，請重新選擇號碼！", num34);
                base.Response.Write("<script>alert('" + str2 + "');window.location=\"" + str12 + "\";</script>");
                base.Response.End();
            }
            else
            {
                string str76;
                string str77;
                DataTable myDT = null;
                switch (str5)
                {
                    case "92565":
                        myDT = CallBLL.cz_wt_6xyz_six_bll.GetWT();
                        break;

                    case "92286":
                        myDT = CallBLL.cz_wt_3z2_six_bll.GetWT();
                        break;

                    case "92285":
                        myDT = CallBLL.cz_wt_3qz_six_bll.GetWT();
                        break;

                    case "92287":
                        myDT = CallBLL.cz_wt_2qz_six_bll.GetWT();
                        break;

                    case "92288":
                        myDT = CallBLL.cz_wt_2zt_six_bll.GetWT();
                        break;

                    case "92289":
                        myDT = CallBLL.cz_wt_tc_six_bll.GetWT();
                        break;

                    case "92575":
                        myDT = CallBLL.cz_wt_4z1_six_bll.GetWT();
                        break;

                    case "92638":
                        myDT = CallBLL.cz_wt_3qz_b_six_bll.GetWT();
                        break;

                    case "92639":
                        myDT = CallBLL.cz_wt_3z2_b_six_bll.GetWT();
                        break;

                    case "92640":
                        myDT = CallBLL.cz_wt_2qz_b_six_bll.GetWT();
                        break;

                    case "92641":
                        myDT = CallBLL.cz_wt_2zt_b_six_bll.GetWT();
                        break;

                    case "92642":
                        myDT = CallBLL.cz_wt_tc_b_six_bll.GetWT();
                        break;

                    case "92643":
                        myDT = CallBLL.cz_wt_4z1_b_six_bll.GetWT();
                        break;

                    case "92566":
                        myDT = CallBLL.cz_wt_sxlwsl_six_bll.GetWT();
                        break;

                    case "92567":
                        myDT = CallBLL.cz_wt_sxlwsl_six_bll.GetWT();
                        break;

                    case "92568":
                        myDT = CallBLL.cz_wt_sxlwsl_six_bll.GetWT();
                        break;

                    case "92636":
                        myDT = CallBLL.cz_wt_sxlwsl_six_bll.GetWT();
                        break;

                    case "92569":
                        myDT = CallBLL.cz_wt_sxlwsl_six_bll.GetWT();
                        break;

                    case "92570":
                        myDT = CallBLL.cz_wt_sxlwsl_six_bll.GetWT();
                        break;

                    case "92571":
                        myDT = CallBLL.cz_wt_sxlwsl_six_bll.GetWT();
                        break;

                    case "92637":
                        myDT = CallBLL.cz_wt_sxlwsl_six_bll.GetWT();
                        break;

                    case "92572":
                        myDT = CallBLL.cz_wt_5bz_six_bll.GetWT();
                        break;

                    case "92588":
                        myDT = CallBLL.cz_wt_6bz_six_bll.GetWT();
                        break;

                    case "92589":
                        myDT = CallBLL.cz_wt_7bz_six_bll.GetWT();
                        break;

                    case "92590":
                        myDT = CallBLL.cz_wt_8bz_six_bll.GetWT();
                        break;

                    case "92591":
                        myDT = CallBLL.cz_wt_9bz_six_bll.GetWT();
                        break;

                    case "92592":
                        myDT = CallBLL.cz_wt_10bz_six_bll.GetWT();
                        break;
                }
                string[] strArray15 = str36.Split(new char[] { '~' });
                string str73 = "";
                string str74 = "";
                ArrayList list3 = new ArrayList();
                ArrayList list4 = new ArrayList();
                if ((((str5.Equals("92566") || str5.Equals("92567")) || (str5.Equals("92568") || str5.Equals("92569"))) || ((str5.Equals("92570") || str5.Equals("92571")) || str5.Equals("92636"))) || str5.Equals("92637"))
                {
                    foreach (string str75 in strArray15)
                    {
                        string[] strArray16;
                        string[] strArray17;
                        str76 = "";
                        str77 = "";
                        if (((str5.Equals("92566") || str5.Equals("92567")) || str5.Equals("92568")) || str5.Equals("92636"))
                        {
                            string str78 = base.get_YearLianID();
                            if (str75.IndexOf(str78.Trim()) >= 0)
                            {
                                strArray16 = str67.Split(new char[] { ',' });
                                str76 = str78 + "|" + strArray16[1];
                                strArray17 = str38.Split(new char[] { ',' });
                                str77 = str78 + "|" + strArray17[1];
                            }
                            else
                            {
                                str76 = get_sxws_min_pl(myDT, str75, str67, str5);
                                str77 = get_sxws_min_pl(myDT, str75, str38, str5);
                                if (str76 == "err")
                                {
                                    base.DeleteCreditLock(str);
                                    base.Response.Write("<script>alert('下註有誤,請重新投註！');window.location=\"" + str12 + "\";</script>");
                                    base.Response.End();
                                    return;
                                }
                            }
                        }
                        else if (str75.IndexOf("00") >= 0)
                        {
                            strArray16 = str67.Split(new char[] { ',' });
                            str76 = 10 + "|" + strArray16[1];
                            strArray17 = str38.Split(new char[] { ',' });
                            str77 = 10 + "|" + strArray17[1];
                        }
                        else
                        {
                            str76 = get_sxws_min_pl(myDT, str75, str67, str5);
                            str77 = get_sxws_min_pl(myDT, str75, str38, str5);
                            if (str76 == "err")
                            {
                                base.DeleteCreditLock(str);
                                base.Response.Write("<script>alert('下註有誤,請重新投註！');window.location=\"" + str12 + "\";</script>");
                                base.Response.End();
                                return;
                            }
                        }
                        list3.Add(str76);
                        list4.Add(str77);
                    }
                    str73 = string.Join("~", list3.ToArray());
                    str74 = string.Join("~", list4.ToArray());
                }
                else
                {
                    foreach (string str75 in strArray15)
                    {
                        str76 = get_ball_min_pl(myDT, str75, str67);
                        str77 = get_ball_min_pl(myDT, str75, str38);
                        if (str76 == "err")
                        {
                            base.DeleteCreditLock(str);
                            base.Response.Write("<script>alert('下註有誤,請重新投註！');window.location=\"" + str12 + "\";</script>");
                            base.Response.End();
                            return;
                        }
                        list3.Add(str76);
                        list4.Add(str77);
                    }
                    str73 = string.Join("~", list3.ToArray());
                    str74 = string.Join("~", list4.ToArray());
                }
                str36 = str36.Replace("00", "10");
                _six2 = new cz_bet_six();
                _six2.set_order_num(Utils.GetOrderNumber());
                _six2.set_checkcode(str14);
                _six2.set_u_name(getUserModelInfo.get_u_name());
                _six2.set_u_nicker(getUserModelInfo.get_u_nicker());
                _six2.set_phase_id(new int?(int.Parse(str16)));
                _six2.set_phase(str15);
                _six2.set_bet_time(new DateTime?(DateTime.Now));
                _six2.set_odds_id(new int?(int.Parse(str5)));
                _six2.set_category(table7.Rows[0]["category"].ToString());
                _six2.set_play_id(new int?(int.Parse(table7.Rows[0]["play_id"].ToString())));
                _six2.set_play_name(str56);
                _six2.set_bet_val(str43.Replace("00", "10"));
                _six2.set_bet_wt(str73);
                _six2.set_bet_group(str36);
                _six2.set_odds(str67);
                _six2.set_amount(new decimal?(decimal.Parse(num29.ToString())));
                _six2.set_profit(0);
                _six2.set_unit_cnt(new int?(num22));
                _six2.set_lm_type(new int?(int.Parse(str40)));
                _six2.set_hy_drawback(new decimal?(decimal.Parse(str24)));
                _six2.set_dl_drawback(new decimal?(decimal.Parse(str23)));
                _six2.set_zd_drawback(new decimal?(decimal.Parse(str22)));
                _six2.set_gd_drawback(new decimal?(decimal.Parse(str21)));
                _six2.set_fgs_drawback(new decimal?(decimal.Parse(str20)));
                _six2.set_zj_drawback(new decimal?(decimal.Parse(str19)));
                _six2.set_dl_rate((float) int.Parse(rate.get_dlzc()));
                _six2.set_zd_rate((float) int.Parse(rate.get_zdzc()));
                _six2.set_gd_rate((float) int.Parse(rate.get_gdzc()));
                _six2.set_fgs_rate(float.Parse(num2.ToString()));
                _six2.set_zj_rate(float.Parse(num.ToString()));
                _six2.set_dl_name(rate.get_dlname());
                _six2.set_zd_name(rate.get_zdname());
                _six2.set_gd_name(rate.get_gdname());
                _six2.set_fgs_name(rate.get_fgsname());
                _six2.set_is_payment(0);
                _six2.set_m_type(new int?(num4));
                _six2.set_kind(str17);
                _six2.set_ip(LSRequest.GetIP());
                _six2.set_lottery_type(new int?(num5));
                _six2.set_lottery_name(base.GetGameNameByID(_six2.get_lottery_type().ToString()));
                _six2.set_isLM(1);
                _six2.set_ordervalidcode(Utils.GetOrderValidCode(_six2.get_u_name(), _six2.get_order_num(), _six2.get_bet_val(), _six2.get_odds(), _six2.get_kind(), Convert.ToInt32(_six2.get_phase_id()), Convert.ToInt32(_six2.get_odds_id()), Convert.ToDouble(_six2.get_amount())));
                _six2.set_odds_zj(str38);
                _six2.set_bet_wt_zj(str74);
                _six2.set_isPhone(1);
                num35 = 0;
                if (!(CallBLL.cz_bet_six_bll.AddBet(_six2, decimal.Parse(num29.ToString()), str, ref num35) && (num35 > 0)))
                {
                    base.DeleteCreditLock(str);
                    base.Response.Write("<script>alert('系統写入錯誤,請重試！');$('#JeuValidate').val('" + base.get_JeuValidate() + "');</script>");
                    base.Response.End();
                }
                else
                {
                    cz_splitgroup_six _six3;
                    double num36 = double.Parse(str7);
                    double num37 = 0.0;
                    double num38 = 0.0;
                    string[] strArray18 = str67.Split(new char[] { ',' });
                    num37 = double.Parse(strArray18[0].ToString());
                    if (strArray18.Length > 1)
                    {
                        num38 = double.Parse(strArray18[1].ToString());
                    }
                    string[] strArray19 = str36.Split(new char[] { '~' });
                    string[] strArray20 = str73.Split(new char[] { '~' });
                    double num39 = 0.0;
                    double num40 = 0.0;
                    string[] strArray21 = str38.Split(new char[] { ',' });
                    num39 = double.Parse(strArray21[0].ToString());
                    if (strArray21.Length > 1)
                    {
                        num40 = double.Parse(strArray21[1].ToString());
                    }
                    string[] strArray22 = str74.Split(new char[] { '~' });
                    string str79 = "";
                    if (str5.Equals("92565"))
                    {
                        string str80 = px(str43.ToString().Trim());
                        _six3 = new cz_splitgroup_six();
                        _six3.set_order_num(_six2.get_order_num());
                        if (strArray20.Length > 0)
                        {
                            if (!string.IsNullOrEmpty(strArray20[0].Trim()))
                            {
                                _six3.set_odds1(new decimal?(decimal.Parse(strArray20[0].Split(new char[] { '|' })[1])));
                            }
                            else
                            {
                                _six3.set_odds1(new decimal?(decimal.Parse(num37.ToString())));
                            }
                        }
                        else
                        {
                            _six3.set_odds1(new decimal?(decimal.Parse(num37.ToString())));
                        }
                        _six3.set_odds2(new decimal?(decimal.Parse(num38.ToString())));
                        _six3.set_odds_id(new int?(int.Parse(str5)));
                        _six3.set_item(str80);
                        _six3.set_item_money(new decimal?(decimal.Parse(str7)));
                        if (strArray22.Length > 0)
                        {
                            if (!string.IsNullOrEmpty(strArray22[0].Trim()))
                            {
                                _six3.set_odds1_zj(new decimal?(decimal.Parse(strArray22[0].Split(new char[] { '|' })[1])));
                            }
                            else
                            {
                                _six3.set_odds1_zj(new decimal?(decimal.Parse(num39.ToString())));
                            }
                        }
                        else
                        {
                            _six3.set_odds1_zj(new decimal?(decimal.Parse(num39.ToString())));
                        }
                        _six3.set_odds2_zj(new decimal?(decimal.Parse(num40.ToString())));
                        int num41 = CallBLL.cz_splitgroup_six_bll.Add(_six3);
                    }
                    else
                    {
                        int num42 = 0;
                        StringBuilder builder = new StringBuilder();
                        foreach (string str81 in strArray19)
                        {
                            string str82;
                            string str83;
                            string str84;
                            string str85;
                            string str86;
                            string str87;
                            str79 = px(str81);
                            if ((((str5.Equals("92566") || str5.Equals("92567")) || (str5.Equals("92568") || str5.Equals("92569"))) || ((str5.Equals("92570") || str5.Equals("92571")) || str5.Equals("92636"))) || str5.Equals("92637"))
                            {
                                str82 = "";
                                str83 = "0";
                                str84 = strArray20[num42].Trim();
                                if (str84 == "")
                                {
                                    str82 = num37.ToString();
                                }
                                else
                                {
                                    str82 = str84.Split(new char[] { '|' })[1];
                                }
                                str85 = "";
                                str86 = "0";
                                str87 = strArray22[num42].Trim();
                                if (str87 == "")
                                {
                                    str85 = num39.ToString();
                                }
                                else
                                {
                                    str85 = str87.Split(new char[] { '|' })[1];
                                }
                                _six3 = new cz_splitgroup_six();
                                _six3.set_order_num(_six2.get_order_num());
                                _six3.set_odds1(new decimal?(decimal.Parse(str82.ToString())));
                                _six3.set_odds2(new decimal?(decimal.Parse(str83.ToString())));
                                _six3.set_odds_id(new int?(int.Parse(str5)));
                                _six3.set_item(str79);
                                _six3.set_item_money(new decimal?(decimal.Parse(num36.ToString())));
                                _six3.set_odds1_zj(new decimal?(decimal.Parse(str85.ToString())));
                                _six3.set_odds2_zj(new decimal?(decimal.Parse(str86.ToString())));
                                builder.Append("insert into cz_splitgroup_six(");
                                builder.Append("item,odds_id,item_money,odds1,odds2,order_num,odds1_zj,odds2_zj)");
                                builder.Append(" values (");
                                builder.AppendFormat("'{0}',{1},{2},{3},{4},'{5}',{6},{7});", new object[] { _six3.get_item(), _six3.get_odds_id(), _six3.get_item_money(), _six3.get_odds1(), _six3.get_odds2(), _six2.get_order_num(), _six3.get_odds1_zj(), _six3.get_odds2_zj() });
                            }
                            else
                            {
                                string[] strArray24;
                                str82 = "";
                                str83 = "0";
                                str84 = strArray20[num42].Trim();
                                if (str84 == "")
                                {
                                    str82 = num37.ToString();
                                    str83 = num38.ToString();
                                }
                                else
                                {
                                    string str88 = str84.Split(new char[] { '|' })[1];
                                    strArray24 = str88.Split(new char[] { ',' });
                                    str82 = strArray24[0];
                                    if (strArray24.Length > 1)
                                    {
                                        str83 = strArray24[1];
                                    }
                                }
                                str85 = "";
                                str86 = "0";
                                str87 = strArray22[num42].Trim();
                                if (str87 == "")
                                {
                                    str85 = num39.ToString();
                                    str86 = num40.ToString();
                                }
                                else
                                {
                                    strArray24 = str87.Split(new char[] { '|' })[1].Split(new char[] { ',' });
                                    str85 = strArray24[0];
                                    if (strArray24.Length > 1)
                                    {
                                        str86 = strArray24[1];
                                    }
                                }
                                _six3 = new cz_splitgroup_six();
                                _six3.set_order_num(_six2.get_order_num());
                                _six3.set_odds1(new decimal?(decimal.Parse(str82.ToString())));
                                _six3.set_odds2(new decimal?(decimal.Parse(str83.ToString())));
                                _six3.set_odds_id(new int?(int.Parse(str5)));
                                _six3.set_item(str79);
                                _six3.set_item_money(new decimal?(decimal.Parse(num36.ToString())));
                                _six3.set_odds1_zj(new decimal?(decimal.Parse(str85.ToString())));
                                _six3.set_odds2_zj(new decimal?(decimal.Parse(str86.ToString())));
                                builder.Append("insert into cz_splitgroup_six(");
                                builder.Append("item,odds_id,item_money,odds1,odds2,order_num,odds1_zj,odds2_zj)");
                                builder.Append(" values (");
                                builder.AppendFormat("'{0}',{1},{2},{3},{4},'{5}',{6},{7});", new object[] { _six3.get_item(), _six3.get_odds_id(), _six3.get_item_money(), _six3.get_odds1(), _six3.get_odds2(), _six2.get_order_num(), _six3.get_odds1_zj(), _six3.get_odds2_zj() });
                            }
                            num42++;
                        }
                        SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@betString", ""), new SqlParameter("@groupString", builder.ToString()) };
                        DbHelperSQL_ADMIN.RunProcedure("ProcInserSixLMBet", parameterArray);
                    }
                    systemSet = CallBLL.cz_system_set_six_bll.GetSystemSet(100);
                    string str89 = "92285,92286,92287,92288,92289,92575,92638,92639,92640,92641,92642,92643";
                    if ((str89.IndexOf(str5) < 0) || systemSet.get_single_number_isdown().Equals(0))
                    {
                        num43 = (double.Parse(num29.ToString()) * num) / 100.0;
                        CallBLL.cz_odds_six_bll.UpdateGrandTotal(Convert.ToDecimal(num43), int.Parse(str5));
                        num44 = double.Parse(table7.Rows[0]["grand_total"].ToString()) + num43;
                        num45 = double.Parse(table7.Rows[0]["downbase"].ToString());
                        num46 = Math.Floor((double) (num44 / num45));
                        if (num46 >= 1.0)
                        {
                            num47 = double.Parse(table7.Rows[0]["down_odds_rate"].ToString()) * num46;
                            num48 = num44 - (num45 * num46);
                            string str90 = "";
                            string str91 = "";
                            if ((base.IsShowLM_B() == 1) && (("92285,92286,92287,92288,92289,92575".IndexOf(str5) > -1) || ("92638,92639,92640,92641,92642,92643".IndexOf(str5) > -1)))
                            {
                                Utils.Six_LM_AB(str5, ref str90, ref str91);
                            }
                            if (str31.Equals("1"))
                            {
                                if (!((CallBLL.cz_odds_six_bll.UpdateGrandTotalCurrentOdds_DoubleOdds(num47.ToString(), num48.ToString(), str5) <= 0) || string.IsNullOrEmpty(str90)))
                                {
                                    CallBLL.cz_odds_six_bll.UpdateGrandTotalCurrentOdds_DoubleOddsLM_AB(num47.ToString(), str90);
                                }
                            }
                            else if (!((CallBLL.cz_odds_six_bll.UpdateGrandTotalCurrentOdds(num47.ToString(), num48.ToString(), str5) <= 0) || string.IsNullOrEmpty(str90)))
                            {
                                CallBLL.cz_odds_six_bll.UpdateGrandTotalCurrentOddsLM_AB(num47.ToString(), str90);
                            }
                            _log = new cz_system_log();
                            _log.set_user_name("系統");
                            _log.set_children_name("");
                            _log.set_category(table7.Rows[0]["category"].ToString());
                            _log.set_play_name(str56);
                            _log.set_put_amount(str57);
                            num56 = 100;
                            _log.set_l_name(base.GetGameNameByID(num56.ToString()));
                            _log.set_l_phase(str15);
                            _log.set_action("降賠率");
                            _log.set_odds_id(int.Parse(str5));
                            str92 = table7.Rows[0]["current_odds"].ToString();
                            _log.set_old_val(str92);
                            if (str92.Split(new char[] { ',' }).Length > 1)
                            {
                                num58 = double.Parse(str92.Split(new char[] { ',' })[0]) - num47;
                                num58 = double.Parse(str92.Split(new char[] { ',' })[1]) - num47;
                                _log.set_new_val(num58.ToString() + "," + num58.ToString());
                            }
                            else
                            {
                                num58 = double.Parse(str92) - num47;
                                _log.set_new_val(num58.ToString());
                            }
                            _log.set_ip(LSRequest.GetIP());
                            _log.set_add_time(DateTime.Now);
                            _log.set_note("系統自動降賠");
                            _log.set_type_id(Convert.ToInt32((LSEnums.LogTypeID) 0));
                            _log.set_lottery_type(100);
                            CallBLL.cz_system_log_bll.Add(_log);
                            _odds = new cz_jp_odds();
                            _odds.set_add_time(DateTime.Now);
                            _odds.set_odds_id(int.Parse(str5));
                            _odds.set_phase_id(int.Parse(str16));
                            _odds.set_play_name(str56);
                            _odds.set_put_amount(str57);
                            _odds.set_odds(num47.ToString());
                            _odds.set_lottery_type(100);
                            _odds.set_phase(str15);
                            _odds.set_old_odds(str92);
                            if (str92.Split(new char[] { ',' }).Length > 1)
                            {
                                num58 = double.Parse(str92.Split(new char[] { ',' })[0]) - num47;
                                num58 = double.Parse(str92.Split(new char[] { ',' })[1]) - num47;
                                _odds.set_new_odds(num58.ToString() + "," + num58.ToString());
                            }
                            else
                            {
                                num58 = double.Parse(str92) - num47;
                                _odds.set_new_odds(num58.ToString());
                            }
                            CallBLL.cz_jp_odds_bll.Add(_odds);
                            if (!string.IsNullOrEmpty(str90))
                            {
                                DataTable table11 = CallBLL.cz_odds_six_bll.GetOddsByID(str90).Tables[0];
                                string str93 = table11.Rows[0]["o_play_name"].ToString();
                                string str94 = table11.Rows[0]["put_amount"].ToString();
                                _log2 = new cz_system_log();
                                _log2.set_user_name("系統");
                                _log2.set_children_name("");
                                _log2.set_category(table11.Rows[0]["category"].ToString());
                                _log2.set_play_name(str93);
                                _log2.set_put_amount(str94);
                                num56 = 100;
                                _log2.set_l_name(base.GetGameNameByID(num56.ToString()));
                                _log2.set_l_phase(str15);
                                _log2.set_action("降賠率");
                                _log2.set_odds_id(int.Parse(str90));
                                str95 = table11.Rows[0]["current_odds"].ToString();
                                _log2.set_old_val(str95);
                                if (str95.Split(new char[] { ',' }).Length > 1)
                                {
                                    num58 = double.Parse(str95.Split(new char[] { ',' })[0]) - num47;
                                    num58 = double.Parse(str95.Split(new char[] { ',' })[1]) - num47;
                                    _log2.set_new_val(num58.ToString() + "," + num58.ToString());
                                }
                                else
                                {
                                    num58 = double.Parse(str95) - num47;
                                    _log2.set_new_val(num58.ToString());
                                }
                                _log2.set_ip(LSRequest.GetIP());
                                _log2.set_add_time(DateTime.Now);
                                _log2.set_note("系統自動降賠(聯動)");
                                _log2.set_type_id(Convert.ToInt32((LSEnums.LogTypeID) 0));
                                _log2.set_lottery_type(100);
                                CallBLL.cz_system_log_bll.Add(_log2);
                                _odds2 = new cz_jp_odds();
                                _odds2.set_add_time(DateTime.Now);
                                _odds2.set_odds_id(int.Parse(str90));
                                _odds2.set_phase_id(int.Parse(str16));
                                _odds2.set_play_name(str93);
                                _odds2.set_put_amount(str94);
                                _odds2.set_odds(num47.ToString());
                                _odds2.set_lottery_type(100);
                                _odds2.set_phase(str15);
                                _odds2.set_old_odds(str95);
                                if (str95.Split(new char[] { ',' }).Length > 1)
                                {
                                    num58 = double.Parse(str95.Split(new char[] { ',' })[0]) - num47;
                                    num58 = double.Parse(str95.Split(new char[] { ',' })[1]) - num47;
                                    _odds2.set_new_odds(num58.ToString() + "," + num58.ToString());
                                }
                                else
                                {
                                    num58 = double.Parse(str95) - num47;
                                    _odds2.set_new_odds(num58.ToString());
                                }
                                CallBLL.cz_jp_odds_bll.Add(_odds2);
                            }
                        }
                    }
                    string str96 = "";
                    string str97 = "";
                    string[] strArray25 = str36.Replace("00", "10").Split(new char[] { '~' });
                    int num51 = 0;
                    foreach (string str62 in strArray25)
                    {
                        if (num51 == 0)
                        {
                            str97 = str97 + "'" + Utils.px(str62) + "'";
                            str96 = str96 + Utils.px(str62);
                        }
                        else
                        {
                            str97 = str97 + ",'" + Utils.px(str62) + "'";
                            str96 = str96 + "~" + Utils.px(str62);
                        }
                        num51++;
                    }
                    fgsWTTable = null;
                    if (getUserModelInfo.get_six_op_odds().Equals(1))
                    {
                        fgsWTTable = base.GetFgsWTTable(100);
                    }
                    CallBLL.cz_autosale_six_bll.DLAutoSaleLM(_six2.get_order_num(), str14, getUserModelInfo.get_u_nicker(), str17, table7.Rows[0]["play_id"].ToString(), str5, str19, str20, str21, str22, str23, str24, str16, str15, _six2.get_ip(), num5, _six2.get_lottery_name(), getUserModelInfo, rate, myDT, base.get_YearLianID(), 0, fgsWTTable, str96, str97);
                    if (getUserModelInfo.get_six_op_odds().Equals(1))
                    {
                        fgsWTTable = base.GetFgsWTTable(100);
                    }
                    CallBLL.cz_autosale_six_bll.ZDAutoSaleLM(_six2.get_order_num(), str14, getUserModelInfo.get_u_nicker(), str17, table7.Rows[0]["play_id"].ToString(), str5, str19, str20, str21, str22, str23, str24, str16, str15, _six2.get_ip(), num5, _six2.get_lottery_name(), getUserModelInfo, rate, myDT, base.get_YearLianID(), 0, fgsWTTable, str96, str97);
                    if (getUserModelInfo.get_six_op_odds().Equals(1))
                    {
                        fgsWTTable = base.GetFgsWTTable(100);
                    }
                    CallBLL.cz_autosale_six_bll.GDAutoSaleLM(_six2.get_order_num(), str14, getUserModelInfo.get_u_nicker(), str17, table7.Rows[0]["play_id"].ToString(), str5, str19, str20, str21, str22, str23, str24, str16, str15, _six2.get_ip(), num5, _six2.get_lottery_name(), getUserModelInfo, rate, myDT, base.get_YearLianID(), 0, fgsWTTable, str96, str97);
                    if (getUserModelInfo.get_six_op_odds().Equals(1))
                    {
                        fgsWTTable = base.GetFgsWTTable(100);
                    }
                    CallBLL.cz_autosale_six_bll.FGSAutoSaleLM(_six2.get_order_num(), str14, getUserModelInfo.get_u_nicker(), str17, table7.Rows[0]["play_id"].ToString(), str5, str19, str20, str21, str22, str23, str24, str16, str15, _six2.get_ip(), num5, _six2.get_lottery_name(), getUserModelInfo, rate, myDT, base.get_YearLianID(), 0, fgsWTTable, str96, str97);
                    if (systemSet.get_single_number_isdown().Equals(1) && (str89.IndexOf(str5) > -1))
                    {
                        num44 = double.Parse(table7.Rows[0]["grand_total"].ToString());
                        num45 = double.Parse(table7.Rows[0]["downbase"].ToString());
                        num47 = double.Parse(table7.Rows[0]["down_odds_rate"].ToString());
                        List<string> values = new List<string>();
                        double num52 = ((((double) _six2.get_amount().Value) / ((double) _six2.get_unit_cnt().Value)) * num) / 100.0;
                        bool flag6 = CallBLL.cz_lm_number_amount_six_bll.SetLMSingleNumber(int.Parse(_six2.get_odds_id().ToString()), _six2.get_bet_group(), num52, ref values);
                        string str98 = string.Join(",", values);
                        DataSet set9 = CallBLL.cz_lm_number_amount_six_bll.GetListByNumber(str5, str98, num45);
                        if (set9 != null)
                        {
                            string str106;
                            string category = _six2.get_category();
                            string str100 = _six2.get_play_name();
                            string str101 = str57;
                            string phase = _six2.get_phase();
                            string str103 = _six2.get_phase_id().ToString();
                            DataTable numberAmountTableDT = set9.Tables["numberAmountTable"];
                            DataTable wtTableDT = set9.Tables["wtTable"];
                            DataTable table15 = set9.Tables["oddsTable"];
                            string str104 = table15.Rows[0]["isDoubleOdds"].ToString();
                            string str105 = table15.Rows[0][str17 + "_diff"].ToString();
                            if (str104.Equals("1"))
                            {
                                str68 = str26.Split(new char[] { ',' })[0];
                                str69 = str26.Split(new char[] { ',' })[1];
                                str70 = "";
                                str71 = "";
                                str70 = str55.Split(new char[] { ',' })[0];
                                str71 = str70;
                                if (str55.Split(new char[] { ',' }).Length > 1)
                                {
                                    str71 = str55.Split(new char[] { ',' })[1];
                                }
                                str106 = (Convert.ToDouble(str68) + Convert.ToDouble(str70)) + "," + (Convert.ToDouble(str69) + Convert.ToDouble(str71));
                                string str107 = table15.Rows[0]["min_odds"].ToString();
                                base.GetOdds_SIX(str5, str26, table15.Rows[0][str17 + "_diff"].ToString(), ref str106);
                                this.SetWtValueDuble(numberAmountTableDT, wtTableDT, str5, str107, str106, num44, num45, num47, category, str100, str101, phase, str103);
                            }
                            else
                            {
                                string str108 = table15.Rows[0]["current_odds"].ToString();
                                string str109 = table15.Rows[0]["min_odds"].ToString();
                                str106 = (Convert.ToDouble(str108) + Convert.ToDouble(str105)).ToString();
                                base.GetOdds_SIX(str5, str108, table15.Rows[0][str17 + "_diff"].ToString(), ref str106);
                                if (double.Parse(str109) < double.Parse(str106))
                                {
                                    this.SetWtValue(numberAmountTableDT, wtTableDT, str5, double.Parse(str109), double.Parse(str106), num44, num45, num47, category, str100, str101, phase, str103);
                                }
                            }
                        }
                    }
                    base.SetUserRate_six(rate);
                    if (FileCacheHelper.get_GetSixPutMoneyCache() == "1")
                    {
                        base.SetUserDrawback_six(ds, num28.ToString());
                    }
                    else
                    {
                        base.SetUserDrawback_six(ds);
                    }
                    getUserModelInfo.set_begin_six("yes");
                    base.DeleteCreditLock(str);
                    base.Response.Write("<script>window.location=\"tj_ok.aspx?lottery_type=" + str9 + "&game_type=" + str11 + "&player_type=" + str10 + "\";</script>");
                    base.Response.End();
                }
            }
        }
    }

    private static string px(string p_str)
    {
        string[] array = p_str.Split(new char[] { ',' });
        Array.Sort<string>(array);
        return string.Join(",", array.ToArray<string>());
    }

    private void SetWtValue(DataTable numberAmountTableDT, DataTable wtTableDT, string odds_id, double min_odds, double current_odds, double p_dqhl, double p_jphl, double p_pljd, string category, string play_name, string put_amount, string phase, string phase_id)
    {
        foreach (DataRow row in numberAmountTableDT.Rows)
        {
            int num = int.Parse(row["number"].ToString());
            double num2 = double.Parse(row["amount"].ToString());
            double num3 = double.Parse(wtTableDT.Select(string.Format(" number={0} ", num))[0]["wt_value"].ToString());
            double num4 = p_jphl;
            double num5 = p_dqhl;
            double num6 = p_pljd;
            double num7 = Math.Floor((double) (num2 / num4));
            if (num7 >= 1.0)
            {
                num6 *= num7;
                double num8 = num4 * num7;
                double num9 = num3 - num6;
                if ((num9 + current_odds) > min_odds)
                {
                    CallBLL.cz_lm_number_amount_six_bll.SetNumberWTValue(int.Parse(odds_id), num, num9, num8);
                }
                else if (current_odds > min_odds)
                {
                    num9 = -(current_odds - min_odds);
                    CallBLL.cz_lm_number_amount_six_bll.SetNumberWTValue(int.Parse(odds_id), num, num9, num8);
                }
                this.SetWtValueLog(odds_id, num3, num9, category, play_name, put_amount, phase, phase_id, num.ToString());
            }
        }
    }

    private void SetWtValueDuble(DataTable numberAmountTableDT, DataTable wtTableDT, string odds_id, string min_odds, string current_odds, double p_dqhl, double p_jphl, double p_pljd, string category, string play_name, string put_amount, string phase, string phase_id)
    {
        foreach (DataRow row in numberAmountTableDT.Rows)
        {
            int num = int.Parse(row["number"].ToString());
            double num2 = double.Parse(row["amount"].ToString());
            double num3 = double.Parse(wtTableDT.Select(string.Format(" number={0} ", num))[0]["wt_value"].ToString());
            double num4 = p_jphl;
            double num5 = p_dqhl;
            double num6 = p_pljd;
            double num7 = Math.Floor((double) (num2 / num4));
            if (num7 >= 1.0)
            {
                num6 *= num7;
                double num8 = num4 * num7;
                double num9 = num3 - num6;
                double num10 = double.Parse(min_odds.Split(new char[] { ',' })[0]);
                double num11 = double.Parse(min_odds.Split(new char[] { ',' })[1]);
                double num12 = double.Parse(current_odds.Split(new char[] { ',' })[0]);
                double num13 = double.Parse(current_odds.Split(new char[] { ',' })[1]);
                if (((num9 + num12) > num10) && ((num9 + num13) > num11))
                {
                    CallBLL.cz_lm_number_amount_six_bll.SetNumberWTValue(int.Parse(odds_id), num, num9, num8);
                }
                else if ((num12 > num10) && (num13 > num11))
                {
                    if ((num12 - num10) > (num13 - num11))
                    {
                        num9 = -(num13 - num11);
                    }
                    else
                    {
                        num9 = -(num12 - num10);
                    }
                    CallBLL.cz_lm_number_amount_six_bll.SetNumberWTValue(int.Parse(odds_id), num, num9, num8);
                }
                this.SetWtValueLog(odds_id, num3, num9, category, play_name, put_amount, phase, phase_id, num.ToString());
            }
        }
    }

    private void SetWtValueLog(string odds_id, double old_val, double new_val, string category, string play_name, string put_amount, string phase, string phase_id, string number)
    {
        cz_system_log _log = new cz_system_log();
        _log.set_user_name("系統");
        _log.set_children_name("");
        _log.set_category(category);
        _log.set_play_name(play_name);
        _log.set_put_amount(put_amount);
        int num = 100;
        _log.set_l_name(base.GetGameNameByID(num.ToString()));
        _log.set_l_phase(phase);
        _log.set_action("降賠率");
        _log.set_odds_id(int.Parse(odds_id));
        _log.set_old_val(string.Format("號碼【{0}】：{1}", number, old_val));
        _log.set_new_val(string.Format("號碼【{0}】：{1}", number, new_val));
        _log.set_ip(LSRequest.GetIP());
        _log.set_add_time(DateTime.Now);
        _log.set_note("系統自動降賠");
        _log.set_type_id(Convert.ToInt32((LSEnums.LogTypeID) 0));
        _log.set_lottery_type(100);
        CallBLL.cz_system_log_bll.Add(_log);
        cz_jp_odds _odds = new cz_jp_odds();
        _odds.set_add_time(DateTime.Now);
        _odds.set_odds_id(int.Parse(odds_id));
        _odds.set_phase_id(int.Parse(phase_id));
        _odds.set_play_name(play_name);
        _odds.set_put_amount(put_amount);
        _odds.set_odds((old_val - new_val).ToString());
        _odds.set_lottery_type(100);
        _odds.set_phase(phase);
        _odds.set_old_odds(old_val.ToString());
        _odds.set_new_odds(new_val.ToString());
        _odds.set_number(number);
        CallBLL.cz_jp_odds_bll.Add(_odds);
    }
}

