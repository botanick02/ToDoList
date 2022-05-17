using Business.Models;
using GraphQL.Types;
using ToDoList.sourceChanger;
using ToDoList.GraphQL.Types;

namespace ToDoList.GraphQL
{
    public class ToDoAppQuery : ObjectGraphType
    {
        private IToDoTaskRepository taskRepository;
        private ICategoryRepository categoryRepository;

        public ToDoAppQuery(ToDoTaskRepositoryResolver taskRep, CategoryRepositoryResolver catRep)
        {
            taskRepository = taskRep(CurrentStorage.CurrentSource);
            categoryRepository = catRep(CurrentStorage.CurrentSource);

            Field<ListGraphType<TaskType>>()
                .Name("Tasks")
                .Resolve(context => taskRepository.ListTasks());
        }

    }
}
