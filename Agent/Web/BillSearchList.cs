namespace Agent.Web
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class BillSearchList : MemberPageBase
    {
        private string amountMax = "";
        private string amountMin = "";
        protected int dataCount;
        protected DataTable dataTable_kc;
        protected DataTable dataTable_six;
        protected string[] FiledName = new string[] { "lid", "finddate", "playid", "isbh", "usertype", "username", "amountmin", "amountmax", "resultmin", "resultmax" };
        protected string[] FiledValue = new string[0];
        private string finddate = "";
        private string isbh = "";
        protected string lotteryID = "";
        protected int page = 1;
        protected int pageCount;
        protected int pageSize = 20;
        private string playid = "";
        private string resultMax = "";
        private string resultMin = "";
        protected string skin = "";
        protected agent_userinfo_session userModel;
        private string username = "";
        private string usertype = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Session["user_name"].ToString();
            this.userModel = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            this.skin = this.userModel.get_u_skin();
            if (!this.userModel.get_u_type().Equals("zj"))
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100057&url=&issuccess=1&isback=0");
                base.Response.End();
            }
            base.Permission_Aspx_ZJ(this.userModel, "po_3_5");
            this.lotteryID = LSRequest.qq("lid");
            this.finddate = LSRequest.qq("finddate");
            this.playid = LSRequest.qq("playid");
            this.isbh = LSRequest.qq("isbh");
            this.usertype = LSRequest.qq("usertype");
            this.username = LSRequest.qq("username");
            this.amountMin = LSRequest.qq("amountmin");
            this.amountMax = LSRequest.qq("amountmax");
            this.resultMin = LSRequest.qq("resultmin");
            this.resultMax = LSRequest.qq("resultmax");
            this.page = Convert.ToInt32("0" + base.q("page"));
            this.page = (this.page == 0) ? 1 : this.page;
            if (this.page < 1)
            {
                this.page = 1;
            }
            List<SqlParameter> list = new List<SqlParameter>();
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(this.playid) && !this.playid.Equals("0"))
            {
                builder.AppendFormat(" and play_id=@play_id ", this.playid);
                SqlParameter parameter = new SqlParameter("@play_id", SqlDbType.NVarChar) {
                    Value = this.playid
                };
                list.Add(parameter);
            }
            string usertype = this.usertype;
            if (usertype != null)
            {
                if (!(usertype == "5"))
                {
                    if (usertype == "4")
                    {
                        if (!string.IsNullOrEmpty(this.username))
                        {
                            builder.AppendFormat(" and dl_name=@dl_name ", this.username);
                            SqlParameter parameter3 = new SqlParameter("@dl_name", SqlDbType.NVarChar) {
                                Value = this.username
                            };
                            list.Add(parameter3);
                        }
                    }
                    else if (usertype == "3")
                    {
                        if (!string.IsNullOrEmpty(this.username))
                        {
                            builder.AppendFormat(" and zd_name=@zd_name ", this.username);
                            SqlParameter parameter4 = new SqlParameter("@zd_name", SqlDbType.NVarChar) {
                                Value = this.username
                            };
                            list.Add(parameter4);
                        }
                    }
                    else if (usertype == "2")
                    {
                        if (!string.IsNullOrEmpty(this.username))
                        {
                            builder.AppendFormat(" and gd_name=@gd_name ", this.username);
                            SqlParameter parameter5 = new SqlParameter("@gd_name", SqlDbType.NVarChar) {
                                Value = this.username
                            };
                            list.Add(parameter5);
                        }
                    }
                    else if ((usertype == "1") && !string.IsNullOrEmpty(this.username))
                    {
                        builder.AppendFormat(" and fgs_name=@fgs_name ", this.username);
                        SqlParameter parameter6 = new SqlParameter("@fgs_name", SqlDbType.NVarChar) {
                            Value = this.username
                        };
                        list.Add(parameter6);
                    }
                }
                else if (!string.IsNullOrEmpty(this.username))
                {
                    builder.AppendFormat(" and u_name=@u_name ", this.username);
                    SqlParameter parameter2 = new SqlParameter("@u_name", SqlDbType.NVarChar) {
                        Value = this.username
                    };
                    list.Add(parameter2);
                }
            }
            builder.AppendFormat(" and bet_time between @beginTime and @endTime ", new object[0]);
            DateTime time = Convert.ToDateTime(this.finddate);
            DateTime now = DateTime.Now;
            now = time.AddDays(1.0);
            string s = this.finddate + " 06:30:00";
            string str3 = now.ToString("yyyy-MM-dd") + " 06:30:00";
            SqlParameter item = new SqlParameter("@beginTime", SqlDbType.NVarChar) {
                Value = s
            };
            list.Add(item);
            SqlParameter parameter12 = new SqlParameter("@endTime", SqlDbType.NVarChar) {
                Value = str3
            };
            list.Add(parameter12);
            if (!string.IsNullOrEmpty(this.amountMin))
            {
                builder.AppendFormat(" and amount >=@amountMin ", new object[0]);
                SqlParameter parameter7 = new SqlParameter("@amountMin", SqlDbType.Float) {
                    Value = this.amountMin
                };
                list.Add(parameter7);
            }
            if (!string.IsNullOrEmpty(this.amountMax))
            {
                builder.AppendFormat(" and amount <=@amountMax ", new object[0]);
                SqlParameter parameter8 = new SqlParameter("@amountMax", SqlDbType.Float) {
                    Value = this.amountMax
                };
                list.Add(parameter8);
            }
            if (!string.IsNullOrEmpty(this.resultMin))
            {
                builder.AppendFormat(" and profit >=@resultMin ", new object[0]);
                SqlParameter parameter9 = new SqlParameter("@resultMin", SqlDbType.Float) {
                    Value = this.resultMin
                };
                list.Add(parameter9);
            }
            if (!string.IsNullOrEmpty(this.resultMax))
            {
                builder.AppendFormat(" and profit <=@resultMax ", new object[0]);
                SqlParameter parameter10 = new SqlParameter("@resultMax", SqlDbType.Float) {
                    Value = this.resultMax
                };
                list.Add(parameter10);
            }
            if (this.isbh.Equals("0"))
            {
                builder.Append(" and isnull(sale_type,'')='' ");
            }
            else if (this.isbh.Equals("1"))
            {
                builder.Append(" and sale_type = 1 ");
            }
            string str4 = "";
            int num = 100;
            if (this.lotteryID.Equals(num.ToString()))
            {
                str4 = "cz_betold_six with(NOLOCK) ";
                DateTime time3 = DateTime.Now;
                time3.ToString("yyyy-MM-dd");
                if ((time3 >= DateTime.Parse(s)) && (time3 <= DateTime.Parse(str3)))
                {
                    str4 = "cz_bet_six with(NOLOCK) ";
                }
                this.dataTable_six = CallBLL.cz_bet_six_bll.BillSearch(this.lotteryID, this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount, builder.ToString(), str4, list);
            }
            else
            {
                str4 = "cz_betold_kc with(NOLOCK) ";
                DateTime time4 = DateTime.Now;
                time4.ToString("yyyy-MM-dd");
                if ((time4 >= DateTime.Parse(s)) && (time4 <= DateTime.Parse(str3)))
                {
                    str4 = "cz_bet_kc with(NOLOCK) ";
                }
                this.dataTable_kc = CallBLL.cz_bet_kc_bll.BillSearch(this.lotteryID, this.page - 1, this.pageSize, ref this.pageCount, ref this.dataCount, builder.ToString(), str4, list);
            }
            this.FiledValue = new string[] { this.lotteryID, this.finddate, this.playid, this.isbh, this.usertype, this.username, this.amountMin, this.amountMax, this.resultMin, this.resultMax };
        }
    }
}

