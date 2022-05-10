using Business.Models;
using System.Collections.Generic;
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
        public bool Create(CategoryModel category)
        {
            var count = xmlDocument.Descendants("Category").Count();
            xmlDocument.Root.Element("Categories").Add(
                new XElement("Category",
                new XAttribute("Id", count + 1),
                new XAttribute("Name", category.Name)
                ));
            xmlDocument.Save(xmlFilePath);

            return true;
        }

        public bool Delete(int id)
        {
            xmlDocument.Descendants("Category").Where(c => c.Attribute("Id").Value.Equals(id.ToString())).Remove();
            xmlDocument.Save(xmlFilePath);

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

        public bool Update(CategoryModel category)
        {
            xmlDocument.Descendants("Category").Where(c => c.Attribute("Id").Value.Equals(category.Id.ToString())).FirstOrDefault()
                .SetAttributeValue("Name", category.Name);
            xmlDocument.Save(xmlFilePath);
            return true;
        }
    }
}
