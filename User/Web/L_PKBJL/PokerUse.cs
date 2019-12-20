namespace User.Web.L_PKBJL
{
    using LotterySystem.Common;
    using System;
    using System.Data;
    using System.Text;
    using User.Web.WebBase;

    public class PokerUse : MemberPageBase
    {
        protected DataTable dataTable;

        protected string GetPokerStr(int min, int max)
        {
            StringBuilder builder = new StringBuilder();
            if (this.dataTable == null)
            {
                builder.Append(this.GetPokerStrNull(min, max));
            }
            else
            {
                for (int i = min; i <= max; i++)
                {
                    int num2 = 8;
                    int num3 = 0;
                    int num4 = 0;
                    DataRow[] rowArray = this.dataTable.Select(string.Format(" poker={0} ", i));
                    if ((this.dataTable != null) && (rowArray.Length > 0))
                    {
                        int num5 = int.Parse(rowArray[0]["poker"].ToString());
                        int num6 = int.Parse(rowArray[0]["used"].ToString());
                        if (num5.Equals(i))
                        {
                            num3 = num6;
                            num4 = num2 - num6;
                        }
                        else
                        {
                            num4 = num2;
                        }
                    }
                    else
                    {
                        num4 = num2;
                    }
                    builder.Append("<li>");
                    builder.AppendFormat("<img src=\"Images/poker{0}.png\" />", i);
                    builder.AppendFormat("<p>出:{0}&nbsp;剩:{1}</p>", num3, num4);
                    builder.Append("</li>");
                }
            }
            return builder.ToString();
        }

        protected string GetPokerStrNull(int min, int max)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = min; i <= max; i++)
            {
                int num2 = 8;
                int num3 = 0;
                int num4 = 0;
                num4 = num2;
                builder.Append("<li>");
                builder.AppendFormat("<img src=\"Images/poker{0}.png\" />", i);
                builder.AppendFormat("<p>出:{0}&nbsp;剩:{1}</p>", num3, num4);
                builder.Append("</li>");
            }
            return builder.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str2 = Utils.GetPKBJL_NumTable(LSRequest.qq("playpage"), "");
            if (!string.IsNullOrEmpty(str2))
            {
                this.dataTable = CallBLL.cz_poker_pkbjl_bll.GetCountByTabletype(str2);
            }
            else
            {
                base.Response.Redirect(string.Format("/MessagePage.aspx?code={0}&issuccess={1}&url={2}&isback={3}", new object[] { "u100022", "1", "", "0" }));
            }
        }
    }
}

