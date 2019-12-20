using LotterySystem.Common;
using System;
using User.Web.WebBase;

public class m_Login : MemberPageBase_Mobile
{
    protected string codeFlag = "false";
    protected string CodeValidate_Mobile = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (int.Parse(FileCacheHelper.get_GetLockedPasswordCount()) == 0)
        {
            this.codeFlag = "true";
        }
        else
        {
            this.codeFlag = "false";
        }
        this.Session.Abandon();
        base.Response.Buffer = true;
        base.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1.0);
        base.Response.Expires = 0;
        base.Response.CacheControl = "no-cache";
        base.Response.AddHeader("Pragma", "No-Cache");
        this.CodeValidate_Mobile = base.CodeValidateByMobile();
    }
}

