using Business.Models;
using GraphQL.Types;
using ToDoList.sourceChanger;

namespace ToDoList.GraphQL.ToDoTasks
{
    public class CategoriesMutations : ObjectGraphType
    {
        private ICategoryRepository categoryRepository;

        public CategoriesMutations(CategoryRepositoryResolver catRep)
        {
            categoryRepository = catRep(CurrentStorage.CurrentSource);
        }
    }
}
