namespace Agent.Web
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public class LogUserChange : MemberPageBase
    {
        protected string cloneName = "";
        protected int dataCount;
        protected DataTable dataTable;
        protected string[] FiledName = new string[] { "uid" };
        protected string[] FiledValue = new string[0];
        protected bool isAll = true;
        protected bool isChild;
        protected bool isCloneUser;
        protected List<string> levelList = new List<string>();
        protected string login_name = "";
        protected DataTable lotteryDT;
        protected string master_name = "";
        protected string page = "1";
        protected int pageCount;
        protected int pageSize = 20;
        protected string u_id = "";
        protected string u_name = "";
        protected string u_type = "";
        protected string url = "";
        protected string zj_name = "";

        protected bool isMyAbove(string edit_user)
        {
            if (edit_user == this.zj_name)
            {
                return true;
            }
            if (!this.levelList.Contains(edit_user))
            {
                return false;
            }
            return (this.master_name != edit_user);
        }

        protected bool isZJLoginName()
        {
            return this.u_type.Equals("zj");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Session["user_name"].ToString();
            this.login_name = str;
            this.master_name = str;
            agent_userinfo_session model = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            base.Permission_Aspx_ZJ(model, "po_2_1");
            base.Permission_Aspx_DL(model, "po_6_1");
            this.u_type = model.get_u_type().Trim();
            this.zj_name = model.get_zjname().Trim();
            if (this.Session["child_user_name"] != null)
            {
                this.isCloneUser = true;
                this.cloneName = this.Session["child_user_name"].ToString();
                this.login_name = this.cloneName;
            }
            if (!this.u_type.Equals("zj"))
            {
                this.levelList.Add(model.get_fgs_name());
                this.levelList.Add(model.get_zd_name());
                this.levelList.Add(model.get_gd_name());
                this.levelList.Add(model.get_dl_name());
            }
            this.u_id = LSRequest.qq("uid");
            if (string.IsNullOrEmpty(this.u_id))
            {
                base.Response.End();
            }
            this.u_name = CallBLL.cz_users_bll.GetUserNameByUid(this.u_id, ref this.isChild);
            if (string.IsNullOrEmpty(this.u_name))
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100014&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            if (CallBLL.cz_users_bll.GetUserInfoByUID(this.u_id) == null)
            {
                cz_users_child userByUID = CallBLL.cz_users_child_bll.GetUserByUID(this.u_id);
                if ((userByUID != null) && userByUID.get_parent_u_name().Equals(model.get_u_name()))
                {
                    if (!base.IsUpperLowerLevels(userByUID.get_parent_u_name(), model.get_u_type(), model.get_u_name()))
                    {
                        base.Response.Redirect("/MessagePage.aspx?code=u100014&url=&issuccess=1&isback=0");
                        base.Response.End();
                    }
                }
                else
                {
                    base.Response.Redirect("/MessagePage.aspx?code=u100014&url=&issuccess=1&isback=0");
                    base.Response.End();
                }
            }
            else if (!base.IsUpperLowerLevels(this.u_name, model.get_u_type(), model.get_u_name()))
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100014&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            this.lotteryDT = base.GetLotteryList();
            DataRow[] source = this.lotteryDT.Select(string.Format(" id=100 ", new object[0]));
            string str2 = "";
            if (source.Count<DataRow>() <= 0)
            {
                str2 = "200,300";
            }
            else if (this.lotteryDT.Rows.Count > 1)
            {
                str2 = "100,200,300";
            }
            else
            {
                str2 = "100,300";
            }
            this.page = LSRequest.qq("page");
            if (string.IsNullOrEmpty(this.page))
            {
                this.page = "1";
            }
            if (int.Parse(this.page) < 1)
            {
                this.page = "1";
            }
            this.dataTable = CallBLL.cz_user_change_log_bll.get_log_table(Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.u_name.Trim(), str2, ref this.isAll);
            this.FiledValue = new string[] { this.u_id };
        }
    }
}

