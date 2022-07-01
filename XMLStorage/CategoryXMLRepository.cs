using Business.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace XMLStorage
{
    public class CategoryXMLRepository : ICategoryRepository
    {
        string xmlFilePath;
        XDocument xmlDocument;
        public CategoryXMLRepository()
        {
            xmlFilePath = @"..\XMLStorage\ToDoList.xml";
            xmlDocument = XDocument.Load(xmlFilePath);
        }
        public List<CategoryModel> GetAllCategories()
        {

            var tasks = xmlDocument.Descendants("Category")
                .Select(c => new CategoryModel()
                {
                    Id = XmlConvert.ToInt32(c.Attribute("Id").Value),
                    Name = c.Attribute("Name").Value
                })
                .ToList();
            return tasks;
        }
        public CategoryModel Create(CategoryModel category)
        {

            var categories = xmlDocument.Descendants("Category");
            var newId = 0;
            foreach (var c in categories)
            {
                if (int.Parse(c.Attribute("Id").Value) >= newId)
                {
                    newId = int.Parse(c.Attribute("Id").Value) + 1;
                }
            }

            xmlDocument.Root.Element("Categories").Add(
                new XElement("Category",
                new XAttribute("Id", newId),
                new XAttribute("Name", category.Name)
                ));
            xmlDocument.Save(xmlFilePath);

            return GetCategory(newId);
        }

        public bool Delete(int id)
        {
            xmlDocument.Descendants("Category").Where(c => c.Attribute("Id").Value.Equals(id.ToString())).Remove();
            xmlDocument.Save(xmlFilePath);
            setTasksCategoryToDefault(id);
            return true;
        }

        public CategoryModel GetCategory(int id)
        {
            var categoryXml = xmlDocument.Descendants("Category").Where(c => c.Attribute("Id").Value.Equals(id.ToString())).FirstOrDefault();
            var category = new CategoryModel()
            {
                Id = XmlConvert.ToInt32(categoryXml.Attribute("Id").Value),
                Name = categoryXml.Attribute("Name").Value
            };
            return category;
        }

        public CategoryModel Update(CategoryModel category)
        {
            xmlDocument.Descendants("Category").Where(c => c.Attribute("Id").Value.Equals(category.Id.ToString())).FirstOrDefault()
                .SetAttributeValue("Name", category.Name);
            xmlDocument.Save(xmlFilePath);
            return GetCategory(category.Id);
        }

        private void setTasksCategoryToDefault(int categoryId)
        {
            var tasksElem = xmlDocument.Root.Element("Tasks");
            var tasks = tasksElem.Elements("Task").Where(task => task.Attribute("CategoryId").Value.Equals(categoryId.ToString())).ToList();
            foreach (var task in tasks)
            {
                Debug.WriteLine(task);
                task.SetAttributeValue("CategoryId", 1);
            }
            xmlDocument.Element("Root").Element("Tasks").ReplaceWith(tasksElem);
            xmlDocument.Save(xmlFilePath);

        }
    }
}
