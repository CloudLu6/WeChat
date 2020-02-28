using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Deepleo.Web.Models;
using SuggestionBox.DB_APP;
using PagedList;
using Deepleo.Web.Attribute;
using System.Web.Security;
using Deepleo.Web.Services;
using System.Text;
using System.Collections.Specialized;
using System.Data.SqlClient;

namespace Deepleo.Web.Controllers
{
    public class DLRecommendationRecordsController : Controller
    {
        private WechartDatabase db = new WechartDatabase();
        private AdminModel admin = new AdminModel();
        private string agentId = WeixinConfig.AgentId;

        [WeixinOAuthAuthorize]
        public ActionResult Home()
        {
            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            string userName = ticket.UserData;
            string userId = ticket.Name;
            return View();
        }
        [WeixinOAuthAuthorize]
        // GET: DLRecommendationRecords
        public ActionResult Index( string searchString,string currentFilter,string SendState, int? page)
        {
            try
            {
                
                var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                string userName = ticket.UserData;
                string userId = ticket.Name;
                Admin ad = admin.Admins.Find(userId, agentId);
                //string userId = "1243710";

                if (ad != null)
                {
                    ViewData["admin"] = 1;
                }
                else
                {
                    ViewData["admin"] = 0;
                }
                
                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }
                RewardYN();
                
                var DLRecommendationRecord = from w in db.DLRecommendationRecords
                                             select w;
                DLRecommendationRecord = DLRecommendationRecord.Where(w => w.RecommenderNo == userId);
                if (!string.IsNullOrEmpty(SendState) && SendState != "all")
                {
                    ViewData["SendState"] = SendState;
                    int Status = Int32.Parse(SendState);
                    DLRecommendationRecord = DLRecommendationRecord.Where(w => w.SendState == Status);
                }
                if (!string.IsNullOrEmpty(searchString))
                {
                    DLRecommendationRecord = DLRecommendationRecord.Where(w => w.ApplicantName.Contains(searchString));
                }
                int pageSize = 8;
                int pageNumber = (page ?? 1);
                var returnMe = DLRecommendationRecord.OrderByDescending(w => w.ApplicantName).ToPagedList(pageNumber, pageSize);

                return View(returnMe);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        // GET: DLRecommendationRecords/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DLRecommendationRecord dLRecommendationRecord = db.DLRecommendationRecords.Find(id);
            if (dLRecommendationRecord == null)
            {
                return HttpNotFound();
            }
            
            DLRecommendationAutoAnswer AutoAnswer = db.DLRecommendationAutoAnswers.Find(1);
            DLRecommendationAutoAnswer AutoAnswer2 = db.DLRecommendationAutoAnswers.Find(2);
            ViewData["VaildIDCard"] = AutoAnswer2.AutoAuswerText;
            if (!RewardYN() || dLRecommendationRecord.SendState == 1)
            { 
                ViewData["RecordPeriod"] = "";
            }
            return View(Tuple.Create(dLRecommendationRecord, AutoAnswer));
            
        }

        private bool RewardYN()
        {
            string str = "SELECT COUNT(*) as countRecord,RewardText FROM [SHAWechart].[dbo].[DLRecommendationRewardPeriod] " +
                " WHERE StartDate<=GETDATE() AND EndDate>GETDATE()-1 group by RewardText ";
            SqlConnection conn = BaseClass.DBCon();
            conn.Open();
            SqlCommand cmd = new SqlCommand(str, conn);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                sdr.Read();
                int count = Int32.Parse(sdr["countRecord"].ToString());
                var RewardText = sdr["RewardText"].ToString();
                sdr.Close();
                conn.Close();
                if (count > 0)
                {
                    ViewData["RecordPeriod"] = RewardText;
                    return true;
                }
                else
                {
                    ViewData["RecordPeriod"] = "";
                    return false;
                }
            }
            else
            {  return false;}
        }
        // GET: DLRecommendationRecords/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DLRecommendationRecords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [WeixinOAuthAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecordNo,RecommenderNo,RecommenderProject,ApplicantName,ApplicantIDCard,ApplicantPhone,RecordTime,SendState,Repeat")] DLRecommendationRecord dLRecommendationRecord)
        {
            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            string userName = ticket.UserData;
            string userId = ticket.Name;

            //string userId = "1243710";
            if (ModelState.IsValid)
            {
                dLRecommendationRecord.RecommenderNo = userId;
                dLRecommendationRecord.Repeat = "N";
                db.DLRecommendationRecords.Add(dLRecommendationRecord);
                db.SaveChanges();
                
                return RedirectToAction("Details", new { id = dLRecommendationRecord.RecordNo });
            }

            return View(dLRecommendationRecord);
        }

