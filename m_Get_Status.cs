using LotterySystem.Common;
using LotterySystem.Model;
using System;
using System.Collections.Generic;
using System.Web;
using User.Web.WebBase;

public class m_Get_Status : MemberPageBase_Mobile
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.Response.Expires = 0;
        base.Response.CacheControl = "no-cache";
        IDictionary<string, object> dictionary = new Dictionary<string, object>();
        if (!base.IsUserLoginByMobileForAjax())
        {
            dictionary.Add("islogin", "0");
            base.OutJson(JsonHandle.ObjectToJson(dictionary));
        }
        else
        {
            dictionary.Add("islogin", "1");
            string user = HttpContext.Current.Session["user_name"].ToString();
            cz_users _users = CallBLL.cz_users_bll.UserLogin(user);
            string str2 = _users.get_kc_kind().Trim();
            decimal num = Convert.ToDecimal(_users.get_kc_credit());
            decimal num2 = Convert.ToDecimal(_users.get_kc_usable_credit());
            decimal num3 = Convert.ToDecimal(_users.get_six_credit());
            decimal num4 = Convert.ToDecimal(_users.get_six_usable_credit());
            string str3 = num3.ToString("F1");
            string str4 = num4.ToString("F1");
            string kCProfit = base.GetKCProfit();
            if (_users.get_kc_iscash().Equals(int.Parse("1")))
            {
                dictionary.Add("t_iscash", "1");
            }
            else
            {
                dictionary.Add("t_iscash", "0");
            }
            dictionary.Add("t_credit", num2.ToString("F1"));
            dictionary.Add("t_amt", double.Parse(kCProfit).ToString("F1"));
            if (_users.get_six_iscash().Equals(int.Parse("1")))
            {
                dictionary.Add("t_iscash_six", "1");
            }
            else
            {
                dictionary.Add("t_iscash_six", "0");
            }
            dictionary.Add("t_credit_six", str3);
            dictionary.Add("t_amt_six", str4);
            if (FileCacheHelper.get_RedisStatOnline().Equals(1))
            {
                base.CheckIsOut(user);
                base.stat_online_redis(user, _users.get_u_type());
            }
            else if (FileCacheHelper.get_RedisStatOnline().Equals(2))
            {
                base.CheckIsOutStack(user);
                base.stat_online_redisStack(user, _users.get_u_type());
            }
            else
            {
                MemberPageBase.stat_online(user, "hy");
            }
            base.OutJson(JsonHandle.ObjectToJson(dictionary));
        }
    }
}

