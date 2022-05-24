using AutoMapper;
using Business.Models;
using GraphQL;
using GraphQL.Types;
using ToDoList.GraphQL.Categories;
using ToDoList.GraphQL.Categories.Inputs;
using ToDoList.sourceChanger;

namespace ToDoList.GraphQL.ToDoTasks
{
    public class CategoriesMutations : ObjectGraphType
    {
        private readonly IMapper mapper;
        private ICategoryRepository categoryRepository;

        public CategoriesMutations(CategoryRepositoryResolver catRep, IMapper mapper)
        {
            this.mapper = mapper;
            categoryRepository = catRep(CurrentStorage.CurrentSource);

            Field<CategoryType, CategoryModel>()
               .Name("Create")
               .Argument<NonNullGraphType<CategoryCreateInputType>, CategoryCreateInput>("Category", "Arguments to create category")
               .Resolve(context =>
               {
                   var categoryCreateInput = context.GetArgument<CategoryCreateInput>("Category");
                   var categoryModel = mapper.Map<CategoryModel>(categoryCreateInput);
                   var res = categoryRepository.Create(categoryModel);
                   if (res)
                   {
                       return categoryModel;
                   }
                   return null;
               });


            Field<CategoryType, CategoryModel>()
               .Name("Update")
               .Argument<NonNullGraphType<CategoryUpdateInputType>, CategoryUpdateInput>("Category", "Arguments to update category")
               .Resolve(context =>
               {
                   var categoryUpdateInput = context.GetArgument<CategoryUpdateInput>("Category");
                   var categoryModel = mapper.Map<CategoryModel>(categoryUpdateInput);
                   var res = categoryRepository.Update(categoryModel);
                   if (res)
                   {
                       return categoryModel;
                   }
                   return null;
               });

            Field<CategoryType, CategoryModel>()
               .Name("Delete")
               .Argument<NonNullGraphType<IntGraphType>,int>("Id", "Argument to delete category")
               .Resolve(context =>
               {
                   var id = context.GetArgument<int>("Id");
                   var res = categoryRepository.Delete(id);
                   if (res)
                   {
                       return new CategoryModel { Id = id};
                   }
                   return null;
               });
        }
    }
}
