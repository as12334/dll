using LotterySystem.Common;
using LotterySystem.WebPageBase;
using System;
using System.Collections.Generic;
using System.Data;
using User.Web.WebBase;

public class m_cl_10_xyft5 : MemberPageBase_Mobile
{
    protected void output_cl_cql_lryl()
    {
        IDictionary<string, object> dictionary = new Dictionary<string, object>();
        if (!base.IsUserLoginByMobileForAjax())
        {
            dictionary.Add("status", "2");
            base.OutJson(JsonHandle.ObjectToJson(dictionary));
        }
        else
        {
            string str = "_m_xyft5_lmcl";
            string str2 = "_m_xyft5_cql";
            List<object> cache = new List<object>();
            if (CacheHelper.GetCache("balance_kc_FileCacheKey" + str) != null)
            {
                cache = CacheHelper.GetCache("balance_kc_FileCacheKey" + str) as List<object>;
            }
            else
            {
                DataTable table = CallBLL.cz_phase_xyft5_bll.GetChangLong().Tables[0];
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        cache.Add(row["c_name"].ToString() + "," + row["c_qs"].ToString());
                    }
                }
                CacheHelper.SetCache("balance_kc_FileCacheKey" + str, cache);
                CacheHelper.SetPublicFileCache("balance_kc_FileCacheKey" + str, cache, PageBase.GetPublicForderPath(base.get_KC_BalanceFileName()));
            }
            string str5 = string.Join("|", cache.ToArray());
            dictionary.Add("long", str5);
            string str6 = LSRequest.qq("page_type").Trim();
            if (!string.IsNullOrEmpty(str6) && ((str6 == "21") || (str6 == "18")))
            {
                Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                if (CacheHelper.GetCache("balance_kc_FileCacheKey" + str2) != null)
                {
                    dictionary2 = CacheHelper.GetCache("balance_kc_FileCacheKey" + str2) as Dictionary<string, object>;
                    dictionary.Add("ph_title", dictionary2["ph_title"]);
                    dictionary.Add("ph_content", dictionary2["ph_content"]);
                }
                else
                {
                    List<string> list2 = CallBLL.cz_phase_xyft5_bll.PaihangList_LMP("xyft5_lmp", 10);
                    string str7 = string.Join("|", new List<string> { "冠亞軍和", "冠亞軍和大小", "冠亞軍和單雙" }.ToArray());
                    dictionary.Add("ph_title", str7);
                    dictionary.Add("ph_content", list2);
                    dictionary2.Add("ph_title", str7);
                    dictionary2.Add("ph_content", list2);
                    CacheHelper.SetCache("balance_kc_FileCacheKey" + str2, dictionary2);
                    CacheHelper.SetPublicFileCache("balance_kc_FileCacheKey" + str2, dictionary2, PageBase.GetPublicForderPath(base.get_KC_BalanceFileName()));
                }
            }
            dictionary.Add("status", "1");
            base.OutJson(JsonHandle.ObjectToJson(dictionary));
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.output_cl_cql_lryl();
    }
}

