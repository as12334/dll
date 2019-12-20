using LotterySystem.Common;
using LotterySystem.WebPageBase;
using System;
using System.Collections.Generic;
using System.Data;
using User.Web.WebBase;

public class m_cl_10_kl8 : MemberPageBase_Mobile
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
            string str = "_m_kl8_lmcl";
            string str2 = "_m_kl8_cql";
            List<object> cache = new List<object>();
            if (CacheHelper.GetCache("balance_kc_FileCacheKey" + str) != null)
            {
                cache = CacheHelper.GetCache("balance_kc_FileCacheKey" + str) as List<object>;
            }
            else
            {
                DataTable table = CallBLL.cz_phase_kl8_bll.GetChangLong().Tables[0];
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
            Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
            if (CacheHelper.GetCache("balance_kc_FileCacheKey" + str2) != null)
            {
                dictionary2 = CacheHelper.GetCache("balance_kc_FileCacheKey" + str2) as Dictionary<string, object>;
                dictionary.Add("ph_title", dictionary2["ph_title"]);
                dictionary.Add("ph_content", dictionary2["ph_content"]);
            }
            else
            {
                List<string> list2 = CallBLL.cz_phase_kl8_bll.PaihangList("kl8", 10);
                string str6 = string.Join("|", new List<string> { "總和數", "總和大小", "前後和", "五行", "總和單雙", "單雙和" }.ToArray());
                dictionary.Add("ph_title", str6);
                dictionary.Add("ph_content", list2);
                dictionary2.Add("ph_title", str6);
                dictionary2.Add("ph_content", list2);
                CacheHelper.SetCache("balance_kc_FileCacheKey" + str2, dictionary2);
                CacheHelper.SetPublicFileCache("balance_kc_FileCacheKey" + str2, dictionary2, PageBase.GetPublicForderPath(base.get_KC_BalanceFileName()));
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

