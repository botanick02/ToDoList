using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using Dapper;
using ToDoList.ViewModels.Task;

namespace ToDoList.Models
{
	public class ToDoTaskDBRepository : IToDoTaskRepository
	{
		private string _connectionString;
		public ToDoTaskDBRepository(IConfiguration configuration)
		{
			_connectionString = configuration["Data:ToDoItems:ConnectionString"];
		}

        public List<ToDoTaskModel> ListItems(bool? isDone = null, int categoryId = 0 )
		{
			using (var conn = new SqlConnection(_connectionString))
			{
				var parameters = new { Id = categoryId, IsDone = isDone };
				string sqlWhere = "";
				if (categoryId != 0){
					sqlWhere += $"WHERE Tasks.CategoryId = @Id ";
				}
                if (isDone.HasValue) {
					if(sqlWhere != "")
                    {
						sqlWhere += $"AND Tasks.IsDone = @IsDone ";
                    }
                    else
                    {
						sqlWhere += $"WHERE Tasks.IsDone = @IsDone ";
					}
                    if (isDone == true)
                    {
						sqlWhere += " ORDER BY Tasks.DoneDate DESC";
                    }
                    else
                    {
						sqlWhere += " ORDER BY COALESCE(Tasks.DeadlineDate,'2079-01-01') ASC";
					}
				}

				string sqlQuery = "SELECT Tasks.Id, Tasks.Title, Categories.Name AS Category, " +
					"Tasks.CreatedDate, Tasks.DeadlineDate, Tasks.IsDone, " +
					"Tasks.DoneDate, Tasks.Description FROM Tasks " +
					"INNER JOIN Categories ON Tasks.CategoryId=Categories.Id " + sqlWhere;
				var tasksList = conn.Query<ToDoTaskModel>(sqlQuery, parameters).ToList();
				return tasksList;
			}
		}

		public bool Create(ToDoTaskCreateViewModel taskCreate)
		{
			int affectedRows = 0;
			using (var conn = new SqlConnection(_connectionString))
			{
				var parameters = new { Title = taskCreate.Title, Description = taskCreate.Description,
					CategoryId = taskCreate.CategoryId, DeadlineDate = taskCreate.DeadlineDate };
				string sqlQuery = $"INSERT INTO Tasks (Title, Description, CreatedDate, DeadlineDate) VALUES(@Title, @Description, '{DateTime.Now}', @DeadlineDate)";
				affectedRows = conn.Execute(sqlQuery, parameters);
			}
			return affectedRows > 0;
		}
	}
}
