using Deepleo.Web.Attribute;
using Deepleo.Web.Models;
using Deepleo.Web.Services;
using Senparc.Weixin.Work.AdvancedAPIs;
using Senparc.Weixin.Work.AdvancedAPIs.OAuth2;
using SuggestionBox.DB_APP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Deepleo.Web.Controllers
{
    public class OAuth2ApiController : Controller
    {
        private WechartDatabase ms = new WechartDatabase();
        // GET: OAuth2Api
        [WeixinOAuthAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Callback()
        {
            var access_token = "";
            var code = "";

            try
            {
                code = Request.QueryString.Get("code");
                if (string.IsNullOrEmpty(code))//没有code表示授权失败
                {
                    return Content("请从正规途径进入");
                }

                var state = Request.QueryString.Get("state");
                var cache_status = System.Web.HttpContext.Current.Cache.Get(state);
                var redirect_url = cache_status == null ? "/" : cache_status.ToString();//没有获取到state,就跳转到首页
                var scope = WeixinConfig.OauthScope;

                access_token = WeixinConfig.TokenHelper.GetToken();//基础支持中的access_token
                GetUserInfoResult UserInfo = OAuth2Api.GetUserId(access_token, code);
                string userId = UserInfo.UserId;
                string user_ticket = UserInfo.user_ticket;
                GetUserDetailResult userDetail = OAuth2Api.GetUserDetail(access_token, user_ticket);
                string userName = userDetail.name;
                AuthorizationManager.SetTicket(false, 1, userId, userName);
                Thread.Sleep(500);//暂停半秒钟，以等待IOS设置Cookies的延迟
                LogWriter.Default.WriteInfo(string.Format("OAuth success: identity: {0} , name: {1} , redirect_rul:{2} ", code, userId, redirect_url));


                EmployeeInfo employeeInfo = ms.EmployeeInfo.Find(userId);
                if (employeeInfo == null)
                {
                    string insStr = "INSERT INTO EmployeeInfo ([EmployeeNo],[EmployeeName],[EmployeePhone]) " +
                         "VALUES ('" + userId + "',N'" + userDetail.name + "','" + userDetail.mobile + "');";
                    BaseClass.OperateData(insStr);
                }
                else
                {
                    string updStr = "UPDATE EmployeeInfo SET EmployeeName =N'" + userDetail.name +
                           "',EmployeePhone= '" + userDetail.mobile + "' WHERE EmployeeNo = '" + userId + "'";
                    BaseClass.OperateData(updStr);
                }


                return new RedirectResult(redirect_url, true);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
    }
}