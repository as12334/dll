using LotterySystem.Common;
using LotterySystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using User.Web.WebBase;

public class GetDrawInfo_pkbjl : MemberPageBase_Mobile
{
    private List<object> balls = new List<object>();
    private string str_amt = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        IDictionary<string, object> dictionary = new Dictionary<string, object>();
        string str = LSRequest.qq("player_type");
        string numTable = "";
        numTable = Utils.GetPKBJL_NumTable(str);
        string str3 = "";
        string str4 = "";
        string str5 = "";
        string str6 = "";
        string str7 = "";
        string[] strArray = null;
        Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
        DataTable kCOpenBall = base.GetKCOpenBall(8, numTable);
        if ((kCOpenBall != null) && (kCOpenBall.Rows.Count > 0))
        {
            DataRow row = kCOpenBall.Rows[0];
            string str8 = row["previousphase"].ToString().Trim();
            str7 = row["currentphase"].ToString().Trim();
            str3 = row["nns"].ToString();
            strArray = (str8 + "," + str3).Split(new char[] { ',' }).ToArray<string>();
            str4 = row["ten_poker"].ToString();
            str6 = row["zhuang_nn"].ToString();
            str5 = row["xian_nn"].ToString();
            dictionary2.Add("player", str5);
            dictionary2.Add("banker", str6);
        }
        DataTable table2 = CallBLL.cz_phase_pkbjl_bll.IsOpenPhaseByTime(DateTime.Now, numTable);
        if ((table2 == null) || (table2.Rows.Count < 1))
        {
            str4 = "";
        }
        else
        {
            DateTime time = Convert.ToDateTime(DateTime.Now.ToString());
            string str9 = table2.Rows[0]["p_id"].ToString().Trim();
            str7 = table2.Rows[0]["phase"].ToString().Trim();
            DateTime time2 = Convert.ToDateTime(table2.Rows[0]["play_open_date"].ToString().Trim());
            DateTime time3 = Convert.ToDateTime(table2.Rows[0]["open_date"].ToString().Trim());
            string str10 = time2.ToString("HH:mm:ss");
            DateTime time4 = Convert.ToDateTime(table2.Rows[0]["stop_date"].ToString().Trim());
            string str11 = "y";
            string str12 = Utils.DateDiff(time4, time).ToString();
            TimeSpan dateDiff = Utils.GetDateDiff(time4, time);
            if (table2.Rows[0]["is_closed"].ToString().Trim() == "0")
            {
                if (time4 < time)
                {
                    str11 = "n";
                }
            }
            else
            {
                str11 = "n";
            }
            if (str11.Equals("n"))
            {
                int num = 8;
                base.Add_TenPoker_Lock(num.ToString(), str9, numTable);
                cz_phase_pkbjl tenPoker = CallBLL.cz_phase_pkbjl_bll.GetTenPoker(str9, numTable);
                if ((tenPoker == null) || string.IsNullOrEmpty(tenPoker.get_ten_poker()))
                {
                    CallBLL.cz_poker_pkbjl_bll.InitToPhase(numTable, str9);
                }
                base.Un_TenPoker_Lock(8.ToString(), str9, numTable);
            }
        }
        str4 = CallBLL.cz_phase_pkbjl_bll.GetNewOpenPhaseByNumTable(numTable).Rows[0]["ten_poker"].ToString();
        dictionary.Add("status", "1");
        dictionary.Add("kaijiang", strArray);
        dictionary.Add("porkList", str4);
        dictionary.Add("jq", str7);
        dictionary.Add("lastResult", dictionary2);
        dictionary.Add("road", base.GetPAILU(numTable));
        string strResult = JsonHandle.ObjectToJson(dictionary);
        base.OutJson(strResult);
    }
}

