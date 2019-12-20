using System;
using System.Web.UI;

public class m_Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.Response.Expires = 0;
        this.Session.Abandon();
        base.Response.Write("<script>top.location.href='Login.aspx'</script>");
    }
}

