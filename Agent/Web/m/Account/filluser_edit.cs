namespace Agent.Web.m.Account
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI.HtmlControls;

    public class filluser_edit : MemberPageBase_Mobile
    {
        protected HtmlForm form1;
        protected DataTable lotteryDT;
        protected string lottrty_kc = "";
        protected string lottrty_six = "";
        protected string uid = "";
        protected string upuname = "";

        private void AddUser()
        {
            string str = LSRequest.qq("nicker");
            string str2 = LSRequest.qq("userKind");
            string message = "";
            if (!base.ValidParamByUserEdit("filluser", ref message, null, this.lottrty_six, this.lottrty_kc))
            {
                base.noRightOptMsg(message);
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
                base.successOptMsg("修改外補會員成功！");
            }
            else
            {
                base.noRightOptMsg("修改外補會員失敗！");
            }
        }

        private void getMemberDetail(ref string strResult)
        {
            base.checkLoginByHandler(0);
            ReturnResult result = new ReturnResult();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            new List<object>();
            string str = this.Session["user_name"].ToString();
            agent_userinfo_session _session = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (!_session.get_u_type().Trim().Equals("zj"))
            {
                base.Response.End();
            }
            base.Permission_Aspx_ZJ_Mobile("po_2_3");
            this.uid = LSRequest.qq("uid");
            string str2 = LSRequest.qq("memberId");
            string str3 = LSRequest.qq("submitType");
            if (str2 != "sales")
            {
                base.Response.End();
            }
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
            cz_saleset_six model = null;
            model = CallBLL.cz_saleset_six_bll.GetModel(this.uid);
            if (model == null)
            {
                base.Response.End();
            }
            switch (str3)
            {
                case "view":
                {
                    Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
                    dictionary2.Add("name", model.get_u_name());
                    dictionary2.Add("nicker", model.get_u_nicker());
                    dictionary2.Add("userKind", model.get_six_kind().ToUpper());
                    dictionary2.Add("date", model.get_add_date());
                    dictionary2.Add("type", model.get_flag());
                    dictionary = dictionary2;
                    result.set_success(200);
                    result.set_data(dictionary);
                    strResult = base.ObjectToJson(result);
                    break;
                }
                case "edit":
                    this.AddUser();
                    break;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str3;
            base.Response.Expires = 0;
            base.Response.CacheControl = "no-cache";
            string str = LSRequest.qq("action").Trim();
            string strResult = "";
            if (((str3 = str) != null) && (str3 == "getMemberDetail"))
            {
                this.getMemberDetail(ref strResult);
            }
            base.OutJson(strResult);
        }
    }
}

