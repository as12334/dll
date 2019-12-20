namespace Agent.Web.L_SIX
{
    using Agent.Web.WebBase;
    using LotterySystem.Model;
    using System;

    public class Betimes_qmwx : MemberPageBase
    {
        protected string isAllow_sale = "1";
        protected string isOperation = "1";
        protected string jeucode = "";
        protected string playIds = "";
        protected string playpage = "";
        protected string userType = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.playIds = base.GetSIX_PlayIDChange("qmwx");
            this.playpage = "qmwx";
            agent_userinfo_session model = this.Session[this.Session["user_name"] + "lottery_session_user_info"] as agent_userinfo_session;
            base.Permission_Aspx_ZJ(model, "po_1_1");
            base.Permission_Aspx_DL(model, "po_5_1");
            string str2 = model.get_u_type();
            if (str2 != null)
            {
                if (!(str2 == "zj"))
                {
                    if (str2 == "fgs")
                    {
                        this.userType = "2";
                        goto Label_00AC;
                    }
                }
                else
                {
                    this.userType = "1";
                    goto Label_00AC;
                }
            }
            this.userType = "0";
        Label_00AC:
            this.jeucode = base.get_JeuValidate();
            this.Session["JeuValidate"] = this.jeucode;
            if (string.IsNullOrEmpty(model.get_allow_sale().ToString()))
            {
                this.isAllow_sale = "0";
            }
            else
            {
                this.isAllow_sale = model.get_allow_sale().ToString();
                if (model.get_users_child_session() != null)
                {
                    if (model.get_u_type().Equals("zj"))
                    {
                        if (model.get_users_child_session().get_permissions_name().IndexOf("po_1_3") < 0)
                        {
                            this.isAllow_sale = "0";
                        }
                    }
                    else if (model.get_users_child_session().get_permissions_name().IndexOf("po_5_2") < 0)
                    {
                        this.isAllow_sale = "0";
                    }
                }
            }
            this.isOperation = base.GetIsOperation(model, 1);
        }
    }
}

