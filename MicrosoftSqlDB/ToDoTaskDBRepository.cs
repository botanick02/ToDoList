using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
                string sqlQuery = "SELECT Tasks.Id, Tasks.Title, Tasks.CategoryId, Categories.Name AS Category, " +
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
                string sqlQuery = "SELECT Tasks.Id, Tasks.Title, Tasks.CategoryId, Categories.Name AS Category, " +
                    "Tasks.CreatedDate, Tasks.DeadlineDate, Tasks.IsDone, " +
                    "Tasks.DoneDate FROM Tasks " +
                    "INNER JOIN Categories ON Tasks.CategoryId=Categories.Id " +
                    "WHERE Tasks.IsDone = 1" + (categoryId.HasValue ? " AND Tasks.CategoryId = @Id" : "") +
                    " ORDER BY Tasks.DoneDate DESC";

                var tasksList = conn.Query<ToDoTaskModel>(sqlQuery, parameters).ToList();
                return tasksList;
            }
        }


        public bool Create(ToDoTaskModel task)
        {
            int affectedRows = 0;
            using (var conn = new SqlConnection(connectionString))
            {
                var parameters = new
                {
                    Title = task.Title,
                    CategoryId = task.CategoryId,
                    DeadlineDate = task.DeadlineDate
                };
                string sqlQuery = $"INSERT INTO Tasks (Title, CategoryId, CreatedDate, DeadlineDate) VALUES(@Title, @CategoryId,  '{DateTime.Now}', @DeadlineDate)";
                affectedRows = conn.Execute(sqlQuery, parameters);
            }
            return affectedRows > 0;
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

        public bool SetDoneStatus(int id, bool status)
        {
            var affectedRows = 0;
            using (var conn = new SqlConnection(connectionString))
            {
                var parameters = new { Id = id, Status = status };
                string sqlQuery = $"UPDATE Tasks SET IsDone = @Status, DoneDate = '{DateTime.Now}' WHERE Id = @Id";
                affectedRows = conn.Execute(sqlQuery, parameters);
            }
            return affectedRows > 0;
        }

        public ToDoTaskModel GetTask(int id)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var parameters = new { Id = id };
                string sqlQuery = "SELECT Tasks.Id, Tasks.Title, Tasks.CategoryId, Categories.Name as Category," +
                    " Tasks.CreatedDate, Tasks.IsDone, Tasks.DeadlineDate, Tasks.DoneDate FROM Tasks" +
                    " LEFT JOIN Categories ON Tasks.CategoryId = Categories.Id WHERE Tasks.Id = @Id";
                ToDoTaskModel res = conn.QueryFirst<ToDoTaskModel>(sqlQuery, parameters);
                return res;
            }
        }
        public bool Update(ToDoTaskModel task)
        {
            var affectedRows = 0;
            using (var conn = new SqlConnection(connectionString))
            {
                var parameters = new
                {
                    Id = task.Id,
                    Title = task.Title,
                    DeadlineDate = task.DeadlineDate,
                    CategoryId = task.CategoryId
                };
                string sqlQuery = "UPDATE Tasks SET Title = @Title, CategoryId = @CategoryId," +
                    " DeadlineDate = @DeadlineDate WHERE Id = @Id";
                affectedRows = conn.Execute(sqlQuery, parameters);
            }
            return affectedRows > 0;
        }
    }
}
