using LotterySystem.Common;
using LotterySystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using User.Web.WebBase;

public class GetOdds_pkbjl : MemberPageBase_Mobile
{
    private List<string> oddsList = new List<string>();
    private DataTable phaseDT = null;

    protected bool isOpenLottery()
    {
        if (this.phaseDT.Rows[0]["isopen"].ToString() == "0")
        {
            return false;
        }
        return true;
    }

    private void out_odds_info()
    {
        string str = LSRequest.qq("GT");
        string str2 = LSRequest.qq("player_type");
        string str3 = "";
        str3 = Utils.GetPKBJL_NumTable(str2);
        string str4 = LSRequest.qq("playtype");
        if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(str3))
        {
            base.Response.End();
        }
        else if (!string.IsNullOrEmpty(str4) && ((str4 != "0") && (str4 != "1")))
        {
            base.Response.End();
        }
        else
        {
            string str5 = this.Session["user_name"].ToString();
            cz_userinfo_session getUserModelInfo = base.GetUserModelInfo;
            string str6 = getUserModelInfo.get_su_type().ToString();
            string str7 = getUserModelInfo.get_u_name().ToString();
            string str8 = getUserModelInfo.get_kc_kind().Trim();
            DataTable table = CallBLL.cz_users_bll.GetCredit(str5, str6).Tables[0];
            if (table == null)
            {
                base.Response.End();
            }
            else
            {
                double num = double.Parse(table.Rows[0]["kc_usable_credit"].ToString());
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("type", "get_oddsinfo");
                DataSet set = CallBLL.cz_play_pkbjl_bll.GetPlay(str, str7, str6, str3);
                if (set == null)
                {
                    base.Response.End();
                }
                else
                {
                    this.phaseDT = set.Tables["phase"];
                    DataTable table2 = set.Tables["odds"];
                    if ((this.phaseDT == null) || (table2 == null))
                    {
                        base.Response.End();
                    }
                    else if (!this.isOpenLottery())
                    {
                        dictionary.Add("status", "3");
                        base.OutJson(JsonHandle.ObjectToJson(dictionary));
                    }
                    else
                    {
                        DataRow row = this.phaseDT.Rows[0];
                        string str9 = row["openning"].ToString();
                        string str10 = row["isopen"].ToString();
                        string str11 = row["opendate"].ToString();
                        string str12 = row["endtime"].ToString();
                        string str13 = row["nn"].ToString();
                        string str14 = row["p_id"].ToString();
                        double num2 = double.Parse(base.GetKCProfit());
                        if (str9.Equals("n") && string.IsNullOrEmpty(CacheHelper.GetCache(string.Concat(new object[] { "Poker_", 8, "_", str14, "_", str3 }))))
                        {
                            int num3 = 8;
                            base.Add_TenPoker_Lock(num3.ToString(), str14, str3);
                            cz_phase_pkbjl tenPoker = CallBLL.cz_phase_pkbjl_bll.GetTenPoker(str14, str3);
                            if ((tenPoker == null) || string.IsNullOrEmpty(tenPoker.get_ten_poker()))
                            {
                                CallBLL.cz_poker_pkbjl_bll.InitToPhase(str3, str14);
                            }
                            base.Un_TenPoker_Lock(8.ToString(), str14, str3);
                        }
                        dictionary.Add("status", "1");
                        dictionary.Add("open", str9);
                        dictionary.Add("k_open_time", str11);
                        dictionary.Add("k_stop_time", str12);
                        dictionary.Add("qishu", str13);
                        dictionary.Add("credit", num.ToString("F1"));
                        dictionary.Add("amount", num2.ToString("F1"));
                        dictionary.Add("k_id", str14);
                        DataTable newOpenPhaseByNumTable = CallBLL.cz_phase_pkbjl_bll.GetNewOpenPhaseByNumTable(str3);
                        dictionary.Add("porkList", newOpenPhaseByNumTable.Rows[0]["ten_poker"].ToString());
                        Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
                        dictionary2.Add("player", newOpenPhaseByNumTable.Rows[0]["xian_nn"].ToString());
                        dictionary2.Add("banker", newOpenPhaseByNumTable.Rows[0]["zhuang_nn"].ToString());
                        dictionary.Add("lastResult", dictionary2);
                        dictionary.Add("road", base.GetPAILU(str3));
                        Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
                        Dictionary<string, object> dictionary4 = new Dictionary<string, object>();
                        string str16 = "";
                        string pl = "";
                        List<double> list = new List<double>();
                        foreach (DataRow row2 in table2.Rows)
                        {
                            double num4;
                            str16 = row2["play_id"].ToString() + "_" + row2["odds_id"].ToString();
                            string s = row2["current_odds"].ToString();
                            if (str9 == "n")
                            {
                                s = "-";
                            }
                            string str22 = row2[str8 + "_diff"].ToString().Trim();
                            if (s != "-")
                            {
                                num4 = double.Parse(s) + double.Parse(str22);
                                pl = num4.ToString();
                            }
                            else
                            {
                                pl = s;
                            }
                            if (s != "-")
                            {
                                base.GetOdds_KC(8, row2["odds_id"].ToString(), ref pl);
                            }
                            if (str4.Equals("0") && !pl.Equals("-"))
                            {
                                if (str16.Equals("81004_82005"))
                                {
                                    num4 = Convert.ToDouble(pl) + base.GetPlayTypeWTValue_PKBJL("82005");
                                    pl = num4.ToString();
                                }
                                if (str16.Equals("81004_82006"))
                                {
                                    pl = (Convert.ToDouble(pl) + base.GetPlayTypeWTValue_PKBJL("82006")).ToString();
                                }
                            }
                            this.oddsList.Add(str16 + "," + pl);
                        }
                        string str23 = string.Join("|", this.oddsList.ToArray());
                        if (this.oddsList.Count<string>() > 0)
                        {
                            dictionary.Add("play_odds", str23);
                        }
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
            this.out_odds_info();
        }
    }
}

