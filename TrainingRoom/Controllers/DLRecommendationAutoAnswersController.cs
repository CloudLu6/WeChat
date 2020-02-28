using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Deepleo.Web.Models;

namespace Deepleo.Web.Controllers
{
    public class DLRecommendationAutoAnswersController : Controller
    {
        private WechartDatabase db = new WechartDatabase();

        // GET: DLRecommendationAutoAnswers
        public ActionResult Index()
        {
            return View(db.DLRecommendationAutoAnswers.ToList());
        }

        // GET: DLRecommendationAutoAnswers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DLRecommendationAutoAnswer dLRecommendationAutoAnswer = db.DLRecommendationAutoAnswers.Find(id);
            if (dLRecommendationAutoAnswer == null)
            {
                return HttpNotFound();
            }
            return View(dLRecommendationAutoAnswer);
        }

        // GET: DLRecommendationAutoAnswers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DLRecommendationAutoAnswers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AutoAnswerId,AutoAuswerText")] DLRecommendationAutoAnswer dLRecommendationAutoAnswer)
        {
            if (ModelState.IsValid)
            {
                db.DLRecommendationAutoAnswers.Add(dLRecommendationAutoAnswer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dLRecommendationAutoAnswer);
        }

        // GET: DLRecommendationAutoAnswers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DLRecommendationAutoAnswer dLRecommendationAutoAnswer = db.DLRecommendationAutoAnswers.Find(id);
            if (dLRecommendationAutoAnswer == null)
            {
                return HttpNotFound();
            }
            return View(dLRecommendationAutoAnswer);
        }

        // POST: DLRecommendationAutoAnswers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AutoAnswerId,Describe,AutoAuswerText")] DLRecommendationAutoAnswer dLRecommendationAutoAnswer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dLRecommendationAutoAnswer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dLRecommendationAutoAnswer);
        }

        // GET: DLRecommendationAutoAnswers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DLRecommendationAutoAnswer dLRecommendationAutoAnswer = db.DLRecommendationAutoAnswers.Find(id);
            if (dLRecommendationAutoAnswer == null)
            {
                return HttpNotFound();
            }
            return View(dLRecommendationAutoAnswer);
        }

        // POST: DLRecommendationAutoAnswers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DLRecommendationAutoAnswer dLRecommendationAutoAnswer = db.DLRecommendationAutoAnswers.Find(id);
            db.DLRecommendationAutoAnswers.Remove(dLRecommendationAutoAnswer);
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
