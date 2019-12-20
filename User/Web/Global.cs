namespace User.Web
{
    using LotterySystem.Common;
    using LotterySystem.WebPageBase;
    using System;
    using System.Configuration;
    using System.IO;
    using System.Web;
    using User.Web.WebBase;

    public class Global : HttpApplication
    {
        private void Application_End(object sender, EventArgs e)
        {
        }

        private void Application_Error(object sender, EventArgs e)
        {
            string str = ConfigurationManager.AppSettings["LogException"];
            if (str.Trim() == "true")
            {
                Exception lastError = base.Server.GetLastError();
                Exception innerException = lastError.InnerException;
                string message = string.Empty;
                string stackTrace = string.Empty;
                if (innerException != null)
                {
                    message = innerException.Message;
                    stackTrace = innerException.StackTrace;
                }
                else
                {
                    message = lastError.Message;
                    stackTrace = lastError.StackTrace;
                }
                string str4 = "";
                if (HttpContext.Current.Session["user_name"] != null)
                {
                    str4 = HttpContext.Current.Session["user_name"].ToString();
                }
                string str5 = string.Format("{0} {1} StackTrace {2}", (str4 == "") ? "" : ("【" + str4 + "】"), message, stackTrace);
                MessageQueueConfig.TaskQueue.Enqueue(new TaskModel(0, str5));
                base.Server.ClearError();
                HttpContext.Current.Response.Redirect("~/CommonError.html");
            }
        }

        private void Application_Start(object sender, EventArgs e)
        {
            CallBLL.Call();
            PageBase.SetCacheByFile("MessageHint");
            PageBase.SetCacheByFile("HtmlHeaderHint");
            FileCacheHelper.SetConfigCacheByFile();
            FileCacheHelper.SetConfigZodiacCacheByFile();
            FileCacheHelper.SetConfigLotteryCacheByFile();
            PageBase.SetWebconfigApp();
            string str = ConfigurationManager.AppSettings["LogException"];
            if (str.Trim() == "true")
            {
                MessageQueueConfig.set_folderType(1);
                MessageQueueConfig.RegisterMessageQueue();
            }
        }

        private void DelOldErrorLog()
        {
            try
            {
                string path = FileCacheHelper.GetWebRootPath(1, "publics") + @"\LogError";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (Directory.Exists(path))
                {
                    FileInfo[] files = new DirectoryInfo(path).GetFiles("*.log");
                    DateTime time2 = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")).AddDays(-10.0);
                    foreach (FileInfo info2 in files)
                    {
                        if (info2.CreationTime < time2)
                        {
                            info2.Delete();
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void Session_End(object sender, EventArgs e)
        {
        }

        private void Session_Start(object sender, EventArgs e)
        {
            if (FileCacheHelper.g_jp_path == "")
            {
                FileCacheHelper.g_jp_path = PageBase.GetPublicForderPath(FileCacheHelper.AutoJPCachesFileName);
            }
        }
    }
}

