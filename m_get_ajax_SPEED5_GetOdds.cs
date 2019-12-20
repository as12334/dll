using LotterySystem.Common;
using LotterySystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using User.Web.WebBase;

public class m_get_ajax_SPEED5_GetOdds : MemberPageBase_Mobile
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
        if (string.IsNullOrEmpty(str))
        {
            base.Response.End();
        }
        else
        {
            string str2 = this.Session["user_name"].ToString();
            cz_userinfo_session getUserModelInfo = base.GetUserModelInfo;
            string str3 = getUserModelInfo.get_su_type().ToString();
            string str4 = getUserModelInfo.get_u_name().ToString();
            string str5 = getUserModelInfo.get_kc_kind().Trim();
            DataTable table = CallBLL.cz_users_bll.GetCredit(str2, str3).Tables[0];
            if (table == null)
            {
                base.Response.End();
            }
            else
            {
                string s = table.Rows[0]["kc_usable_credit"].ToString();
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("type", "get_oddsinfo");
                DataSet set = CallBLL.cz_play_speed5_bll.GetPlay(str, str4, str3);
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
                        string str7 = row["openning"].ToString();
                        string str8 = row["isopen"].ToString();
                        string str9 = row["opendate"].ToString();
                        string str10 = row["endtime"].ToString();
                        string str11 = row["nn"].ToString();
                        string str12 = row["p_id"].ToString();
                        string kCProfit = base.GetKCProfit();
                        dictionary.Add("status", "1");
                        dictionary.Add("open", str7);
                        dictionary.Add("k_open_time", str9);
                        dictionary.Add("k_stop_time", str10);
                        dictionary.Add("qishu", str11);
                        dictionary.Add("credit", double.Parse(s).ToString("F1"));
                        dictionary.Add("amount", double.Parse(kCProfit).ToString("F1"));
                        dictionary.Add("k_id", str12);
                        Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                        Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
                        string str14 = "";
                        string pl = "";
                        List<double> list = new List<double>();
                        foreach (DataRow row2 in table2.Rows)
                        {
                            str14 = row2["play_id"].ToString() + "_" + row2["odds_id"].ToString();
                            string str19 = row2["current_odds"].ToString();
                            if (str7 == "n")
                            {
                                str19 = "-";
                            }
                            string str20 = row2[str5 + "_diff"].ToString().Trim();
                            if (str19 != "-")
                            {
                                pl = (double.Parse(str19) + double.Parse(str20)).ToString();
                            }
                            else
                            {
                                pl = str19;
                            }
                            if (str19 != "-")
                            {
                                base.GetOdds_KC(11, row2["odds_id"].ToString(), ref pl);
                            }
                            this.oddsList.Add(str14 + "," + pl);
                        }
                        string str21 = string.Join("|", this.oddsList.ToArray());
                        if (this.oddsList.Count<string>() > 0)
                        {
                            dictionary.Add("play_odds", str21);
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

