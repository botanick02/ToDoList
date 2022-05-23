using AutoMapper;
using Business.Models;
using GraphQL;
using GraphQL.Types;
using ToDoList.GraphQL.ToDoTasks.Inputs;
using ToDoList.sourceChanger;

namespace ToDoList.GraphQL.ToDoTasks
{
    public class ToDoTasksMutations : ObjectGraphType
    {
        private IToDoTaskRepository taskRepository;
        private readonly IMapper mapper;
        public ToDoTasksMutations(ToDoTaskRepositoryResolver taskRep, IMapper mapper)
        {
            taskRepository = taskRep(CurrentStorage.CurrentSource);
            this.mapper = mapper;


            Field<ToDoTaskType, ToDoTaskModel>()
               .Name("Create")
               .Argument<NonNullGraphType<ToDoTaskCreateInputType>, ToDoTaskCreateInput>("ToDoTask", "Arguments to create task")
               .Resolve(context =>
               {
                   var toDoTaskCreateInput = context.GetArgument<ToDoTaskCreateInput>("ToDoTask");
                   var taskModel = mapper.Map<ToDoTaskModel>(toDoTaskCreateInput);
                   var res = taskRepository.Create(taskModel);
                   return taskModel;
               });
        }

    }
}
