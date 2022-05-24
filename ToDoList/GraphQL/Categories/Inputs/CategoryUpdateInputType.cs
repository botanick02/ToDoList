using GraphQL.Types;

namespace ToDoList.GraphQL.Categories.Inputs
{
    public class CategoryUpdateInputType : InputObjectGraphType<CategoryUpdateInput>
    {
        public CategoryUpdateInputType()
        {
            Field<NonNullGraphType<IntGraphType>, int>()
                .Name("Id")
                .Resolve(context => context.Source.Id);

            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("Name")
                .Resolve(context => context.Source.Name);
        }
    }
}
