using LotterySystem.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using User.Web.WebBase;

public class GetDrawInfo_vrcar : MemberPageBase_Mobile
{
    private List<object> balls = new List<object>();
    private string str_amt = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        IDictionary<string, object> dictionary = new Dictionary<string, object>();
        string str = "";
        DataTable kCOpenBall = base.GetKCOpenBall(0x12);
        string[] strArray = null;
        if ((kCOpenBall != null) && (kCOpenBall.Rows.Count > 0))
        {
            string str2 = kCOpenBall.Rows[0]["previousphase"].ToString().Trim();
            string str3 = kCOpenBall.Rows[0]["sold"].ToString().Trim();
            string str4 = kCOpenBall.Rows[0]["surplus"].ToString().Trim();
            str = kCOpenBall.Rows[0]["nns"].ToString().Trim();
            strArray = (str2 + "," + str).Split(new char[] { ',' }).ToArray<string>();
        }
        dictionary.Add("status", "1");
        dictionary.Add("kaijiang", strArray);
        string strResult = JsonHandle.ObjectToJson(dictionary);
        base.OutJson(strResult);
    }
}

