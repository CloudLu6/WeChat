using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Deepleo.Web.Models;
using System.Web.UI;
using Deepleo.Web.Attribute;
using System.Web.Security;

namespace Deepleo.Web.Controllers
{
    public class DLRecommendationRewardPeriodsController : Controller
    {
        private WechartDatabase db = new WechartDatabase();

        // GET: DLRecommendationRewardPeriods
        [WeixinOAuthAuthorize]
        public ActionResult Index()
        {
            var DLList = from w in db.DLRecommendationRewardPeriods
                         where w.flag.Equals(1)
                         select w;
            return View(DLList.ToList());
        }

        // GET: DLRecommendationRewardPeriods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DLRecommendationRewardPeriod dLRecommendationRewardPeriod = db.DLRecommendationRewardPeriods.Find(id);
            if (dLRecommendationRewardPeriod == null)
            {
                return HttpNotFound();
            }
            return View(dLRecommendationRewardPeriod);
        }

        // GET: DLRecommendationRewardPeriods/Create
        [WeixinOAuthAuthorize]
        public ActionResult Create()
        {
            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            string userName = ticket.UserData;
            string userId = ticket.Name;
            ViewData["EmpNo"] = userId;
            ViewData["CurrentTime"] = DateTime.Now.ToString();
            return View();
        }

        // POST: DLRecommendationRewardPeriods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RewardID,StartDate,EndDate,ModifyUser,ModifyTime,flag,RewardText")] DLRecommendationRewardPeriod dLRecommendationRewardPeriod)
        {
            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            string userName = ticket.UserData;
            string userId = ticket.Name; 
            if (ModelState.IsValid)
            {
                dLRecommendationRewardPeriod.flag = 1;
                dLRecommendationRewardPeriod.ModifyUser = userId;
                dLRecommendationRewardPeriod.ModifyTime = Convert.ToDateTime(DateTime.Now.ToString()); ;
                db.DLRecommendationRewardPeriods.Add(dLRecommendationRewardPeriod);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dLRecommendationRewardPeriod);
        }

        // GET: DLRecommendationRewardPeriods/Edit/5
        [WeixinOAuthAuthorize]
        public ActionResult Edit(int? id)
        {
            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            string userName = ticket.UserData;
            string userId = ticket.Name;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DLRecommendationRewardPeriod dLRecommendationRewardPeriod = db.DLRecommendationRewardPeriods.Find(id);
            if (dLRecommendationRewardPeriod == null)
            {
                return HttpNotFound();
            }
            ViewData["EmpNo"] = userId;
            ViewData["CurrentTime"] = DateTime.Now.ToString();
            return View(dLRecommendationRewardPeriod);
        }

        // POST: DLRecommendationRewardPeriods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RewardID,StartDate,EndDate,ModifyUser,ModifyTime,flag,RewardText")] DLRecommendationRewardPeriod dLRecommendationRewardPeriod)
        {
            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            string userName = ticket.UserData;
            string userId = ticket.Name;
            if (ModelState.IsValid)
            {
                dLRecommendationRewardPeriod.flag = 1;
                dLRecommendationRewardPeriod.ModifyUser = userId;
                dLRecommendationRewardPeriod.ModifyTime = Convert.ToDateTime(DateTime.Now.ToString()); 
                db.Entry(dLRecommendationRewardPeriod).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dLRecommendationRewardPeriod);
        }

        // GET: DLRecommendationRewardPeriods/Delete/5
        [WeixinOAuthAuthorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DLRecommendationRewardPeriod dLRecommendationRewardPeriod = db.DLRecommendationRewardPeriods.Find(id);
            if (dLRecommendationRewardPeriod == null)
            {
                return HttpNotFound();
            }
            return View(dLRecommendationRewardPeriod);
        }

        // POST: DLRecommendationRewardPeriods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            string userName = ticket.UserData;
            string userId = ticket.Name;
            DLRecommendationRewardPeriod dLRecommendationRewardPeriod = db.DLRecommendationRewardPeriods.Find(id);
            dLRecommendationRewardPeriod.flag = 0;
            dLRecommendationRewardPeriod.ModifyUser = userId;
            db.Entry(dLRecommendationRewardPeriod).State = EntityState.Modified;
            //db.DLRecommendationRewardPeriods.Remove(dLRecommendationRewardPeriod);
            db.SaveChanges();
            return RedirectToAction("Index");
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
