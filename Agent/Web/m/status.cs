namespace Agent.Web.M
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;

    public class status : MemberPageBase_Mobile
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.Response.Expires = 0;
            base.Response.CacheControl = "no-cache";
            base.checkLoginByHandler(0);
            string str = base.get_children_name();
            string str2 = this.Session["user_name"].ToString();
            agent_userinfo_session _session = this.Session[str2 + "lottery_session_user_info"] as agent_userinfo_session;
            string str3 = _session.get_u_type();
            if (FileCacheHelper.get_RedisStatOnline().Equals(1) || FileCacheHelper.get_RedisStatOnline().Equals(2))
            {
                bool flag = false;
                if ((_session.get_users_child_session() != null) && _session.get_users_child_session().get_is_admin().Equals(1))
                {
                    flag = true;
                }
                if (!flag)
                {
                    if (FileCacheHelper.get_RedisStatOnline().Equals(1))
                    {
                        base.CheckIsOut((str == "") ? str2 : str);
                        base.stat_online_redis((str == "") ? str2 : str, str3);
                    }
                    else if (FileCacheHelper.get_RedisStatOnline().Equals(2))
                    {
                        base.CheckIsOutStack((str == "") ? str2 : str);
                        base.stat_online_redisStack((str == "") ? str2 : str, str3);
                    }
                }
            }
            else
            {
                MemberPageBase.stat_online((str == "") ? str2 : str, str3);
            }
            ReturnResult result = new ReturnResult();
            result.set_success(200);
            string strResult = base.ObjectToJson(result);
            base.OutJson(strResult);
        }
    }
}

