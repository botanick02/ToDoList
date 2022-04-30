using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using Dapper;

namespace ToDoList.Models
{
	public class CategoryDBRepository : ICategoryRepository
	{
		private string _connectionString;
		public CategoryDBRepository(IConfiguration configuration)
		{
			_connectionString = configuration["Data:ToDoItems:ConnectionString"];
		}
		public List<CategoryModel> GetAllCategories()
		{
			using (var conn = new SqlConnection(_connectionString))
			{
				string sqlQuery = "SELECT * FROM Categories";
				var itemsList = conn.Query<CategoryModel>(sqlQuery).ToList();
				return itemsList;
			}
			return null;
		}
		public bool Delete(int categoryId)
        {
			var affectedRows = 0;
			using (var conn = new SqlConnection(_connectionString))
			{ 
				var parameters = new {Id = categoryId};
				string sqlQuery = $"DELETE FROM Categories WHERE Id = @Id";
				affectedRows = conn.Execute(sqlQuery, parameters);
			}
			return affectedRows > 0;
		}
		public bool Create(string name)
        {
			int affectedRows = 0;
			using (var conn = new SqlConnection(_connectionString))
			{
				var parameters = new { Name = name };
				string sqlQuery = $"INSERT INTO Categories VALUES(@Name)";
				affectedRows = conn.Execute(sqlQuery, parameters);
			}
			return affectedRows > 0;
		}
	}
}
