using LotterySystem.Common;
using LotterySystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using User.Web.WebBase;

public class GetOdds_pk10 : MemberPageBase_Mobile
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
                double num = double.Parse(table.Rows[0]["kc_usable_credit"].ToString());
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("type", "get_oddsinfo");
                DataSet set = CallBLL.cz_play_pk10_bll.GetPlay(str, str4, str3);
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
                        string str6 = row["openning"].ToString();
                        string str7 = row["isopen"].ToString();
                        string str8 = row["opendate"].ToString();
                        string str9 = row["endtime"].ToString();
                        string str10 = row["nn"].ToString();
                        string str11 = row["p_id"].ToString();
                        double num2 = double.Parse(base.GetKCProfit());
                        dictionary.Add("status", "1");
                        dictionary.Add("open", str6);
                        dictionary.Add("k_open_time", str8);
                        dictionary.Add("k_stop_time", str9);
                        dictionary.Add("qishu", str10);
                        dictionary.Add("credit", num.ToString("F1"));
                        dictionary.Add("amount", num2.ToString("F1"));
                        dictionary.Add("k_id", str11);
                        Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                        Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
                        string str12 = "";
                        string pl = "";
                        List<double> list = new List<double>();
                        foreach (DataRow row2 in table2.Rows)
                        {
                            str12 = row2["play_id"].ToString() + "_" + row2["odds_id"].ToString();
                            string s = row2["current_odds"].ToString();
                            if (str6 == "n")
                            {
                                s = "-";
                            }
                            string str18 = row2[str5 + "_diff"].ToString().Trim();
                            if (s != "-")
                            {
                                pl = (double.Parse(s) + double.Parse(str18)).ToString();
                            }
                            else
                            {
                                pl = s;
                            }
                            if (s != "-")
                            {
                                base.GetOdds_KC(2, row2["odds_id"].ToString(), ref pl);
                            }
                            this.oddsList.Add(str12 + "," + pl);
                        }
                        string str19 = string.Join("|", this.oddsList.ToArray());
                        if (this.oddsList.Count<string>() > 0)
                        {
                            dictionary.Add("play_odds", str19);
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

