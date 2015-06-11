using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.SqlClient;

namespace TODOList.Models
{
    public class TaskContextInitializer : DropCreateDatabaseAlways<TaskContext>
    {
        //private const string _databaseName = "myDb";
        //private const string _sqlServerInstance = ".";

        //public override void InitializeDatabase(TaskContext context)
        //{
        //    context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction
        //        , string.Format("ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE", context.Database.Connection.Database));

        //    base.InitializeDatabase(context);
        //}

        protected override void Seed(TaskContext context)
        {
            context.Categories.Add(new Category { Text = "main" });
            context.Categories.Add(new Category { Text = "other" });
            context.Tasks.Add(new Task { DateTime = DateTime.Now.ToUniversalTime(), Text = "new task", Completion = false, Category = new Category {Text = "Task for test"} });
            //context.Tasks.Add(new Task { DateTime = DateTime.Now.ToUniversalTime(), Text = "new task", Completion = false, Category = context.Categories.Find(2) });//error
            context.SubTasks.Add(new SubTask { TaskId = 0, Category = context.Categories.Find(2), Completion = false, Text = "subtask 1 - create plan" });
            context.SubTasks.Add(new SubTask { TaskId = 0, Category = context.Categories.Find(2), Completion = false, Text = "subtask 2 - training" });
            context.SaveChanges();
            context.Tasks.Add(new Task { DateTime = DateTime.Now.ToUniversalTime(), Text = "new task", Completion = false, Category = context.Categories.Find(2) });
            context.SubTasks.Add(new SubTask { TaskId = 1, Category = context.Categories.Find(1), Completion = false, Text = "subtask 1 - create plan" });
            context.SaveChanges();
            //context.Tasks.First().ListSubtask.Add(context.SubTasks.First());
            //context.Tasks.First().ListSubtask.Add(context.SubTasks.Last());
            //context.SaveChanges();
            //db.Tasks.Where(p => p.Completion == true)
            //   .ToList().ForEach(p => db.Tasks.Remove(p));
            //    db.SaveChanges();
            //}
            base.Seed(context);
        }

        //public new void InitializeDatabase(TaskContext context)
        //{
        //    //Database may not exist first time through.
        //    if (CheckDatabaseExists())
        //    {
        //        //Allow database to be dropped even if it is open in SQL Management Studio
        //        context.Database.ExecuteSqlCommand(
        //            String.Format("ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE",
        //                          _databaseName));
        //    }

        //    base.InitializeDatabase(context);


        //    //Set to multi user in case it is still set to multi user
        //    context.Database.ExecuteSqlCommand(
        //        String.Format("ALTER DATABASE {0} SET MULTI_USER WITH ROLLBACK IMMEDIATE",
        //                      _databaseName));
        //}

        //private bool CheckDatabaseExists()
        //{
        //    try
        //    {
        //        using (var tmpConn = new SqlConnection(String.Format("server={0};Trusted_Connection=yes",
        //                                                             _sqlServerInstance)))
        //        {
        //            var sqlCreateDbQuery = String.Format("SELECT database_id FROM sys.databases WHERE Name = '{0}'",
        //                                                 _databaseName);

        //            using (var sqlCmd = new SqlCommand(sqlCreateDbQuery, tmpConn))
        //            {
        //                tmpConn.Open();
        //                var databaseId = (int)sqlCmd.ExecuteScalar();
        //                tmpConn.Close();

        //                return (databaseId > 0);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Failed to open a connection, either the DB does not exist or we are not allowed to acces it.
        //        //If the later, EF will throw a useful error later.
        //        return false;
        //    }
        //}




    }
}