        // GET: DLRecommendationRecords/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DLRecommendationRecord dLRecommendationRecord = db.DLRecommendationRecords.Find(id);
            if (dLRecommendationRecord == null)
            {
                return HttpNotFound();
            }
            return View(dLRecommendationRecord);
        }

        // POST: DLRecommendationRecords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[WeixinOAuthAuthorize]
        [WeixinOAuthAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecordNo,RecommenderNo,RecommenderProject,ApplicantName,ApplicantIDCard,ApplicantPhone,RecordTime,SendState,Repeat")] DLRecommendationRecord dLRecommendationRecord)
        {
            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            string userName = ticket.UserData;
            string userId = ticket.Name;

            //string userId = "1243710";
            if (ModelState.IsValid)
            {
                dLRecommendationRecord.RecommenderNo = userId;
                db.Entry(dLRecommendationRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = dLRecommendationRecord.RecordNo });
            }
            return View(dLRecommendationRecord);
        }

        // GET: DLRecommendationRecords/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DLRecommendationRecord dLRecommendationRecord = db.DLRecommendationRecords.Find(id);
            if (dLRecommendationRecord == null)
            {
                return HttpNotFound();
            }
            return View(dLRecommendationRecord);
        }

        // POST: DLRecommendationRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DLRecommendationRecord dLRecommendationRecord = db.DLRecommendationRecords.Find(id);
            db.DLRecommendationRecords.Remove(dLRecommendationRecord);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: DLRecommendationRecords/ConfirmRecommend/5
        public ActionResult ConfirmRecommend(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DLRecommendationRecord dLRecommendationRecord = db.DLRecommendationRecords.Find(id);
            if (dLRecommendationRecord == null)
            {
                return HttpNotFound();
            }
            //发送状态设为1(已发送)
            string updStr = " UPDATE DLRecommendationRecord SET RecordTime =GETDATE(),SendState=1  WHERE RecordNo ='" + id + "'";
            BaseClass.OperateData(updStr);
            return RedirectToAction("Details", new { id = id });
        }

        public ActionResult ConfirmRepeatRecommend(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DLRecommendationRecord dLRecommendationRecord = db.DLRecommendationRecords.Find(id);
            if (dLRecommendationRecord == null)
            {
                return HttpNotFound();
            }

            //发送状态设为1(已发送)
            string updStr = " UPDATE DLRecommendationRecord SET RecordTime =GETDATE(),SendState=1,Repeat = 'Y' WHERE RecordNo ='" + id + "'";
            BaseClass.OperateData(updStr);

            return RedirectToAction("Details", new { id = id });
        }

        [HttpPost]
        public String vaildIDCard()
        {
            try
            {
                string applicantIDCard = Request.QueryString.Get("applicantIDCard");
              
                int count = 0;
                //var rewardRecord = from w in db.V_RewardRecord
                //                   where w.StartDate <= DateTime.Now && w.EndDate >= DateTime.Now && w.ApplicantIDCard.Equals(applicantIDCard)
                //                   select w;
                string str = "SELECT COUNT(*) as countRecord FROM [SHAWechart].[dbo].[V_RewardRecord] " +
                    " WHERE StartDate<=GETDATE() AND EndDate>=GETDATE()-1 AND ApplicantIDCard = " + applicantIDCard;
                SqlConnection conn = BaseClass.DBCon();
                conn.Open();
                SqlCommand cmd = new SqlCommand(str, conn);
                SqlDataReader sdr = cmd.ExecuteReader();
                sdr.Read();
                count = Int32.Parse(sdr["countRecord"].ToString());
                sdr.Close();
                conn.Close();
                if (count == 0)
                    return "1";
                else
                    return "0";
            }
            catch (Exception ex)
            {
                return  ex.Message ;
            }
        }


        // GET: http://localhost:55210/DLRecommendationRecords/Report
        public ActionResult Report(string searchString, string currentFilter, string SendState, string searchDate, string currentDate, int? page)
        {
            try
            {
               
                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }
                var DLRecommenderInfo = from w in db.V_DLRecommenderInfo
                                             select w;
                if (!string.IsNullOrEmpty(searchDate))
                {
                    string[] dateArray = searchDate.Split('-');// 一定是单引 
                    ViewData["startDate"] = dateArray[0];
                    ViewData["endDate"] = dateArray[1];
                    DateTime startDate = Convert.ToDateTime(dateArray[0]);
                    DateTime endDate = Convert.ToDateTime(dateArray[1]).AddDays(1);
                   DLRecommenderInfo = from w in db.V_DLRecommenderInfo
                                             where w.RecordTime >= startDate && w.RecordTime < endDate
                                 select w;
                }
                
                int pageSize = 15;
                int pageNumber = (page ?? 1);
                var returnMe = DLRecommenderInfo.OrderByDescending(w => w.ApplicantName).ToPagedList(pageNumber, pageSize);

                return View(returnMe);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
      
    }
}
