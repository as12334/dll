using LotterySystem.Common;
using LotterySystem.WebPageBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using User.Web.WebBase;

public class m_cl_10_pcdd : MemberPageBase_Mobile
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
            string str = "_m_pcdd_lmcl";
            string str2 = "_m_pcdd_cql";
            List<object> cache = new List<object>();
            if (CacheHelper.GetCache("balance_kc_FileCacheKey" + str) != null)
            {
                cache = CacheHelper.GetCache("balance_kc_FileCacheKey" + str) as List<object>;
            }
            else
            {
                DataTable table = CallBLL.cz_phase_pcdd_bll.GetChangLong().Tables[0];
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
            string str6 = string.Join("|", cache.ToArray());
            dictionary.Add("long", str6);
            if (!string.IsNullOrEmpty(LSRequest.qq("page_type").Trim()))
            {
                string str8 = "pcdd_lmp";
                Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                if (CacheHelper.GetCache("balance_kc_FileCacheKey" + str2 + str8) != null)
                {
                    dictionary2 = CacheHelper.GetCache("balance_kc_FileCacheKey" + str2 + str8) as Dictionary<string, object>;
                    dictionary.Add("ph_title", dictionary2["ph_title"]);
                    dictionary.Add("ph_content", dictionary2["ph_content"]);
                }
                else
                {
                    Dictionary<string, string> paiHang = CallBLL.cz_phase_pcdd_bll.GetPaiHang(10);
                    if ((paiHang != null) && (paiHang.Count > 0))
                    {
                        ArrayList list2 = new ArrayList();
                        ArrayList list3 = new ArrayList();
                        foreach (KeyValuePair<string, string> pair in paiHang)
                        {
                            list2.Add(pair.Key);
                            if (pair.Key.Equals("波色"))
                            {
                                list3.Add(pair.Value.Replace("波", ""));
                            }
                            else
                            {
                                list3.Add(pair.Value);
                            }
                        }
                        string str9 = string.Join("|", list2.ToArray());
                        dictionary.Add("ph_title", str9);
                        dictionary.Add("ph_content", list3);
                        dictionary2.Add("ph_title", str9);
                        dictionary2.Add("ph_content", list3);
                        CacheHelper.SetCache("balance_kc_FileCacheKey" + str2 + str8, dictionary2);
                        CacheHelper.SetPublicFileCache("balance_kc_FileCacheKey" + str2 + str8, dictionary2, PageBase.GetPublicForderPath(base.get_KC_BalanceFileName()));
                    }
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

