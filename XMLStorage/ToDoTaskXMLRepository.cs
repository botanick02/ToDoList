using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
using System.Diagnostics;

namespace XMLStorage
{
    public class ToDoTaskXMLRepository : IToDoTaskRepository
    {
        string xmlFilePath;
        XDocument xmlDocument;
        public ToDoTaskXMLRepository()
        {
            xmlFilePath = @"..\XMLStorage\ToDoList.xml";
            xmlDocument = XDocument.Load(xmlFilePath);
        }
        public List<ToDoTaskModel> ListTasks(bool? isDone, int? categoryId)
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
            var root = xmlDocument.Root;
            var tasksXML = new XElement("Tasks",
                from c in root.Element("Tasks").Elements("Task")
                join o in root.Element("Categories").Elements("Category")
                        on (string)c.Attribute("CategoryId") equals
                           (string)o.Attribute("Id")
                where (string)c.Attribute("IsDone") == "False"
                orderby DateTime.Parse(c.Attribute("DeadlineDate").Value != "" ? c.Attribute("DeadlineDate").Value : "01-01-2100")
                select new XElement("Task",
                new XAttribute("Id", (string)c.Attribute("Id")),
                new XAttribute("Title", (string)c.Attribute("Title")),
                new XAttribute("CategoryId", (string)c.Attribute("CategoryId")),
                new XAttribute("Category", (string)o.Attribute("Name")),
                new XAttribute("CreatedDate", (string)c.Attribute("CreatedDate")),
                new XAttribute("DeadlineDate", (string)c.Attribute("DeadlineDate"))
                )
                );
            var query = from t in tasksXML.Elements("Task") select t;

            if (categoryId != null)
            {
                query = query.Where(t => t.Attribute("CategoryId").Value.Equals(categoryId.ToString()));
            }

            var tasks = query.Select(t => new ToDoTaskModel()
            {
                Id = XmlConvert.ToInt32(t.Attribute("Id").Value),
                Title = t.Attribute("Title").Value,
                CategoryId = XmlConvert.ToInt32(t.Attribute("CategoryId").Value),
                Category = t.Attribute("Category").Value,
                CreatedDate = DateTime.Parse(t.Attribute("CreatedDate").Value),
                DeadlineDate = t.Attribute("DeadlineDate").Value != "" ? (DateTime?)DateTime.Parse(t.Attribute("DeadlineDate").Value) : null,
                IsDone = false,
            }).ToList();
            return tasks;
        }
        private List<ToDoTaskModel> ListCompletedTasks(int? categoryId)
        {
            var root = xmlDocument.Root;
            var tasksXML = new XElement("Tasks",
                from c in root.Element("Tasks").Elements("Task")
                join o in root.Element("Categories").Elements("Category")
                        on (string)c.Attribute("CategoryId") equals
                           (string)o.Attribute("Id")
                where (string)c.Attribute("IsDone") == "True"
                orderby DateTime.Parse(c.Attribute("DoneDate").Value) descending
                select new XElement("Task",
                new XAttribute("Id", (string)c.Attribute("Id")),
                new XAttribute("Title", (string)c.Attribute("Title")),
                new XAttribute("CategoryId", (string)c.Attribute("CategoryId")),
                new XAttribute("Category", (string)o.Attribute("Name")),
                new XAttribute("CreatedDate", (string)c.Attribute("CreatedDate")),
                new XAttribute("DoneDate", (string)c.Attribute("DoneDate"))
                )
                );


            var query = from task in tasksXML.Elements("Task") select task;

            if (categoryId != null)
            {
                query = query.Where(task => task.Attribute("CategoryId").Value.Equals(categoryId.ToString()));
            }

            var tasks = query.Select(t => new ToDoTaskModel()
            {
                Id = XmlConvert.ToInt32(t.Attribute("Id").Value),
                Title = t.Attribute("Title").Value,
                CategoryId = XmlConvert.ToInt32(t.Attribute("CategoryId").Value),
                Category = t.Attribute("Category").Value,
                CreatedDate = DateTime.Parse(t.Attribute("CreatedDate").Value),
                IsDone = true,
                DoneDate = t.Attribute("DoneDate").Value != "" ? (DateTime?)DateTime.Parse(t.Attribute("DoneDate").Value) : null,
            }).ToList();


            return tasks;
        }
        public ToDoTaskModel GetTask(int id)
        {
            var root = xmlDocument.Root;
            var tasksXML = new XElement("Tasks",
                from c in root.Element("Tasks").Elements("Task")
                join o in root.Element("Categories").Elements("Category")
                        on (string)c.Attribute("CategoryId") equals
                           (string)o.Attribute("Id")
                orderby DateTime.Parse(c.Attribute("DeadlineDate").Value != "" ? c.Attribute("DeadlineDate").Value : "01-01-2100")
                select new XElement("Task",
                new XAttribute("Id", (string)c.Attribute("Id")),
                new XAttribute("Title", (string)c.Attribute("Title")),
                new XAttribute("CategoryId", (string)c.Attribute("CategoryId")),
                new XAttribute("Category", (string)o.Attribute("Name")),
                new XAttribute("CreatedDate", (string)c.Attribute("CreatedDate")),
                new XAttribute("IsDone", (string)c.Attribute("IsDone")),
                new XAttribute("DoneDate", (string)c.Attribute("DoneDate")),
                new XAttribute("DeadlineDate", (string)c.Attribute("DeadlineDate"))
                )
                );
            var task = tasksXML.Elements("Task").Where(t => t.Attribute("Id").Value.Equals(id.ToString())).Select(t => new ToDoTaskModel()
            {
                Id = XmlConvert.ToInt32(t.Attribute("Id").Value),
                Title = t.Attribute("Title").Value,
                CategoryId = XmlConvert.ToInt32(t.Attribute("CategoryId").Value),
                Category = t.Attribute("Category").Value,
                DeadlineDate = t.Attribute("DeadlineDate").Value != "" ? (DateTime?)DateTime.Parse(t.Attribute("DeadlineDate").Value) : null,
                IsDone = bool.Parse(t.Attribute("IsDone").Value),
            }).First();
            return task;
        }

