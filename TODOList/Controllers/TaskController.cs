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
    public class TaskController : Controller
    {
        //private
        public TaskContext db = new TaskContext();

        //
        // GET: /Task/

        public ActionResult Index()
        {
            //System.Data.SqlClient.SqlConnection.ClearAllPools();
            
            return View(db.Tasks.ToList());
        }

        //
        // GET: /Task/Details/5

        public ActionResult Details(int id = 0)
        {
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        //
        // GET: /Task/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Task/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Task task)
        {
            task.Completion = false;
            task.DateTime = DateTime.Now;
            Category c = db.Categories.SingleOrDefault(x => x.Text == task.TextCategory);
            if (c == null)
            {
                db.Categories.Add(new Category { Text = task.TextCategory });
                db.SaveChanges();
                task.CategoryId = db.Categories.SingleOrDefault(x => x.Text == task.TextCategory).CategoryId;
            }
            else
            {
                task.CategoryId = c.CategoryId;
            }
            

            if (ModelState.IsValid)
            {
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(task);
        }

        //
        // GET: /Task/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        //
        // POST: /Task/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(task);
        }

        //
        // GET: /Task/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        //
        // POST: /Task/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteCompleted()
        {
            try
            {
                //db.Tasks.Remove(x => x.Completion);
                //List<Task> t = db.Tasks.SingleOrDefault(x => x.Completion == true);

                //var order = (from o in context.SalesOrderHeaders
                //             where o.SalesOrderID == orderId
                //             select o).First();

                //Task t = db.Tasks.Include(x => x.Moderator).SingleOrDefault(p => p.Completion == true);
                //db.Tasks.AsParallel();
                //TaskContext db2 = new TaskContext();
                //Task t = db2.Tasks.Where(x => x.Completion == true).SingleOrDefault();
                ////db.Database.
                //while (t != null)
                //{
                //    db2.Tasks.Remove(t);
                //    t = db.Tasks.SingleOrDefault(x => x.Completion == true);
                //}
                //db.SaveChanges();
                db.Tasks.Where(p => p.Completion == true)
               .ToList().ForEach(p => db.Tasks.Remove(p));
                db.SaveChanges();
            }
            catch(Exception e)
            {

            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}