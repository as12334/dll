using LotterySystem.Common;
using LotterySystem.WebPageBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using User.Web.WebBase;

public class m_get_ajax_JSCQSC_GetCLPH : MemberPageBase_Mobile
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
            int num;
            string str = "_m_jscqsc_lmcl";
            string str2 = "_m_jscqsc_cql";
            string str3 = "_m_jscqsc_lryl";
            List<object> cache = new List<object>();
            if (CacheHelper.GetCache("balance_kc_FileCacheKey" + str) != null)
            {
                cache = CacheHelper.GetCache("balance_kc_FileCacheKey" + str) as List<object>;
            }
            else
            {
                DataTable table = CallBLL.cz_phase_jscqsc_bll.GetChangLong().Tables[0];
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        cache.Add(row["c_name"].ToString() + "," + row["c_qs"].ToString());
                    }
                    CacheHelper.SetCache("balance_kc_FileCacheKey" + str, cache);
                    CacheHelper.SetPublicFileCache("balance_kc_FileCacheKey" + str, cache, PageBase.GetPublicForderPath(base.get_KC_BalanceFileName()));
                }
            }
            string str6 = string.Join("|", cache.ToArray());
            dictionary.Add("long", str6);
            string str7 = LSRequest.qq("page_type").Trim();
            if (!string.IsNullOrEmpty(str7))
            {
                Dictionary<string, object> dictionary2;
                List<string> list2;
                string str8;
                if ((str7 == "17") || (str7 == "24"))
                {
                    dictionary2 = new Dictionary<string, object>();
                    if (CacheHelper.GetCache("balance_kc_FileCacheKey" + str2 + str7) != null)
                    {
                        dictionary2 = CacheHelper.GetCache("balance_kc_FileCacheKey" + str2 + str7) as Dictionary<string, object>;
                        dictionary.Add("ph_title", dictionary2["ph_title"]);
                        dictionary.Add("ph_content", dictionary2["ph_content"]);
                    }
                    else
                    {
                        list2 = CallBLL.cz_phase_jscqsc_bll.PaihangList_LMP(str7, 10);
                        List<string> list3 = new List<string>();
                        for (num = 1; num <= 5; num++)
                        {
                            list3.Add(string.Format("第{0}球大小", num));
                            list3.Add(string.Format("第{0}球單雙", num));
                        }
                        list3.Add("總和大小");
                        list3.Add("總和單雙");
                        list3.Add("龍虎");
                        str8 = string.Join("|", list3.ToArray());
                        dictionary.Add("ph_title", str8);
                        dictionary.Add("ph_content", list2);
                        dictionary2.Add("ph_title", str8);
                        dictionary2.Add("ph_content", list2);
                        CacheHelper.SetCache("balance_kc_FileCacheKey" + str2 + str7, dictionary2);
                        CacheHelper.SetPublicFileCache("balance_kc_FileCacheKey" + str2 + str7, dictionary2, PageBase.GetPublicForderPath(base.get_KC_BalanceFileName()));
                    }
                }
                else
                {
                    string str9 = str7;
                    if (Utils.IsNumeric(str9))
                    {
                        dictionary2 = new Dictionary<string, object>();
                        if (CacheHelper.GetCache("balance_kc_FileCacheKey" + str2 + str9) != null)
                        {
                            dictionary2 = CacheHelper.GetCache("balance_kc_FileCacheKey" + str2 + str9) as Dictionary<string, object>;
                            dictionary.Add("ph_title", dictionary2["ph_title"]);
                            dictionary.Add("ph_content", dictionary2["ph_content"]);
                        }
                        else
                        {
                            list2 = CallBLL.cz_phase_jscqsc_bll.PaihangListDQ(str9, 10);
                            str8 = string.Join("|", new List<string> { string.Format("第{0}球", str9), "大小", "單雙", "總和大小", "總和單雙", "龍虎" }.ToArray());
                            dictionary.Add("ph_title", str8);
                            dictionary.Add("ph_content", list2);
                            dictionary2.Add("ph_title", str8);
                            dictionary2.Add("ph_content", list2);
                            CacheHelper.SetCache("balance_kc_FileCacheKey" + str2 + str9, dictionary2);
                            CacheHelper.SetPublicFileCache("balance_kc_FileCacheKey" + str2 + str9, dictionary2, PageBase.GetPublicForderPath(base.get_KC_BalanceFileName()));
                        }
                    }
                }
            }
            string str10 = LSRequest.qq("t");
            if (((!string.IsNullOrEmpty(str10) && Utils.IsNumeric(str10)) && (int.Parse(str10) > 0)) && (int.Parse(str10) <= 5))
            {
                string str11 = "";
                if (CacheHelper.GetCache("balance_kc_FileCacheKey" + str3 + str10) != null)
                {
                    str11 = CacheHelper.GetCache("balance_kc_FileCacheKey" + str3 + str10) as string;
                }
                else
                {
                    DataSet lRYL = CallBLL.cz_phase_jscqsc_bll.GetLRYL(str10);
                    DataTable table2 = null;
                    if (lRYL != null)
                    {
                        table2 = lRYL.Tables[0];
                        ArrayList list4 = new ArrayList();
                        if ((table2 != null) && (table2.Rows.Count > 0))
                        {
                            ArrayList list5 = new ArrayList();
                            for (num = 0; num < table2.Columns.Count; num++)
                            {
                                list5.Add(table2.Columns[num].ColumnName);
                            }
                            string str12 = string.Join(",", table2.Rows[0].ItemArray.Skip<object>(1).ToArray<object>());
                            string str13 = string.Join(",", table2.Rows[1].ItemArray.Skip<object>(1).ToArray<object>());
                            str11 = str12 + "|" + str13;
                        }
                    }
                    CacheHelper.SetCache("balance_kc_FileCacheKey" + str3 + str10, str11);
                    CacheHelper.SetPublicFileCache("balance_kc_FileCacheKey" + str3 + str10, str11, PageBase.GetPublicForderPath(base.get_KC_BalanceFileName()));
                }
                dictionary.Add("lryl", str11);
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

