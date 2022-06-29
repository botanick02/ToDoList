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
               .Argument<NonNullGraphType<CategoryCreateInputType>, CategoryCreateInput>("CategoryCreateInputType", "Arguments to create category")
               .Resolve(context =>
               {
                   var categoryCreateInput = context.GetArgument<CategoryCreateInput>("CategoryCreateInputType");
                   var categoryModel = mapper.Map<CategoryModel>(categoryCreateInput);
                   var res = categoryRepository.Create(categoryModel);
                   return res;
               });


            Field<CategoryType, CategoryModel>()
               .Name("Update")
               .Argument<NonNullGraphType<CategoryUpdateInputType>, CategoryUpdateInput>("CategoryUpdateInputType", "Arguments to update category")
               .Resolve(context =>
               {
                   var categoryUpdateInput = context.GetArgument<CategoryUpdateInput>("CategoryUpdateInputType");
                   var categoryModel = mapper.Map<CategoryModel>(categoryUpdateInput);
                   var res = categoryRepository.Update(categoryModel);
                   return res;
               });

            Field<CategoryType, CategoryModel>()
               .Name("Delete")
               .Argument<NonNullGraphType<IntGraphType>, CategoryCreateInput>("Id", "Argument to delete category")
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
