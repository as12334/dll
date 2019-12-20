using LotterySystem.Common;
using LotterySystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using User.Web.WebBase;

public class GetOdds_SIX : MemberPageBase_Mobile
{
    private List<string> oddsList = new List<string>();
    private DataTable phaseDT = null;

    protected bool isOpenLottery(cz_phase_six openPhase)
    {
        DateTime now = DateTime.Now;
        string str = "n";
        DateTime time2 = Convert.ToDateTime(openPhase.get_stop_date());
        DateTime time3 = Convert.ToDateTime(openPhase.get_sn_stop_date());
        if (time2 >= now)
        {
            str = "y";
        }
        if (time3 >= now)
        {
            str = "y";
        }
        if (openPhase.get_is_closed().ToString().Equals("1") || str.Equals("n"))
        {
            return false;
        }
        return true;
    }

    protected void out_six_odds_info()
    {
        string str = LSRequest.qq("playid");
        string str2 = LSRequest.qq("playpage");
        if (string.IsNullOrEmpty(str))
        {
            base.Response.End();
        }
        else
        {
            string str3 = this.Session["user_name"].ToString();
            cz_userinfo_session getUserModelInfo = base.GetUserModelInfo;
            string str4 = getUserModelInfo.get_su_type().ToString();
            string str5 = getUserModelInfo.get_u_name().ToString();
            string str6 = getUserModelInfo.get_six_kind().Trim();
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("type", "get_oddsinfo");
            dictionary.Add("playpage", str2);
            DataTable table = CallBLL.cz_users_bll.GetCredit(str5, str4).Tables[0];
            string str7 = table.Rows[0]["six_credit"].ToString();
            string str8 = table.Rows[0]["six_iscash"];
            decimal num = Convert.ToDecimal(str7);
            string str9 = "0";
            if (str8.Equals("1"))
            {
                str9 = "1";
            }
            decimal num2 = Convert.ToDecimal(table.Rows[0]["six_usable_credit"].ToString());
            dictionary.Add("iscash", str9);
            dictionary.Add("credit", num.ToString("F1"));
            dictionary.Add("usable_credit", num2.ToString("F1"));
            cz_phase_six currentPhase = CallBLL.cz_phase_six_bll.GetCurrentPhase();
            if (!this.isOpenLottery(currentPhase))
            {
                dictionary.Add("status", "3");
                base.OutJson(JsonHandle.ObjectToJson(dictionary));
            }
            else
            {
                DateTime now = DateTime.Now;
                string str11 = "n";
                string str12 = "0";
                string str13 = "";
                string str14 = currentPhase.get_open_date().ToString();
                string str15 = "";
                DateTime time2 = Convert.ToDateTime(currentPhase.get_stop_date());
                DateTime time3 = Convert.ToDateTime(currentPhase.get_sn_stop_date());
                TimeSpan span = Utils.DateDiff(time2, now);
                TimeSpan span2 = Utils.DateDiff(time3, now);
                string str16 = string.Concat(new object[] { (span.Days * 0x18) + span.Hours, ":", span.Minutes, ":", span.Seconds });
                string str17 = string.Concat(new object[] { (span2.Days * 0x18) + span2.Hours, ":", span2.Minutes, ":", span2.Seconds });
                if (time2 < now)
                {
                    str15 = str16;
                }
                else
                {
                    str11 = "y";
                    str12 = "1";
                    str15 = str16;
                }
                if (time3 < now)
                {
                    str13 = str17;
                }
                else
                {
                    str11 = "y";
                    str12 = "1";
                    str13 = str17;
                }
                if ((currentPhase != null) && currentPhase.get_is_closed().Equals(0))
                {
                    int num3;
                    if (time2 < now)
                    {
                        num3 = CallBLL.cz_odds_six_bll.UpdateIsOpen(1, "91001,91002,91003,91004,91005,91007,91038");
                    }
                    if (time3 < now)
                    {
                        num3 = CallBLL.cz_odds_six_bll.UpdateIsOpen(0, "");
                    }
                }
                string str18 = (currentPhase != null) ? currentPhase.get_phase() : "";
                string str19 = (currentPhase != null) ? currentPhase.get_p_id().ToString() : "";
                dictionary.Add("openning", str11);
                dictionary.Add("isopen", str12);
                int index = str14.IndexOf(' ');
                dictionary.Add("drawopen_time", str14.Substring(index));
                string str20 = "";
                string[] strArray = str13.Split(new char[] { ':' });
                str20 = (Convert.ToInt32(strArray[0]) < 10) ? ("0" + strArray[0]) : strArray[0];
                str20 = (str20 + ":" + ((Convert.ToInt32(strArray[1]) < 10) ? ("0" + strArray[1]) : strArray[1])) + ":" + ((Convert.ToInt32(strArray[2]) < 10) ? ("0" + strArray[2]) : strArray[2]);
                dictionary.Add("stop_time", str20);
                dictionary.Add("nn", str18);
                dictionary.Add("p_id", str19);
                dictionary.Add("profit", "");
                DataSet play = CallBLL.cz_play_six_bll.GetPlay(str, str4);
                if (play == null)
                {
                    base.Response.End();
                }
                else
                {
                    DataTable table2 = play.Tables["odds"];
                    if (table2 == null)
                    {
                        base.Response.End();
                    }
                    else
                    {
                        DataSet cache;
                        DataTable table3 = null;
                        DataTable table4 = null;
                        DataTable table5 = null;
                        DataTable table6 = null;
                        DataTable table7 = null;
                        DataTable table8 = null;
                        DataTable wT = null;
                        DataTable table10 = null;
                        DataTable table11 = null;
                        DataTable table12 = null;
                        DataTable table13 = null;
                        DataTable table14 = null;
                        DataTable table15 = null;
                        DataTable table16 = null;
                        DataTable table17 = null;
                        DataTable table18 = null;
                        DataTable table19 = null;
                        DataTable table20 = null;
                        DataTable table21 = null;
                        DataTable table22 = null;
                        string str37 = str2;
                        if (str37 != null)
                        {
                            if (!(str37 == "six_lm"))
                            {
                                if (str37 == "six_lm_b")
                                {
                                    wT = CallBLL.cz_wt_3qz_b_six_bll.GetWT();
                                    table10 = CallBLL.cz_wt_3z2_b_six_bll.GetWT();
                                    table11 = CallBLL.cz_wt_2qz_b_six_bll.GetWT();
                                    table12 = CallBLL.cz_wt_2zt_b_six_bll.GetWT();
                                    table13 = CallBLL.cz_wt_tc_b_six_bll.GetWT();
                                    table14 = CallBLL.cz_wt_4z1_b_six_bll.GetWT();
                                }
                                else if (str37 == "six_bz")
                                {
                                    table15 = CallBLL.cz_wt_5bz_six_bll.GetWT();
                                    table16 = CallBLL.cz_wt_6bz_six_bll.GetWT();
                                    table17 = CallBLL.cz_wt_7bz_six_bll.GetWT();
                                    table18 = CallBLL.cz_wt_8bz_six_bll.GetWT();
                                    table19 = CallBLL.cz_wt_9bz_six_bll.GetWT();
                                    table20 = CallBLL.cz_wt_10bz_six_bll.GetWT();
                                }
                                else if (str37 == "six_lx")
                                {
                                    table21 = CallBLL.cz_wt_sxlwsl_six_bll.GetWT();
                                    table22 = CallBLL.cz_wt_6xyz_six_bll.GetWT();
                                }
                                else if (str37 == "six_ws")
                                {
                                    table21 = CallBLL.cz_wt_sxlwsl_six_bll.GetWT();
                                }
                            }
                            else
                            {
                                table3 = CallBLL.cz_wt_3qz_six_bll.GetWT();
                                table4 = CallBLL.cz_wt_3z2_six_bll.GetWT();
                                table5 = CallBLL.cz_wt_2qz_six_bll.GetWT();
                                table6 = CallBLL.cz_wt_2zt_six_bll.GetWT();
                                table7 = CallBLL.cz_wt_tc_six_bll.GetWT();
                                table8 = CallBLL.cz_wt_4z1_six_bll.GetWT();
                            }
                        }
                        DataTable drawbackByPlayIds = null;
                        if (FileCacheHelper.get_GetSixPutMoneyCache() == "1")
                        {
                            foreach (string str21 in str.Split(new char[] { ',' }))
                            {
                                DataTable table24;
                                if (CacheHelper.GetCache("six_drawback_FileCacheKey" + str21 + this.Session["user_name"].ToString()) != null)
                                {
                                    cache = CacheHelper.GetCache("six_drawback_FileCacheKey" + str21 + this.Session["user_name"].ToString()) as DataSet;
                                    if (drawbackByPlayIds == null)
                                    {
                                        drawbackByPlayIds = cache.Tables[0];
                                    }
                                    else
                                    {
                                        table24 = drawbackByPlayIds.Clone();
                                        table24 = cache.Tables[0];
                                        if (table24 != null)
                                        {
                                            drawbackByPlayIds.Merge(table24);
                                        }
                                    }
                                }
                                else if (drawbackByPlayIds == null)
                                {
                                    drawbackByPlayIds = CallBLL.cz_drawback_six_bll.GetDrawbackByPlayIds(str21, getUserModelInfo.get_u_name());
                                }
                                else
                                {
                                    table24 = drawbackByPlayIds.Clone();
                                    table24 = CallBLL.cz_drawback_six_bll.GetDrawbackByPlayIds(str21, getUserModelInfo.get_u_name());
                                    if (table24 != null)
                                    {
                                        drawbackByPlayIds.Merge(table24);
                                    }
                                }
                            }
                        }
                        else if (CacheHelper.GetCache("six_drawback_FileCacheKey" + this.Session["user_name"].ToString()) != null)
                        {
                            cache = CacheHelper.GetCache("six_drawback_FileCacheKey" + this.Session["user_name"].ToString()) as DataSet;
                            drawbackByPlayIds = cache.Tables[0];
                        }
                        else
                        {
                            drawbackByPlayIds = CallBLL.cz_drawback_six_bll.GetDrawbackByPlayIds(str, getUserModelInfo.get_u_name());
                        }
                        Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                        foreach (DataRow row in table2.Rows)
                        {
                            string key = "";
                            string pl = "";
                            string s = "";
                            string str25 = "";
                            string str26 = "";
                            string str27 = "";
                            string str28 = "";
                            List<double> source = new List<double>();
                            Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
                            key = row["play_id"].ToString() + "_" + row["odds_id"].ToString();
                            string str29 = row["current_odds"].ToString();
                            string str30 = row[str6 + "_diff"].ToString().Trim();
                            if (!row["is_open"].ToString().Equals("0"))
                            {
                                try
                                {
                                    string[] strArray3 = row["current_odds"].ToString().Split(new char[] { ',' });
                                    string[] strArray4 = row[str6 + "_diff"].ToString().Trim().Split(new char[] { ',' });
                                    base.GetOdds_SIX(row["odds_id"].ToString(), row["current_odds"].ToString(), row[str6 + "_diff"].ToString(), ref pl);
                                }
                                catch (Exception exception)
                                {
                                    string message = exception.Message;
                                }
                            }
                            else if (row["current_odds"].ToString().Split(new char[] { ',' }).Length > 1)
                            {
                                pl = "-,-";
                            }
                            else
                            {
                                pl = "-";
                            }
                            DataRow[] rowArray = drawbackByPlayIds.Select(string.Format(" play_id={0} and u_name='{1}' ", row["play_id"].ToString(), getUserModelInfo.get_u_name()));
                            string str32 = rowArray[0]["single_phase_amount"].ToString();
                            string str33 = rowArray[0]["single_max_amount"].ToString();
                            string str34 = rowArray[0]["single_min_amount"].ToString();
                            s = row["allow_min_amount"].ToString();
                            str25 = row["allow_max_amount"].ToString();
                            str26 = row["max_appoint"].ToString();
                            str27 = row["total_amount"].ToString();
                            str28 = row["allow_max_put_amount"].ToString();
                            if (Convert.ToDecimal(str25) > Convert.ToDecimal(str28))
                            {
                                str25 = row["allow_max_put_amount"].ToString();
                            }
                            if (Convert.ToDecimal(str25) > Convert.ToDecimal(str27))
                            {
                                str25 = row["total_amount"].ToString();
                            }
                            if (double.Parse(s) < double.Parse(str34))
                            {
                                s = str34;
                            }
                            if (double.Parse(str25) > double.Parse(str33))
                            {
                                str25 = str33;
                            }
                            if ((str2.Equals("six_lm") || str2.Equals("six_lm_b")) || str2.Equals("six_bz"))
                            {
                                switch (row["play_id"].ToString())
                                {
                                    case "91016":
                                        foreach (DataRow row2 in table3.Rows)
                                        {
                                            source.Add(double.Parse(row2["wt_value"].ToString()));
                                        }
                                        if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                        {
                                            source.Clear();
                                        }
                                        break;

                                    case "91017":
                                        foreach (DataRow row2 in table4.Rows)
                                        {
                                            source.Add(double.Parse(row2["wt_value"].ToString()));
                                        }
                                        if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                        {
                                            source.Clear();
                                        }
                                        break;

                                    case "91018":
                                        foreach (DataRow row2 in table5.Rows)
                                        {
                                            source.Add(double.Parse(row2["wt_value"].ToString()));
                                        }
                                        if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                        {
                                            source.Clear();
                                        }
                                        break;

                                    case "91019":
                                        foreach (DataRow row2 in table6.Rows)
                                        {
                                            source.Add(double.Parse(row2["wt_value"].ToString()));
                                        }
                                        if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                        {
                                            source.Clear();
                                        }
                                        break;

                                    case "91020":
                                        foreach (DataRow row2 in table7.Rows)
                                        {
                                            source.Add(double.Parse(row2["wt_value"].ToString()));
                                        }
                                        if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                        {
                                            source.Clear();
                                        }
                                        break;

                                    case "91040":
                                        foreach (DataRow row2 in table8.Rows)
                                        {
                                            source.Add(double.Parse(row2["wt_value"].ToString()));
                                        }
                                        if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                        {
                                            source.Clear();
                                        }
                                        break;

                                    case "91060":
                                        foreach (DataRow row2 in wT.Rows)
                                        {
                                            source.Add(double.Parse(row2["wt_value"].ToString()));
                                        }
                                        if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                        {
                                            source.Clear();
                                        }
                                        break;

                                    case "91061":
                                        foreach (DataRow row2 in table10.Rows)
                                        {
                                            source.Add(double.Parse(row2["wt_value"].ToString()));
                                        }
                                        if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                        {
                                            source.Clear();
                                        }
                                        break;

                                    case "91062":
                                        foreach (DataRow row2 in table11.Rows)
                                        {
                                            source.Add(double.Parse(row2["wt_value"].ToString()));
                                        }
                                        if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                        {
                                            source.Clear();
                                        }
                                        break;

                                    case "91063":
                                        foreach (DataRow row2 in table12.Rows)
                                        {
                                            source.Add(double.Parse(row2["wt_value"].ToString()));
                                        }
                                        if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                        {
                                            source.Clear();
                                        }
                                        break;

                                    case "91064":
                                        foreach (DataRow row2 in table13.Rows)
                                        {
                                            source.Add(double.Parse(row2["wt_value"].ToString()));
                                        }
                                        if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                        {
                                            source.Clear();
                                        }
                                        break;

                                    case "91065":
                                        foreach (DataRow row2 in table14.Rows)
                                        {
                                            source.Add(double.Parse(row2["wt_value"].ToString()));
                                        }
                                        if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                        {
                                            source.Clear();
                                        }
                                        break;

                                    case "91037":
                                        foreach (DataRow row2 in table15.Rows)
                                        {
                                            source.Add(double.Parse(row2["wt_value"].ToString()));
                                        }
                                        if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                        {
                                            source.Clear();
                                        }
                                        break;

                                    case "91047":
                                        foreach (DataRow row2 in table16.Rows)
                                        {
                                            source.Add(double.Parse(row2["wt_value"].ToString()));
                                        }
                                        if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                        {
                                            source.Clear();
                                        }
                                        break;

                                    case "91048":
                                        foreach (DataRow row2 in table17.Rows)
                                        {
                                            source.Add(double.Parse(row2["wt_value"].ToString()));
                                        }
                                        if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                        {
                                            source.Clear();
                                        }
                                        break;

                                    case "91049":
                                        foreach (DataRow row2 in table18.Rows)
                                        {
                                            source.Add(double.Parse(row2["wt_value"].ToString()));
                                        }
                                        if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                        {
                                            source.Clear();
                                        }
                                        break;

                                    case "91050":
                                        foreach (DataRow row2 in table19.Rows)
                                        {
                                            source.Add(double.Parse(row2["wt_value"].ToString()));
                                        }
                                        if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                        {
                                            source.Clear();
                                        }
                                        break;

                                    case "91051":
                                        foreach (DataRow row2 in table20.Rows)
                                        {
                                            source.Add(double.Parse(row2["wt_value"].ToString()));
                                        }
                                        if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                        {
                                            source.Clear();
                                        }
                                        break;
                                }
                            }
                            if (str2.Equals("six_lx"))
                            {
                                string str36 = row["odds_id"].ToString();
                                if (str36.Equals("92565"))
                                {
                                    foreach (DataRow row2 in table22.Rows)
                                    {
                                        source.Add(double.Parse(row2["wt_value"].ToString()));
                                    }
                                    if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                    {
                                        source.Clear();
                                    }
                                }
                                else
                                {
                                    DataRow[] rowArray2 = table21.Select(string.Format(" odds_id={0} ", Convert.ToInt32(str36)));
                                    foreach (DataRow row2 in rowArray2)
                                    {
                                        source.Add(double.Parse(row2["wt_value"].ToString()));
                                    }
                                    if (source.Count<double>(p => (p == 0.0)) == source.Count)
                                    {
                                        source.Clear();
                                    }
                                }
                            }
                            dictionary3.Add("pl", pl);
                            dictionary3.Add("plx", new List<double>(source));
                            dictionary3.Add("min_amount", s);
                            dictionary3.Add("max_amount", str25);
                            dictionary3.Add("top_amount", str26);
                            dictionary3.Add("dq_max_amount", str27);
                            dictionary3.Add("dh_max_amount", str28);
                            dictionary2.Add(key, new Dictionary<string, object>(dictionary3));
                        }
                        dictionary.Add("status", "1");
                        dictionary.Add("play_odds", dictionary2);
                        base.OutJson(JsonHandle.ObjectToJson(dictionary));
                    }
                }
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        IDictionary<string, object> dictionary = new Dictionary<string, object>();
        if (!base.IsUserLoginByMobileForAjax())
        {
            dictionary.Add("status", "2");
            base.OutJson(JsonHandle.ObjectToJson(dictionary));
        }
        else
        {
            this.out_six_odds_info();
        }
    }
}

