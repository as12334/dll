namespace Agent.Web.Account
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;

    public class filluser_edit : MemberPageBase
    {
        protected string checkredio = "";
        protected cz_saleset_six cz_saleset_six_model;
        protected DataTable lotteryDT;
        protected string lottrty_kc = "";
        protected string lottrty_six = "";
        protected string uid = "";
        protected string upuname = "";
        protected agent_userinfo_session userModel;
        protected string utypeTxt = "";

        private void AddUser()
        {
            string str = LSRequest.qq("userNicker");
            string str2 = LSRequest.qq("userKind_six");
            string message = "";
            if (!base.ValidParamByUserEdit("filluser", ref message, null, this.lottrty_six, this.lottrty_kc))
            {
                base.Response.Write(base.ShowDialogBox(message, null, 400));
                base.Response.End();
            }
            if ((!str2.ToUpper().Equals("A") && !str2.ToUpper().Equals("B")) && !str2.ToUpper().Equals("C"))
            {
                base.Response.End();
            }
            cz_saleset_six _six = new cz_saleset_six();
            _six.set_u_id(this.uid);
            _six.set_u_nicker(str.Trim());
            _six.set_six_kind(str2.Trim().ToUpper());
            if (CallBLL.cz_saleset_six_bll.UpdateUser(_six))
            {
                base.Response.Write(base.ShowDialogBox("修改外補會員成功！", base.UserReturnBackUrl, 0));
                base.Response.End();
            }
            else
            {
                base.Response.Write(base.ShowDialogBox("修改外補會員失敗！", base.UserReturnBackUrl, 400));
                base.Response.End();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Session["user_name"].ToString();
            this.userModel = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (!this.userModel.get_u_type().Trim().Equals("zj"))
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100035&url=&issuccess=1&isback=0&isopen=1");
                base.Response.End();
            }
            base.Permission_Aspx_ZJ(this.userModel, "po_2_3");
            this.lotteryDT = base.GetLotteryList();
            DataTable table = this.lotteryDT.DefaultView.ToTable(true, new string[] { "master_id" });
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (Convert.ToInt32(table.Rows[i][0]).Equals(1))
                {
                    this.lottrty_six = table.Rows[i][0].ToString();
                }
                else if (Convert.ToInt32(table.Rows[i][0]).Equals(2))
                {
                    this.lottrty_kc = table.Rows[i][0].ToString();
                }
            }
            this.uid = LSRequest.qq("uid");
            this.cz_saleset_six_model = CallBLL.cz_saleset_six_bll.GetModel(this.uid);
            if (this.cz_saleset_six_model == null)
            {
                base.Response.Redirect("../MessagePage.aspx?code=u100034&url=&issuccess=1&isback=0&isopen=1");
                base.Response.End();
            }
            if (LSRequest.qq("hdnadd").Equals("hdnadd"))
            {
                this.AddUser();
            }
        }
    }
}

