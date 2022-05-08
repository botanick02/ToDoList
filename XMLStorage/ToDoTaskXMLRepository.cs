using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XMLStorage
{
	internal class ToDoTaskXMLRepository : IToDoTaskRepository
	{
		XDocument xmlDocument;
		public ToDoTaskXMLRepository()
		{
			xmlDocument = XDocument.Load("/ToDOList.xml");
		}
		public List<ToDoTaskModel> ListTasks(bool? isDone, int categoryId)
		{
			throw new NotImplementedException();
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
