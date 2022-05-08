using Business.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;

namespace XMLStorage
{
    public class ToDoTaskXMLRepository : IToDoTaskRepository
    {
        XDocument xmlDocument;
        public ToDoTaskXMLRepository()
        {
            xmlDocument = XDocument.Load(@"..\XMLStorage\ToDoList.xml");
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
            XElement root = xmlDocument.Element("Root");
            var tasksXML = new XElement("Tasks",
                from c in root.Element("Tasks").Elements("Task")
                join o in root.Element("Categories").Elements("Category")
                        on (string)c.Attribute("CategoryId") equals
                           (string)o.Attribute("Id")
                where (string)c.Attribute("IsDone") == "false"
                orderby DateTime.Parse(c.Attribute("DeadlineDate").Value != "" ? c.Attribute("DeadlineDate").Value : "01-01-2100 00:00:00") 
                select new XElement("Task",
                new XElement("Id", (string)c.Attribute("Id")),
                new XElement("Title", (string)c.Attribute("Title")),
                new XElement("CategoryId", (string)c.Attribute("CategoryId")),
                new XElement("Category", (string)o.Attribute("Name")),
                new XElement("CreatedDate", (string)c.Attribute("CreatedDate")),
                new XElement("DeadlineDate", (string)c.Attribute("DeadlineDate")),
                new XElement("IsDone", (string)c.Attribute("IsDone")),
                new XElement("DoneDate", (string)c.Attribute("DoneDate"))
                )
                );
            var tasks = tasksXML.Elements("Task")
                .Select(x => new ToDoTaskModel()
                {
                    Id = XmlConvert.ToInt32(x.Element("Id").Value),
                    Title = x.Element("Title").Value,
                    CategoryId = XmlConvert.ToInt32(x.Element("CategoryId").Value),
                    Category = x.Element("Category").Value,
                    CreatedDate = DateTime.Parse(x.Element("CreatedDate").Value),
                    DeadlineDate = x.Element("DeadlineDate").Value != "" ? (DateTime?)DateTime.Parse(x.Element("DeadlineDate").Value) : null,
                    IsDone = false,
                    DoneDate = x.Element("DoneDate").Value != "" ? (DateTime?)DateTime.Parse(x.Element("DoneDate").Value) : null,
                })
                .ToList();
            return tasks;
        }
        private List<ToDoTaskModel> ListCompletedTasks(int? categoryId)
        {
            XElement root = xmlDocument.Element("Root");
            var tasksXML = new XElement("Tasks",
                from c in root.Element("Tasks").Elements("Task")
                join o in root.Element("Categories").Elements("Category")
                        on (string)c.Attribute("CategoryId") equals
                           (string)o.Attribute("Id")
                where (string)c.Attribute("IsDone") == "true"
                orderby DateTime.Parse(c.Attribute("DoneDate").Value) descending 
                select new XElement("Task",
                new XElement("Id", (string)c.Attribute("Id")),
                new XElement("Title", (string)c.Attribute("Title")),
                new XElement("CategoryId", (string)c.Attribute("CategoryId")),
                new XElement("Category", (string)o.Attribute("Name")),
                new XElement("CreatedDate", (string)c.Attribute("CreatedDate")),
                new XElement("DeadlineDate", (string)c.Attribute("DeadlineDate")),
                new XElement("IsDone", (string)c.Attribute("IsDone")),
                new XElement("DoneDate", (string)c.Attribute("DoneDate"))
                )
                );
            var tasks = tasksXML.Elements("Task")
                .Select(x => new ToDoTaskModel()
                {
                    Id = XmlConvert.ToInt32(x.Element("Id").Value),
                    Title = x.Element("Title").Value,
                    CategoryId = XmlConvert.ToInt32(x.Element("CategoryId").Value),
                    Category = x.Element("Category").Value,
                    CreatedDate = DateTime.Parse(x.Element("CreatedDate").Value),
                    DeadlineDate = x.Element("DeadlineDate").Value != "" ? (DateTime?)DateTime.Parse(x.Element("DeadlineDate").Value) : null,
                    IsDone = false,
                    DoneDate = x.Element("DoneDate").Value != "" ? (DateTime?)DateTime.Parse(x.Element("DoneDate").Value) : null,
                })
                .ToList();
            return tasks;
        }
        public ToDoTaskModel GetTask(int id)
        {
            throw new NotImplementedException();
        }

        public bool Create(ToDoTaskModel task)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool SetDoneStatus(int id, bool status)
        {
            throw new NotImplementedException();
        }

        public bool Update(ToDoTaskModel task)
        {
            throw new NotImplementedException();
        }

    }
}
