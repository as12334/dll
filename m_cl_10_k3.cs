using LotterySystem.Common;
using LotterySystem.WebPageBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using User.Web.WebBase;

public class m_cl_10_k3 : MemberPageBase_Mobile
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.write_json();
    }

    protected void write_json()
    {
        IDictionary<string, object> dictionary = new Dictionary<string, object>();
        if (!base.IsUserLoginByMobileForAjax())
        {
            dictionary.Add("status", "2");
            base.OutJson(JsonHandle.ObjectToJson(dictionary));
        }
        else
        {
            string str = "_m_jsk3_jqkj";
            dictionary.Add("status", "1");
            List<object> cache = new List<object>();
            if (CacheHelper.GetCache("balance_kc_FileCacheKey" + str) != null)
            {
                cache = CacheHelper.GetCache("balance_kc_FileCacheKey" + str) as List<object>;
            }
            else
            {
                string str2 = "";
                string str3 = "";
                string str4 = "";
                string str5 = "";
                DataSet topPhase = CallBLL.cz_phase_jsk3_bll.GetTopPhase(0x16);
                if (topPhase != null)
                {
                    DataTable table = topPhase.Tables[0];
                    Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                    StringBuilder builder = new StringBuilder();
                    foreach (DataRow row in table.Rows)
                    {
                        str2 = row["phase"].ToString().Trim().Substring(9, 2);
                        str3 = row["play_open_date"].ToString();
                        int num = Convert.ToInt32(row["n1"].ToString());
                        int num2 = Convert.ToInt32(row["n2"].ToString());
                        int num3 = Convert.ToInt32(row["n3"].ToString());
                        str4 = ((num + num2) + num3).ToString();
                        if ((num == num2) && (num2 == num3))
                        {
                            str5 = "通吃";
                        }
                        else if (Convert.ToInt32(str4) <= 10)
                        {
                            str5 = "小";
                        }
                        else
                        {
                            str5 = "大";
                        }
                        string item = string.Format("{0},{1},{2},{3},{4},{5}", new object[] { str2, num, num2, num3, str4, str5 });
                        cache.Add(item);
                    }
                }
                CacheHelper.SetCache("balance_kc_FileCacheKey" + str, cache);
                CacheHelper.SetPublicFileCache("balance_kc_FileCacheKey" + str, cache, PageBase.GetPublicForderPath(base.get_KC_BalanceFileName()));
            }
            dictionary.Add("k3Long", cache);
            base.OutJson(JsonHandle.ObjectToJson(dictionary));
        }
    }
}

