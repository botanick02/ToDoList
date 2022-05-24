using GraphQL.Types;

namespace ToDoList.GraphQL.Categories.Inputs
{
    public class CategoryCreateInputType : InputObjectGraphType<CategoryCreateInput>
    {
        public CategoryCreateInputType()
        {
            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("Name")
                .Resolve(context => context.Source.Name);
        }
    }
}
