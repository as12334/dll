namespace Agent.Web.L_SIX
{
    using Agent.Web.WebBase;
    using LotterySystem.Model;
    using System;

    public class Betimes_zmt3 : MemberPageBase
    {
        protected string isAllow_sale = "1";
        protected string isOperation = "1";
        protected string jeucode = "";
        protected string playIds = "";
        protected string playpage = "";
        protected agent_userinfo_session uModel;
        protected string userType = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.playIds = base.GetSIX_PlayIDChange("zmt3");
            this.playpage = "zmt3";
            this.uModel = this.Session[this.Session["user_name"] + "lottery_session_user_info"] as agent_userinfo_session;
            base.Permission_Aspx_ZJ(this.uModel, "po_1_1");
            base.Permission_Aspx_DL(this.uModel, "po_5_1");
            string str2 = this.uModel.get_u_type();
            if (str2 != null)
            {
                if (!(str2 == "zj"))
                {
                    if (str2 == "fgs")
                    {
                        this.userType = "2";
                        goto Label_00C0;
                    }
                }
                else
                {
                    this.userType = "1";
                    goto Label_00C0;
                }
            }
            this.userType = "0";
        Label_00C0:
            this.jeucode = base.get_JeuValidate();
            this.Session["JeuValidate"] = this.jeucode;
            if (string.IsNullOrEmpty(this.uModel.get_allow_sale().ToString()))
            {
                this.isAllow_sale = "0";
            }
            else
            {
                this.isAllow_sale = this.uModel.get_allow_sale().ToString();
                if (this.uModel.get_users_child_session() != null)
                {
                    if (this.uModel.get_u_type().Equals("zj"))
                    {
                        if (this.uModel.get_users_child_session().get_permissions_name().IndexOf("po_1_3") < 0)
                        {
                            this.isAllow_sale = "0";
                        }
                    }
                    else if (this.uModel.get_users_child_session().get_permissions_name().IndexOf("po_5_2") < 0)
                    {
                        this.isAllow_sale = "0";
                    }
                }
            }
            this.isOperation = base.GetIsOperation(this.uModel, 1);
        }
    }
}

