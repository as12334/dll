namespace Agent.Web.Handler
{
    using Agent.Web.WebBase;
    using LotterySystem.Model;
    using System;
    using System.Web;
    using System.Web.SessionState;

    public class BaseHandler : MemberPageBase, IHttpHandler, IRequiresSessionState
    {
        public bool IsFGSWT_Opt(int lid)
        {
            string str = HttpContext.Current.Session["user_name"].ToString();
            if (HttpContext.Current.Session["user_type"].ToString().Equals("fgs"))
            {
                cz_users userInfoByUName = CallBLL.cz_users_bll.GetUserInfoByUName(str);
                agent_userinfo_session _session = HttpContext.Current.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
                if (!userInfoByUName.get_six_op_odds().Equals(_session.get_six_op_odds()) || !userInfoByUName.get_kc_op_odds().Equals(_session.get_kc_op_odds()))
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}

