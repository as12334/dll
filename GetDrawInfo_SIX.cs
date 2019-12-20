using LotterySystem.Common;
using LotterySystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using User.Web.WebBase;

public class GetDrawInfo_SIX : MemberPageBase_Mobile
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IDictionary<string, object> dictionary = new Dictionary<string, object>();
        cz_phase_six currentPhaseClose = CallBLL.cz_phase_six_bll.GetCurrentPhaseClose();
        if (currentPhaseClose != null)
        {
            string str = (currentPhaseClose != null) ? currentPhaseClose.get_phase() : "";
            string str2 = (currentPhaseClose != null) ? string.Format("{0},{1},{2},{3},{4},{5},{6}", new object[] { currentPhaseClose.get_n1(), currentPhaseClose.get_n2(), currentPhaseClose.get_n3(), currentPhaseClose.get_n4(), currentPhaseClose.get_n5(), currentPhaseClose.get_n6(), currentPhaseClose.get_sn() }) : "";
            dictionary.Add("draw_phase", str);
            dictionary.Add("draw_result", str2.Split(new char[] { ',' }).ToArray<string>());
        }
        dictionary.Add("type", "get_drawinfo");
        dictionary.Add("status", "1");
        string strResult = JsonHandle.ObjectToJson(dictionary);
        base.OutJson(strResult);
    }
}