        public ToDoTaskModel Create(ToDoTaskModel task)
        {
            var newId = xmlDocument.Descendants("Task").Count() + 1;
            var newTask = new XElement("Task",
                new XAttribute("Id", newId),
                new XAttribute("Title", task.Title),
                new XAttribute("CategoryId", task.CategoryId),
                new XAttribute("IsDone", "False"),
                new XAttribute("CreatedDate", DateTime.Now),
                new XAttribute("DoneDate", ""),
                new XAttribute("DeadlineDate", task.DeadlineDate.ToString() ?? "")
                );
            xmlDocument.Root.Element("Tasks").Add(newTask);
            xmlDocument.Save(xmlFilePath);
            return GetTask(newId);
        }

        public bool Delete(int id)
        {
            xmlDocument.Descendants("Task").Where(c => c.Attribute("Id").Value.Equals(id.ToString())).Remove();
            xmlDocument.Save(xmlFilePath);
            return true;
        }

        public ToDoTaskModel ToggleDoneStatus(int id)
        {
            var isDoneCurent = bool.Parse(xmlDocument.Descendants("Task").Where(c => c.Attribute("Id").Value.Equals(id.ToString())).First().Attribute("IsDone").Value);

            var taskXml = xmlDocument.Descendants("Task").Where(t => t.Attribute("Id").Value.Equals(id.ToString())).First();
            taskXml.SetAttributeValue("IsDone", (!isDoneCurent).ToString());
            if (isDoneCurent)
            {
                taskXml.SetAttributeValue("DoneDate", "");
            }
            else
            {
                taskXml.SetAttributeValue("DoneDate", DateTime.Now);
            }
            

            xmlDocument.Descendants("Task").Where(t => t.Attribute("Id").Value.Equals(id.ToString())).First()
                .ReplaceWith(taskXml);
            xmlDocument.Save(xmlFilePath);
            return GetTask(id);
        }

        public ToDoTaskModel Update(ToDoTaskModel task)
        {
            var taskXml = xmlDocument.Descendants("Task").Where(t => t.Attribute("Id").Value.Equals(task.Id.ToString())).First();
            taskXml.SetAttributeValue("Title", task.Title);
            taskXml.SetAttributeValue("CategoryId", task.CategoryId);
            taskXml.SetAttributeValue("DeadlineDate", task.DeadlineDate.ToString());
            xmlDocument.Descendants("Task").Where(t => t.Attribute("Id").Value.Equals(task.Id.ToString())).First()
                .ReplaceWith(taskXml);
            xmlDocument.Save(xmlFilePath);
            return GetTask(task.Id);
        }

    }
}
