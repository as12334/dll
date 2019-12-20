using LotterySystem.Common;
using LotterySystem.WebPageBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using User.Web.WebBase;

public class m_cl_10 : MemberPageBase_Mobile
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
            ArrayList list3;
            string str = "_m_kl10_lmcl";
            string str2 = "_m_kl10_cql";
            string str3 = "_m_kl10_lryl";
            List<object> cache = new List<object>();
            if (CacheHelper.GetCache("balance_kc_FileCacheKey" + str) != null)
            {
                cache = CacheHelper.GetCache("balance_kc_FileCacheKey" + str) as List<object>;
            }
            else
            {
                DataTable table = CallBLL.cz_phase_kl10_bll.GetChangLong().Tables[0];
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
                Dictionary<string, string> paiHang;
                ArrayList list2;
                string str9;
                if (str7 == "16")
                {
                    str8 = "kl10_lmp";
                    dictionary2 = new Dictionary<string, object>();
                    if (CacheHelper.GetCache("balance_kc_FileCacheKey" + str2 + str8) != null)
                    {
                        dictionary2 = CacheHelper.GetCache("balance_kc_FileCacheKey" + str2 + str8) as Dictionary<string, object>;
                        dictionary.Add("ph_title", dictionary2["ph_title"]);
                        dictionary.Add("ph_content", dictionary2["ph_content"]);
                    }
                    else
                    {
                        paiHang = CallBLL.cz_phase_kl10_bll.GetPaiHang(str8, 10);
                        if ((paiHang != null) && (paiHang.Count > 0))
                        {
                            list2 = new ArrayList();
                            list3 = new ArrayList();
                            foreach (KeyValuePair<string, string> pair in paiHang)
                            {
                                list2.Add(pair.Key);
                                list3.Add(pair.Value);
                            }
                            str9 = string.Join("|", list2.ToArray());
                            dictionary.Add("ph_title", str9);
                            dictionary.Add("ph_content", list3);
                            dictionary2.Add("ph_title", str9);
                            dictionary2.Add("ph_content", list3);
                            CacheHelper.SetCache("balance_kc_FileCacheKey" + str2 + str8, dictionary2);
                            CacheHelper.SetPublicFileCache("balance_kc_FileCacheKey" + str2 + str8, dictionary2, PageBase.GetPublicForderPath(base.get_KC_BalanceFileName()));
                        }
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
                            paiHang = CallBLL.cz_phase_kl10_bll.GetPaiHang(str8, 10);
                            if ((paiHang != null) && (paiHang.Count > 0))
                            {
                                list2 = new ArrayList();
                                list3 = new ArrayList();
                                foreach (KeyValuePair<string, string> pair in paiHang)
                                {
                                    list2.Add(pair.Key);
                                    list3.Add(pair.Value);
                                }
                                str9 = string.Join("|", list2.ToArray());
                                dictionary.Add("ph_title", str9);
                                dictionary.Add("ph_content", list3);
                                dictionary2.Add("ph_title", str9);
                                dictionary2.Add("ph_content", list3);
                            }
                            CacheHelper.SetCache("balance_kc_FileCacheKey" + str2 + str8, dictionary2);
                            CacheHelper.SetPublicFileCache("balance_kc_FileCacheKey" + str2 + str8, dictionary2, PageBase.GetPublicForderPath(base.get_KC_BalanceFileName()));
                        }
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
                    DataSet lRYL = CallBLL.cz_phase_kl10_bll.GetLRYL(str10);
                    DataTable table2 = null;
                    if (lRYL != null)
                    {
                        table2 = lRYL.Tables[0];
                        ArrayList list4 = new ArrayList();
                        if ((table2 != null) && (table2.Rows.Count > 0))
                        {
                            list3 = new ArrayList();
                            for (int i = 0; i < table2.Columns.Count; i++)
                            {
                                list3.Add(table2.Columns[i].ColumnName);
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

