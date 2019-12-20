namespace Agent.Web.WebBase
{
    using LotterySystem.Common;
    using LotterySystem.Model;
    using LotterySystem.WebPageBase;
    using System;
    using System.Web;

    public class RuleJudge
    {
        public static bool ChildOperateValid(agent_userinfo_session sessionInfo, string permissions_name)
        {
            if (!sessionInfo.get_u_type().Trim().Equals("zj"))
            {
                return false;
            }
            if ((sessionInfo.get_users_child_session() != null) && (sessionInfo.get_users_child_session().get_permissions_name().IndexOf(permissions_name) < 0))
            {
                return false;
            }
            return true;
        }

        public static bool ChildOperateValid(agent_userinfo_session sessionInfo, string permissions_name, HttpContext context)
        {
            if ((sessionInfo.get_u_type().Trim().Equals("zj") && (context.Session["child_user_name"] != null)) && (sessionInfo.get_users_child_session().get_permissions_name().IndexOf(permissions_name) < 0))
            {
                ReturnResult result = new ReturnResult();
                result.set_success(400);
                result.set_tipinfo(PageBase.GetMessageByCache("u100013", "MessageHint"));
                string s = JsonHandle.ObjectToJson(result);
                HttpContext.Current.Response.ContentType = "text/json";
                HttpContext.Current.Response.Write(s);
                return false;
            }
            return true;
        }

        public static bool ChildOperateValidPermissions(agent_userinfo_session sessionInfo, string permissions_name)
        {
            if ((sessionInfo.get_users_child_session() != null) && (sessionInfo.get_users_child_session().get_permissions_name().IndexOf(permissions_name) < 0))
            {
                return false;
            }
            return true;
        }
    }
}

