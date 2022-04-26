using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;

namespace ToDoList.Models
{
	public class CategoryDBHandle
	{
		private SqlConnection _connection;
		private IConfiguration _configuration;
		public CategoryDBHandle(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		private void connection()
		{
			_connection = new SqlConnection(_configuration["Data:ToDoItems:ConnectionString"]);
		}
		public List<CategoryModel> ListCategories()
		{
			List<CategoryModel> itemsList = new List<CategoryModel>();
			connection();
			using (var sqlConnection = _connection)
			{
				string sqlQuery = "SELECT * FROM Categories";
				SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
				sqlConnection.Open();
				SqlDataReader reader = command.ExecuteReader();

				if (reader.HasRows)
				{
					while (reader.Read())
					{
						CategoryModel item = new CategoryModel();
						item.Id = reader.GetInt32(0);
						item.Name = reader.GetString(1);
						itemsList.Add(item);
					}
				}
				sqlConnection.Close();
			}
			return itemsList;
		}
		public bool Delete(int categoryId)
        {
			connection();
			int res = 0;
			using (var sqlConnection = _connection)
			{
				string sqlQuery = $"DELETE FROM Categories WHERE Id = {categoryId}";
				SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
				try
				{
					sqlConnection.Open();
					res = command.ExecuteNonQuery();
					sqlConnection.Close();
				}
				catch { }
			}
			return res > 0 ? true : false;
		}
		public bool Create(string name)
        {
			connection();
			int res = 0;
			using (var sqlConnection = _connection)
			{
				string sqlQuery = $"INSERT INTO Categories VALUES(@name)";

				SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
				SqlParameter param = new SqlParameter();
				command.Parameters.AddWithValue("@name", name);
				try
				{
					sqlConnection.Open();
					res = command.ExecuteNonQuery();
					sqlConnection.Close();
				}
				catch { }
			}
			return res > 0 ? true : false;
		}
	}
}
