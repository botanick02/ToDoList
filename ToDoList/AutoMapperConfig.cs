using AutoMapper;
using Business.Models;
using ToDoList.GraphQL.Categories.Inputs;
using ToDoList.GraphQL.ToDoTasks.Inputs;
using ToDoList.ViewModels;

namespace ToDoList
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration Configure()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg => {
                cfg.CreateMap<ToDoTaskCreateViewModel, ToDoTaskModel>();
                cfg.CreateMap<ToDoTaskViewModel, ToDoTaskModel>().ReverseMap();
                cfg.CreateMap<ToDoTaskEditViewModel, ToDoTaskModel>().ReverseMap();
                cfg.CreateMap<CategoryCreateViewModel, CategoryModel>();
                cfg.CreateMap<CategoryViewModel, CategoryModel>().ReverseMap();

                cfg.CreateMap<ToDoTaskModel, ToDoTaskCreateInput>().ReverseMap();
                cfg.CreateMap<ToDoTaskModel, ToDoTaskUpdateInput>().ReverseMap();
                cfg.CreateMap<CategoryModel, CategoryCreateInput>().ReverseMap();
                cfg.CreateMap<CategoryModel, CategoryUpdateInput>().ReverseMap();

            });
            mapperConfiguration.CreateMapper();
            return mapperConfiguration;
        }
    }
}
