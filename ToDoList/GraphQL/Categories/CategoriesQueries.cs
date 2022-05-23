using Business.Models;
using GraphQL;
using GraphQL.Types;
using ToDoList.GraphQL.Categories;
using ToDoList.sourceChanger;

namespace ToDoList.GraphQL.ToDoTasks
{
    public class CategoriesQueries : ObjectGraphType
    {
        private ICategoryRepository categoryRepository;

        public CategoriesQueries(CategoryRepositoryResolver catRep)
        {
            categoryRepository = catRep(CurrentStorage.CurrentSource);

            Field<ListGraphType<CategoryType>>()
                .Name("GetAll")
                .Resolve(context =>
                {
                    return categoryRepository.GetAllCategories();
                });

            Field<NonNullGraphType<CategoryType>, CategoryModel>()
              .Name("GetById")
              .Argument<NonNullGraphType<IntGraphType>, int>("Id", "Argument to get task")
              .Resolve(context =>
              {
                  int id = context.GetArgument<int>("Id");
                  return categoryRepository.GetCategory(id);
              });

        }
    }
}
