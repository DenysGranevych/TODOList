using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TODOList.Models;

namespace TODOList.Controllers
{
    public class SubTaskController : Controller
    {
        private TaskContext db = new TaskContext();

        //
        // GET: /SubTask/

        public ActionResult Index()
        {
            return View(db.SubTasks.ToList());
        }

        //
        // GET: /SubTask/Details/5

        public ActionResult Details(int id = 0)
        {
            SubTask subtask = db.SubTasks.Find(id);
            if (subtask == null)
            {
                return HttpNotFound();
            }
            return View(subtask);
        }

        //
        // GET: /SubTask/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /SubTask/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubTask subtask)
        {
            if (ModelState.IsValid)
            {
                db.SubTasks.Add(subtask);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(subtask);
        }

        //
        // GET: /SubTask/Edit/5

        public ActionResult Edit(int id = 0)
        {
            SubTask subtask = db.SubTasks.Find(id);
            if (subtask == null)
            {
                return HttpNotFound();
            }
            return View(subtask);
        }

        //
        // POST: /SubTask/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SubTask subtask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subtask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subtask);
        }

        //
        // GET: /SubTask/Delete/5

        public ActionResult Delete(int id = 0)
        {
            SubTask subtask = db.SubTasks.Find(id);
            if (subtask == null)
            {
                return HttpNotFound();
            }
            return View(subtask);
        }

        //
        // POST: /SubTask/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubTask subtask = db.SubTasks.Find(id);
            db.SubTasks.Remove(subtask);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ChangeComplite(SubTask subTask)
        {

            //if (ModelState.IsValid)
            try
            {
                //db.Tasks.Attach(task);
                subTask.Completion = !subTask.Completion;
                db.Entry(subTask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }
            //return View(task);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}