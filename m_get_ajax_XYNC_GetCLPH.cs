using LotterySystem.Common;
using LotterySystem.WebPageBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using User.Web.WebBase;

public class m_get_ajax_XYNC_GetCLPH : MemberPageBase_Mobile
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
            string str = "_m_xync_lmcl";
            string str2 = "_m_xync_cql";
            string str3 = "_m_xync_lryl";
            List<object> cache = new List<object>();
            if (CacheHelper.GetCache("balance_kc_FileCacheKey" + str) != null)
            {
                cache = CacheHelper.GetCache("balance_kc_FileCacheKey" + str) as List<object>;
            }
            else
            {
                DataTable table = CallBLL.cz_phase_xync_bll.GetChangLong().Tables[0];
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
            string str7 = LSRequest.qq("page_type").Trim();
            if (!string.IsNullOrEmpty(str7))
            {
                string str8;
                Dictionary<string, object> dictionary2;
                List<string> list2;
                string str9;
                if (str7 == "26")
                {
                    str8 = "xync_lmp";
                    dictionary2 = new Dictionary<string, object>();
                    if (CacheHelper.GetCache("balance_kc_FileCacheKey" + str2 + str8) != null)
                    {
                        dictionary2 = CacheHelper.GetCache("balance_kc_FileCacheKey" + str2 + str8) as Dictionary<string, object>;
                        dictionary.Add("ph_title", dictionary2["ph_title"]);
                        dictionary.Add("ph_content", dictionary2["ph_content"]);
                    }
                    else
                    {
                        list2 = CallBLL.cz_phase_xync_bll.PaihangListLMP(str8, 10);
                        List<string> list3 = new List<string> { "家禽野獸", "總和大小", "總和單雙", "總和尾大小" };
                        str9 = string.Join("|", list3.ToArray());
                        dictionary.Add("ph_title", str9);
                        dictionary.Add("ph_content", list2);
                        dictionary2.Add("ph_title", str9);
                        dictionary2.Add("ph_content", list2);
                        CacheHelper.SetCache("balance_kc_FileCacheKey" + str2 + str8, dictionary2);
                        CacheHelper.SetPublicFileCache("balance_kc_FileCacheKey" + str2 + str8, dictionary2, PageBase.GetPublicForderPath(base.get_KC_BalanceFileName()));
                    }
                }
                else
                {
                    str8 = str7;
                    if (Utils.IsNumeric(str8))
                    {
                        dictionary2 = new Dictionary<string, object>();
                        if (CacheHelper.GetCache("balance_kc_FileCacheKey" + str2 + str8) != null)
                        {
                            dictionary2 = CacheHelper.GetCache("balance_kc_FileCacheKey" + str2 + str8) as Dictionary<string, object>;
                            dictionary.Add("ph_title", dictionary2["ph_title"]);
                            dictionary.Add("ph_content", dictionary2["ph_content"]);
                        }
                        else
                        {
                            list2 = CallBLL.cz_phase_xync_bll.PaihangListDQ(str8, 10);
                            str9 = string.Join("|", new List<string> { string.Format("第{0}球", str8), "單雙", "大小", "合數單雙", "尾大小", "方位", "中發白", "家禽野獸", "總和大小", "總和單雙", "總和尾大小" }.ToArray());
                            dictionary.Add("ph_title", str9);
                            dictionary.Add("ph_content", list2);
                            dictionary2.Add("ph_title", str9);
                            dictionary2.Add("ph_content", list2);
                        }
                        CacheHelper.SetCache("balance_kc_FileCacheKey" + str2 + str8, dictionary2);
                        CacheHelper.SetPublicFileCache("balance_kc_FileCacheKey" + str2 + str8, dictionary2, PageBase.GetPublicForderPath(base.get_KC_BalanceFileName()));
                    }
                }
            }
            string str10 = LSRequest.qq("t");
            if (((!string.IsNullOrEmpty(str10) && Utils.IsNumeric(str10)) && (int.Parse(str10) > 0)) && (int.Parse(str10) <= 8))
            {
                string str11 = "";
                if (CacheHelper.GetCache("balance_kc_FileCacheKey" + str3 + str10) != null)
                {
                    str11 = CacheHelper.GetCache("balance_kc_FileCacheKey" + str3 + str10) as string;
                }
                else
                {
                    DataSet lRYL = CallBLL.cz_phase_xync_bll.GetLRYL(str10);
                    Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
                    DataTable table2 = null;
                    if (lRYL != null)
                    {
                        table2 = lRYL.Tables[0];
                        ArrayList list4 = new ArrayList();
                        if ((table2 != null) && (table2.Rows.Count > 0))
                        {
                            ArrayList list5 = new ArrayList();
                            for (int i = 0; i < table2.Columns.Count; i++)
                            {
                                list5.Add(table2.Columns[i].ColumnName);
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

