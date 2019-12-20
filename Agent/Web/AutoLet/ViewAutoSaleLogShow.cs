namespace Agent.Web.AutoLet
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;

    public class ViewAutoSaleLogShow : MemberPageBase
    {
        protected string backUrl = "";
        protected string cloneName = "";
        protected cz_users cz_users_model;
        protected int dataCount;
        protected DataTable dataTable;
        protected string[] FiledName = new string[] { "uid", "lid" };
        protected string[] FiledValue = new string[0];
        protected bool isAll = true;
        protected bool isCloneUser;
        protected List<string> levelList = new List<string>();
        protected string login_name = "";
        protected DataTable lotteryDT;
        protected string lotteryId = "";
        protected string master_name = "";
        protected string mId = "";
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
            base.Permission_Aspx_DL(model, "po_5_2");
            this.zj_name = model.get_zjname().Trim();
            this.u_type = model.get_u_type().Trim();
            if (this.Session["child_user_name"] != null)
            {
                this.isCloneUser = true;
                this.cloneName = this.Session["child_user_name"].ToString();
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
                this.u_id = model.get_u_id();
            }
            if (string.IsNullOrEmpty(this.u_id))
            {
                base.Response.End();
            }
            this.backUrl = string.Format("", new object[0]);
            this.cz_users_model = CallBLL.cz_users_bll.GetUserInfoByUID(this.u_id);
            if (string.IsNullOrEmpty(this.cz_users_model.get_u_name()))
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100014&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            if (!base.IsUpperLowerLevels(this.cz_users_model.get_u_name(), model.get_u_type(), model.get_u_name()))
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100014&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            this.mId = LSRequest.qq("mid");
            this.lotteryId = LSRequest.qq("lid");
            if (string.IsNullOrEmpty(this.lotteryId))
            {
                this.lotteryId = "";
            }
            int num = 1;
            if (this.mId.Equals(num.ToString()))
            {
                this.lotteryId = 100.ToString();
            }
            this.lotteryDT = base.GetLotteryList();
            base.IsLotteryExistForSysLog(this.lotteryId, "u100032", "1", "");
            string lotteryId = "";
            ArrayList list = new ArrayList();
            if (this.lotteryId == "")
            {
                foreach (DataRow row in this.lotteryDT.Rows)
                {
                    int num3 = 100;
                    if (!row["id"].ToString().Equals(num3.ToString()))
                    {
                        list.Add(row["id"].ToString());
                    }
                }
                lotteryId = string.Join(",", list.ToArray());
            }
            else
            {
                lotteryId = this.lotteryId;
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
            this.dataTable = CallBLL.cz_autosale_log_bll.get_log_table(Convert.ToInt32(this.page) - 1, this.pageSize, ref this.pageCount, ref this.dataCount, this.cz_users_model.get_u_name().Trim(), lotteryId, ref this.isAll);
            this.FiledValue = new string[] { this.u_id, this.lotteryId };
            int num4 = 100;
            if (this.lotteryId.Equals(num4.ToString()))
            {
                this.backUrl = string.Format("/AutoLet/AutoLet_Show_six.aspx?uid={0}&mid={1}", this.u_id, 1);
            }
            else
            {
                this.backUrl = string.Format("/AutoLet/AutoLet_Show_kc.aspx?uid={0}&mid={1}", this.u_id, 2);
            }
        }
    }
}

