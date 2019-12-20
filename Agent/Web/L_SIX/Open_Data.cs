namespace Agent.Web.L_SIX
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Web.UI.HtmlControls;

    public class Open_Data : MemberPageBase
    {
        protected HtmlForm form1;

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Session["user_name"].ToString();
            agent_userinfo_session model = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (!model.get_u_type().Trim().Equals("zj"))
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100014&url=&issuccess=1&isback=0");
            }
            base.Permission_Aspx_ZJ(model, "po_3_1");
            if (base.IsChildSync())
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100080&url=&issuccess=1&isback=0");
            }
            string s = LSRequest.qq("pid");
            string str3 = "/LotteryPeriod/AwardPeriod.aspx?lid=100";
            cz_phase_six phaseModel = CallBLL.cz_phase_six_bll.GetPhaseModel(int.Parse(s));
            if (!phaseModel.get_is_closed().Equals(1))
            {
                base.Response.Redirect(string.Format("../MessagePage.aspx?code=u100070&url={0}&issuccess=1&isback=1", base.Server.UrlDecode(str3)));
            }
            if (!phaseModel.get_is_payment().Equals(1))
            {
                base.Response.Redirect(string.Format("../MessagePage.aspx?code=u100070&url={0}&issuccess=1&isback=1", base.Server.UrlDecode(str3)));
            }
            if (CallBLL.cz_phase_six_bll.OpenData(s))
            {
                string str4 = null;
                if (model.get_users_child_session() != null)
                {
                    str4 = model.get_users_child_session().get_u_name();
                }
                string str5 = "";
                string str6 = "";
                cz_lotteryopen_log _log = new cz_lotteryopen_log();
                _log.set_phase_id(phaseModel.get_p_id());
                _log.set_phase(phaseModel.get_phase());
                _log.set_u_name(model.get_u_name());
                _log.set_children_name(str4);
                _log.set_action("開放數據");
                _log.set_old_val(str5);
                _log.set_new_val(str6);
                _log.set_ip(LSRequest.GetIP());
                _log.set_add_time(DateTime.Now);
                _log.set_note(string.Format("【本期編號:{0}】開放數據", _log.get_phase()));
                _log.set_type_id(0);
                _log.set_lottery_id(100);
                CallBLL.cz_lotteryopen_log_bll.Insert(_log);
                base.Response.Write(base.ShowDialogBox("開放數據成功！", string.Format("LotteryPeriod/AwardPeriod.aspx?lid={0}", 100), 0));
            }
            else
            {
                base.Response.Redirect(string.Format("../MessagePage.aspx?code=u100070&url={0}&issuccess=1&isback=1", base.Server.UrlDecode(str3)));
            }
        }
    }
}

