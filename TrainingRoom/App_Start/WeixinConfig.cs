using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Deepleo.Weixin.SDK.Helpers;

namespace Deepleo.Web
{
    public class WeixinConfig
    {
        public static string domain { private set; get; }
        public static string EncodingAESKey { private set; get; }
        public static string AppID { private set; get; }
        public static string AppSecret { private set; get; }
        public static string AgentId { private set; get; }
        public static string OauthScope { private set; get; }

        public static string appPath { private set; get; }
        public static TokenHelper TokenHelper { private set; get; }

        public static void Register()
        {
            appPath = System.Configuration.ConfigurationManager.AppSettings["appPath"];
            domain = System.Configuration.ConfigurationManager.AppSettings["Domain"];
            EncodingAESKey = System.Configuration.ConfigurationManager.AppSettings["EncodingAESKey"];
            AppID = System.Configuration.ConfigurationManager.AppSettings["AppID"];
            AppSecret = System.Configuration.ConfigurationManager.AppSettings["AppSecret"];
            AgentId = System.Configuration.ConfigurationManager.AppSettings["AgentId"];
            var openJSSDK = int.Parse(System.Configuration.ConfigurationManager.AppSettings["OpenJSSDK"]) > 0;
            OauthScope = System.Configuration.ConfigurationManager.AppSettings["OauthScope"];
            TokenHelper = new TokenHelper(6000, AppID, AppSecret, openJSSDK);
            TokenHelper.Run();
        }
    }
}