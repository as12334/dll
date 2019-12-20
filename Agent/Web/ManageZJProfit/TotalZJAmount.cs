namespace Agent.Web.ManageZJProfit
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.DBUtility;
    using LotterySystem.WebPageBase;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class TotalZJAmount : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!FileCacheHelper.get_ManageZJProfit().Equals("1"))
            {
                base.Response.End();
            }
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            base.Response.ContentType = "text/json";
            string str = LSRequest.qq("lotteryType");
            string str2 = LSRequest.qq("lotteryPhase");
            string str3 = LSRequest.qq("validcode");
            string str4 = "0";
            string str5 = "0";
            double num = 0.0;
            double num2 = 0.0;
            double num3 = 0.0;
            string str6 = "";
            if ((string.IsNullOrEmpty(str) || string.IsNullOrEmpty(str2)) || !str3.Equals(FileCacheHelper.get_ManageZJProfitValidCode()))
            {
                dictionary.Add("code", 400);
                dictionary.Add("tip_info", "參數錯誤!");
                dictionary.Add("lotteryType", str);
                dictionary.Add("phase", str2);
                dictionary.Add("minRate", str4);
                dictionary.Add("maxRate", str5);
                dictionary.Add("sum_amount", num);
                dictionary.Add("sum_profit", num2);
                dictionary.Add("sum_drawback", num3);
                dictionary.Add("data", null);
                base.Response.Write(JsonHandle.ObjectToJson(dictionary));
                base.Response.End();
            }
            str6 = Utils.LotteryTypeById_ccms(str);
            if (string.IsNullOrEmpty(str6))
            {
                dictionary.Add("code", 400);
                dictionary.Add("tip_info", "參數錯誤!");
                dictionary.Add("lotteryType", str);
                dictionary.Add("phase", str2);
                dictionary.Add("minRate", str4);
                dictionary.Add("maxRate", str5);
                dictionary.Add("sum_amount", num);
                dictionary.Add("sum_profit", num2);
                dictionary.Add("sum_drawback", num3);
                dictionary.Add("data", null);
                base.Response.Write(JsonHandle.ObjectToJson(dictionary));
                base.Response.End();
            }
            SqlParameter[] parameterArray2 = new SqlParameter[2];
            SqlParameter parameter = new SqlParameter("@lottery_id", SqlDbType.Int) {
                Value = str6
            };
            parameterArray2[0] = parameter;
            SqlParameter parameter2 = new SqlParameter("@id_ccms", SqlDbType.NVarChar) {
                Value = str
            };
            parameterArray2[1] = parameter2;
            SqlParameter[] parameterArray = parameterArray2;
            DataSet set = DbHelperSQL.Query(" select top 1 * from cz_zj_profit_set where lottery_id=@lottery_id and id_ccms=@id_ccms and  (minRate != 0 or maxRate != 0 ) ", parameterArray);
            if (((set != null) && (set.Tables.Count > 0)) && (set.Tables[0].Rows.Count > 0))
            {
                str4 = set.Tables[0].Rows[0]["minRate"].ToString();
                str5 = set.Tables[0].Rows[0]["maxRate"].ToString();
                string str7 = set.Tables[0].Rows[0]["openTime"].ToString();
                string str8 = set.Tables[0].Rows[0]["u_type"].ToString();
                string str9 = set.Tables[0].Rows[0]["u_name"].ToString();
                DataTable table = CallBLL.cz_bet_kc_bll.GetZJTotalsByTime(str6, str7, str8, str9);
                if (table != null)
                {
                    num = double.Parse(table.Rows[0]["sum_amount"].ToString());
                    num2 = double.Parse(table.Rows[0]["sum_profit"].ToString());
                    num3 = double.Parse(table.Rows[0]["sum_drawback"].ToString());
                }
            }
            int num4 = 0;
            DataTable table2 = CallBLL.cz_bet_kc_bll.GetZJTotals(str6, str2, ref num4);
            switch (num4)
            {
                case 1:
                    dictionary.Add("code", 400);
                    dictionary.Add("tip_info", "找不到獎期！");
                    break;

                case 2:
                    dictionary.Add("code", 400);
                    dictionary.Add("tip_info", "該獎期正在開盤中！");
                    break;

                default:
                    dictionary.Add("code", 200);
                    dictionary.Add("tip_info", "");
                    break;
            }
            dictionary.Add("lotteryType", str);
            dictionary.Add("phase", str2);
            dictionary.Add("minRate", str4);
            dictionary.Add("maxRate", str5);
            dictionary.Add("sum_amount", num);
            dictionary.Add("sum_profit", num2);
            dictionary.Add("sum_drawback", num3);
            dictionary.Add("data", table2);
            base.Response.Write(JsonHandle.ObjectToJson(dictionary));
            base.Response.End();
        }
    }
}

