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
            ViewBag.MyProperty = task;
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
                //!!!!!!
                c = new Category { Text = task.TextCategory };
                db.Categories.Add(c);
                db.SaveChanges();
                task.Category = c;
            }
            else
            {
                task.Category = c;
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
            Category c = db.Categories.SingleOrDefault(x => x.Text == task.Category.Text);
            if (c == null)
            {
                //!!!!!!
                c = new Category { Text = task.Category.Text };
                db.Categories.Add(c);
                db.SaveChanges();
                task.Category = c;
            }
            else
            {
                task.Category = c;//тут не працює але я не розумію чому((((
            }
            
            if (ModelState.IsValid)
            {
                //db.Tasks.Attach(task);
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

            List<SubTask> listSub = task.ListSubtask.ToList();
                foreach (SubTask itemsub in listSub)
                {
                    db.SubTasks.Remove(itemsub);
                    db.SaveChanges();
                }
            db.Tasks.Remove(task);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteCompleted()
        {
            try
            {
                List<Task> list = db.Tasks.Where(p => p.Completion == true).ToList();
                foreach (Task item in list)
                {

                    //db.SubTasks.Where(p => p.Task == item).ToList().ForEach(p => db.SubTasks.Remove(p));
                    List<SubTask> listSub = item.ListSubtask.ToList();
                    foreach (SubTask itemsub in listSub)
                    {
                        db.SubTasks.Remove(itemsub);
                        db.SaveChanges();
                    }
                    db.SaveChanges();
                    db.Tasks.Remove(item);
                    db.SaveChanges();
                }
               // db.Tasks.Where(p => p.Completion == true)
               //.ToList().ForEach(p => db.Tasks.Remove(p));
                //db.SaveChanges();
            }
            catch(Exception e)
            {
                //return e.ToString();
            }
            return RedirectToAction("Index");
        }

        public ActionResult ChangeComplite(Task task)
        {

            //if (ModelState.IsValid)
            try
            {
                //db.Tasks.Attach(task);
                task.Completion = !task.Completion;
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                return RedirectToAction("Index");
            }
            //return View(task);
        }

        public void CheckCompliteTask()
        {
            foreach (Task task in db.Tasks.ToList())
            {
                bool Complite = true;
                foreach (SubTask subtask in task.ListSubtask.ToList())
                {
                    if (subtask.Completion == false)
                    {
                        Complite = false;
                    }
                }
                if (Complite == true && task.ListSubtask.ToList().Count > 0)
                {
                    task.Completion = true;
                    db.Entry(task).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        public ActionResult ChangeCompliteSubTask(SubTask subTask)
        {

            //if (ModelState.IsValid)
            try
            {
                //db.Tasks.Attach(task);
                db.SaveChanges();
                subTask.Completion = !subTask.Completion;
                db.Entry(subTask).State = EntityState.Modified;
                db.SaveChanges();
                CheckCompliteTask();
                return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }
            //return View(task);
        }


        public ActionResult AddSubtask(int id = 0)
        {
            SubTask subtask = new SubTask();
            //subtask.Task = task;
            return View(subtask);
            //return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSubtask(SubTask subtask, int id = 0)
        {
            if (ModelState.IsValid)
            {
                //subtask.Task.TaskId = id;
                subtask.Task = db.Tasks.Find(id);
                db.SubTasks.Add(subtask);
                db.SaveChanges();
                //db.Tasks.Find(id).ListSubtask.Add(subtask);
                return RedirectToAction("Index");
            }

            return View(subtask);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}