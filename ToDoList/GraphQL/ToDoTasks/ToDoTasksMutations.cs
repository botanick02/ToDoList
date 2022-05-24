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
                   if (res)
                   {
                       return taskModel;
                   }
                   return null;
               });

            Field<ToDoTaskType, ToDoTaskModel>()
               .Name("Update")
               .Argument<NonNullGraphType<ToDoTaskUpdateInputType>, ToDoTaskUpdateInput>("ToDoTask", "Arguments to update task")
               .Resolve(context =>
               {
                   var toDoTaskUpdateInput = context.GetArgument<ToDoTaskUpdateInput>("ToDoTask");
                   var taskModel = mapper.Map<ToDoTaskModel>(toDoTaskUpdateInput);
                   var res = taskRepository.Update(taskModel);
                   if (res)
                   {
                       return taskModel;
                   }
                   return null;
               });

            Field<ToDoTaskType, ToDoTaskModel>()
               .Name("ToggleDoneStatus")
               .Argument<NonNullGraphType<IntGraphType>, int>("Id", "Argument to toggle done status")
               .Resolve(context =>
               {
                   var id = context.GetArgument<int>("Id");
                   var res = taskRepository.ToggleDoneStatus(id);
                   if (res)
                   {
                       var taskModel = taskRepository.GetTask(id);
                       return taskModel;
                   }
                   return null;
               });

            Field<ToDoTaskType, ToDoTaskModel>()
              .Name("Delete")
              .Argument<NonNullGraphType<IntGraphType>, int>("Id", "Argument to delete task")
              .Resolve(context =>
              {
                  var id = context.GetArgument<int>("Id");
                  var res = taskRepository.Delete(id);
                  if (res)
                  {
                      return new ToDoTaskModel { Id = id};
                  }
                  return null;
              });
        }

    }
}
