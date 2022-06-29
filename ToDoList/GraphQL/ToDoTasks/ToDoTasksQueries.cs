using Business.Models;
using GraphQL;
using GraphQL.Types;
using ToDoList.sourceChanger;

namespace ToDoList.GraphQL.ToDoTasks
{
    public class ToDoTasksQueries : ObjectGraphType
    {
        private IToDoTaskRepository taskRepository;

        public ToDoTasksQueries(ToDoTaskRepositoryResolver taskRep, CategoryRepositoryResolver catRep)
        {
            taskRepository = taskRep(CurrentStorage.CurrentSource);

            Field<NonNullGraphType<ListGraphType<ToDoTaskType>>, List<ToDoTaskModel>>()
                .Name("GetAll")
                .Argument<BooleanGraphType, bool?>("IsDone", "Argument to get tasks")
                .Argument<IntGraphType, int?>("CategoryId", "Argument to get tasks")
                .Resolve(context =>
                {
                    bool? isDone = context.GetArgument<bool?>("IsDone");
                    int? categoryId = context.GetArgument<int?>("CategoryId");

                    return taskRepository.ListTasks(isDone, categoryId);
                });

            Field<NonNullGraphType<ToDoTaskType>, ToDoTaskModel>()
              .Name("GetById")
              .Argument<NonNullGraphType<IntGraphType>, int>("Id", "Argument to get task")
              .Resolve(context =>
              {
                  int id = context.GetArgument<int>("Id");
                  return taskRepository.GetTask(id);
              });
        }

    }
}
