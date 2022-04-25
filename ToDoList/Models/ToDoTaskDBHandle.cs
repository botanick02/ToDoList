using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;

namespace ToDoList.Models
{
	public class ToDoTaskDBHandle
	{
		private SqlConnection _connection;
		private IConfiguration _configuration;
		public ToDoTaskDBHandle(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		private void connection()
		{
			_connection = new SqlConnection(_configuration["Data:ToDoItems:ConnectionString"]);
		}
		public List<ToDoTaskModel> ListItems(bool? isDone = null, int categoryId = 0 )
		{
			List<ToDoTaskModel> itemsList = new List<ToDoTaskModel>();
			connection();
			using (var sqlConnection = _connection)
			{
				string sqlWhere = "";
				if (categoryId != 0){
					sqlWhere += $"WHERE Tasks.CategoryId = {categoryId}";
				}
                if (isDone.HasValue) {
					if(sqlWhere != "")
                    {
						sqlWhere += $"AND Tasks.IsDone = '{isDone}' ";
                    }
                    else
                    {
						sqlWhere += $"WHERE Tasks.IsDone = '{isDone}' ";
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
				SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
				sqlConnection.Open();
				SqlDataReader reader = command.ExecuteReader();

				if (reader.HasRows)
				{
					while (reader.Read())
					{
						ToDoTaskModel item = new ToDoTaskModel();
						item.Id = reader.GetInt32(0);
						item.Title = reader.GetString(1);
						item.Category = reader.GetString(2);
						item.CreatedDate = reader.GetDateTime(3);

						if (!reader.IsDBNull(4)) { item.DeadlineDate = reader.GetDateTime(4); }
						item.IsDone = reader.GetBoolean(5);
						if (!reader.IsDBNull(6)) { item.DoneDate = reader.GetDateTime(6); }
						if (!reader.IsDBNull(7)) { item.Description = reader.GetString(7); }
						itemsList.Add(item);
					}
				}
				sqlConnection.Close();
			}
			return itemsList;
		}
	}
}
