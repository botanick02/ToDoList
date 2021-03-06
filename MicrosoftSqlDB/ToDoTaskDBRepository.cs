using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using Business.Models;
using Dapper;

namespace MicrosoftSqlDB.Models
{
    public class ToDoTaskDBRepository : IToDoTaskRepository
    {
        private string connectionString;

        public ToDoTaskDBRepository()
        {
            connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ToDoListDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        public List<ToDoTaskModel> ListTasks(bool? isDone = null, int? categoryId = null)
        {
            var tasks = new List<ToDoTaskModel>();
            if (!isDone.HasValue)
            {
                tasks.AddRange(ListCurrentTasks(categoryId));
                tasks.AddRange(ListCompletedTasks(categoryId));
            }
            else if (isDone.Value)
            {
                tasks.AddRange(ListCompletedTasks(categoryId));
            }
            else
            {
                tasks.AddRange(ListCurrentTasks(categoryId));
            }
            return tasks;
        }
        private List<ToDoTaskModel> ListCurrentTasks(int? categoryId)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var parameters = new { Id = categoryId };
                var sqlQuery = "SELECT Tasks.Id, Tasks.Title, Tasks.CategoryId, Categories.Name AS Category, " +
                    "Tasks.CreatedDate, Tasks.DeadlineDate, Tasks.IsDone, " +
                    "Tasks.DoneDate FROM Tasks " +
                    "INNER JOIN Categories ON Tasks.CategoryId=Categories.Id " +
                    "WHERE Tasks.IsDone = 0" + (categoryId.HasValue ? " AND Tasks.CategoryId = @Id" : "") +
                    " ORDER BY COALESCE(Tasks.DeadlineDate,'2079-01-01') ASC";
                var tasksList = conn.Query<ToDoTaskModel>(sqlQuery, parameters).ToList();
                return tasksList;
            }
        }
        private List<ToDoTaskModel> ListCompletedTasks(int? categoryId)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var parameters = new { Id = categoryId };
                var sqlQuery = "SELECT Tasks.Id, Tasks.Title, Tasks.CategoryId, Categories.Name AS Category, " +
                    "Tasks.CreatedDate, Tasks.DeadlineDate, Tasks.IsDone, " +
                    "Tasks.DoneDate FROM Tasks " +
                    "INNER JOIN Categories ON Tasks.CategoryId=Categories.Id " +
                    "WHERE Tasks.IsDone = 1" + (categoryId.HasValue ? " AND Tasks.CategoryId = @Id" : "") +
                    " ORDER BY Tasks.DoneDate DESC";

                var tasksList = conn.Query<ToDoTaskModel>(sqlQuery, parameters).ToList();
                return tasksList;
            }
        }


        public ToDoTaskModel Create(ToDoTaskModel task)
        {
            ToDoTaskModel res = null;
            using (var conn = new SqlConnection(connectionString))
            {
                var parameters = new
                {
                    Title = task.Title,
                    CategoryId = task.CategoryId,
                    DeadlineDate = task.DeadlineDate
                };
                string sqlQuery = $"INSERT INTO Tasks (Title, CategoryId, CreatedDate, DeadlineDate)" +
                    $" VALUES(@Title, @CategoryId,  '{DateTime.Now}', @DeadlineDate); SELECT SCOPE_IDENTITY() AS [Id];";
                var addedTask = conn.QueryFirst<ToDoTaskModel>(sqlQuery, parameters);
                if (addedTask != null)
                {
                    res = GetTask(addedTask.Id);
                }
            }
            return res;
        }

        public bool Delete(int id)
        {
            var affectedRows = 0;
            using (var conn = new SqlConnection(connectionString))
            {
                var parameters = new { Id = id };
                string sqlQuery = $"DELETE FROM Tasks WHERE Id = @Id";
                affectedRows = conn.Execute(sqlQuery, parameters);
            }
            return affectedRows > 0;
        }

        public ToDoTaskModel ToggleDoneStatus(int id)
        {
            var affectedRows = 0;
            using (var conn = new SqlConnection(connectionString))
            {
                var selectPparameters = new { Id = id};
                var sqlQuery = "SELECT IsDone FROM Tasks WHERE Id = @Id";
                var isDoneCurrent = conn.QueryFirst<ToDoTaskModel>(sqlQuery, selectPparameters).IsDone;

                var updateParameters = new { Id = id, Status = !isDoneCurrent };
                var doneDate = isDoneCurrent ? "" : DateTime.Now.ToString();
                sqlQuery = $"UPDATE Tasks SET IsDone = @Status, DoneDate = '{doneDate}' WHERE Id = @Id";
                affectedRows = conn.Execute(sqlQuery, updateParameters);
                if(affectedRows > 0)
                {
                    return GetTask(id);
                }
            }
            return null;
        }

        public ToDoTaskModel GetTask(int id)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var parameters = new { Id = id };
                var sqlQuery = "SELECT Tasks.Id, Tasks.Title, Tasks.CategoryId, Categories.Name as Category," +
                    " Tasks.CreatedDate, Tasks.IsDone, Tasks.DeadlineDate, Tasks.DoneDate FROM Tasks" +
                    " LEFT JOIN Categories ON Tasks.CategoryId = Categories.Id WHERE Tasks.Id = @Id";
                ToDoTaskModel res = conn.QueryFirst<ToDoTaskModel>(sqlQuery, parameters);
                return res;
            }
        }
        public ToDoTaskModel Update(ToDoTaskModel task)
        {
            ToDoTaskModel res = null;
            using (var conn = new SqlConnection(connectionString))
            {
                var parameters = new
                {
                    Id = task.Id,
                    Title = task.Title,
                    DeadlineDate = task.DeadlineDate,
                    CategoryId = task.CategoryId
                };
                var sqlQuery = "UPDATE Tasks SET Title = @Title, CategoryId = @CategoryId," +
                    " DeadlineDate = @DeadlineDate WHERE Id = @Id";
                var affectedRows = conn.Execute(sqlQuery, parameters);
                if(affectedRows > 0)
                {
                    res = GetTask(task.Id);
                }
            }
            return res;
        }
    }
}
