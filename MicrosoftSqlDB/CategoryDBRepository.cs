﻿using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Business.Models;
using Dapper;

namespace MicrosoftSqlDB.Models
{
    public class CategoryDBRepository : ICategoryRepository
    {
        private string connectionString;
        public CategoryDBRepository()
        {
            connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ToDoListDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }
        public List<CategoryModel> GetAllCategories()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM Categories";
                var itemsList = conn.Query<CategoryModel>(sqlQuery).ToList();
                return itemsList;
            }
        }
        public CategoryModel GetCategory(int id)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var parameters = new { Id = id };
                string sqlQuery = "SELECT * FROM Categories WHERE Id = @Id";
                var res = conn.QueryFirst<CategoryModel>(sqlQuery, parameters);
                return res;
            }
        }
        public bool Delete(CategoryModel category)
        {
            var affectedRows = 0;
            using (var conn = new SqlConnection(connectionString))
            {
                var parameters = new { Id = category.Id };
                string sqlQuery = $"DELETE FROM Categories WHERE Id = @Id";
                affectedRows = conn.Execute(sqlQuery, parameters);
            }
            return affectedRows > 0;
        }
        public bool Create(CategoryModel category)
        {
            int affectedRows = 0;
            using (var conn = new SqlConnection(connectionString))
            {
                var parameters = new { Name = category.Name };
                string sqlQuery = $"INSERT INTO Categories VALUES(@Name)";
                affectedRows = conn.Execute(sqlQuery, parameters);
            }
            return affectedRows > 0;
        }

        public bool Update(CategoryModel category)
        {
            int affectedRows = 0;
            using (var conn = new SqlConnection(connectionString))
            {
                var parameters = new { Name = category.Name, Id = category.Id };
                string sqlQuery = $"UPDATE Categories SET Name = @Name WHERE Id = @Id";
                affectedRows = conn.Execute(sqlQuery, parameters);
            }
            return affectedRows > 0;
        }
    }
}
